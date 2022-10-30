using System.ComponentModel.DataAnnotations;

namespace ABC_Vending_API.DTOs;


public class CreateWarehouseDto
{
	[Required(ErrorMessage="A warehouse name is required.")]
	[StringLength(50)]
	public string Name { get; set; }

	[Required(ErrorMessage="A location is required.")]
	[StringLength(100)]
	public string Location { get; set; }
}
