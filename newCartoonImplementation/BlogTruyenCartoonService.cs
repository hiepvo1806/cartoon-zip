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
        public BlogTruyenCartoonService(ILogService<Site> logService)
        {
            _logger = logService;
        }
        public string DownloadChapter(string locationOnDisk, string url)
        {
            string fileError = "";
            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(url);
                var parseResultType1 = RenderType1(downloadString)
                    .Split(',')
                    .Select(s => s.Trim(',', '\"')).ToArray();
                for (var i = 0; i < Math.Max(parseResultType1.Length, 0); i++)
                {
                    string link;
                    var isDownloaded = false;
                    var downloadTry = 0;
                    link = parseResultType1[i];
                    while (!isDownloaded && downloadTry < 2)
                    {
                        string fileName = "";
                        try
                        {
                            fileName = link.Split('/').Last();
                            var location = string.Format(@"{0}\{1}", locationOnDisk,
                                    fileName.Split(' ', '?', '/').First());

                            client.DownloadFile(new Uri(link),
                                $@"{locationOnDisk}\{fileName.Split(' ', '?', '/').First()}");
                            isDownloaded = true;
                        }
                        catch (Exception e)
                        {
                            _logger.WriteCurrentLog(e.Message);
                        }
                    }
                    if (downloadTry == 2) fileError = "Try twice \r\n" + fileError;
                }
            }

            return string.IsNullOrEmpty(fileError) ? fileError : "Error in these pic:" + fileError;
        }

        private string RenderType1(string inputFullHtml)
        {
            try
            {
                //var beginString = "var slides_page_url_path = [";
                var beginString = "<article id=\"content\">";
                var a = inputFullHtml.IndexOf(beginString);
                var b = inputFullHtml.Remove(0, a + beginString.Length);
                var c =  b.Remove(b.IndexOf("</article>"));
                return c.Replace("<img src=", "").Replace("/>", ",").Replace("\"", "");
            }
            catch
            {
                return "";
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
                    ChapterUrl = "https://blogtruyen.com"+ chapterUrl,
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
