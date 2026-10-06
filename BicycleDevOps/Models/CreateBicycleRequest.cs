using System.ComponentModel.DataAnnotations;

namespace BicycleApi.Models;

public class CreateBicycleRequest
{
    [Required]
    public string Brand { get; set; } = "";

    [Range(typeof(decimal), "0.01", "1000000")]
    public decimal Price { get; set; }
}
