using NewCartoonInterfaces;
using NewCartoonInterfaces.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace myCartoonZip
{
    partial class MangaDownloadForm
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
            this.LogTabTextBox = new System.Windows.Forms.RichTextBox();
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
            this.button2 = new System.Windows.Forms.Button();
            this.mangaFormTab = new System.Windows.Forms.TabControl();
            this.TTTTabPage = new System.Windows.Forms.TabPage();
            this.BlogTruyenTabPage = new System.Windows.Forms.TabPage();
            this.BlogTruyenChapterFilterLabel = new System.Windows.Forms.Label();
            this.BlogTruyenChapterListView = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BlogTruyenSaveDirBtn = new System.Windows.Forms.Button();
            this.BlogTruyenSaveDirTextBox = new System.Windows.Forms.TextBox();
            this.BlogTruyenUrlToSaveLabel = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BlogTruyenMangaFilterTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            this.TTTGroupbox.SuspendLayout();
            this.blogTruyenGroupBox.SuspendLayout();
            this.mangaFormTab.SuspendLayout();
            this.TTTTabPage.SuspendLayout();
            this.BlogTruyenTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TTTSearchLabel
            // 
            this.TTTSearchLabel.AutoSize = true;
            this.TTTSearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TTTSearchLabel.Location = new System.Drawing.Point(6, 22);
            this.TTTSearchLabel.Name = "TTTSearchLabel";
            this.TTTSearchLabel.Size = new System.Drawing.Size(75, 25);
            this.TTTSearchLabel.TabIndex = 0;
            this.TTTSearchLabel.Text = "Search";
            // 
            // TTTSearchTextbox
            // 
            this.TTTSearchTextbox.Location = new System.Drawing.Point(89, 27);
            this.TTTSearchTextbox.Name = "TTTSearchTextbox";
            this.TTTSearchTextbox.Size = new System.Drawing.Size(264, 20);
            this.TTTSearchTextbox.TabIndex = 1;
            this.TTTSearchTextbox.TextChanged += new System.EventHandler(this.SearchTextChangedHandler);
            // 
            // TTTLoadFromUrlBtn
            // 
            this.TTTLoadFromUrlBtn.Location = new System.Drawing.Point(359, 15);
            this.TTTLoadFromUrlBtn.Name = "TTTLoadFromUrlBtn";
            this.TTTLoadFromUrlBtn.Size = new System.Drawing.Size(102, 42);
            this.TTTLoadFromUrlBtn.TabIndex = 2;
            this.TTTLoadFromUrlBtn.Text = "Load from Url";
            this.TTTLoadFromUrlBtn.UseVisualStyleBackColor = true;
            this.TTTLoadFromUrlBtn.Click += new System.EventHandler(this.TTTLoadButtonFromURLHanlder);
            // 
            // LogTabTextBox
            // 
            this.LogTabTextBox.Location = new System.Drawing.Point(6, 6);
            this.LogTabTextBox.Name = "LogTabTextBox";
            this.LogTabTextBox.Size = new System.Drawing.Size(1112, 646);
            this.LogTabTextBox.TabIndex = 3;
            this.LogTabTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 5;
            // 
            // TTTMangaListView
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
            this.TTTMangaListView.Location = new System.Drawing.Point(26, 99);
            this.TTTMangaListView.Name = "TTTMangaListView";
            this.TTTMangaListView.Size = new System.Drawing.Size(568, 434);
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
            // TTTLoadChapterBtn
            // 
            this.TTTLoadChapterBtn.Location = new System.Drawing.Point(467, 15);
            this.TTTLoadChapterBtn.Name = "TTTLoadChapterBtn";
            this.TTTLoadChapterBtn.Size = new System.Drawing.Size(95, 42);
            this.TTTLoadChapterBtn.TabIndex = 7;
            this.TTTLoadChapterBtn.Text = "Load Chapters";
            this.TTTLoadChapterBtn.UseVisualStyleBackColor = true;
            this.TTTLoadChapterBtn.Click += new System.EventHandler(this.TTTLoadChaptersHandler);
            // 
            // TTTDownloadChapterBtn
            // 
            this.TTTDownloadChapterBtn.Location = new System.Drawing.Point(639, 44);
            this.TTTDownloadChapterBtn.Name = "TTTDownloadChapterBtn";
            this.TTTDownloadChapterBtn.Size = new System.Drawing.Size(151, 23);
            this.TTTDownloadChapterBtn.TabIndex = 8;
            this.TTTDownloadChapterBtn.Text = "Download Chapters";
            this.TTTDownloadChapterBtn.UseVisualStyleBackColor = true;
            this.TTTDownloadChapterBtn.Click += new System.EventHandler(this.TTTDownloadChaptersHandler);
            // 
            // TTTUrlToSaveLabel
            // 
            this.TTTUrlToSaveLabel.AutoSize = true;
            this.TTTUrlToSaveLabel.Location = new System.Drawing.Point(640, 76);
            this.TTTUrlToSaveLabel.Name = "TTTUrlToSaveLabel";
            this.TTTUrlToSaveLabel.Size = new System.Drawing.Size(58, 13);
            this.TTTUrlToSaveLabel.TabIndex = 9;
            this.TTTUrlToSaveLabel.Text = "Url to save";
            // 
            // TTTUrlToSaveTextbox
            // 
            this.TTTUrlToSaveTextbox.Location = new System.Drawing.Point(724, 73);
            this.TTTUrlToSaveTextbox.Name = "TTTUrlToSaveTextbox";
            this.TTTUrlToSaveTextbox.Size = new System.Drawing.Size(264, 20);
            this.TTTUrlToSaveTextbox.TabIndex = 10;
            // 
            // TTTUrlToSaveBtn
            // 
            this.TTTUrlToSaveBtn.Location = new System.Drawing.Point(994, 72);
            this.TTTUrlToSaveBtn.Name = "TTTUrlToSaveBtn";
            this.TTTUrlToSaveBtn.Size = new System.Drawing.Size(30, 20);
            this.TTTUrlToSaveBtn.TabIndex = 11;
            this.TTTUrlToSaveBtn.Text = "...";
            this.TTTUrlToSaveBtn.UseVisualStyleBackColor = true;
            this.TTTUrlToSaveBtn.Click += new System.EventHandler(this.SelectUrlToSaveChaps);
            // 
            // TTTchapterListView
            // 
            this.TTTchapterListView.AllowColumnReorder = true;
            this.TTTchapterListView.CheckBoxes = true;
            this.TTTchapterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader6});
            this.TTTchapterListView.FullRowSelect = true;
            this.TTTchapterListView.GridLines = true;
            this.TTTchapterListView.Location = new System.Drawing.Point(639, 99);
            this.TTTchapterListView.Name = "TTTchapterListView";
            this.TTTchapterListView.Size = new System.Drawing.Size(436, 434);
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
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.TTTBackgroundDoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.TTTBackgroundComplete);
            // 
            // TTTGroupbox
            // 
            this.TTTGroupbox.Controls.Add(this.TTTLoadFromUrlBtn);
            this.TTTGroupbox.Controls.Add(this.TTTLoadChapterBtn);
            this.TTTGroupbox.Controls.Add(this.TTTSearchLabel);
            this.TTTGroupbox.Controls.Add(this.TTTSearchTextbox);
            this.TTTGroupbox.Location = new System.Drawing.Point(26, 24);
            this.TTTGroupbox.Name = "TTTGroupbox";
            this.TTTGroupbox.Size = new System.Drawing.Size(568, 64);
            this.TTTGroupbox.TabIndex = 13;
            this.TTTGroupbox.TabStop = false;
            this.TTTGroupbox.Text = "Truyen Tranh Tuan";
            // 
            // blogTruyenGroupBox
            // 
            this.blogTruyenGroupBox.Controls.Add(this.blogTruyenLoadChapterBtn);
            this.blogTruyenGroupBox.Controls.Add(this.blogTruyenUrl);
            this.blogTruyenGroupBox.Controls.Add(this.button2);
            this.blogTruyenGroupBox.Location = new System.Drawing.Point(16, 23);
            this.blogTruyenGroupBox.Name = "blogTruyenGroupBox";
            this.blogTruyenGroupBox.Size = new System.Drawing.Size(714, 74);
            this.blogTruyenGroupBox.TabIndex = 14;
            this.blogTruyenGroupBox.TabStop = false;
            this.blogTruyenGroupBox.Text = "blog truyen";
            // 
            // blogTruyenLoadChapterBtn
            // 
            this.blogTruyenLoadChapterBtn.Location = new System.Drawing.Point(319, 15);
            this.blogTruyenLoadChapterBtn.Name = "blogTruyenLoadChapterBtn";
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(514, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 43);
            this.button2.TabIndex = 15;
            this.button2.Text = "Download Chapters";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // mangaFormTab
            // 
            this.mangaFormTab.Controls.Add(this.TTTTabPage);
            this.mangaFormTab.Controls.Add(this.BlogTruyenTabPage);
            this.mangaFormTab.Controls.Add(this.tabPage1);
            this.mangaFormTab.Location = new System.Drawing.Point(24, 23);
            this.mangaFormTab.Name = "mangaFormTab";
            this.mangaFormTab.SelectedIndex = 0;
            this.mangaFormTab.Size = new System.Drawing.Size(1132, 684);
            this.mangaFormTab.TabIndex = 15;
            // 
            // TTTTabPage
            // 
            this.TTTTabPage.Controls.Add(this.TTTMangaListView);
            this.TTTTabPage.Controls.Add(this.TTTchapterListView);
            this.TTTTabPage.Controls.Add(this.TTTUrlToSaveBtn);
            this.TTTTabPage.Controls.Add(this.TTTGroupbox);
            this.TTTTabPage.Controls.Add(this.TTTUrlToSaveTextbox);
            this.TTTTabPage.Controls.Add(this.TTTUrlToSaveLabel);
            this.TTTTabPage.Controls.Add(this.label3);
            this.TTTTabPage.Controls.Add(this.TTTDownloadChapterBtn);
            this.TTTTabPage.Location = new System.Drawing.Point(4, 22);
            this.TTTTabPage.Name = "TTTTabPage";
            this.TTTTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TTTTabPage.Size = new System.Drawing.Size(1124, 658);
            this.TTTTabPage.TabIndex = 0;
            this.TTTTabPage.Text = "Truyen Tranh Tuan";
            this.TTTTabPage.UseVisualStyleBackColor = true;
            // 
            // BlogTruyenTabPage
            // 
            this.BlogTruyenTabPage.Controls.Add(this.BlogTruyenMangaFilterTextBox);
            this.BlogTruyenTabPage.Controls.Add(this.BlogTruyenChapterFilterLabel);
            this.BlogTruyenTabPage.Controls.Add(this.BlogTruyenChapterListView);
            this.BlogTruyenTabPage.Controls.Add(this.BlogTruyenSaveDirBtn);
            this.BlogTruyenTabPage.Controls.Add(this.BlogTruyenSaveDirTextBox);
            this.BlogTruyenTabPage.Controls.Add(this.BlogTruyenUrlToSaveLabel);
            this.BlogTruyenTabPage.Controls.Add(this.blogTruyenGroupBox);
            this.BlogTruyenTabPage.Location = new System.Drawing.Point(4, 22);
            this.BlogTruyenTabPage.Name = "BlogTruyenTabPage";
            this.BlogTruyenTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BlogTruyenTabPage.Size = new System.Drawing.Size(1124, 658);
            this.BlogTruyenTabPage.TabIndex = 1;
            this.BlogTruyenTabPage.Text = "Blog Truyen";
            this.BlogTruyenTabPage.UseVisualStyleBackColor = true;
            // 
            // BlogTruyenChapterFilterLabel
            // 
            this.BlogTruyenChapterFilterLabel.AutoSize = true;
            this.BlogTruyenChapterFilterLabel.Location = new System.Drawing.Point(20, 148);
            this.BlogTruyenChapterFilterLabel.Name = "BlogTruyenChapterFilterLabel";
            this.BlogTruyenChapterFilterLabel.Size = new System.Drawing.Size(69, 13);
            this.BlogTruyenChapterFilterLabel.TabIndex = 20;
            this.BlogTruyenChapterFilterLabel.Text = "Chapter Filter";
            // 
            // BlogTruyenChapterListView
            // 
            this.BlogTruyenChapterListView.AllowColumnReorder = true;
            this.BlogTruyenChapterListView.CheckBoxes = true;
            this.BlogTruyenChapterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.BlogTruyenChapterListView.FullRowSelect = true;
            this.BlogTruyenChapterListView.GridLines = true;
            this.BlogTruyenChapterListView.Location = new System.Drawing.Point(16, 173);
            this.BlogTruyenChapterListView.Name = "BlogTruyenChapterListView";
            this.BlogTruyenChapterListView.Size = new System.Drawing.Size(714, 434);
            this.BlogTruyenChapterListView.TabIndex = 19;
            this.BlogTruyenChapterListView.UseCompatibleStateImageBehavior = false;
            this.BlogTruyenChapterListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Chuong";
            this.columnHeader8.Width = 200;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "url";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Ngay dang";
            this.columnHeader10.Width = 150;
            // 
            // BlogTruyenSaveDirBtn
            // 
            this.BlogTruyenSaveDirBtn.Location = new System.Drawing.Point(374, 117);
            this.BlogTruyenSaveDirBtn.Name = "BlogTruyenSaveDirBtn";
            this.BlogTruyenSaveDirBtn.Size = new System.Drawing.Size(30, 20);
            this.BlogTruyenSaveDirBtn.TabIndex = 18;
            this.BlogTruyenSaveDirBtn.Text = "...";
            this.BlogTruyenSaveDirBtn.UseVisualStyleBackColor = true;
            // 
            // BlogTruyenSaveDirTextBox
            // 
            this.BlogTruyenSaveDirTextBox.Location = new System.Drawing.Point(104, 118);
            this.BlogTruyenSaveDirTextBox.Name = "BlogTruyenSaveDirTextBox";
            this.BlogTruyenSaveDirTextBox.Size = new System.Drawing.Size(264, 20);
            this.BlogTruyenSaveDirTextBox.TabIndex = 17;
            // 
            // BlogTruyenUrlToSaveLabel
            // 
            this.BlogTruyenUrlToSaveLabel.AutoSize = true;
            this.BlogTruyenUrlToSaveLabel.Location = new System.Drawing.Point(20, 121);
            this.BlogTruyenUrlToSaveLabel.Name = "BlogTruyenUrlToSaveLabel";
            this.BlogTruyenUrlToSaveLabel.Size = new System.Drawing.Size(58, 13);
            this.BlogTruyenUrlToSaveLabel.TabIndex = 16;
            this.BlogTruyenUrlToSaveLabel.Text = "Url to save";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LogTabTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1124, 658);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BlogTruyenManagaFilterTextBox
            // 
            this.BlogTruyenMangaFilterTextBox.Location = new System.Drawing.Point(104, 147);
            this.BlogTruyenMangaFilterTextBox.Name = "BlogTruyenManagaFilterTextBox";
            this.BlogTruyenMangaFilterTextBox.Size = new System.Drawing.Size(264, 20);
            this.BlogTruyenMangaFilterTextBox.TabIndex = 21;
            this.BlogTruyenMangaFilterTextBox.TextChanged += new System.EventHandler(this.BlogTruyenManagaFilterTextBox_TextChanged);
            // 
            // MangaDownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1338, 780);
            this.Controls.Add(this.mangaFormTab);
            this.Name = "MangaDownloadForm";
            this.Text = "My Cartooon Zip";
            this.Load += new System.EventHandler(this.FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
            this.TTTGroupbox.ResumeLayout(false);
            this.TTTGroupbox.PerformLayout();
            this.blogTruyenGroupBox.ResumeLayout(false);
            this.blogTruyenGroupBox.PerformLayout();
            this.mangaFormTab.ResumeLayout(false);
            this.TTTTabPage.ResumeLayout(false);
            this.TTTTabPage.PerformLayout();
            this.BlogTruyenTabPage.ResumeLayout(false);
            this.BlogTruyenTabPage.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TTTSearchLabel;
        private System.Windows.Forms.TextBox TTTSearchTextbox;
        private System.Windows.Forms.Button TTTLoadFromUrlBtn;
        private System.Windows.Forms.RichTextBox LogTabTextBox;
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
        private TabPage tabPage1;
        private Button button2;
        private Label BlogTruyenChapterFilterLabel;
        private ListView BlogTruyenChapterListView;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private Button BlogTruyenSaveDirBtn;
        private TextBox BlogTruyenSaveDirTextBox;
        private Label BlogTruyenUrlToSaveLabel;
        private TextBox BlogTruyenMangaFilterTextBox;
    }
}

