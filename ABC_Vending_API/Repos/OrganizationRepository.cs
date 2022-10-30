using ABC_Vending_API.Contexts;
using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Repos;

public class OrganizationRepository : IOrganizationRepository
{
	public async Task AddNewProductToOrganizationAsync(Product product)
	{
		using (var ctx = new OrganizationContext())
		{
			await ctx.Products.AddAsync(product).ConfigureAwait(false);
			await ctx.SaveChangesAsync().ConfigureAwait(false);
		}
	}

	public async Task AddVendingMachine(VendingMachine vendingMachine)
	{
		using (var ctx = new OrganizationContext())
		{
			ctx.VendingMachines.Add(vendingMachine);
			await ctx.SaveChangesAsync().ConfigureAwait(false);
		}
	}

	public async Task<IEnumerable<Product>> GetProductsForWarehouseAsync(Guid WarehouseId)
	{
		using (var ctx = new OrganizationContext())
		{
			return await ctx.Products.Where(p => p.WarehouseId == WarehouseId).ToArrayAsync().ConfigureAwait(false);
		}
	}

	public async Task RemoveStockForWarehouseAsync(Guid WarehouseId, Guid ProductId, int Qty)
	{
		using (var ctx = new OrganizationContext())
		{
			var product = await ctx.Products.FirstOrDefaultAsync(p => p.WarehouseId == WarehouseId && p.Id == ProductId).ConfigureAwait(false);
			int amountStocked = product?.Stock ?? 0;

			if (product == null)
			{
				throw new KeyNotFoundException("Product was not found at the warehouse");
			}			

			if (amountStocked < Qty) 
			{
				throw new Exception("Amount removed may not exceed amount stocked.");
			}

			if(amountStocked == Qty)
			{
				ctx.Products.Remove(product);
			}

			if(amountStocked > Qty)
			{
				ctx.Products.Update(product with { Stock = amountStocked - Qty });
			}

			await ctx.SaveChangesAsync().ConfigureAwait(false);
		}
	}

	public Task RemoveStockFromVendingMachineAsync(Guid MachineId, Guid ProductId, int Qty)
	{
		throw new NotImplementedException();
	}

	public Task StockProductForVendingMachineAsync(Guid MachineId, Guid ProductId, int Qty)
	{	
		throw new NotImplementedException();
	}

	public async Task StockProductForWarehouseAsync(Guid WarehouseId, Guid ProductId, int Qty)
	{
		using (var ctx = new OrganizationContext())
		{
			var (warehouse, product) = 
				(
					await ctx.Warehouses.FirstOrDefaultAsync(w => w.Id == WarehouseId).ConfigureAwait(false),
					await ctx.Products.FirstOrDefaultAsync(p => p.Id == ProductId).ConfigureAwait(false)
				);

			if (product == null)
			{
				throw new KeyNotFoundException("Product is not in database");
			}
			if (warehouse == null)
			{
				await ctx.Products.AddAsync(product with { WarehouseId = WarehouseId, Stock = Qty });
			}
			else
			{
				ctx.Products.Update(product with { Stock = product.Stock + Qty });
			}
			await ctx.SaveChangesAsync().ConfigureAwait(false);
		}
	}

	public async Task<int> WarehouseTotalMachineCountAsync(Guid WarehouseId)
	{
		using (var ctx = new OrganizationContext())
		{
			return await ctx.VendingMachines.Where(vm => vm.WarehouseId == WarehouseId).CountAsync().ConfigureAwait(false);
		}
	}

	public async Task<int> WarehouseTotalProductCountAsync(Guid WarehouseId)
	{
		using (var ctx = new OrganizationContext())
		{
			return await ctx.Products.Where(p => p.WarehouseId == WarehouseId).CountAsync().ConfigureAwait(false);
		}
	}
}
