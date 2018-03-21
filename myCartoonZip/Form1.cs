﻿using NewCartoonInterfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using NewCartoonInterfaces.Model;
using System.Data;
using System.ComponentModel;
using System.IO;

namespace myCartoonZip
{
    public partial class Form1 : Form
    {
        private readonly ICartoonService _cartoonService;
        public Site homePageViewModel { get; set; }
        public Manga truyenPageModel { get; set; }
        public  List<ListViewItem> mainViewMangaListModel { get; set; }
        public List<string> DisplayedResult { get; set; }

        public ILogService<List<ListViewItem>> _logger;
        private string savedMangaListLocation = $"{Directory.GetCurrentDirectory()}\\saveObj.txt";

        public Form1(ICartoonService cartoonService,ILogService<List<ListViewItem>> logger)
        {
            InitializeComponent();
            _cartoonService = cartoonService;
            _logger = logger;

        }

        private void FormLoad(object sender, EventArgs e)
        {
            mainViewMangaListModel = _logger.GetObjectFromFile(savedMangaListLocation);
            SetListMangaToView(mainViewMangaListModel);
        }

        private void LoadButtonFromURLHanlder(object sender, EventArgs e)
        {
            var url = @"http://truyentranhtuan.com/danh-sach-truyen/";
            mainViewMangaListModel = new List<ListViewItem>();
            homePageViewModel = _cartoonService.ParseMainPageContent(url);
            var homePageModelProps = typeof(Manga)
                .GetProperties().OrderBy(o => o.Name).Where(q => q.Name != "Name" && q.PropertyType == typeof(string)).ToList();
            homePageViewModel.MangaList.ForEach(x =>
            {
                var addedItem = new ListViewItem(x.Name, 0);
                homePageModelProps.ForEach(i => {
                    var val = i.GetValue(x, null);
                    if (val != null) {
                        addedItem.SubItems.Add(val.ToString());
                    }  
                    
                });
                mainViewMangaListModel.Add(addedItem);
                
            });
            SetListMangaToView(mainViewMangaListModel);
            _logger.SaveObjectToFile(savedMangaListLocation, mainViewMangaListModel);
        }
        

        private void LoadChaptersHandler(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("No item selected", "Error",
                    MessageBoxButtons.OK);
            }
            else
            {
                var selectedTruyen = this.listView1.SelectedItems[0];
                foreach (ListViewItem i in this.listView2.Items)
                {
                    this.listView2.Items.Remove(i);
                }
                var url = selectedTruyen.SubItems[3].Text;
                truyenPageModel = _cartoonService.ParseChapterPage(url);
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
                    this.listView2.Items.AddRange(new ListViewItem[] {
                     addedItem
                    });
                });
            }
        }

        private void DownloadChaptersHandler(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            this.DisplayedResult = new List<string>();

            var locationOnDisk = this.textBox2.Text;
            if (string.IsNullOrEmpty(locationOnDisk) || this.listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show(string.IsNullOrEmpty(locationOnDisk) ? "Empty Save Directory" : "Please select Manga to Download", "Error",
                           MessageBoxButtons.OK);
            }
            else
            {
                try
                {

                    foreach (ListViewItem selectedTruyen in this.listView2.SelectedItems)
                    {
                        var url = selectedTruyen.SubItems[1].Text;
                        var saveDir = locationOnDisk + '\\' + selectedTruyen.SubItems[0].Text.Replace(":","_");
                        Directory.CreateDirectory(saveDir);
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.DoWork += new DoWorkEventHandler(BackgroundDoWork);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundComplete);
                        worker.RunWorkerAsync(new TruyenObj()
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
        private void SearchTextChangedHandler(object sender, EventArgs e)
        {
            SetListMangaToView(mainViewMangaListModel);
        }
        private void SelectUrlToSaveChaps(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox2.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void SetListMangaToView(List<ListViewItem> mangaList)
        {
            var searchText = textBox1.Text;
            this.listView1.Items.Clear();
            this.listView1.Items.AddRange(mangaList.Where(q => q.Text.ToLower().Contains(searchText)).ToArray());
        }

        private void BackgroundDoWork(object sender, DoWorkEventArgs e)
        {
            TruyenObj inputManga = e.Argument as TruyenObj;
            e.Result = new {
                result = _cartoonService.DownloadChapter(inputManga.saveDir, inputManga.url),
                chuong = ((string)inputManga.url).Remove(inputManga.url.Length - 1).Split('/').Last(),
                selectTruyen = inputManga.selectedTruyen
            };
        }

        private void BackgroundComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (dynamic)e.Result;
            string displayedText = "";
            if (string.IsNullOrEmpty((string)result.result))
            {
                displayedText = "\n Download Complete :" + (string)result.chuong;
                DisplayedResult.Add(displayedText);
               
                ((ListViewItem)result.selectTruyen).Checked = true;
            }
            else {
                displayedText = "Error :" + (string)result.chuong;
            }
            richTextBox1.Text = "Begin download" + string.Join("\r\n", DisplayedResult.OrderBy(s => s).ToArray());
        }

    }
}

public class TruyenObj
{
    public string saveDir { get; set; }
    public string url { get; set; }
    public ListViewItem selectedTruyen { get; set; }
}
