using NewCartoonInterfaces.Model;
using System.Collections.Generic;

namespace NewCartoonInterfaces
{
    public class Site
    {
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public List<Manga> MangaList { get; set; }
        
        public Site()
        {
            MangaList = new List<Manga>();
        }
    }
}
