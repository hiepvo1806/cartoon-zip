using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCartoonInterfaces.Model
{
    public class Manga
    {
        public string Name { get; set; }
        public byte[] MainPicture { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string LastedChapterName { get; set; }
        public string ModifiedDate { get; set; }
        public string CreatedDate { get; set; }
        public List<Chapter> ChapterList { get; set; }
        public Manga()
        {
            ChapterList = new List<Chapter>();
        }
    }

    public class Chapter
    {
        public string Name { get; set; }
        public string ModifiedDate { get; set; }
        public string ChapterUrl { get; set; }
        //public List<string> ImgUrls { get; set; }
    }
}
