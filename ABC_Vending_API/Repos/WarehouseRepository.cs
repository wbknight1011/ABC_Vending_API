using ABC_Vending_API.Contexts;
using ABC_Vending_API.DTOs;
using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Repos;

public class WarehouseRepository : IWarehouesRepository
{
	public async Task AddWarehouseAsync(CreateWarehouseDto createDto)
	{
		using (var ctx = new WarehouseContext())
		{
			await ctx.Warehouses.AddAsync(new Warehouse(Guid.NewGuid(), createDto.Name, createDto.Location)).ConfigureAwait(false);
		}
	}

	public async Task DeleteWarehouseAsync(Guid Id)
	{
		using (var ctx = new WarehouseContext())
		{
			var warehouseRemoved = await ctx.Warehouses.SingleOrDefaultAsync(w => w.Id == Id).ConfigureAwait(false);
			if (warehouseRemoved != null)
			{
				ctx.Warehouses.Remove(warehouseRemoved);
				await ctx.SaveChangesAsync().ConfigureAwait(false);
			}
		}
	}

	public async Task<Warehouse> GetWarehouseAsync(Guid Id)
	{
		using (var ctx = new WarehouseContext())
		{
			return await ctx.Warehouses.SingleOrDefaultAsync(x => x.Id == Id).ConfigureAwait(false);
		}
	}

	public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
	{
		using (var ctx = new WarehouseContext())
		{
			return await ctx.Warehouses.ToArrayAsync().ConfigureAwait(false);
		}
	}
}
