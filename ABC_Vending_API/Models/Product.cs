namespace ABC_Vending_API.Models;

public record Product(Guid Id, Guid? VendingMachineId, Guid? WarehouseId, string Name, string Category, decimal Cost, int Stock)
{
}
