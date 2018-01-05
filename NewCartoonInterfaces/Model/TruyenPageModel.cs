using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartoonInterface
{
    public class TruyenPageModel
    {
        public byte[] MainPicture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Chapter> ChapterList { get; set; }
        public TruyenPageModel() {
            ChapterList = new List<Chapter>();
        }
    }

    public class Chapter
    {
        public string TenChuong { get; set; }
        public string NgayCapNhat { get; set; }
        public string ChuongUrl { get; set; }
    }
}
