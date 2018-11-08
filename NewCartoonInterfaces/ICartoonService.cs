using NewCartoonInterfaces.Model;

namespace NewCartoonInterfaces
{
    public interface ICartoonService
    {
        Site ParseMainPageContent();
        Manga ParseChapterPage(string url);
        string DownloadChapter(string locationOnDisk,string url, bool? isSortByOrder = false);
    }
}
