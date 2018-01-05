using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewCartoonInterfaces
{
    public interface ILogService<T>
    {
        void WriteCurrentLog(string content, string fileName = "");
        void SaveObjectToFile(string url, T objToSave);
        T GetObjectFromFile(string url);
    }
}
