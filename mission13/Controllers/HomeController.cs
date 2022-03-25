using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mission13.Models;

namespace mission13.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBowlingRepository _repo { get; set; }

        public HomeController(ILogger<HomeController> logger, IBowlingRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // Home Page route
        public IActionResult Index(int id = 0)
        {
            var listBowlers = new List<Bowler>();
            
            // Show all bowlers
            if (id == 0)
            {
                listBowlers = _repo.Bowlers
                .ToList();

                ViewBag.Selected = "All Bowlers";
            }

            // Show bowlers on filtered team
            else
            {
                listBowlers = _repo.Bowlers
                .Where(x => x.TeamID == id)
                .ToList();

                Team team = _repo.Teams
                    .FirstOrDefault(x => x.TeamID == id);
                ViewBag.Selected = team.TeamName;
            }
            

            return View(listBowlers);
        }

        // Add Bowler routes
        [HttpGet]
        public IActionResult AddBowler()
        {
            var newBowler = new Bowler();
            ViewBag.Teams = _repo.Teams.ToList();
            return View("BowlerForm", newBowler);
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _repo.AddBowler(bowler);
                return RedirectToAction("Confirmation", bowler);
            }
            else
            {
                var newBowler = new Bowler();
                ViewBag.Teams = _repo.Teams.ToList();
                return View("BowlerForm", newBowler);
            }
        }

        // Edit Bowler routes
        [HttpGet]
        public IActionResult EditBowler(int id)
        {
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == id);
            ViewBag.Teams = _repo.Teams.ToList();
            return View("BowlerForm", bowler);
        }

        [HttpPost]
        public IActionResult EditBowler(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveBowler(bowler);
                return RedirectToAction("Confirmation", bowler);
            }
            else
            {
                return View("BowlerForm", bowler);
            }
        }

        // Delete Bowler routes
        [HttpGet]
        public IActionResult DeleteBowler(int id)
        {
            var bowler = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == id);
            return View(bowler);
        }

        [HttpPost]
        public IActionResult DeleteBowler(Bowler bowler)
        {
            _repo.DeleteBowler(bowler);
            return RedirectToAction("Index");
        }

        public IActionResult Confirmation(Bowler bowler)
        {
            return View(bowler);
        }

        // Default Error route
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
