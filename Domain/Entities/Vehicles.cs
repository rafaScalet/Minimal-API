using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI.Entities;

public class Vehicles
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; } = default!;

	[Required]
	[StringLength(150)]
	public string Name { get; set; } = default!;

	[Required]
	[StringLength(100)]
	public string Mark { get; set; } = default!;

	[Required]
	public int Year { get; set; } = default!;
}