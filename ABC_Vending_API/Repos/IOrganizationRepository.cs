using ABC_Vending_API.Models;

namespace ABC_Vending_API.Repos;

public interface IOrganizationRepository
{
	/// <summary>
	/// Returns the number of distinct products for a given warehouse.
	/// </summary>
	/// <param name="WarehouseId"></param>
	/// <returns></returns>
	public Task<int> GetDistinctProductCount(Guid WarehouseId);

	/// <summary>
	/// Returns the number of vending machines for a given warehouse.
	/// </summary>
	/// <param name="WarehouseId"></param>
	/// <returns></returns>
	public Task<int> GetVendingMachineCount(Guid WarehouseId);

	/// <summary>
	/// Retrieves the product list for a given warehouse.
	/// </summary>
	/// <param name="WarehouseId"></param>
	/// <returns></returns>
	public Task<IEnumerable<Product>> GetProductsForWarehouseAsync(Guid WarehouseId);


	public Task AddNewProductToOrganizationAsync(Product product);

	/// <summary>
	/// Stocks a number of a given product to a given warehouse by Id.
	/// </summary>
	/// <param name="WarehouseId"></param>
	/// <param name="ProductId"></param>
	/// <param name="Qty"></param>
	/// <returns></returns>
	public Task StockProductForWarehouseAsync(Guid WarehouseId, Guid ProductId, int Qty);

	/// <summary>
	/// Removes a number of a given product to a given warehouse by Id. Will not remove products the warehouse does not have.
	/// </summary>
	/// <param name="WarehouseId"></param>
	/// <param name="ProductId"></param>
	/// <param name="Qty"></param>
	/// <returns></returns>
	public Task RemoveStockForWarehouseAsync(Guid WarehouseId, Guid ProductId, int Qty);

	/// <summary>
	/// Adds a vending machine to the collection.
	/// </summary>
	/// <param name="vendingMachine"></param>
	/// <returns></returns>
	public Task AddVendingMachine(VendingMachine vendingMachine);

	/// <summary>
	/// Adds a number of a given product to a given vending machine by Id.
	/// </summary>
	/// <param name="MachineId"></param>
	/// <param name="ProductId"></param>
	/// <param name="Qty"></param>
	/// <returns></returns>
	public Task StockProductForVendingMachineAsync(Guid MachineId, Guid ProductId, int Qty);


	/// <summary>
	/// Removes a number of a given product from a given vending machine by Id. Will not remove products the machine does not have.
	/// </summary>
	/// <param name="MachineId"></param>
	/// <param name="ProductId"></param>
	/// <param name="Qty"></param>
	/// <returns></returns>
	public Task RemoveStockFromVendingMachineAsync(Guid MachineId, Guid ProductId, int Qty);
}
