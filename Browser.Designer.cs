using System.Windows.Forms;

namespace ReapersBrowser
{
    partial class Browser
    {
        bool initialized = false;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        System.ComponentModel.IContainer components = null;

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
        string lastUrl;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browser));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.web_back = new System.Windows.Forms.ToolStripButton();
            this.web_forward = new System.Windows.Forms.ToolStripButton();
            this.web_refresh = new System.Windows.Forms.ToolStripButton();
            this.remove_tab = new System.Windows.Forms.ToolStripButton();
            this.add_tab = new System.Windows.Forms.ToolStripButton();
            this.bookmark_button = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.search_bar = new System.Windows.Forms.ToolStripTextBox();
            this.more_options = new System.Windows.Forms.ToolStripDropDownButton();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.history_combo_box = new System.Windows.Forms.ToolStripComboBox();
            this.overwrite_history = new System.Windows.Forms.ToolStripMenuItem();
            this.bookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookmarks_combo_box = new System.Windows.Forms.ToolStripComboBox();
            this.overwrite_bookmarks = new System.Windows.Forms.ToolStripMenuItem();
            this.themesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reaperModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllBlockedSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.default_browser_text_box = new System.Windows.Forms.ToolStripTextBox();
            this.apply_default_browser = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.about_page = new System.Windows.Forms.TabPage();
            this.browser_directory_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.history_page = new System.Windows.Forms.TabPage();
            this.history_textbox = new System.Windows.Forms.TextBox();
            this.bookmarks_page = new System.Windows.Forms.TabPage();
            this.bookmarks_textbox = new System.Windows.Forms.TextBox();
            this.blocked_sites_page = new System.Windows.Forms.TabPage();
            this.blocked_sites_textbox = new System.Windows.Forms.TextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.about_page.SuspendLayout();
            this.history_page.SuspendLayout();
            this.bookmarks_page.SuspendLayout();
            this.blocked_sites_page.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.web_back,
            this.web_forward,
            this.web_refresh,
            this.remove_tab,
            this.add_tab,
            this.bookmark_button,
            this.toolStripButton1,
            this.search_bar,
            this.more_options});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1264, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // web_back
            // 
            this.web_back.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.web_back.ForeColor = System.Drawing.Color.White;
            this.web_back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.web_back.Name = "web_back";
            this.web_back.Size = new System.Drawing.Size(23, 22);
            this.web_back.Text = "<";
            this.web_back.Click += new System.EventHandler(this.web_back_Click);
            // 
            // web_forward
            // 
            this.web_forward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.web_forward.ForeColor = System.Drawing.Color.White;
            this.web_forward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.web_forward.Name = "web_forward";
            this.web_forward.Size = new System.Drawing.Size(23, 22);
            this.web_forward.Text = ">";
            this.web_forward.Click += new System.EventHandler(this.web_forward_Click);
            // 
            // web_refresh
            // 
            this.web_refresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.web_refresh.ForeColor = System.Drawing.Color.White;
            this.web_refresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.web_refresh.Name = "web_refresh";
            this.web_refresh.Size = new System.Drawing.Size(23, 22);
            this.web_refresh.Text = "↻";
            this.web_refresh.Click += new System.EventHandler(this.web_refresh_Click);
            // 
            // remove_tab
            // 
            this.remove_tab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.remove_tab.ForeColor = System.Drawing.Color.White;
            this.remove_tab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.remove_tab.Name = "remove_tab";
            this.remove_tab.Size = new System.Drawing.Size(23, 22);
            this.remove_tab.Text = "-";
            this.remove_tab.Click += new System.EventHandler(this.remove_tab_Click);
            // 
            // add_tab
            // 
            this.add_tab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.add_tab.ForeColor = System.Drawing.Color.White;
            this.add_tab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.add_tab.Name = "add_tab";
            this.add_tab.Size = new System.Drawing.Size(23, 22);
            this.add_tab.Text = "+";
            this.add_tab.Click += new System.EventHandler(this.add_tab_Click);
            // 
            // bookmark_button
            // 
            this.bookmark_button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bookmark_button.ForeColor = System.Drawing.Color.White;
            this.bookmark_button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bookmark_button.Name = "bookmark_button";
            this.bookmark_button.Size = new System.Drawing.Size(23, 22);
            this.bookmark_button.Text = "☆";
            this.bookmark_button.Click += new System.EventHandler(this.bookmark_button_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "🔒";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // search_bar
            // 
            this.search_bar.AutoToolTip = true;
            this.search_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.search_bar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.search_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.search_bar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.search_bar.ForeColor = System.Drawing.Color.White;
            this.search_bar.Name = "search_bar";
            this.search_bar.Size = new System.Drawing.Size(1045, 25);
            this.search_bar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_bar_KeyDown);
            // 
            // more_options
            // 
            this.more_options.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.more_options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historyToolStripMenuItem,
            this.bookmarksToolStripMenuItem,
            this.themesToolStripMenuItem,
            this.securityToolStripMenuItem,
            this.defaultBrowserToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.more_options.ForeColor = System.Drawing.Color.White;
            this.more_options.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.more_options.Name = "more_options";
            this.more_options.Size = new System.Drawing.Size(24, 22);
            this.more_options.Text = "⋮";
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.historyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.history_combo_box,
            this.overwrite_history});
            this.historyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.historyToolStripMenuItem.Text = "History";
            // 
            // history_combo_box
            // 
            this.history_combo_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.history_combo_box.ForeColor = System.Drawing.Color.White;
            this.history_combo_box.Name = "history_combo_box";
            this.history_combo_box.Size = new System.Drawing.Size(121, 23);
            // 
            // overwrite_history
            // 
            this.overwrite_history.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.overwrite_history.ForeColor = System.Drawing.Color.White;
            this.overwrite_history.Name = "overwrite_history";
            this.overwrite_history.Size = new System.Drawing.Size(181, 22);
            this.overwrite_history.Text = "Overwrite/Remove";
            this.overwrite_history.Click += new System.EventHandler(this.overwrite_history_Click);
            // 
            // bookmarksToolStripMenuItem
            // 
            this.bookmarksToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bookmarksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bookmarks_combo_box,
            this.overwrite_bookmarks});
            this.bookmarksToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
            this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.bookmarksToolStripMenuItem.Text = "Bookmarks";
            // 
            // bookmarks_combo_box
            // 
            this.bookmarks_combo_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bookmarks_combo_box.ForeColor = System.Drawing.Color.White;
            this.bookmarks_combo_box.Name = "bookmarks_combo_box";
            this.bookmarks_combo_box.Size = new System.Drawing.Size(121, 23);
            // 
            // overwrite_bookmarks
            // 
            this.overwrite_bookmarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.overwrite_bookmarks.ForeColor = System.Drawing.Color.White;
            this.overwrite_bookmarks.Name = "overwrite_bookmarks";
            this.overwrite_bookmarks.Size = new System.Drawing.Size(181, 22);
            this.overwrite_bookmarks.Text = "Overwrite/Remove";
            this.overwrite_bookmarks.Click += new System.EventHandler(this.overwrite_bookmarks_Click);
            // 
            // themesToolStripMenuItem
            // 
            this.themesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.themesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.darkModeToolStripMenuItem,
            this.lightModeToolStripMenuItem,
            this.reaperModeToolStripMenuItem});
            this.themesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.themesToolStripMenuItem.Name = "themesToolStripMenuItem";
            this.themesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.themesToolStripMenuItem.Text = "Themes";
            // 
            // darkModeToolStripMenuItem
            // 
            this.darkModeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.darkModeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.darkModeToolStripMenuItem.Name = "darkModeToolStripMenuItem";
            this.darkModeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.darkModeToolStripMenuItem.Text = "Dark Mode";
            this.darkModeToolStripMenuItem.Click += new System.EventHandler(this.darkModeToolStripMenuItem_Click);
            // 
            // lightModeToolStripMenuItem
            // 
            this.lightModeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lightModeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.lightModeToolStripMenuItem.Name = "lightModeToolStripMenuItem";
            this.lightModeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.lightModeToolStripMenuItem.Text = "Light Mode";
            this.lightModeToolStripMenuItem.Click += new System.EventHandler(this.lightModeToolStripMenuItem_Click);
            // 
            // reaperModeToolStripMenuItem
            // 
            this.reaperModeToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.reaperModeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.reaperModeToolStripMenuItem.Name = "reaperModeToolStripMenuItem";
            this.reaperModeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.reaperModeToolStripMenuItem.Text = "Reaper Mode";
            this.reaperModeToolStripMenuItem.Click += new System.EventHandler(this.reaperModeToolStripMenuItem_Click);
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAllBlockedSitesToolStripMenuItem});
            this.securityToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.securityToolStripMenuItem.Text = "Security";
            // 
            // deleteAllBlockedSitesToolStripMenuItem
            // 
            this.deleteAllBlockedSitesToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deleteAllBlockedSitesToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deleteAllBlockedSitesToolStripMenuItem.Name = "deleteAllBlockedSitesToolStripMenuItem";
            this.deleteAllBlockedSitesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.deleteAllBlockedSitesToolStripMenuItem.Text = "Delete All Blocked Sites";
            this.deleteAllBlockedSitesToolStripMenuItem.Click += new System.EventHandler(this.deleteAllBlockedSitesToolStripMenuItem_Click);
            // 
            // defaultBrowserToolStripMenuItem
            // 
            this.defaultBrowserToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.defaultBrowserToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.default_browser_text_box,
            this.apply_default_browser});
            this.defaultBrowserToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.defaultBrowserToolStripMenuItem.Name = "defaultBrowserToolStripMenuItem";
            this.defaultBrowserToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.defaultBrowserToolStripMenuItem.Text = "Default Browser";
            // 
            // default_browser_text_box
            // 
            this.default_browser_text_box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.default_browser_text_box.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.default_browser_text_box.ForeColor = System.Drawing.Color.White;
            this.default_browser_text_box.Name = "default_browser_text_box";
            this.default_browser_text_box.Size = new System.Drawing.Size(200, 23);
            this.default_browser_text_box.Text = "https://www.bing.com";
            // 
            // apply_default_browser
            // 
            this.apply_default_browser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.apply_default_browser.ForeColor = System.Drawing.Color.White;
            this.apply_default_browser.Name = "apply_default_browser";
            this.apply_default_browser.Size = new System.Drawing.Size(260, 22);
            this.apply_default_browser.Text = "Apply";
            this.apply_default_browser.Click += new System.EventHandler(this.apply_default_browser_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 45);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1264, 647);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1256, 621);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Reaper\\Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl2.Controls.Add(this.about_page);
            this.tabControl2.Controls.Add(this.history_page);
            this.tabControl2.Controls.Add(this.bookmarks_page);
            this.tabControl2.Controls.Add(this.blocked_sites_page);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.ItemSize = new System.Drawing.Size(400, 42);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1256, 621);
            this.tabControl2.TabIndex = 0;
            // 
            // about_page
            // 
            this.about_page.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.about_page.Controls.Add(this.browser_directory_label);
            this.about_page.Controls.Add(this.label2);
            this.about_page.Location = new System.Drawing.Point(4, 4);
            this.about_page.Name = "about_page";
            this.about_page.Padding = new System.Windows.Forms.Padding(3);
            this.about_page.Size = new System.Drawing.Size(1248, 571);
            this.about_page.TabIndex = 1;
            this.about_page.Text = "About";
            // 
            // browser_directory_label
            // 
            this.browser_directory_label.AutoSize = true;
            this.browser_directory_label.ForeColor = System.Drawing.Color.White;
            this.browser_directory_label.Location = new System.Drawing.Point(3, 3);
            this.browser_directory_label.Name = "browser_directory_label";
            this.browser_directory_label.Size = new System.Drawing.Size(96, 13);
            this.browser_directory_label.TabIndex = 2;
            this.browser_directory_label.Text = "Browser Directory: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "v0.1";
            // 
            // history_page
            // 
            this.history_page.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.history_page.Controls.Add(this.history_textbox);
            this.history_page.Location = new System.Drawing.Point(4, 4);
            this.history_page.Name = "history_page";
            this.history_page.Size = new System.Drawing.Size(1248, 571);
            this.history_page.TabIndex = 2;
            this.history_page.Text = "History";
            // 
            // history_textbox
            // 
            this.history_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.history_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.history_textbox.ForeColor = System.Drawing.Color.White;
            this.history_textbox.Location = new System.Drawing.Point(0, 0);
            this.history_textbox.MaxLength = 1000000000;
            this.history_textbox.Multiline = true;
            this.history_textbox.Name = "history_textbox";
            this.history_textbox.Size = new System.Drawing.Size(1248, 571);
            this.history_textbox.TabIndex = 0;
            this.history_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.history_textbox_KeyDown);
            // 
            // bookmarks_page
            // 
            this.bookmarks_page.Controls.Add(this.bookmarks_textbox);
            this.bookmarks_page.Location = new System.Drawing.Point(4, 4);
            this.bookmarks_page.Name = "bookmarks_page";
            this.bookmarks_page.Size = new System.Drawing.Size(1248, 571);
            this.bookmarks_page.TabIndex = 3;
            this.bookmarks_page.Text = "Bookmarks";
            this.bookmarks_page.UseVisualStyleBackColor = true;
            // 
            // bookmarks_textbox
            // 
            this.bookmarks_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.bookmarks_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bookmarks_textbox.ForeColor = System.Drawing.Color.White;
            this.bookmarks_textbox.Location = new System.Drawing.Point(0, 0);
            this.bookmarks_textbox.MaxLength = 1000000000;
            this.bookmarks_textbox.Multiline = true;
            this.bookmarks_textbox.Name = "bookmarks_textbox";
            this.bookmarks_textbox.Size = new System.Drawing.Size(1248, 571);
            this.bookmarks_textbox.TabIndex = 1;
            this.bookmarks_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bookmarks_textbox_KeyDown);
            // 
            // blocked_sites_page
            // 
            this.blocked_sites_page.Controls.Add(this.blocked_sites_textbox);
            this.blocked_sites_page.Location = new System.Drawing.Point(4, 4);
            this.blocked_sites_page.Name = "blocked_sites_page";
            this.blocked_sites_page.Size = new System.Drawing.Size(1248, 571);
            this.blocked_sites_page.TabIndex = 4;
            this.blocked_sites_page.Text = "Blocked Sites";
            this.blocked_sites_page.UseVisualStyleBackColor = true;
            // 
            // blocked_sites_textbox
            // 
            this.blocked_sites_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.blocked_sites_textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blocked_sites_textbox.ForeColor = System.Drawing.Color.White;
            this.blocked_sites_textbox.Location = new System.Drawing.Point(0, 0);
            this.blocked_sites_textbox.MaxLength = 1000000000;
            this.blocked_sites_textbox.Multiline = true;
            this.blocked_sites_textbox.Name = "blocked_sites_textbox";
            this.blocked_sites_textbox.Size = new System.Drawing.Size(1248, 571);
            this.blocked_sites_textbox.TabIndex = 2;
            this.blocked_sites_textbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.blocked_sites_textbox_KeyDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1264, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Browser";
            this.Text = "Reaper\'s Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Browser_FormClosing);
            this.Load += new System.EventHandler(this.Browser_Load);
            this.SizeChanged += new System.EventHandler(this.Browser_SizeChanged);
            this.Resize += new System.EventHandler(this.Browser_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.about_page.ResumeLayout(false);
            this.about_page.PerformLayout();
            this.history_page.ResumeLayout(false);
            this.history_page.PerformLayout();
            this.bookmarks_page.ResumeLayout(false);
            this.bookmarks_page.PerformLayout();
            this.blocked_sites_page.ResumeLayout(false);
            this.blocked_sites_page.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton web_back;
        public System.Windows.Forms.ToolStripButton web_forward;
        public System.Windows.Forms.ToolStripButton web_refresh;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.ToolStripButton remove_tab;
        public System.Windows.Forms.ToolStripButton add_tab;
        public System.Windows.Forms.ToolStripTextBox search_bar;
        public System.Windows.Forms.ToolStripDropDownButton more_options;
        public System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox history_combo_box;
        public System.Windows.Forms.ToolStripMenuItem bookmarksToolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox bookmarks_combo_box;
        public System.Windows.Forms.ToolStripButton bookmark_button;
        public System.Windows.Forms.ToolStripMenuItem overwrite_history;
        public System.Windows.Forms.ToolStripMenuItem overwrite_bookmarks;
        System.Windows.Forms.ToolStripMenuItem themesToolStripMenuItem;
        System.Windows.Forms.ToolStripMenuItem darkModeToolStripMenuItem;
        System.Windows.Forms.ToolStripMenuItem lightModeToolStripMenuItem;
        System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        System.Windows.Forms.ToolStripButton toolStripButton1;
        ToolStripMenuItem defaultBrowserToolStripMenuItem;
        ToolStripTextBox default_browser_text_box;
        ToolStripMenuItem apply_default_browser;
        ToolStripMenuItem exitToolStripMenuItem;
        TabPage tabPage1;
        TabControl tabControl2;
        TabPage about_page;
        ToolStripMenuItem settingsToolStripMenuItem;
        Label label2;
        TabPage history_page;
        TextBox history_textbox;
        Label browser_directory_label;
        ToolStrip toolStrip2;
        TabPage bookmarks_page;
        TextBox bookmarks_textbox;
        TabPage blocked_sites_page;
        TextBox blocked_sites_textbox;
        ToolStripMenuItem deleteAllBlockedSitesToolStripMenuItem;
        ToolStripMenuItem reaperModeToolStripMenuItem;
    }
}