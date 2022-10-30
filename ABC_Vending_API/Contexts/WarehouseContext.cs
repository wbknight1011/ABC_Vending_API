using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Contexts;

public class WarehouseContext : DbContext
{
	public DbSet<Warehouse> Warehouses {get; set;}
}
