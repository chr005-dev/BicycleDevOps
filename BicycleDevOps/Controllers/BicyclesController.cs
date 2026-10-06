using BicycleApi.Models;
using BicycleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BicycleApi.Controllers;

[ApiController]
[Route("api/bicycles")]
public class BicyclesController(BicycleService service)
    : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<Bicycle>> GetAll()
    {
        return Ok(service.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Bicycle> GetById(int id)
    {
        var bicycle = service.GetById(id);

        if (bicycle is null)
        {
            return NotFound();
        }

        return Ok(bicycle);
    }

    [HttpPost]
    public ActionResult<Bicycle> Add(
        CreateBicycleRequest request)
    {
        var bicycle = service.Add(
            request.Brand, request.Price);

        return CreatedAtAction(
            nameof(GetById),
            new { id = bicycle.Id },
            bicycle);
    }
}