using AboutMe.Helper;
using AboutMe.Models;
using GameCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AboutMe.Controllers
{
    public class GameController : Controller
    {
        private readonly IMatchFactory _matchFactory;

        public GameController(IMatchFactory matchFactory)
        {
            _matchFactory = matchFactory;
        }

        public IActionResult BeforeStart()
        {
            var vm = new GameViewModel();
            return View(vm);
        }


        public IActionResult Start(GameViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("BeforeStart", vm);

            var match = _matchFactory.BuildMatch(vm.Name);

            HttpContext.Session.SetObjectAsJson("Match", match);

            var stage = match.NextStage();

            return View(stage.ViewName, stage.ViewModel);
        }
    }
}