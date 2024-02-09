using BikesRent.BusinessLogicLayer;
using BikesRent.BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikesRent.Web.Controllers;

public class BikesController : Controller
{
    private readonly IBikeService _bikeService;

    public BikesController(IBikeService bikeService)
    {
        _bikeService = bikeService;
    }

    public async Task<IActionResult> Index()
    {
        var bikeViewModels = await _bikeService.GetAllBikes();
        
        return View(bikeViewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBikeModel model)
    {
        await _bikeService.CreateBike(model);
        
        return RedirectToAction("Index");
    }
}