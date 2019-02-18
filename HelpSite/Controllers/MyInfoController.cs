using System.Collections.Generic;
using HelpSite.Entities;
using HelpSite.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelpSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyInfoController : ControllerBase
    {
        private readonly MyInfoService _myInfoService;

        public MyInfoController(MyInfoService myInfoService)
        {
            _myInfoService = myInfoService;
        }

        [HttpPost("changeLoveThings")]
        public IActionResult ChangeThingsILove([FromBody]List<string> things)
        {
            _myInfoService.CacheThingsILove(things);
            return Ok();
        }
        [HttpPost("changeHateThings")]
        public IActionResult ChangeThingsIHate([FromBody]List<string> things)
        {
            _myInfoService.CacheThingsIHate(things);
            return Ok();
        }
        [HttpPost("changeMovies")]
        public IActionResult ChangeMyFavoriteMovies([FromBody]List<Movie> movies)
        {
            _myInfoService.CacheMovies(movies);
            return Ok();
        }
        [HttpPost("changeMusicBands")]
        public IActionResult ChangeMyFavoriteMusicBands([FromBody]List<MusicBand> musicBands)
        {
            _myInfoService.CacheMusicBands(musicBands);
            return Ok();
        }
        [HttpPost("changeGames")]
        public IActionResult ChangeMyFavoriteGames([FromBody]List<Game> games)
        {
            _myInfoService.CacheGames(games);
            return Ok();
        }
    }
}