﻿namespace Contracts.Services
{
    public interface IEmailService<T> where T : class
    {
        Task SendEmailAsync(T request, CancellationToken cancellationToken = new());
        void SendEmail(T request);
    }
}
