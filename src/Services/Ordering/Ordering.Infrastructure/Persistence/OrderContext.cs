using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Contracts.Domains.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;
using System.Reflection;

namespace Ordering.Domain.Persistence
{
    public class OrderContext : DbContext
    {
        private readonly IMediator _mediator;
        public OrderContext(DbContextOptions<OrderContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Order> Orders { get; set; }
        private List<BaseEvent> _baseEvents = [];

        private void SetBaseEventsBeforeSaveChange()
        {
            var domainEntities = ChangeTracker.Entries<IEventEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvent().Any())
                .ToList();

            _baseEvents = domainEntities
                .SelectMany(x => x.DomainEvent())
                .ToList();
            domainEntities.ForEach(e => e.ClearDomainEvent());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseEventsBeforeSaveChange();
            var modified = ChangeTracker.Entries()
                                        .Where(e => e.State == EntityState.Modified ||
                                            e.State == EntityState.Added ||
                                            e.State == EntityState.Deleted);

            foreach (var item in modified)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        if (item.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            item.State = EntityState.Added;
                        }
                        break;

                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false;
                        if (item.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            item.State = EntityState.Modified;
                        }
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            await _mediator.DispatchDomainEventAsync(_baseEvents);


            return result;
        }
    }
}
