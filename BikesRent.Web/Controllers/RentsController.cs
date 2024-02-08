using BikesRent.DataAccessLayer;
using BikesRent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikesRent.Web.Controllers;

public class RentsController : Controller
{
    private readonly BikesDbContext _dbContext;

    public RentsController(BikesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IActionResult> Index()
    {
        var rentHistories = await _dbContext.RentHistories.Include(x=>x.Bike)
                                                                .Include(x=> x.User)
                                                                .ToListAsync();
        var rentViewModels = new List<RentHistoryViewModel>();
        
        foreach (var rent in rentHistories) 
        {
            rentViewModels.Add(new RentHistoryViewModel
            {
                Id = rent.Id,
                Bike = rent.Bike,
                User= rent.User,
                StartTime = rent.StartTime,
                EndTime = rent.EndTime,
                IsActive = rent.IsActive
            });
        }
        
        return View(rentViewModels);
    }
}