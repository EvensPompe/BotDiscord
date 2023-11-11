using System.Collections.Generic;

namespace Music
{
    public class Artists
    {
        public int limit { get; set; }
        #nullable enable
        public string? previous { get; set; }
        public string? next { get; set; }
        public int offset { get; set; }
        public int total { get; set; }

        public List<ArtistItem> items { get; set; }
    }
}