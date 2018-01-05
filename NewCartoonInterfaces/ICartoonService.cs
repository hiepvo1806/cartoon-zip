using CartoonInterface;
using NewCartoonInterfaces.Model;

namespace NewCartoonInterfaces
{
    public interface ICartoonService
    {
        Site ParseMainPageContent(string url);
        TruyenPageModel ParsePersonalPage(string url);
        string DownloadTruyen(string locationOnDisk,string url);
    }
}
