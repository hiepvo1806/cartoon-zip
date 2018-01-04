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
                var beginStrings = new[]
                {
                    "var slides_page_url_path = [",
                    "var slides_page_path = ["
                };

                //var beginString = "var slides_page_url_path = [";
                //var a = downloadString.IndexOf(beginString);
                //var b = downloadString.Remove(0, a + beginString.Length);
                //string c="";

                //try
                //{
                //    c = b.Remove(b.IndexOf("\"]"));
                //}
                //catch {
                //    beginString = "var slides_page_path = [";
                //    a = downloadString.IndexOf(beginString);
                //    b = downloadString.Remove(0, a + beginString.Length);
                //    c = b.Remove(b.IndexOf("\"]"));
                //}
                int a = 0;
                string b = "";
                string c = "";
                var allLink = new List<string>();
                var beginString = beginStrings[0];
                try
                {
                    allLink = new List<string>();
                    a = downloadString.IndexOf(beginString);
                     b = downloadString.Remove(0, a + beginString.Length);
                     c = b.Remove(b.IndexOf("\"]"));
                    var firstLink =  c.Split(',').ToList().First().Trim(',', '\"');
                    var fileName = firstLink.Split('/').Last();
                    client.DownloadFile(new Uri(firstLink),
                        $@"{locationOnDisk}\{fileName.Split(' ', '?', '/').First()}");
                    File.Delete($@"{locationOnDisk}\{fileName.Split(' ', '?', '/').First()}");
                }
                catch
                {
                    allLink = new List<string>();
                    beginString = beginStrings[1];
                    a = downloadString.IndexOf(beginString);
                    b = downloadString.Remove(0, a + beginString.Length);
                    c = b.Remove(b.IndexOf("\"]"));
                }

                c.Split(',').ToList().ForEach(q => allLink.Add(q.Trim(',', '\"')));
                var duplicateFile = allLink.Select(s => s.Split('/').Last()).GroupBy(s => s).Any(s => s.Count() > 1);
                for (var i = 0;i< allLink.Count; i++)
                {
                    try
                    {
                        var fileName = allLink[i].Split('/').Last();
                        var locationFile = string.Format(@"{0}\{1}", locationOnDisk,
                            fileName.Split(' ', '?', '/').First());
                        if(duplicateFile) {
                            locationFile = string.Format(@"{0}\{1}_{2}", locationOnDisk,
                            i,fileName.Split(' ', '?', '/').First());
                        }
                        client.DownloadFile(new Uri(allLink[i]), locationFile);
                    }
                    catch(Exception e) {
                        if (duplicateFile) fileError = "DUPLICATE FILE FOUND \r\n" + fileError;
                        fileError += ","+ i;
                        //throw e;
                    }
                }
                if (duplicateFile) fileError = "DUPLICATE FILE FOUND \r\n" + fileError;

            }
            
            return string.IsNullOrEmpty( fileError) ? fileError : "Error in these pic:" + fileError;
        }

    }
}
