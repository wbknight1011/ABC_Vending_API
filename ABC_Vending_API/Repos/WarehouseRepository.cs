using ABC_Vending_API.Contexts;
using ABC_Vending_API.DTOs;
using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Repos;

public class WarehouseRepository : IWarehouesRepository
{
	private readonly OrganizationContext _ctx;

	public WarehouseRepository(OrganizationContext ctx)
	{
		_ctx = ctx;
	}
	public async Task AddWarehouseAsync(CreateWarehouseDto createDto)
	{
		await _ctx.Warehouses.AddAsync(new Warehouse(Guid.NewGuid(), createDto.Name, createDto.Location)).ConfigureAwait(false);
	}

	public async Task DeleteWarehouseAsync(Guid Id)
	{
		var warehouseRemoved = await _ctx.Warehouses.SingleOrDefaultAsync(w => w.Id == Id).ConfigureAwait(false);
		if (warehouseRemoved != null)
		{
			_ctx.Warehouses.Remove(warehouseRemoved);
			await _ctx.SaveChangesAsync().ConfigureAwait(false);
		}
	}

	public async Task<Warehouse> GetWarehouseAsync(Guid Id)
	{
		return await _ctx.Warehouses.SingleOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
	}

	public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
	{
		return await _ctx.Warehouses.ToArrayAsync().ConfigureAwait(false);
	}
}
