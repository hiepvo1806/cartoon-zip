using NewCartoonInterfaces.Model;

namespace NewCartoonInterfaces
{
    public interface ICartoonService
    {
        Site ParseMainPageContent(string url);
        Manga ParseChapterPage(string url);
        string DownloadChapter(string locationOnDisk,string url);
    }
}
