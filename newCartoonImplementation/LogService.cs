using NewCartoonInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newCartoonImplementation
{
    public class LogService<T> : ILogService<T> where T:new()
    {
        private Object thisLock = new Object();
        public T GetObjectFromFile(string url)
        {
            T result;
            try
            {
                using (Stream stream = File.Open(url, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    result = ((T)bformatter.Deserialize(stream));
                }
            }
            catch
            {
                result = new T();
            }
            return result;
        }

        public void SaveObjectToFile(string url, T objToSave)
        {
            using (Stream stream = File.Open(url, FileMode.OpenOrCreate))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, objToSave);
            }
        }

        public void WriteCurrentLog(string content,string fileName = "")
        {
            lock (thisLock)
            {
                string logFileName = $"{fileName}{DateTime.Now.ToString(@"dd-MM-yyyy_HH-mm")}.txt";
                FileStream fs;
                if (!File.Exists(logFileName))
                {
                    fs = File.Create(logFileName);
                }
                else fs = File.OpenRead(logFileName);

                string currentContent = "";
                using (var reader = new StreamReader(fs))
                {
                    currentContent = reader.ReadToEnd();
                    reader.Close();
                }
                currentContent = $"{currentContent} \r\n{content}";
                using (var writer = new StreamWriter(logFileName))
                {
                    writer.Write(content);
                    writer.Close();
                }
                fs.Close();

            }
        }
    }
}
