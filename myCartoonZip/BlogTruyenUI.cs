using myCartoonZip.ViewModel;
using NewCartoonInterfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
                this.chapterCountNo.Text = this.TTTchapterListView.Items.Count.ToString();
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
            this.chapterCountNo.Text = this.TTTchapterListView.Items.Count.ToString();
        }
        private void BlogTruyenMangaFilterTextBox_Leave(object sender, EventArgs e)
        {
            BlogTruyenSetListMangaToView(BlogTruyenCurrentMangaModel);
        }

        private void BlogTruyen_DownloadChapter(object sender, EventArgs e)
        {
            this.LogTabTextBox.Text = "";
            this.DisplayedResult = new List<string>();

            var locationOnDisk = this.TTTUrlToSaveTextbox.Text;
            if (string.IsNullOrEmpty(locationOnDisk) || this.TTTchapterListView.SelectedItems.Count == 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(locationOnDisk) ? "Empty Save Directory" : "Please select Manga to Download", "Error",
                           MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    foreach (ListViewItem selectedTruyen in this.TTTchapterListView.SelectedItems)
                    {
                        var url = selectedTruyen.SubItems[1].Text;
                        var saveDir = locationOnDisk + '\\' + selectedTruyen.SubItems[0].Text.Replace(":", "_");
                        Directory.CreateDirectory(saveDir);
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += new DoWorkEventHandler(BlogTruyen_BackgroundDoWork);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BlogTruyen_BackgroundComplete);
                        worker.RunWorkerAsync(new MangaCacheModel()
                        {
                            saveDir = saveDir,
                            url = url,
                            selectedTruyen = selectedTruyen
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error",
                            MessageBoxButtons.OK);
                }
            }
        }

        private void BlogTruyen_BackgroundDoWork(object sender, DoWorkEventArgs e)
        {
            MangaCacheModel inputManga = e.Argument as MangaCacheModel;
            e.Result = new
            {
                result = blogTruyenService.DownloadChapter(inputManga.saveDir, inputManga.url, this.IsSortByOrder.Checked),
                chuong = ((string)inputManga.url).Remove(inputManga.url.Length - 1).Split('/').Last(),
                selectTruyen = inputManga.selectedTruyen
            };
        }

        private void BlogTruyen_BackgroundComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            //var result = (dynamic)e.Result;
            //string displayedText = "";
            //if (string.IsNullOrEmpty((string)result.result))
            //{
            //    displayedText = "\n Download Complete :" + (string)result.chuong;
            //    DisplayedResult.Add(displayedText);

            //    ((ListViewItem)result.selectTruyen).Checked = true;
            //}
            //else
            //{
            //    displayedText = "Error :" + (string)result.chuong;
            //}
            //LogTabTextBox.Text = "Begin download" + string.Join("\r\n", DisplayedResult.OrderBy(s => s).ToArray());
        }
    }
}
