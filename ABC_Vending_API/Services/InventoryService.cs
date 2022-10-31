using ABC_Vending_API.Models;
using ABC_Vending_API.Repos;

namespace ABC_Vending_API.Services;

public class InventoryService : IInventoryService
{
	private IWarehouesRepository _warehouseRepo;
	private IOrganizationRepository _organizationRepo;

	public InventoryService(IWarehouesRepository warehouseRepo, IOrganizationRepository organizationRepo)
	{
		_warehouseRepo = warehouseRepo;
		_organizationRepo = organizationRepo;
	}

	public async Task<IEnumerable<Product>> GetWarehouseProducts(Guid warehouseId)
	{
		return await _organizationRepo.GetProductsForWarehouseAsync(warehouseId).ConfigureAwait(false);
	}

	public async Task<IEnumerable<WarehouseViewModel>> GetWarehouses()
	{
		var warehouses = await _warehouseRepo.GetWarehousesAsync().ConfigureAwait(false);
		List<WarehouseViewModel> view = new List<WarehouseViewModel>();

		foreach(var warehouse in warehouses)
		{
			int distinctMachines = await _organizationRepo.GetVendingMachineCount(warehouse.Id);
			int distinctProducts = await _organizationRepo.GetDistinctProductCount(warehouse.Id);
			view.Add(new WarehouseViewModel(warehouse, distinctMachines, distinctProducts));
		}

		return view;
	}
}
