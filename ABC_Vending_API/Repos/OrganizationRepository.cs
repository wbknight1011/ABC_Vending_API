using ABC_Vending_API.Contexts;
using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Repos;

public class OrganizationRepository : IOrganizationRepository
{
	private readonly OrganizationContext _ctx;

	public OrganizationRepository(OrganizationContext ctx)
	{
		_ctx = ctx;
	}

	public async Task AddNewProductToOrganizationAsync(Product product)
	{

		await _ctx.Products.AddAsync(product).ConfigureAwait(false);
		await _ctx.SaveChangesAsync().ConfigureAwait(false);

	}

	public async Task AddVendingMachine(VendingMachine vendingMachine)
	{

		_ctx.VendingMachines.Add(vendingMachine);
		await _ctx.SaveChangesAsync().ConfigureAwait(false);

	}

	public async Task<IEnumerable<Product>> GetProductsForWarehouseAsync(Guid WarehouseId)
	{

		return await _ctx.Products.Where(p => p.WarehouseId == WarehouseId).ToArrayAsync().ConfigureAwait(false);

	}

	public async Task RemoveStockForWarehouseAsync(Guid WarehouseId, Guid ProductId, int Qty)
	{

		var product = await _ctx.Products.FirstOrDefaultAsync(p => p.WarehouseId == WarehouseId && p.Id == ProductId).ConfigureAwait(false);
		int amountStocked = product?.Stock ?? 0;

		if (product == null)
		{
			throw new KeyNotFoundException("Product was not found at the warehouse");
		}

		if (amountStocked < Qty)
		{
			throw new Exception("Amount removed may not exceed amount stocked.");
		}

		if (amountStocked == Qty)
		{
			_ctx.Products.Remove(product);
		}

		if (amountStocked > Qty)
		{
			_ctx.Products.Update(product with { Stock = amountStocked - Qty });
		}

		await _ctx.SaveChangesAsync().ConfigureAwait(false);

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

		var (warehouse, product) =
			(
				await _ctx.Warehouses.FirstOrDefaultAsync(w => w.Id == WarehouseId).ConfigureAwait(false),
				await _ctx.Products.FirstOrDefaultAsync(p => p.Id == ProductId).ConfigureAwait(false)
			);

		if (product == null)
		{
			throw new KeyNotFoundException("Product is not in database");
		}
		if (warehouse == null)
		{
			await _ctx.Products.AddAsync(product with { WarehouseId = WarehouseId, Stock = Qty });
		}
		else
		{
			_ctx.Products.Update(product with { Stock = product.Stock + Qty });
		}
		await _ctx.SaveChangesAsync().ConfigureAwait(false);

	}

	public async Task<int> GetVendingMachineCount(Guid WarehouseId)
	{

		return await _ctx.VendingMachines.Where(vm => vm.WarehouseId == WarehouseId).CountAsync().ConfigureAwait(false);

	}

	public async Task<int> GetDistinctProductCount(Guid WarehouseId)
	{
		return await _ctx.Products.Where(p => p.WarehouseId == WarehouseId).CountAsync().ConfigureAwait(false);
	}
}
