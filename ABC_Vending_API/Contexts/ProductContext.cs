using ABC_Vending_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ABC_Vending_API.Contexts;

public class ProductContext
{
	public DbSet<Product> Products { get; set; }
}
