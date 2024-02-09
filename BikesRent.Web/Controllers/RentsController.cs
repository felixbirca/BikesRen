using BikesRent.BusinessLogicLayer;
using BikesRent.BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BikesRent.Web.Controllers;

public class RentsController : Controller
{
    private readonly ISubscriptionService _subscriptionService;
    private readonly IUserService _userService;
    private readonly IBikeService _bikeService;

    public RentsController(ISubscriptionService subscriptionService, IUserService userService, IBikeService bikeService)
    {
        _subscriptionService = subscriptionService;
        _userService = userService;
        _bikeService = bikeService;
    }

    public async Task<IActionResult> Index()
    {
        var rentHistories = (await _subscriptionService.GetAll()).ToList();

        return View(rentHistories);
    }

    public async Task<IActionResult> Rent()
    {
        var userId = HttpContext.Request.Cookies["user_id"];

        if (userId == null)
        {
            return View("Login", await _userService.GetAll());
        }

        var rentBikeViewModel = new RentBikeViewModel();
        rentBikeViewModel.AvailableBikes = await _bikeService.GetAvailableBikes();
        rentBikeViewModel.CanRentMore = await _subscriptionService.CanRentABike(userId);

        return View(rentBikeViewModel);
    }

    [HttpPost]
    public ActionResult Login(string userId)
    {
        HttpContext.Response.Cookies.Append("user_id", userId);
        return RedirectToAction("Rent");
    }

    [HttpPost]
    public async Task<IActionResult> Rent(RentBikeModel model)
    {
        var userId = HttpContext.Request.Cookies["user_id"];
        model.UserId = userId;
        await _subscriptionService.RentBike(model);

        return RedirectToAction("Index");
    }
}