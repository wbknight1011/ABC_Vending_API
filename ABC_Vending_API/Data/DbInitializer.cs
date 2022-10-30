using ABC_Vending_API.Contexts;
using ABC_Vending_API.Models;

namespace ABC_Vending_API.Data;

public static class DbInitializer
{
	public static void Initialize(OrganizationContext context)
	{
		context.Database.EnsureCreated();

		// If there are any warehouses, Database has already been initialized.
		if (context.Warehouses.Any())
		{
			return;
		}

		var firstWarehouseId = Guid.NewGuid();
		var secondWarehouseId = Guid.NewGuid();
		var thirdWarehouseId = Guid.NewGuid();
		var fourthWarehouseId = Guid.NewGuid();

		var warehouses = new Warehouse[]
		{
			new Warehouse(firstWarehouseId, "First Opened DB Warehouse LLC", "Gainesville, FL"),
			new Warehouse(secondWarehouseId, "Second Warehouse API Inc", "Tampa, FL"),
			new Warehouse(thirdWarehouseId, "Warehousing for Thirds API", "Albequerqe, AZ"),
			new Warehouse(fourthWarehouseId, "Fourth Time is Our Charm", "Sweetwater, AL"),
		};

		var machines = new VendingMachine[] 
		{
			new VendingMachine(Guid.NewGuid(), firstWarehouseId, "Coca Cola Machine"),
			new VendingMachine(Guid.NewGuid(), firstWarehouseId, "Pepsi Cola Machine"),
			new VendingMachine(Guid.NewGuid(), secondWarehouseId, "Coca Cola Machine"),
			new VendingMachine(Guid.NewGuid(), secondWarehouseId, "Mixed Product Machine"),
			new VendingMachine(Guid.NewGuid(), secondWarehouseId, "Pepsi Cola Machine"),
			new VendingMachine(Guid.NewGuid(), thirdWarehouseId, "Coca Cola Machine"),
			new VendingMachine(Guid.NewGuid(), fourthWarehouseId, "Coca Cola Machine"),
		};

		var products = new Product[] 
		{
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Pepsi", "Soda", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Coca cola", "Soda", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Dr. Pepper", "Soda", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Mountain Dew", "Soda", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Doritos", "Chips", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Cheetos", "Chips", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Honey Bun", "Confectionary", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, firstWarehouseId, "Powdered Donuts", "Confectionary", 1.25m, 1255),			
			new Product(Guid.NewGuid(), null, secondWarehouseId, "Doritos", "Chips", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, secondWarehouseId, "Cheetos", "Chips", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, secondWarehouseId, "Honey Bun", "Confectionary", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, secondWarehouseId, "Powdered Donuts", "Confectionary", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, thirdWarehouseId, "Doritos", "Chips", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, thirdWarehouseId, "Cheetos", "Chips", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, thirdWarehouseId, "Honey Bun", "Confectionary", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, thirdWarehouseId, "Powdered Donuts", "Confectionary", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, fourthWarehouseId, "Honey Bun", "Confectionary", 1.25m, 1255),
			new Product(Guid.NewGuid(), null, fourthWarehouseId, "Powdered Donuts", "Confectionary", 1.25m, 1255),
		};

		context.Warehouses.AddRange(warehouses);

		context.Products.AddRange(products);
		
		context.VendingMachines.AddRange(machines);

		context.SaveChanges();
	}
}
