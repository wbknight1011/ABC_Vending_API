using ABC_Vending_API.Models;
using ABC_Vending_API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Vending_API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowLocalHosts")]
public class ProductController : ControllerBase
{
	IInventoryService _inventoryService;
	public ProductController( IInventoryService inventoryService)
	{
		_inventoryService = inventoryService;
	}

	// GET: api/<ProductConroller>/12345-4453
	[HttpGet("{warehouseId}")]
	public async Task<IEnumerable<Product>> Get([FromRoute]Guid warehouseId)
	{
		return await _inventoryService.GetWarehouseProducts(warehouseId);
	}
}
