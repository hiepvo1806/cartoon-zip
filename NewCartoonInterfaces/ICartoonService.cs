using CartoonInterface;
using NewCartoonInterfaces.Model;

namespace NewCartoonInterfaces
{
    public interface ICartoonService
    {
        HomePageModel ParseMainPageContent(string url);
        TruyenPageModel ParsePersonalPage(string url);
        string DownloadTruyen(string locationOnDisk,string url);
    }
}
