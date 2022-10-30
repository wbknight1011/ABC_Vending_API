using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Contexts;

public class OrganizationContext : DbContext
{
	public OrganizationContext(DbContextOptions<OrganizationContext> options) 
		: base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<Warehouse> Warehouses { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<VendingMachine> VendingMachines { get; set; }
}
