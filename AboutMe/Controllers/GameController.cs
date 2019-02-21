using AboutMe.Models;
using Domain;
using GameCore;
using GameCore.GameModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

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
            string matchGUID;
            if ((matchGUID = HttpContext.Session.GetString("MatchGuid")) != null)
            {
                return RedirectToAction("AnswerTheQuestion");
            }

            var vm = new GameViewModel();
            return View(vm);
        }


        public IActionResult Start(GameViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("BeforeStart", vm);
            }
            
            string matchGUID;

            HttpContext.Session.SetString("PlayerName", vm.Name);

            if ((matchGUID = HttpContext.Session.GetString("MatchGuid")) == null)
            {
                matchGUID = $"{vm.Name}-{Guid.NewGuid()}";
                HttpContext.Session.SetString("MatchGuid", matchGUID);
            }

            return RedirectToAction("AnswerTheQuestion");
        }

        public IActionResult AnswerTheQuestion()
        {
            Match match = GetMatch();
            Stage stage = match.NextStage();

            BeforeNextStage(stage.ViewModel);

            if (stage.ViewName == "Score")
            {
                _dbContext.Players.Add(match.Player);
                _dbContext.SaveChanges();
            }

            return View(stage.ViewName, stage.ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AnswerChooseOne(ChooseOneViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AnswerTheQuestion");
            }

            GetMatch().Answer(viewModel.Answer);

            return RedirectToAction("AnswerTheQuestion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AnswerCommon(BaseQuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AnswerTheQuestion");
            }

            GetMatch().Answer(viewModel.Answer.ToString());

            return RedirectToAction("AnswerTheQuestion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Finish(ScoreViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AnswerTheQuestion");
            }

            var player = GetMatch().Player;

            var persistedPlayer = _dbContext.Players.SingleOrDefault(p => p.Id == player.Id);

            if (persistedPlayer != null)
                persistedPlayer.MessageForGM = viewModel.Answer;

            _dbContext.SaveChanges();

            ClearCache();

            return RedirectToAction("ScoreBoard");
        }

        public IActionResult ScoreBoard()
        {

            return View(_dbContext.Players.OrderByDescending(p => p.Score).ToList());

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

        private void ClearCache()
        {
            _memoryCache.Remove(HttpContext.Session.GetString("MatchGuid"));

            HttpContext.Session.Remove("MatchGuid");
        }

        private void BeforeNextStage(BaseQuestionViewModel vm)
        {
            ViewData["Question"] = vm.Question;
        }
    }
}