using System.Collections.Generic;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyInfoController : ControllerBase
    {
        private readonly MyInfoContext _dbContext;

        public MyInfoController(MyInfoContext context)
        {
            _dbContext = context;
        }

        [HttpPost("addThings")]
        public IActionResult NewThing([FromBody]List<Thing> things)
        {
            _dbContext.Things.AddRange(things);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("addMovies")]
        public IActionResult ChangeMyFavoriteMovies([FromBody]List<Movie> movies)
        {
            _dbContext.Movies.AddRange(movies);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("addMusicBands")]
        public IActionResult ChangeMyFavoriteMusicBands([FromBody]List<MusicBand> musicBands)
        {
            _dbContext.MusicBands.AddRange(musicBands);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("addGames")]
        public IActionResult ChangeMyFavoriteGames([FromBody]List<Game> games)
        {
            _dbContext.Games.AddRange(games);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}