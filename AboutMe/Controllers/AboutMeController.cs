
using AboutMe.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AboutMe.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly MyInfoContext _dbContext;

        public AboutMeController(MyInfoContext context)
        {
            _dbContext = context;
        }

        public IActionResult HateAndLove()
        {
            var viewModel = new HateAndLoveViewModel()
            {
                ThingsIHate = _dbContext.Things.Where(t => !t.Loved).Select(t => t.Text).ToList(),
                ThingsILove = _dbContext.Things.Where(t => t.Loved).Select(t => t.Text).ToList()
            };
            return View(viewModel);
        }

        public IActionResult FavoriteTitles()
        {
            var viewModel = new FavoriteTitlesViewModel()
            {
                Games = _dbContext.Games.Where(g => !g.Fake).ToList(),
                Movies = _dbContext.Movies.Where(m => !m.Fake).ToList(),
                MusicBands = _dbContext.MusicBands.Where(m => !m.Fake).ToList()
            };
            return View(viewModel);
        }
    }
}