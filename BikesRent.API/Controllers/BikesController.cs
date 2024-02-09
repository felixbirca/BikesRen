using System.Collections;
using BikesRent.BusinessLogicLayer;
using BikesRent.BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikesRent.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BikesController : ControllerBase
{
    private readonly IBikeService _bikeService;

    public BikesController(IBikeService bikeService)
    {
        _bikeService = bikeService;
    }
    
    [HttpGet]
    public async Task<ICollection<BikeViewModel>> Index()
    {
        var bikeViewModels = await _bikeService.GetAllBikes();
        
        return bikeViewModels;
    }

    [HttpGet]
    public async Task<BikeViewModel> GetById(string id)
    {
        var bike = (await _bikeService.GetAllBikes()).Where(x => x.Id == id).FirstOrDefault();
        return bike;
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(CreateBikeModel model)
    {
        await _bikeService.CreateBike(model);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> Edit(UpdateBikeModel model)
    {
        await _bikeService.UpdateBike(model);
        return Ok();
    }
}