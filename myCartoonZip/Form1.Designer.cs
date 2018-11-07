using NewCartoonInterfaces;
using NewCartoonInterfaces.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace myCartoonZip
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TTTSearchLabel = new System.Windows.Forms.Label();
            this.TTTSearchTextbox = new System.Windows.Forms.TextBox();
            this.TTTLoadFromUrlBtn = new System.Windows.Forms.Button();
            this.TTTStatusDownloadLogTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.TTTMangaListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TTTLoadChapterBtn = new System.Windows.Forms.Button();
            this.TTTDownloadChapterBtn = new System.Windows.Forms.Button();
            this.TTTUrlToSaveLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TTTUrlToSaveTextbox = new System.Windows.Forms.TextBox();
            this.TTTUrlToSaveBtn = new System.Windows.Forms.Button();
            this.TTTchapterListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TTTGroupbox = new System.Windows.Forms.GroupBox();
            this.blogTruyenGroupBox = new System.Windows.Forms.GroupBox();
            this.blogTruyenLoadChapterBtn = new System.Windows.Forms.Button();
            this.blogTruyenUrl = new System.Windows.Forms.TextBox();
            this.mangaFormTab = new System.Windows.Forms.TabControl();
            this.TTTTabPage = new System.Windows.Forms.TabPage();
            this.BlogTruyenTabPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            this.TTTGroupbox.SuspendLayout();
            this.blogTruyenGroupBox.SuspendLayout();
            this.mangaFormTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TTTSearchLabel
            // 
            this.TTTSearchLabel.AutoSize = true;
            this.TTTSearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TTTSearchLabel.Location = new System.Drawing.Point(362, 269);
            this.TTTSearchLabel.Name = "label1";
            this.TTTSearchLabel.Size = new System.Drawing.Size(75, 25);
            this.TTTSearchLabel.TabIndex = 0;
            this.TTTSearchLabel.Text = "Search";
            // 
            // TTTSearchTextbox
            // 
            this.TTTSearchTextbox.Location = new System.Drawing.Point(445, 274);
            this.TTTSearchTextbox.Name = "textBox1";
            this.TTTSearchTextbox.Size = new System.Drawing.Size(210, 20);
            this.TTTSearchTextbox.TabIndex = 1;
            this.TTTSearchTextbox.TextChanged += new System.EventHandler(this.SearchTextChangedHandler);
            // 
            // TTTLoadFromUrlBtn
            // 
            this.TTTLoadFromUrlBtn.Location = new System.Drawing.Point(305, 16);
            this.TTTLoadFromUrlBtn.Name = "button1";
            this.TTTLoadFromUrlBtn.Size = new System.Drawing.Size(102, 42);
            this.TTTLoadFromUrlBtn.TabIndex = 2;
            this.TTTLoadFromUrlBtn.Text = "Load from Url";
            this.TTTLoadFromUrlBtn.UseVisualStyleBackColor = true;
            this.TTTLoadFromUrlBtn.Click += new System.EventHandler(this.LoadButtonFromURLHanlder);
            // 
            // richTextBox1
            // 
            this.TTTStatusDownloadLogTextBox.Location = new System.Drawing.Point(902, 493);
            this.TTTStatusDownloadLogTextBox.Name = "richTextBox1";
            this.TTTStatusDownloadLogTextBox.Size = new System.Drawing.Size(374, 157);
            this.TTTStatusDownloadLogTextBox.TabIndex = 3;
            this.TTTStatusDownloadLogTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(380, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 5;
            // 
            // mangaListView
            // 
            this.TTTMangaListView.AllowColumnReorder = true;
            this.TTTMangaListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.TTTMangaListView.FullRowSelect = true;
            this.TTTMangaListView.GridLines = true;
            this.TTTMangaListView.HideSelection = false;
            this.TTTMangaListView.Location = new System.Drawing.Point(365, 321);
            this.TTTMangaListView.Name = "mangaListView";
            this.TTTMangaListView.Size = new System.Drawing.Size(506, 329);
            this.TTTMangaListView.TabIndex = 6;
            this.TTTMangaListView.UseCompatibleStateImageBehavior = false;
            this.TTTMangaListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ten truyen";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "chuong";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ngay cap nhat";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "url";
            this.columnHeader4.Width = 400;
            // 
            // button2
            // 
            this.TTTLoadChapterBtn.Location = new System.Drawing.Point(769, 262);
            this.TTTLoadChapterBtn.Name = "button2";
            this.TTTLoadChapterBtn.Size = new System.Drawing.Size(95, 42);
            this.TTTLoadChapterBtn.TabIndex = 7;
            this.TTTLoadChapterBtn.Text = "Load Chapters";
            this.TTTLoadChapterBtn.UseVisualStyleBackColor = true;
            this.TTTLoadChapterBtn.Click += new System.EventHandler(this.LoadChaptersHandler);
            // 
            // button3
            // 
            this.TTTDownloadChapterBtn.Location = new System.Drawing.Point(872, 102);
            this.TTTDownloadChapterBtn.Name = "button3";
            this.TTTDownloadChapterBtn.Size = new System.Drawing.Size(151, 23);
            this.TTTDownloadChapterBtn.TabIndex = 8;
            this.TTTDownloadChapterBtn.Text = "Download Chapters";
            this.TTTDownloadChapterBtn.UseVisualStyleBackColor = true;
            this.TTTDownloadChapterBtn.Click += new System.EventHandler(this.DownloadChaptersHandler);
            // 
            // label2
            // 
            this.TTTUrlToSaveLabel.AutoSize = true;
            this.TTTUrlToSaveLabel.Location = new System.Drawing.Point(852, 132);
            this.TTTUrlToSaveLabel.Name = "label2";
            this.TTTUrlToSaveLabel.Size = new System.Drawing.Size(58, 13);
            this.TTTUrlToSaveLabel.TabIndex = 9;
            this.TTTUrlToSaveLabel.Text = "Url to save";
            // 
            // textBox2
            // 
            this.TTTUrlToSaveTextbox.Location = new System.Drawing.Point(936, 129);
            this.TTTUrlToSaveTextbox.Name = "textBox2";
            this.TTTUrlToSaveTextbox.Size = new System.Drawing.Size(264, 20);
            this.TTTUrlToSaveTextbox.TabIndex = 10;
            // 
            // button4
            // 
            this.TTTUrlToSaveBtn.Location = new System.Drawing.Point(1206, 128);
            this.TTTUrlToSaveBtn.Name = "button4";
            this.TTTUrlToSaveBtn.Size = new System.Drawing.Size(30, 20);
            this.TTTUrlToSaveBtn.TabIndex = 11;
            this.TTTUrlToSaveBtn.Text = "...";
            this.TTTUrlToSaveBtn.UseVisualStyleBackColor = true;
            this.TTTUrlToSaveBtn.Click += new System.EventHandler(this.SelectUrlToSaveChaps);
            // 
            // chapterListView
            // 
            this.TTTchapterListView.AllowColumnReorder = true;
            this.TTTchapterListView.CheckBoxes = true;
            this.TTTchapterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6});
            this.TTTchapterListView.FullRowSelect = true;
            this.TTTchapterListView.GridLines = true;
            this.TTTchapterListView.Location = new System.Drawing.Point(902, 151);
            this.TTTchapterListView.Name = "chapterListView";
            this.TTTchapterListView.Size = new System.Drawing.Size(374, 329);
            this.TTTchapterListView.TabIndex = 12;
            this.TTTchapterListView.UseCompatibleStateImageBehavior = false;
            this.TTTchapterListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Chuong";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "url";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ngay dang";
            this.columnHeader6.Width = 150;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundDoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundComplete);
            // 
            // groupBox1
            // 
            this.TTTGroupbox.Controls.Add(this.TTTLoadFromUrlBtn);
            this.TTTGroupbox.Location = new System.Drawing.Point(356, 246);
            this.TTTGroupbox.Name = "groupBox1";
            this.TTTGroupbox.Size = new System.Drawing.Size(515, 64);
            this.TTTGroupbox.TabIndex = 13;
            this.TTTGroupbox.TabStop = false;
            this.TTTGroupbox.Text = "Truyen Tranh Tuan";
            // 
            // groupBox2
            // 
            this.blogTruyenGroupBox.Controls.Add(this.blogTruyenLoadChapterBtn);
            this.blogTruyenGroupBox.Controls.Add(this.blogTruyenUrl);
            this.blogTruyenGroupBox.Location = new System.Drawing.Point(356, 164);
            this.blogTruyenGroupBox.Name = "groupBox2";
            this.blogTruyenGroupBox.Size = new System.Drawing.Size(515, 64);
            this.blogTruyenGroupBox.TabIndex = 14;
            this.blogTruyenGroupBox.TabStop = false;
            this.blogTruyenGroupBox.Text = "blog truyen";
            // 
            // button5
            // 
            this.blogTruyenLoadChapterBtn.Location = new System.Drawing.Point(319, 15);
            this.blogTruyenLoadChapterBtn.Name = "button5";
            this.blogTruyenLoadChapterBtn.Size = new System.Drawing.Size(189, 43);
            this.blogTruyenLoadChapterBtn.TabIndex = 1;
            this.blogTruyenLoadChapterBtn.Text = "Load chapter";
            this.blogTruyenLoadChapterBtn.UseVisualStyleBackColor = true;
            this.blogTruyenLoadChapterBtn.Click += new System.EventHandler(this.blogTruyenClicked);
            // 
            // blogTruyenUrl
            // 
            this.blogTruyenUrl.Location = new System.Drawing.Point(9, 27);
            this.blogTruyenUrl.Name = "blogTruyenUrl";
            this.blogTruyenUrl.Size = new System.Drawing.Size(288, 20);
            this.blogTruyenUrl.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.mangaFormTab.Controls.Add(this.TTTTabPage);
            this.mangaFormTab.Controls.Add(this.BlogTruyenTabPage);
            this.mangaFormTab.Location = new System.Drawing.Point(13, 32);
            this.mangaFormTab.Name = "tabControl1";
            this.mangaFormTab.SelectedIndex = 0;
            this.mangaFormTab.Size = new System.Drawing.Size(335, 432);
            this.mangaFormTab.TabIndex = 15;
            // 
            // TTTTabPage
            // 
            this.TTTTabPage.Location = new System.Drawing.Point(4, 22);
            this.TTTTabPage.Name = "tabPage1";
            this.TTTTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TTTTabPage.Size = new System.Drawing.Size(327, 406);
            this.TTTTabPage.TabIndex = 0;
            this.TTTTabPage.Text = "Truyen Tranh Tuan";
            this.TTTTabPage.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.BlogTruyenTabPage.Location = new System.Drawing.Point(4, 22);
            this.BlogTruyenTabPage.Name = "tabPage2";
            this.BlogTruyenTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BlogTruyenTabPage.Size = new System.Drawing.Size(192, 74);
            this.BlogTruyenTabPage.TabIndex = 1;
            this.BlogTruyenTabPage.Text = "Blog Truyen";
            this.BlogTruyenTabPage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 774);
            this.Controls.Add(this.mangaFormTab);
            this.Controls.Add(this.blogTruyenGroupBox);
            this.Controls.Add(this.TTTSearchTextbox);
            this.Controls.Add(this.TTTSearchLabel);
            this.Controls.Add(this.TTTchapterListView);
            this.Controls.Add(this.TTTLoadChapterBtn);
            this.Controls.Add(this.TTTUrlToSaveBtn);
            this.Controls.Add(this.TTTUrlToSaveTextbox);
            this.Controls.Add(this.TTTUrlToSaveLabel);
            this.Controls.Add(this.TTTDownloadChapterBtn);
            this.Controls.Add(this.TTTMangaListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TTTStatusDownloadLogTextBox);
            this.Controls.Add(this.TTTGroupbox);
            this.Name = "Form1";
            this.Text = "My Cartooon Zip";
            this.Load += new System.EventHandler(this.FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
            this.TTTGroupbox.ResumeLayout(false);
            this.blogTruyenGroupBox.ResumeLayout(false);
            this.blogTruyenGroupBox.PerformLayout();
            this.mangaFormTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TTTSearchLabel;
        private System.Windows.Forms.TextBox TTTSearchTextbox;
        private System.Windows.Forms.Button TTTLoadFromUrlBtn;
        private System.Windows.Forms.RichTextBox TTTStatusDownloadLogTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource form1BindingSource1;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.ListView TTTMangaListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Button TTTLoadChapterBtn;
        private Button TTTDownloadChapterBtn;
        private Label TTTUrlToSaveLabel;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox TTTUrlToSaveTextbox;
        private Button TTTUrlToSaveBtn;
        private ListView TTTchapterListView;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private GroupBox TTTGroupbox;
        private GroupBox blogTruyenGroupBox;
        private Button blogTruyenLoadChapterBtn;
        private TextBox blogTruyenUrl;
        private TabControl mangaFormTab;
        private TabPage TTTTabPage;
        private TabPage BlogTruyenTabPage;
    }
}

