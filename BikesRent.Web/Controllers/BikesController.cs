using BikesRent.DataAccessLayer;
using BikesRent.DataAccessLayer.Entities;
using BikesRent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikesRent.Web.Controllers;

public class BikesController : Controller
{
    private readonly BikesDbContext _dbContext;

    public BikesController(BikesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
        var bikes = await _dbContext.Bikes.ToListAsync();
        var bikeViewModels = new List<BikeViewModel>();

        foreach (var bike in bikes) 
        {
            bikeViewModels.Add(new BikeViewModel
            {
                Id = bike.Id,
                Brand = bike.Brand,
                IsElectric = bike.IsElectric,
                Type = bike.Type
            });
        }
        
        return View(bikeViewModels);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBikeModel model)
    {
        await _dbContext.Bikes.AddAsync(new Bike
        {
            Brand = model.Brand,
            IsElectric = model.IsElectric,
            Type = model.Type
        });

        await _dbContext.SaveChangesAsync();
        
        return RedirectToAction("Index");
    }
    
}