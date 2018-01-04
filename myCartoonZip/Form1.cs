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

namespace myCartoonZip
{
    public partial class Form1 : Form
    {
        private readonly ICartoonService _cartoonService;
        public HomePageModel homePageModel { get; set; }
        public TruyenPageModel truyenPageModel { get; set; }
        public  List<ListViewItem> OriginalItemList { get; set; }
        public ILogService _logger;
        public Form1(ICartoonService cartoonService,ILogService logger)
        {
            InitializeComponent();
            _cartoonService = cartoonService;
            _logger = logger;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void ListViewExample()
        {
            ListViewItem item1 = new ListViewItem("item1", 0);
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("1");
            item1.SubItems.Add("2");
            item1.SubItems.Add("3");
            ListViewItem item2 = new ListViewItem("item2", 1);
            item2.SubItems.Add("4");
            item2.SubItems.Add("5");
            item2.SubItems.Add("6");
            ListViewItem item3 = new ListViewItem("item3", 0);
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("7");
            item3.SubItems.Add("8");
            item3.SubItems.Add("9");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

            //Add the items to the ListView.
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });
        }

        private void SelectUrlToSaveChaps(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.textBox2.Text = folderBrowserDialog1.SelectedPath;
            }

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
                        worker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
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



        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            TruyenObj argumentTest = e.Argument as TruyenObj;
            e.Result = new {
                result = _cartoonService.DownloadTruyen(argumentTest.saveDir, argumentTest.url),
                chuong = ((string)argumentTest.url).Remove(argumentTest.url.Length - 1).Split('/').Last(),
                selectTruyen = argumentTest.selectedTruyen
            };
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
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

        private void Search_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBox1.Text;
            this.listView1.Items.Clear();
            this.listView1.Items.AddRange(OriginalItemList.Where(q=>q.Text.ToLower().Contains(searchText)).ToArray());
        }

        private void LoadButtonFromURLHanlder(object sender, EventArgs e)
        {
            var url = @"http://truyentranhtuan.com/danh-sach-truyen/";
            OriginalItemList = new List<ListViewItem>();
            homePageModel = _cartoonService.ParseMainPageContent(url);
            var homePageModelProps = typeof(TruyenHomePageModel).GetProperties().OrderBy(o => o.Name).Where(q => q.Name != "TenTruyen").ToList();
            homePageModel.DanhSachTruyenMain.ForEach(x =>
            {
                var addedItem = new ListViewItem(x.TenTruyen, 0);
                homePageModelProps.ForEach(i => {
                    var val = i.GetValue(x, null).ToString();
                    addedItem.SubItems.Add(val);
                });
                OriginalItemList.Add(addedItem);
                this.listView1.Items.Add(addedItem);
            });
            OriginalItemList = OriginalItemList.ToList();
        }
    }
}

public class TruyenObj
{
    public string saveDir { get; set; }
    public string url { get; set; }
    public ListViewItem selectedTruyen { get; set; }
}
