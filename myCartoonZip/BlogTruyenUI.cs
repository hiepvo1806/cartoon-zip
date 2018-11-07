using NewCartoonInterfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myCartoonZip
{
    public partial class MangaDownloadForm : Form
    {
        private void blogTruyenClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.blogTruyenUrl.Text))
            {
                MessageBox.Show("No Blog Truyen link!", "Error",
                    MessageBoxButtons.OK);
            }
            else
            {
                this._resetChapterListView();
                var url = this.blogTruyenUrl.Text;
                truyenPageModel = blogTruyenService.ParseChapterPage(url);
                var truyenPageProp = typeof(Chapter)
                    .GetProperties().OrderBy(o => o.Name)
                    .Where(q => q.Name != "Name").ToList();
                truyenPageModel.ChapterList.ForEach(x =>
                {
                    var addedItem = new ListViewItem(x.Name, 0);
                    truyenPageProp.ForEach(i =>
                    {
                        var val = i.GetValue(x, null).ToString();
                        addedItem.SubItems.Add(val);
                    });
                    this.TTTchapterListView.Items.AddRange(new ListViewItem[] {
                     addedItem
                    });
                });
            }
        }
    }
}
