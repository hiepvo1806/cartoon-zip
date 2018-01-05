using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewCartoonInterfaces;
namespace newCartoonImplementation.site
{
    public class TTTParser : BasePageParser
    {
        public override string Alias  => "Truyen Tranh Tuan";
        public TTTParser()
        {
        }



    }



    public class BasePageParser
    {
        public virtual string Alias { get; set; }
        public Site SiteInfo { get; set; }
    }
}
