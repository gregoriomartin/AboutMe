using System.Collections.Generic;

namespace HelpSite.Entities
{
    public class Game : Title
    {
        public List<string> Plataforms { get; set; }
        public int Metascore { get; set; }
    }
}
