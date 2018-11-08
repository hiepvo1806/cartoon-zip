using HtmlAgilityPack;
using NewCartoonInterfaces;
using NewCartoonInterfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace newCartoonImplementation
{
    public class BlogTruyenCartoonService : ICartoonService
    {
        private readonly ILogService<Site> _logger;
        //private string mangaProviderUrl = @"http://truyentranhtuan.com/danh-sach-truyen/";
        private string mainUrl = "https://blogtruyen.com";
        public BlogTruyenCartoonService(ILogService<Site> logService)
        {
            _logger = logService;
        }
        public string DownloadChapter(string locationOnDisk, string url, bool? isSortByOrder = false)
        {
            string fileError = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)";
                    string downloadString = client.DownloadString(url);
                    var imgLinkArr = ParseFullPageHtmlToLinkArr(downloadString);

                    for (var i = 0; i < Math.Max(imgLinkArr.Length, 0); i++)
                    {
                        string imgLink;
                        var isDownloaded = false;
                        var downloadTry = 0;
                        imgLink = imgLinkArr[i];
                        while (!isDownloaded && downloadTry < 2)
                        {
                            string fileName = "";
                            try
                            {
                                fileName = imgLink.Split('/').Last();
                                fileName = fileName.Split(' ', '?', '/').First();
                                if (isSortByOrder == true)
                                {
                                    fileName = i.ToString() + '.' + fileName.Split('.').Last();
                                }

                                var location = string.Format(@"{0}\{1}", locationOnDisk,
                                        fileName);

                                client.DownloadFile(new Uri(imgLink),
                                    $@"{locationOnDisk}\{fileName}");
                                isDownloaded = true;
                            }
                            catch (Exception e)
                            {
                                fileError += e.Message + "\r\n";
                                _logger.WriteCurrentLog(e.Message);
                            }
                        }
                        if (downloadTry == 2) fileError = "Try twice \r\n" + fileError;
                    }
                }
                catch (Exception e)
                {
                    _logger.WriteCurrentLog(e.Message);
                }

            }

            return string.IsNullOrEmpty(fileError) ? fileError : "Error in these pic:" + fileError;
        }

        private string[] ParseFullPageHtmlToLinkArr(string inputFullHtml)
        {
            try
            {
                //var beginString = "var slides_page_url_path = [";
                var beginString = "<article id=\"content\">";
                var a = inputFullHtml.IndexOf(beginString);
                var b = inputFullHtml.Remove(0, a + beginString.Length);
                var c = b.Remove(b.IndexOf("</article>"));
                var d = c.Replace("<img src=", "").Replace("/>", ",").Replace("\"", "");
                return d.Split(',')
                        .Select(s => s.Trim(',', '\"')).ToArray();
            }
            catch
            {
                return (new List<string>()).ToArray();
            }
        }

        public Manga ParseChapterPage(string url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            var page = document.DocumentNode;
            var result = new Manga();
            var allChapterElements = page.SelectNodes("//div[contains(@id, 'list-chapters')]").First();
            foreach (var chapterElement in allChapterElements.SelectNodes("//a"))
            {
                var chapterName = chapterElement.InnerText.Trim(' ', '\r', '\n');
                if (chapterElement.Attributes.Count() < 2) continue;
                var chapterUrl = chapterElement.Attributes[1].Value;
                result.ChapterList.Add(new Chapter
                {
                    Name = chapterName,
                    ChapterUrl = mainUrl + chapterUrl,
                    ModifiedDate = ""
                });
            }
            return result;
        }

        public Site ParseMainPageContent()
        {
            throw new NotImplementedException();
        }
    }
}
