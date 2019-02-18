
using HelpSite.Models;
using HelpSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpSite.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly MyInfoService _myInfoService;

        public AboutMeController(MyInfoService myInfoService)
        {
            _myInfoService = myInfoService;
        }

        public IActionResult HateAndLove()
        {
            var viewModel = new HateAndLoveViewModel()
            {
                ThingsIHate = _myInfoService.ThingsIHate,
                ThingsILove = _myInfoService.ThingsILove
            };
            return View(viewModel);
        }

        public IActionResult FavoriteTitles()
        {
            var viewModel = new FavoriteTitlesViewModel()
            {
                Games = _myInfoService.Games,
                Movies = _myInfoService.Movies,
                MusicBands = _myInfoService.MusicBands
            };
            return View(viewModel);
        }
    }
}