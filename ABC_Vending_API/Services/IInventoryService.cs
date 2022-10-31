using ABC_Vending_API.Models;

namespace ABC_Vending_API.Services;

public interface IInventoryService
{
	public Task<IEnumerable<WarehouseViewModel>> GetWarehouses();
	public Task<IEnumerable<Product>> GetWarehouseProducts(Guid warehouseId);
}
