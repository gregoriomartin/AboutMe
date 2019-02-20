using Domain;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class MusicBand : Title
    {
        public List<string> Members { get; set; }
        public List<string> FavoriteSongs { get; set; }
    }
}
