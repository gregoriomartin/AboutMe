using HelpSite.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HelpSite.Services
{
    public class MyInfoService
    {
        public MyInfoService()
        {
            ThingsILove = new List<string>();
            ThingsIHate = new List<string>();
            Games = new List<Game>();
            Movies = new List<Movie>();
            MusicBands = new List<MusicBand>();
        }

        public List<string> ThingsILove { get; private set; }
        public List<string> ThingsIHate { get; private set; } 
        public List<Game> Games { get; private set; }
        public List<Movie> Movies { get; private set; }
        public List<MusicBand> MusicBands { get; private set; }

        public void CacheThingsILove(List<string> thingsILove)
        {
            ThingsILove = thingsILove;
        }

        public void CacheThingsIHate(List<string> thingsIHate)
        {
            ThingsIHate = thingsIHate;
        }

        public void CacheGames(List<Game> games)
        {
            Games = games;
        }

        public void CacheMovies(List<Movie> movies)
        {
            Movies = movies;
        }

        public void CacheMusicBands(List<MusicBand> musicBands)
        {
            MusicBands = musicBands;
        }
    }
}
