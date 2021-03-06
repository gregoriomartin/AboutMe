﻿using HelpSite.Entities;
using System.Collections.Generic;

namespace HelpSite.Models
{
    public class FavoriteTitlesViewModel
    {
        public List<Movie> Movies { get; set; }
        public List<Game> Games { get; set; }
        public List<MusicBand> MusicBands { get; set; }
    }
}
