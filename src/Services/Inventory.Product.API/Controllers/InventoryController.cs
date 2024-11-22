using Infrastructure.Common.Models;
using Inventory.Product.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Inventory;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [Route("items/{itemNo}", Name ="GetAllByItemNo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNo([Required] string itemNo)
        {
            var result = await _inventoryService.GetAllByItemNoAsync(itemNo);
            return Ok(result);
        }

        [Route("items/{itemNo}/paging", Name = "GetAllByItemNoPaging")]
        [HttpGet]
        public async Task<ActionResult<PagedList<InventoryEntryDto>>> GetAllByItemNoPaging([Required] string itemNo,
            [FromQuery] GetInventoryPagingQuery query)
        {
            query.SetItemNo(itemNo);
            var result = await _inventoryService.GetAllByItemNoPagingAsync(query);
            return Ok(result);
        }

        [Route("{id}", Name = "GetInventoryById")]
        [HttpGet]
        public async Task<ActionResult<InventoryEntryDto>> GetInventoryById([Required] string id)
        {
            var result = await _inventoryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("purchase/{itemNo}", Name = "PurchaseOrder")]
        public async Task<ActionResult<InventoryEntryDto>> PurchaseOrder(
            [Required] string itemNo,
            [FromBody] PurchaseProductDto model)
        {
            var result = await _inventoryService.PurchaseItemAsync(itemNo, model);
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteById")]
        public async Task<ActionResult<InventoryEntryDto>> DeleteById([Required] string id)
        {
            var entity = await _inventoryService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _inventoryService.DeteleAsync(id);
            return NoContent();
        }
    }
}
