using ABC_Vending_API.DTOs;
using ABC_Vending_API.Models;

namespace ABC_Vending_API.Repos;

public interface IWarehouesRepository
{
	public Task<IEnumerable<Warehouse>> GetWarehousesAsync();
	public Task<Warehouse> GetWarehouseAsync(Guid Id);
	public Task AddWarehouseAsync(CreateWarehouseDto createDto);
	public Task DeleteWarehouseAsync(Guid Id);
}
