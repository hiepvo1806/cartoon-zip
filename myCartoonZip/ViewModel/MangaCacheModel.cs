using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myCartoonZip.ViewModel
{
    public class MangaCacheModel
    {
        public string saveDir { get; set; }
        public string url { get; set; }
        public ListViewItem selectedTruyen { get; set; }
    }
}
