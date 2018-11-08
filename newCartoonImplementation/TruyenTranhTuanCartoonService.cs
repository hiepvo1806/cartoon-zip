using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using NewCartoonInterfaces;
using NewCartoonInterfaces.Model;

namespace newCartoonImplementation
{
    public class TruyenTranhTuanCartoonService : ICartoonService
    {
        private string mangaProviderUrl = @"http://truyentranhtuan.com/danh-sach-truyen/";
        private ILogService<Site> _logger;
        public TruyenTranhTuanCartoonService(ILogService<Site> logService)
        {
            _logger = logService;
        }

        //Main HomePage Content 
        public Site ParseMainPageContent()
        {
            var web = new HtmlWeb();
            var document = web.Load(mangaProviderUrl);
            var page = document.DocumentNode;
            var result = new Site();
            result.MangaList = new List<Manga>();

            var newChapterSection = page.SelectNodes("//div[contains(@id, 'new-chapter')]").First();
            var resultWriteToLog = new List<string>();
            foreach (var item in newChapterSection.SelectNodes("//span[contains(@class, 'manga')]"))
            {
                var parentDiv = item.ParentNode;
                var mangaName = item.InnerText.Trim(' ');
                var mangaUrl = item.FirstChild.Attributes[0].Value;

                var lastestChapterElement = parentDiv.ChildNodes.Where(q => q.Attributes.Any(a => a.Name == "class" && a.Value == "chapter")).First();
                var lastestChapterName = lastestChapterElement.InnerText.Trim(' ', '\r', '\n');

                var uploadDateNode = parentDiv.ChildNodes.Where(q => q.Attributes.Any(a => a.Name == "class" && a.Value == "current-date")).First();
                var uploadDate = uploadDateNode.InnerText.Trim(' ', '\r', '\n');
                result.MangaList.Add(new Manga
                {
                    Name = mangaName,
                    Url = mangaUrl,
                    LastedChapterName = lastestChapterName,
                    ModifiedDate = uploadDate
                });

                //logging
                var logText = string.Format("{0}\t{1}",
                    mangaName,
                    mangaUrl);
                resultWriteToLog.Add(logText);
            }
            _logger.WriteCurrentLog(string.Join("\r\n", resultWriteToLog), "log_");
            return result;
        }

        //Chapter page of each manga Mainpage>pick manga>chapter + manga detail
        public Manga ParseChapterPage(string url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            var page = document.DocumentNode;
            var result = new Manga();
            var allChapterElements = page.SelectNodes("//div[contains(@id, 'manga-chapter')]").First();
            foreach (var chapterElement in allChapterElements.SelectNodes("//span[contains(@class, 'chapter-name')]"))
            {
                var chapterName = chapterElement.InnerText.Trim(' ', '\r', '\n');
                var chapterUrl = chapterElement.ChildNodes.FirstOrDefault(q => q.Name == "a").Attributes[0].Value;
                result.ChapterList.Add(new Chapter
                {
                    Name = chapterName,
                    ChapterUrl = chapterUrl,
                    ModifiedDate = ""
                });
            }
            return result;
        }

        public string DownloadChapter(string locationOnDisk, string url, bool? isSortByOrder = false)
        {
            string fileError = "";
            using (WebClient client = new WebClient())
            {
                string downloadString = client.DownloadString(url);
                var allLink = new List<string>();

                var parseToLinkArrType1 = RenderType1(downloadString);
                var parseToLinkArrType2 = RenderType2(downloadString);

                var isDuplicateFileName = parseToLinkArrType1
                        .Select(s => s.Split('/').Last())
                        .GroupBy(s => s).Any(s => s.Count() > 1);

                for (var i = 0; i < Math.Max(parseToLinkArrType1.Length, parseToLinkArrType2.Length); i++)
                {
                    string link;
                    var isDownloaded = false;
                    var downloadTry = 0;
                    link = parseToLinkArrType1[i];
                    while (!isDownloaded && downloadTry < 2)
                    {
                        string fileName = "";
                        try
                        {
                            fileName = link.Split('/').Last();
                            fileName = fileName.Split(' ', '?', '/').First();
                            if (isSortByOrder == true)
                            {
                                fileName = i.ToString() + '.' + fileName.Split('.').Last();
                            }
                            var location =
                                isDuplicateFileName == false ?
                                string.Format(@"{0}\{1}", locationOnDisk,
                                    fileName)
                                : string.Format(@"{0}\{1}_{2}", locationOnDisk,
                                i, fileName);

                            client.DownloadFile(new Uri(link),
                                $@"{locationOnDisk}\{fileName}");
                            isDownloaded = true;
                        }
                        catch
                        {
                            link = parseToLinkArrType2.FirstOrDefault(s => s.IndexOf(fileName) != -1);
                            if (string.IsNullOrEmpty(link))
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

            return string.IsNullOrEmpty(fileError) ? fileError : "Error in these pic:" + fileError;
        }

        private string[] RenderType1(string inputFullHtml)
        {
            try
            {
                var beginString = "var slides_page_path = [";
                var a = inputFullHtml.IndexOf(beginString);
                var b = inputFullHtml.Remove(0, a + beginString.Length);
                var c = b.Remove(b.IndexOf("\"]"));
                return c.Split(',')
                    .Select(s => s.Trim(',', '\"')).ToArray();
            }
            catch
            {
                return (new List<string>()).ToArray();
            }
        }

        private string[] RenderType2(string inputFullHtml)
        {
            try
            {
                var beginString = "var slides_page_url_path = [";
                var a = inputFullHtml.IndexOf(beginString);
                var b = inputFullHtml.Remove(0, a + beginString.Length);
                var c = b.Remove(b.IndexOf("\"]"));
                return c.Split(',')
                    .Select(s => s.Trim(',', '\"')).ToArray();
            }
            catch
            {
                return (new List<string>()).ToArray();
            }
        }
    }
}
