using AboutMe.Models;
using Domain;
using GameCore;
using GameCore.GameModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AboutMe.Controllers
{
    public class GameController : Controller
    {
        private readonly IMatchFactory _matchFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly MyInfoContext _dbContext;

        public GameController(IMatchFactory matchFactory,
            IMemoryCache memoryCache,
            MyInfoContext dbContext)
        {
            _matchFactory = matchFactory;
            _memoryCache = memoryCache;
            _dbContext = dbContext;
        }

        public IActionResult BeforeStart()
        {
            var vm = new GameViewModel();
            return View(vm);
        }


        public IActionResult Start(GameViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("BeforeStart", vm);
            }

            Match match;
            Stage stage;
            string matchGUID;

            HttpContext.Session.SetString("PlayerName", vm.Name);

            if ((matchGUID = HttpContext.Session.GetString("MatchGuid")) == null)
            {
                matchGUID = $"{vm.Name}-{Guid.NewGuid()}";
                HttpContext.Session.SetString("MatchGuid", matchGUID);
            }

            if (!_memoryCache.TryGetValue(matchGUID, out ICacheEntry entry))
            {
                match = GetMatch();

                stage = match.NextStage();
            }

            return Question();
        }

        public IActionResult Question()
        {
            Match match;
            Stage stage;
            string matchGUID = HttpContext.Session.GetString("MatchGuid");

            if (_memoryCache.TryGetValue(matchGUID, out ICacheEntry entry))
            {
                match = (Match)entry.Value;
                stage = match.NextStage();
                BeforeNextStage(stage.ViewModel);

                return View(stage.ViewName, stage.ViewModel);
            }

            return RedirectToAction("ScoreBoard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AnswerChooseOne(ChooseOneViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Question();
            }

            GetMatch().Answer(viewModel.Answer);

            return Question();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AnswerCommon(BaseQuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Question();
            }

            GetMatch().Answer(viewModel.Answer.ToString());

            return Question();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Finish(ScoreViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Question();
            }

            return RedirectToAction("ScoreBoard");
        }

        public IActionResult ScoreBoard()
        {

            return View(_dbContext.Players.ToList());

        }


        private Match GetMatch()
        {
            Match match;
            string matchGUID = HttpContext.Session.GetString("MatchGuid");
            if (_memoryCache.TryGetValue(matchGUID, out ICacheEntry entry))
            {
                match = (Match)entry.Value;
            }
            else
            {
                match = _matchFactory.BuildMatch(HttpContext.Session.GetString("PlayerName"));
                CacheMatch(match, matchGUID);
            }
            return match;
        }

        private void CacheMatch(Match match, string guid)
        {
            var entry = _memoryCache.CreateEntry(guid);
            entry.Value = match;
            var cacheOpt = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20));

            _memoryCache.Set(guid, entry, cacheOpt);

            HttpContext.Session.SetString("MatchGuid", guid);
        }

        private void BeforeNextStage(BaseQuestionViewModel vm)
        {
            ViewData["Question"] = vm.Question;
        }
    }
}