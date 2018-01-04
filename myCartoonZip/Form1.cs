using newCartoonImplementation;
using NewCartoonInterfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using myCartoonZip.ViewModel;
using NewCartoonInterfaces.Model;
using System.Data;
using CartoonInterface;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace myCartoonZip
{
    public partial class Form1 : Form
    {
        private readonly ICartoonService _cartoonService;
        public HomePageModel homePageModel { get; set; }
        public TruyenPageModel truyenPageModel { get; set; }
        public  List<ListViewItem> view_mangaDisplayList { get; set; }
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
            view_mangaDisplayList = _logger.GetObjectFromFile(savedMangaListLocation);
            SetListManga(view_mangaDisplayList);
        }

        private void LoadButtonFromURLHanlder(object sender, EventArgs e)
        {
            var url = @"http://truyentranhtuan.com/danh-sach-truyen/";
            view_mangaDisplayList = new List<ListViewItem>();
            homePageModel = _cartoonService.ParseMainPageContent(url);
            var homePageModelProps = typeof(TruyenHomePageModel).GetProperties().OrderBy(o => o.Name).Where(q => q.Name != "TenTruyen").ToList();
            homePageModel.DanhSachTruyenMain.ForEach(x =>
            {
                var addedItem = new ListViewItem(x.TenTruyen, 0);
                homePageModelProps.ForEach(i => {
                    var val = i.GetValue(x, null).ToString();
                    addedItem.SubItems.Add(val);
                });
                view_mangaDisplayList.Add(addedItem);
                this.listView1.Items.Add(addedItem);
            });
            view_mangaDisplayList = view_mangaDisplayList.ToList();
            _logger.SaveObjectToFile(savedMangaListLocation, view_mangaDisplayList);
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
                truyenPageModel = _cartoonService.ParsePersonalPage(url);
                var truyenPageProp = typeof(PersonalTruyen)
                    .GetProperties().OrderBy(o => o.Name)
                    .Where(q => q.Name != "TenChuong").ToList();
                truyenPageModel.DanhSachChuong.ForEach(x =>
                {
                    var addedItem = new ListViewItem(x.TenChuong, 0);
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
            this.richTextBox1.Text = "Begin download";
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
                        System.IO.Directory.CreateDirectory(saveDir);
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
            SetListManga(view_mangaDisplayList);
        }
        private void SelectUrlToSaveChaps(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox2.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void SetListManga(List<ListViewItem> mangaList)
        {
            var searchText = textBox1.Text;
            this.listView1.Items.Clear();
            this.listView1.Items.AddRange(mangaList.Where(q => q.Text.ToLower().Contains(searchText)).ToArray());
        }

        private void BackgroundDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            TruyenObj argumentTest = e.Argument as TruyenObj;
            e.Result = new {
                result = _cartoonService.DownloadTruyen(argumentTest.saveDir, argumentTest.url),
                chuong = ((string)argumentTest.url).Remove(argumentTest.url.Length - 1).Split('/').Last(),
                selectTruyen = argumentTest.selectedTruyen
            };
        }

        private void BackgroundComplete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            var result = (dynamic)e.Result;
            if (string.IsNullOrEmpty((string)result.result))
            {
                this.richTextBox1.Text += "\r\n Download Complete :" +(string) result.chuong;
                ((ListViewItem)result.selectTruyen).Checked = true;
            }
            else {
                this.richTextBox1.Text += "\r\n Error :" + (string)result.chuong;
            }
        }

    }
}

public class TruyenObj
{
    public string saveDir { get; set; }
    public string url { get; set; }
    public ListViewItem selectedTruyen { get; set; }
}
