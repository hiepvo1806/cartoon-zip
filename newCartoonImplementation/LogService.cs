using NewCartoonInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newCartoonImplementation
{
    public class LogService : ILogService
    {
        public async void WriteCurrentLog(string content)
        {
            string logFileName = $"{DateTime.Now.ToString(@"dd-MM-yyyy_HH-mm")}.txt";
            if (!File.Exists(logFileName))
                File.Create(logFileName);
            string currentContent = "";
            using (var reader = new StreamReader(logFileName))
            {
                currentContent = await reader.ReadToEndAsync();
            }
            currentContent = $"{currentContent} \r\n{content}";
            using (var writer = new StreamWriter(logFileName))
            {
                await writer.WriteAsync(content);
            }
        }
    }
}
