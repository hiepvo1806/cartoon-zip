using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CartoonInterface;
using HtmlAgilityPack;
using NewCartoonInterfaces;
using NewCartoonInterfaces.Model;

namespace newCartoonImplementation
{
    public class CartoonService : ICartoonService
    {
        public HomePageModel GetPageContent(string url)
        {
            //get the page
            var web = new HtmlWeb();
            var document = web.Load(url);
            var page = document.DocumentNode;
            var result = new HomePageModel();
            result.DanhSachTruyenMain = new List<TruyenHomePageModel>();

            var newChapterSection = page.SelectNodes("//div[contains(@id, 'new-chapter')]").First();
            var resultWriteToLog = new List<string>();
            foreach (var item in newChapterSection.SelectNodes("//span[contains(@class, 'manga')]"))
            {
                var parentDiv = item.ParentNode;
                var tenTruyen = item.InnerText.Trim(' ');
                var truyenUrl = item.FirstChild.Attributes[0].Value;

                var chuongMoiNhatNode = parentDiv.ChildNodes.Where(q => q.Attributes.Any(a => a.Name == "class" && a.Value == "chapter")).First();
                var chuongMoiNhat = chuongMoiNhatNode.InnerText.Trim(' ', '\r', '\n');

                var ngayCapNhatNode = parentDiv.ChildNodes.Where(q => q.Attributes.Any(a => a.Name == "class" && a.Value == "current-date")).First(); 
                var ngayCapNhat = ngayCapNhatNode.InnerText.Trim(' ', '\r', '\n');
                result.DanhSachTruyenMain.Add(new TruyenHomePageModel
                {
                    TenTruyen = tenTruyen,
                    Url = truyenUrl,
                    ChuongMoiNhat = chuongMoiNhat,
                    NgayCapNhat = ngayCapNhat
                });
                var logText = string.Format("{0}\t{1}",
                    tenTruyen,
                    truyenUrl);
                resultWriteToLog.Add(logText);
            }
            File.WriteAllText(@"E:\WriteText.txt", string.Join("\r\n", resultWriteToLog));
            return result;
        }

        public HomePageModel ParseMainPageContent(string url)
        {
            return GetPageContent(url);
        }
        public TruyenPageModel ParsePersonalPage(string url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            var page = document.DocumentNode;
            var result = new TruyenPageModel();
            //manga-chapter
            var chapters = page.SelectNodes("//div[contains(@id, 'manga-chapter')]").First();
            foreach (var item in chapters.SelectNodes("//span[contains(@class, 'chapter-name')]"))
            {
                var tenChuong = item.InnerText.Trim(' ', '\r', '\n');
                var chuongUrl = item.ChildNodes.FirstOrDefault(q=>q.Name=="a").Attributes[0].Value;
                result.DanhSachChuong.Add(new PersonalTruyen
                {
                    TenChuong = tenChuong,
                    ChuongUrl = chuongUrl,
                    NgayCapNhat = ""
                });
            }
            return result;
        }

        public string DownloadTruyen(string locationOnDisk,string url)
        {
            string fileError = "";
            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(url);
                var allLink = new List<string>();

                var parseResultType1 = RenderType1(downloadString)
                    .Split(',')
                    .Select(s=> s.Trim(',', '\"')).ToArray();
                var parseResultType2 = RenderType2(downloadString)
                    .Split(',')
                    .Select(s => s.Trim(',', '\"')).ToArray();

                var duplicateFile = parseResultType1
                        .Select(s => s.Split('/').Last())
                        .GroupBy(s => s).Any(s => s.Count() > 1);

                for (var i = 0; i < Math.Max(parseResultType1.Length, parseResultType2.Length); i++)
                {
                    string link;
                    var isDownloaded = false;
                    var downloadTry = 0;
                    link = parseResultType1[i];
                    while (!isDownloaded && downloadTry<2)
                    {
                        string fileName = "";
                        try
                        {
                            fileName = link.Split('/').Last();
                            var location =
                                duplicateFile == false ?
                                string.Format(@"{0}\{1}", locationOnDisk,
                                    fileName.Split(' ', '?', '/').First())
                                : string.Format(@"{0}\{1}_{2}", locationOnDisk,
                                i, fileName.Split(' ', '?', '/').First());

                            client.DownloadFile(new Uri(link),
                                $@"{locationOnDisk}\{fileName.Split(' ', '?', '/').First()}");
                            isDownloaded = true;
                        }
                        catch
                        {
                            link = parseResultType2.FirstOrDefault(s=>s.IndexOf(fileName) !=-1);
                            if(string.IsNullOrEmpty(link))
                            {
                                downloadTry = 1;
                                fileError = "null link in parse 2 \r\n" + fileError;
                            }
                            downloadTry++;
                        }
                    }
                    if (downloadTry == 2) fileError = "Try twice \r\n" + fileError;
                }
            }
            
            return string.IsNullOrEmpty( fileError) ? fileError : "Error in these pic:" + fileError;
        }

        private string RenderType1 (string inputFullHtml)
        {
            try
            {
                //var beginString = "var slides_page_url_path = [";
                var beginString = "var slides_page_path = [";
                var a = inputFullHtml.IndexOf(beginString);
                var b = inputFullHtml.Remove(0, a + beginString.Length);
                return b.Remove(b.IndexOf("\"]"));
            }
            catch
            {
                return "";
            }

          
        }

        private string RenderType2(string inputFullHtml)
        {
            try
            {
                var beginString = "var slides_page_url_path = [";
                //var beginString = "var slides_page_path = [";
                var a = inputFullHtml.IndexOf(beginString);
                var b = inputFullHtml.Remove(0, a + beginString.Length);
                return b.Remove(b.IndexOf("\"]"));
            }
            catch
            {
                return "";
            }

        }

    }
}
