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
               
                BlogTruyenCurrentMangaModel.ChapterList.ForEach(x =>
                {
                    BlogTruyenConvertChapterToListViewItem(x);
                });
            }
        }

        private void BlogTruyenConvertChapterToListViewItem(Chapter inputChapter)
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
            this.TTTchapterListView.Items.AddRange(new ListViewItem[] {
                     addedItem
                    });

        }

        //private void _resetBlogTruyenChapterListView()
        //{
        //    foreach (ListViewItem i in this.BlogTruyenChapterListView.Items)
        //    {
        //        this.BlogTruyenChapterListView.Items.Remove(i);
        //    }
        //}

        //private void BlogTruyenManagaFilterTextBox_TextChanged(object sender, EventArgs e)
        //{
        //    BlogTruyenSetListMangaToView(BlogTruyenCurrentMangaModel);
        //}

        private void BlogTruyenSetListMangaToView(Manga manga)
        {
            var searchText = BlogTruyenMangaFilterTextBox.Text;
            this.ResetChapterListView();
            foreach(var item in manga.ChapterList.Where(q => q.Name.ToLower().Contains(searchText.ToLower())).ToArray())
            {
                BlogTruyenConvertChapterToListViewItem(item);
            }
        }
        private void BlogTruyenMangaFilterTextBox_Leave(object sender, EventArgs e)
        {
            BlogTruyenSetListMangaToView(BlogTruyenCurrentMangaModel);
        }
    }
}
