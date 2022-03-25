using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using mission13.Models;

namespace mission13.Components
{
    public class TeamViewComponent : ViewComponent
    {
        private IBowlingRepository _repo { get; set; }

        public TeamViewComponent(IBowlingRepository repo)
        {
            _repo = repo;
        }

        public IViewComponentResult Invoke()
        {
            var teams = _repo.Teams.ToList();

            return View(teams);
        }
    }
}
