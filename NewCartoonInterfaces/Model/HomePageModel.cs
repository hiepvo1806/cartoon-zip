using NewCartoonInterfaces.Model;
using System.Collections.Generic;

namespace NewCartoonInterfaces
{
    public class HomePageModel
    {
        public List<TruyenHomePageModel> DanhSachTruyenMain { get; set; }
        public List<TruyenHomePageModel> MostReadDanhSach { get; set; }
        public HomePageModel()
        {
            DanhSachTruyenMain = new List<TruyenHomePageModel>();
            MostReadDanhSach = new List<TruyenHomePageModel>();
        }
    }
}
