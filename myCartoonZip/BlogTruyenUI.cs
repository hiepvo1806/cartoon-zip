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
        public Manga BlogTruyenCurrentMangaModel { get; set; }
        private void blogTruyenClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.blogTruyenUrl.Text))
            {
                MessageBox.Show("No Blog Truyen link!", "Error",
                    MessageBoxButtons.OK);
            }
            else
            {
                this.ResetChapterListView();
                var url = this.blogTruyenUrl.Text;
                BlogTruyenCurrentMangaModel = blogTruyenService.ParseChapterPage(url);
                var listViewItems = new List<ListViewItem>();
                BlogTruyenCurrentMangaModel.ChapterList.ForEach(x =>
                {
                    listViewItems.AddRange( BlogTruyenConvertChapterToListViewItem(x) );
                });
                this.TTTchapterListView.Items.AddRange(listViewItems.ToArray());
            }
        }

        private ListViewItem[] BlogTruyenConvertChapterToListViewItem(Chapter inputChapter)
        {
            var truyenPageProp = typeof(Chapter)
                   .GetProperties().OrderBy(o => o.Name)
                   .Where(q => q.Name != "Name").ToList();
            var addedItem = new ListViewItem(inputChapter.Name, 0);
            truyenPageProp.ForEach(i =>
            {
                var val = i.GetValue(inputChapter, null).ToString();
                addedItem.SubItems.Add(val);
            });
            return new ListViewItem[] {
                     addedItem
                    };
        }

        private void BlogTruyenSetListMangaToView(Manga manga)
        {
            var searchText = BlogTruyenMangaFilterTextBox.Text;
            this.ResetChapterListView();
            var listViewItems = new List<ListViewItem>();
            foreach (var item in manga.ChapterList.Where(q => q.Name.ToLower().Contains(searchText.ToLower())).ToArray())
            {
                listViewItems.AddRange( BlogTruyenConvertChapterToListViewItem(item) );
            }
            this.TTTchapterListView.Items.AddRange(listViewItems.ToArray());
        }
        private void BlogTruyenMangaFilterTextBox_Leave(object sender, EventArgs e)
        {
            BlogTruyenSetListMangaToView(BlogTruyenCurrentMangaModel);
        }
    }
}
