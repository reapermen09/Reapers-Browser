using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReapersBrowser
{
    public partial class Browser : Form
    {
        public static readonly string data_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        public static readonly string bookmarks_file_path = Path.Combine(data_file_path, "bookmark.dat");
        public static readonly string blocked_sites_file_path = Path.Combine(data_file_path, "blocked_websites.dat");
        public static readonly string history_file_path = Path.Combine(data_file_path, "history.dat");
        public static readonly string themes_file_path = Path.Combine(data_file_path, "themes.dat");
        public static readonly string default_browser_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "default_browser.dat");

        bool secure_connection = true;
        Timer safetyTimer;
        public Browser()
        {
            InitializeComponent();
        }

        void Browser_Load(object sender, EventArgs e)
        {
            if (!File.Exists(data_file_path))
            {
                Directory.CreateDirectory(data_file_path);
            }
            if (!File.Exists(bookmarks_file_path))
            {
                File.WriteAllText(bookmarks_file_path, "");
            }

            if (tabControl1 == null)
            {
                tabControl1 = new TabControl();
                tabControl1.Dock = DockStyle.Fill;
                Controls.Add(tabControl1);
            }
            tabControl1.TabPages.Remove(tabPage1);
            LoadBookmarks();
            LoadBlockedSitesFromFile();
            UpdateHistoryComboBox();
            UpdateHistoryTextBox();
            UpdateBookmarksTextBox();
            UpdateBlockedSitesTextBox();
            LoadTheme();
            safetyTimer = new Timer();
            safetyTimer.Interval = 100;
            safetyTimer.Tick += SafetyTimer_Tick;
            StartSafetyChecks();
            NewTab("https://sites.google.com/view/reapers-browser/home");
            NewTab();
            browser_directory_label.Text = $"Browser Directory: {Environment.CurrentDirectory}";
            search_bar.Dock = DockStyle.Fill;
        }

        void LoadBookmarksToolStrip()
        {
            toolStrip2.Items.Clear();

            List<string> bookmarks = File.ReadAllLines(bookmarks_file_path).ToList();
            ToolStripDropDownButton overflowDropDown = new ToolStripDropDownButton("...");
            bool overflowStarted = false;

            int totalWidth = 0;
            int maxWidth = toolStrip2.Width - 50;

            foreach (string bookmark in bookmarks)
            {
                if (bookmark.Contains("[Title:\"") && bookmark.Contains("\"][Link:\""))
                {
                    string[] parts = bookmark.Split(new string[] { "\"][Link:\"" }, StringSplitOptions.None);

                    if (parts.Length == 2)
                    {
                        string title = parts[0].Replace("[Title:\"", "").Replace("\"", "").Trim();
                        string url = parts[1].Replace("\"]", "").Replace("\"", "").Trim();

                        ToolStripButton btn = new ToolStripButton(title)
                        {
                            AutoSize = true,
                            DisplayStyle = ToolStripItemDisplayStyle.Text
                        };

                        btn.Click += (s, e) =>
                        {
                            if (IsBlockedSite(url.ToLower()))
                            {
                                MessageBox.Show("Access to this website is blocked.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            if (IsMaliciousContent(url.ToLower()))
                            {
                                MessageBox.Show("Malicious content detected. Navigating to a safe page.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            OpenBookmark(url);
                        };

                        int btnWidth = TextRenderer.MeasureText(title, btn.Font).Width + 20;
                        totalWidth += btnWidth;

                        if (totalWidth > maxWidth)
                        {
                            ToolStripMenuItem overflowItem = new ToolStripMenuItem(title);
                            overflowItem.Click += (s, e) =>
                            {
                                if (IsBlockedSite(url.ToLower()))
                                {
                                    MessageBox.Show("Access to this website is blocked.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;
                                }

                                if (IsMaliciousContent(url.ToLower()))
                                {
                                    MessageBox.Show("Malicious content detected. Navigating to a safe page.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                OpenBookmark(url);
                            };
                            overflowDropDown.DropDownItems.Add(overflowItem);
                            overflowStarted = true;
                        }
                        else
                        {
                            toolStrip2.Items.Add(btn);
                        }
                    }
                }
            }

            if (overflowStarted)
                toolStrip2.Items.Add(overflowDropDown);

            UpdateBookmarksTextBox();
        }



        void OpenBookmark(string bookmark)
        {
            search_bar.Text = bookmark;
            NewTab(bookmark);
        }


        void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDarkMode();
            SaveTheme("dark");
        }

        void lightModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLightMode();
            SaveTheme("light");
        }

        void reaperModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetReaperMode();
            SaveTheme("reaper");
        }

        void SetDarkMode()
        {
            BackColor = Color.FromArgb(40, 40, 40);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10, FontStyle.Regular);

            toolStrip1.BackColor = Color.FromArgb(30, 30, 30);
            toolStrip1.ForeColor = Color.White;
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            toolStrip2.BackColor = Color.FromArgb(30, 30, 30);
            toolStrip2.ForeColor = Color.White;
            toolStrip2.RenderMode = ToolStripRenderMode.Professional;
            toolStrip2.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            foreach (ToolStripItem item in toolStrip1.Items)
            {
                item.ForeColor = Color.White;
                if (item is ToolStripButton btn)
                {
                    btn.BackColor = Color.FromArgb(50, 50, 50);
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Margin = new Padding(2);
                }
                else if (item is ToolStripTextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BackColor = Color.FromArgb(60, 60, 60);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            foreach (ToolStripItem item in toolStrip2.Items)
            {
                item.ForeColor = Color.White;
                if (item is ToolStripButton btn)
                {
                    btn.BackColor = Color.FromArgb(50, 50, 50);
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Margin = new Padding(2);
                }
                else if (item is ToolStripTextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BackColor = Color.FromArgb(60, 60, 60);
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            tabControl1.BackColor = Color.FromArgb(50, 50, 50);
            tabControl1.ForeColor = Color.White;
            tabControl1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.FromArgb(50, 50, 50);
                    comboBox.ForeColor = Color.White;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
            }
        }

        void SetReaperMode()
        {
            BackColor = Color.Black;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10, FontStyle.Regular);

            toolStrip1.BackColor = Color.Black;
            toolStrip1.ForeColor = Color.White;
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            toolStrip2.BackColor = Color.Black;
            toolStrip2.ForeColor = Color.White;
            toolStrip2.RenderMode = ToolStripRenderMode.Professional;
            toolStrip2.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            foreach (ToolStripItem item in toolStrip1.Items)
            {
                item.ForeColor = Color.White;
                if (item is ToolStripButton btn)
                {
                    btn.BackColor = Color.Black;
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Margin = new Padding(2);
                }
                else if (item is ToolStripTextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BackColor = Color.Black;
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            foreach (ToolStripItem item in toolStrip2.Items)
            {
                item.ForeColor = Color.White;
                if (item is ToolStripButton btn)
                {
                    btn.BackColor = Color.Black;
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Margin = new Padding(2);
                }
                else if (item is ToolStripTextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BackColor = Color.Black;
                    txt.ForeColor = Color.White;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            tabControl1.BackColor = Color.Black;
            tabControl1.ForeColor = Color.White;
            tabControl1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.Black;
                    comboBox.ForeColor = Color.White;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
            }
        }

        void SetLightMode()
        {
            BackColor = Color.White;
            ForeColor = Color.Black;
            Font = new Font("Segoe UI", 10, FontStyle.Regular);

            toolStrip1.BackColor = Color.LightGray;
            toolStrip1.ForeColor = Color.Black;
            toolStrip1.RenderMode = ToolStripRenderMode.Professional;
            toolStrip1.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            toolStrip2.BackColor = Color.LightGray;
            toolStrip2.ForeColor = Color.Black;
            toolStrip2.RenderMode = ToolStripRenderMode.Professional;
            toolStrip2.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                item.ForeColor = Color.Black;
                if (item is ToolStripButton btn)
                {
                    btn.BackColor = Color.FromArgb(240, 240, 240);
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Margin = new Padding(2);
                }
                else if (item is ToolStripTextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.Black;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            foreach (ToolStripItem item in toolStrip2.Items)
            {
                item.ForeColor = Color.Black;
                if (item is ToolStripButton btn)
                {
                    btn.BackColor = Color.FromArgb(240, 240, 240);
                    btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    btn.Margin = new Padding(2);
                }
                else if (item is ToolStripTextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    txt.BackColor = Color.White;
                    txt.ForeColor = Color.Black;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            tabControl1.BackColor = Color.FromArgb(230, 230, 230);
            tabControl1.ForeColor = Color.Black;
            tabControl1.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            foreach (Control control in this.Controls)
            {
                if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = Color.White;
                    comboBox.ForeColor = Color.Black;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
            }
        }

        void SaveTheme(string theme)
        {
            try
            {
                string themeEntry = $"Theme:{theme}";
                File.WriteAllText(themes_file_path, themeEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving theme: {ex.Message}");
            }
        }


        string LoadTheme()
        {
            try
            {
                if (File.Exists(themes_file_path))
                {
                    string themeContent = File.ReadAllText(themes_file_path).Trim();

                    if (themeContent.Contains("dark"))
                    {
                        SetDarkMode();
                        return "dark";
                    }
                    else if (themeContent.Contains("light"))
                    {
                        SetLightMode();
                        return "light";
                    }
                    else if (themeContent.Contains("reaper"))
                    {
                        SetReaperMode();
                        return "reaper";
                    }
                    else
                    {
                        SetDarkMode();
                        return "dark";
                    }
                }
                else
                {
                    SaveTheme("dark");
                    return "dark";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading theme: {ex.Message}");
                return "dark";
            }
        }
        void UpdateSearchBarWithCurrentURL()
        {
            var webView = GetCurrentWebView();
            if (webView != null && webView.CoreWebView2 != null)
            {
                if (webView.Source.ToString().StartsWith("https://sites.google.com/view/reapers-browser/"))
                {
                    switch (webView.Source.ToString().ToLower().Substring("https://sites.google.com/view/reapers-browser/".Length))
                    {
                        case "home":
                            search_bar.Text = "Reaper/ReapersBrowser/Home";
                            break;
                        case "downloads":
                            search_bar.Text = "Reaper/ReapersBrowser/Downloads";
                            break;
                        case "changelog":
                            search_bar.Text = "Reaper/ReapersBrowser/Changelog";
                            break;
                        case "license":
                            search_bar.Text = "Reaper/ReapersBrowser/License";
                            break;
                        case "user-data-transfer-tutorial":
                            search_bar.Text = "Reaper/ReapersBrowser/UserDataTransferTutorial";
                            break;
                        default:
                            search_bar.Text = "Reaper/ReapersBrowser/Unknown";
                            break;
                    }
                    Text = $"Reaper's Browser - {search_bar.Text}";
                }
                else
                {
                    search_bar.Text = webView.CoreWebView2.Source;
                    Text = $"Reaper's Browser - {webView.CoreWebView2.DocumentTitle}";
                }
            }
        }
        void toolStripButton1_Click(object sender, EventArgs e)
        {
            var webView = GetCurrentWebView();
            if (webView != null && webView.CoreWebView2 != null)
            {
                if (secure_connection)
                {
                    try
                    {
                        if (webView.Source.ToString().StartsWith("https://sites.google.com/view/reapers-browser/"))
                        {
                            string display_text = "";
                            switch (webView.Source.ToString().ToLower().Substring("https://sites.google.com/view/reapers-browser/".Length))
                            {
                                case "home":
                                    display_text = "Reaper/ReapersBrowser/Home";
                                    break;
                                case "downloads":
                                    display_text = "Reaper/ReapersBrowser/Downloads";
                                    break;
                                case "changelog":
                                    display_text = "Reaper/ReapersBrowser/Changelog";
                                    break;
                                case "license":
                                    display_text = "Reaper/ReapersBrowser/License";
                                    break;
                                case "user-data-transfer-tutorial":
                                    display_text = "Reaper/ReapersBrowser/UserDataTransferTutorial";
                                    break;
                                default:
                                    display_text = "Reaper/ReapersBrowser/Unknown";
                                    break;
                            }
                            MessageBox.Show($"Secure connection to {display_text}", "Secure Connection");
                        }
                        else
                        {
                            MessageBox.Show($"Secure connection to {webView.CoreWebView2.Source}", "Secure Connection");
                        }
                    }
                    catch
                    {
                        MessageBox.Show($"Unable to fetch connection data", "Unknown Connection");
                    }
                }
                else MessageBox.Show($"Insecure connection to {webView.CoreWebView2.Source}", "Insecure Connection");
            }
            else MessageBox.Show("No web page loaded in the current tab.", "Error");
        }

        int og_w = 1280;
        int last_search_bar_width = 1045;
        bool resize_done = false;

        void init_win_size(int offset = 0)
        {
            int distanceFrom = Width - og_w + offset;

            int newWidth = last_search_bar_width + distanceFrom;

            newWidth = Math.Max(100, Math.Min(newWidth, Width - 275));

            search_bar.Size = new System.Drawing.Size(newWidth, 25);

            toolStrip1.Width = Width + offset;

            last_search_bar_width = newWidth;

            resize_done = true;
        }

        void Browser_Resize(object sender, EventArgs e)
        {
            if (resize_done) init_win_size();
        }
        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var args = new FormClosingEventArgs(CloseReason.UserClosing, false);

            ExitPrompt(args, false);
        }

        void apply_default_browser_Click(object sender, EventArgs e)
        {
            string browser = default_browser_text_box.Text.Trim();
            if (!string.IsNullOrEmpty(browser))
            {
                save_default_browser(browser);
            }
            else
            {
                MessageBox.Show("Please enter a valid search engine name.");
            }
        }

        string save_default_browser(string def_browser)
        {
            try
            {
                File.WriteAllText(default_browser_path, def_browser);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed: " + ex.Message;
            }
        }

        string load_default_browser()
        {
            try
            {
                if (!File.Exists(default_browser_path) || string.IsNullOrWhiteSpace(File.ReadAllText(default_browser_path)))
                {
                    File.WriteAllText(default_browser_path, "https://www.bing.com");
                    return "https://www.bing.com";
                }

                return File.ReadAllText(default_browser_path);
            }
            catch
            {
                return "https://www.bing.com";
            }
        }

        async void Browser_SizeChanged(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            if (!resize_done) init_win_size();
        }

        void TabControl1_SelectedIndexChanged(object sender, EventArgs e) => UpdateSearchBarWithCurrentURL();

        void remove_tab_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 1)
            {
                TabPage selectedTab = tabControl1.SelectedTab;
                WebView2 webView = null;
                if (selectedTab.Controls.Count > 0 && selectedTab.Controls[0] is WebView2) webView = (WebView2)selectedTab.Controls[0];

                tabControl1.TabPages.Remove(selectedTab);

                webView?.Dispose();
                if (selectedTab != tabPage1) selectedTab.Dispose();
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes) Environment.Exit(0);
            }
        }

        void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add(tabPage1);
        }

        void StartSafetyChecks()
        {
            safetyTimer.Start();
        }

        HashSet<string> warnedUrls = new HashSet<string>();
        bool navigationHandlerAttached = false;

        void SafetyTimer_Tick(object sender, EventArgs e)
        {
            var webView = GetCurrentWebView();
            if (webView == null || webView.CoreWebView2 == null)
            {
                safetyTimer.Start();
                return;
            }

            string currentUrl = webView.CoreWebView2.Source?.ToLower() ?? "";
            string homepageUrlPath = "https://sites.google.com/view/reapers-browser/home";

            if (currentUrl.StartsWith("https://") || currentUrl == homepageUrlPath)
            {
                secure_connection = true;
                toolStripButton1.Text = "🔒";
            }
            else
            {
                secure_connection = false;
                toolStripButton1.Text = "⚠️";
                warnedUrls.Add("https");
            }

            if (!navigationHandlerAttached)
            {
                webView.CoreWebView2.NavigationStarting += WebView2NavigationHandler;
                webView.CoreWebView2.NavigationCompleted += WebView2NavigationCompleted;
                navigationHandlerAttached = true;
            }

            webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;

            safetyTimer.Start();
        }
        void WebView2NavigationHandler(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            string uri = args.Uri.ToLower();

            if (File.Exists(blocked_sites_file_path))
            {
                string[] blockedSites = File.ReadAllLines(blocked_sites_file_path);
                foreach (string site in blockedSites)
                {
                    string blocked = site.Trim().ToLower();
                    if (!string.IsNullOrEmpty(blocked) && uri.Contains(blocked))
                    {
                        args.Cancel = true;
                        ((CoreWebView2)sender).Navigate("about:blank");
                        MessageBox.Show("Access to this website is blocked.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }

            if (uri.Contains("malicious-code") && !warnedUrls.Contains("malicious"))
            {
                warnedUrls.Add("malicious");
                args.Cancel = true;
                ((CoreWebView2)sender).Navigate("about:blank");
                MessageBox.Show("Malicious content detected. Navigating to a safe page.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void WebView2NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            var webView = GetCurrentWebView();
            if (webView == null || webView.CoreWebView2 == null)
                return;

            if (webView.Parent is TabPage tabPage)
            {
                string title = webView.CoreWebView2.DocumentTitle;
                if (string.IsNullOrEmpty(title)) title = "New Tab";

                tabPage.Text = title.Length > 16 ? title.Substring(0, 16) + "..." : title;
            }

            search_bar.Text = webView.CoreWebView2.Source;

            LogHistory(webView.CoreWebView2.Source);
            UpdateHistoryComboBox();
            UpdateHistoryTextBox();
        }


        void LogHistory(string url)
        {
            string timestamp = DateTime.Now.ToString("[yyyy-MM-dd HH:mm]");
            string entry = $"{timestamp} {url}";
            File.AppendAllText(history_file_path, entry + Environment.NewLine);
            UpdateHistoryComboBox();
            UpdateHistoryTextBox();
        }

        void UpdateHistoryComboBox()
        {
            if (history_combo_box != null)
            {
                string[] lines = File.Exists(history_file_path)
                    ? File.ReadAllLines(history_file_path)
                    : new string[0];

                history_combo_box.Items.Clear();
                foreach (string line in lines)
                {
                    if (line.Contains("https://"))
                    {
                        string url = line.Substring(line.IndexOf("https://"));
                        history_combo_box.Items.Add(url);
                    }
                }
            }
        }

        public WebView2 GetCurrentWebView()
        {
            if (tabControl1.SelectedTab?.Controls[0] is WebView2 webView)
            {
                return webView;
            }

            return null;
        }



        void LoadBookmarks()
        {
            if (bookmarks_combo_box != null)
            {
                if (File.Exists(bookmarks_file_path))
                {
                    string[] bookmarks = File.ReadAllLines(bookmarks_file_path);
                    bookmarks_combo_box.Items.Clear();
                    foreach (string bookmark in bookmarks)
                    {
                        bookmarks_combo_box.Items.Add(bookmark);
                    }
                }
                LoadBookmarksToolStrip();
            }
        }

        void web_back_Click(object sender, EventArgs e)
        {
            GetCurrentWebView()?.CoreWebView2.GoBack();
        }

        void web_forward_Click(object sender, EventArgs e)
        {
            GetCurrentWebView()?.CoreWebView2.GoForward();
        }

        void web_refresh_Click(object sender, EventArgs e)
        {
            GetCurrentWebView()?.CoreWebView2.Reload();
        }


        void add_tab_Click(object sender, EventArgs e)
        {
            var webView = GetCurrentWebView();
            NewTab();
        }

        private async Task<Image> LoadFavicon(Uri faviconUri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] faviconBytes = await client.GetByteArrayAsync(faviconUri);
                    using (MemoryStream ms = new MemoryStream(faviconBytes))
                    {
                        return new Bitmap(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Bitmap($@"{Environment.CurrentDirectory}\def_favi.png");
            }
        }

        WebView2 NewTab(string url = "default_browser")
        {
            var tabPage = new TabPage("New Tab");
            var webView = new WebView2 { Dock = DockStyle.Fill };
            tabPage.Controls.Add(webView);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

            if (url == "default_browser")
                url = load_default_browser();

            if (tabControl1.ImageList == null)
            {
                tabControl1.ImageList = new ImageList();
            }

            webView.CoreWebView2InitializationCompleted += async (sender, args) =>
            {
                webView.CoreWebView2.NewWindowRequested += (sender2, e) =>
                {
                    e.Handled = true;
                    NewTab(e.Uri);
                };

                webView.CoreWebView2.Navigate(url);

                webView.CoreWebView2.NavigationCompleted += async (senderNav, argsNav) =>
                {
                    string currentUrl = webView.CoreWebView2.Source.ToLower();

                    if (IsBlockedSite(currentUrl))
                    {
                        webView.CoreWebView2.Navigate("about:blank");
                        MessageBox.Show("Access to this website is blocked.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    if (IsMaliciousContent(currentUrl))
                    {
                        webView.CoreWebView2.Navigate("about:blank");
                        MessageBox.Show("Malicious content detected. Navigating to a safe page.", "Security Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    search_bar.Text = webView.CoreWebView2.Source;

                    string title = webView.CoreWebView2.DocumentTitle;
                    tabPage.Text = title.Length > 16 ? title.Substring(0, 16) + "..." : title;

                    var faviconUri = new Uri(currentUrl + "/favicon.ico");
                    var favicon = await LoadFavicon(faviconUri);

                    if (favicon != null && favicon is Bitmap bitmapFavicon)
                    {
                        try
                        {
                            tabPage.ImageIndex = tabControl1.ImageList.Images.Add(bitmapFavicon, Color.Transparent);
                        }
                        catch (ExternalException ex) {}
                    }

                    LogHistory(webView.CoreWebView2.Source);
                    UpdateHistoryComboBox();
                    UpdateHistoryTextBox();
                    UpdateSearchBarWithCurrentURL();
                };
            };

            webView.EnsureCoreWebView2Async();

            return webView;
        }



        void search_bar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var webView = GetCurrentWebView();
                if (webView != null)
                {
                    string input = search_bar.Text.Trim();

                    if (IsBlockedSite(input) || IsMaliciousContent(input))
                    {
                        MessageBox.Show("Access to this site is blocked or contains malicious content.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.SuppressKeyPress = true;
                        return;
                    }

                    if (Uri.IsWellFormedUriString(input, UriKind.Absolute) &&
                        (input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                         input.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                         input.StartsWith("file:///", StringComparison.OrdinalIgnoreCase)))
                    {
                        webView.CoreWebView2.Navigate(input);
                    }
                    else if (!string.IsNullOrWhiteSpace(input))
                    {
                        string bingSearchUrl = $"{load_default_browser()}/search?q=" + Uri.EscapeDataString(input);
                        webView.CoreWebView2.Navigate(bingSearchUrl);
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid URL, search query, or file path.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (!string.IsNullOrWhiteSpace(input) && input.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
                    {
                        string localFilePath = input.Substring("file://".Length);
                        if (File.Exists(localFilePath))
                        {
                            webView.CoreWebView2.Navigate(new Uri(localFilePath).AbsoluteUri);
                        }
                        else
                        {
                            MessageBox.Show("No such file found: " + localFilePath, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                e.SuppressKeyPress = true;
            }
        }

        bool IsBlockedSite(string url)
        {
            if (File.Exists(blocked_sites_file_path))
            {
                string[] blockedSites = File.ReadAllLines(blocked_sites_file_path);
                foreach (string blocked in blockedSites)
                {
                    string cleaned = blocked.Trim().ToLower();
                    if (!string.IsNullOrEmpty(cleaned) && url.ToLower().Contains(cleaned))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool IsMaliciousContent(string url)
        {
            if (url.ToLower().Contains("malicious-code"))
            {
                return true;
            }
            return false;
        }
        void bookmark_button_Click(object sender, EventArgs e)
        {
            var webView = GetCurrentWebView();
            if (webView != null)
            {
                string currentUrl = webView.Source.ToString();
                string currentTitle = webView.CoreWebView2.DocumentTitle;

                string[] bookmarks = File.Exists(bookmarks_file_path)
                    ? File.ReadAllLines(bookmarks_file_path)
                    : new string[0];

                if (Array.Exists(bookmarks, bookmark => bookmark.Contains($"[Link:\"{currentUrl}\"]")))
                {
                    MessageBox.Show("This page is already bookmarked.");
                }
                else
                {
                    string bookmark = $"[Title:\"{currentTitle}\"][Link:\"{currentUrl}\"]";
                    File.AppendAllText(bookmarks_file_path, bookmark + Environment.NewLine);

                    bookmarks_combo_box.Items.Add(currentTitle);
                }

                LoadBookmarksToolStrip();
            }
        }

        void UpdateHistoryTextBox()
        {
            if (history_textbox != null)
            {
                if (File.Exists(history_file_path))
                {
                    string[] lines = File.ReadAllLines(history_file_path);
                    history_textbox.Clear();

                    foreach (string line in lines)
                    {
                        history_textbox.AppendText($"{line}\r\n");
                    }
                }
                else
                {
                    history_textbox.AppendText("No history found.\r\n");
                }
            }
        }


        private void bookmarks_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveBookmarks(bookmarks_textbox.Text);
                e.SuppressKeyPress = true;
            }
        }

        void SaveBookmarks(string text)
        {
            var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(line => line.Trim())
                            .Where(line => line.StartsWith("[Title:\"") && line.Contains("\"][Link:\"") && line.EndsWith("\"]"))
                            .ToList();

            File.WriteAllLines(bookmarks_file_path, lines);

            UpdateBookmarksTextBox();
            LoadBookmarksToolStrip();
        }

        void UpdateBookmarksTextBox()
        {
            if (bookmarks_combo_box != null)
            {
                if (File.Exists(bookmarks_file_path))
                {
                    string[] lines = File.ReadAllLines(bookmarks_file_path);
                    bookmarks_textbox.Clear();

                    foreach (string line in lines)
                    {
                        if (line.Contains("[Title:\"") && line.Contains("\"][Link:\""))
                        {
                            string[] parts = line.Split(new string[] { "\"][Link:\"" }, StringSplitOptions.None);

                            if (parts.Length == 2)
                            {
                                string title = parts[0].Replace("[Title:\"", "").Replace("\"", "").Trim();
                                string url = parts[1].Replace("\"]", "").Replace("\"", "").Trim();

                                bookmarks_textbox.AppendText($"[Title:\"{title}\"][Link:\"{url}\"]\r\n");
                            }
                        }
                    }
                }
                else
                {
                    bookmarks_textbox.AppendText("No bookmarks found.\r\n");
                }
            }
        }





        void history_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = history_textbox.Text;

                SaveHistory(text);

                UpdateHistoryTextBox();
                e.SuppressKeyPress = true;
            }
        }
        void blocked_sites_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = blocked_sites_textbox.Text.Trim();

                if (!string.IsNullOrEmpty(text))
                {
                    if (!IsBlockedSite(text))
                    {
                        SaveBlockedSitesToFile(text);
                        UpdateBlockedSitesTextBox();
                    }
                }

                e.SuppressKeyPress = true;
            }
        }

        void SaveBlockedSitesToFile(string newEntry)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(blocked_sites_file_path));

            using (StreamWriter writer = new StreamWriter(blocked_sites_file_path, true))
            {
                writer.WriteLine(newEntry.Trim());
            }
        }


        void LoadBlockedSitesFromFile()
        {
            if (blocked_sites_textbox == null) return;

            blocked_sites_textbox.Clear();

            if (File.Exists(blocked_sites_file_path))
            {
                string[] lines = File.ReadAllLines(blocked_sites_file_path);
                foreach (string line in lines)
                {
                    string cleaned = line.Trim();
                    if (!string.IsNullOrEmpty(cleaned))
                    {
                        blocked_sites_textbox.AppendText(cleaned + Environment.NewLine);
                    }
                }
            }
            else
            {
                blocked_sites_textbox.AppendText("No blocked sites found." + Environment.NewLine);
            }
        }




        void UpdateBlockedSitesTextBox()
        {
            blocked_sites_textbox.Clear();

            if (File.Exists(blocked_sites_file_path))
            {
                string[] lines = File.ReadAllLines(blocked_sites_file_path);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        blocked_sites_textbox.AppendText($"{line.Trim()}\r\n");
                    }
                }
            }
            else
            {
                blocked_sites_textbox.AppendText("No blocked sites found.\r\n");
            }
        }


        void SaveHistory(string content)
        {
            if (!File.Exists(history_file_path))
            {
                File.Create(history_file_path).Close();
            }

            File.WriteAllText(history_file_path, content);
        }

        void overwrite_history_Click(object sender, EventArgs e)
        {
            if (File.Exists(history_file_path))
            {
                string[] lines = File.ReadAllLines(history_file_path);
                using (FileStream fs = new FileStream(history_file_path, FileMode.Open, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    foreach (string line in lines)
                    {
                        writer.WriteLine(new string('0', line.Length));
                    }
                }

                using (StreamWriter writer = new StreamWriter(history_file_path))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        writer.WriteLine(Guid.NewGuid().ToString());
                    }
                }

                File.WriteAllText(history_file_path, string.Empty);
            }

            history_combo_box.Items.Clear();
        }


        void overwrite_bookmarks_Click(object sender, EventArgs e)
        {
            var confirmation = MessageBox.Show("Are you sure you want to overwrite all bookmarks?", "Confirm", MessageBoxButtons.YesNo);

            if (confirmation == DialogResult.Yes)
            {
                if (File.Exists(bookmarks_file_path))
                {
                    string[] lines = File.ReadAllLines(bookmarks_file_path);
                    using (FileStream fs = new FileStream(bookmarks_file_path, FileMode.Open, FileAccess.Write))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        foreach (string line in lines)
                        {
                            writer.WriteLine(new string('0', line.Length));
                        }
                    }

                    using (StreamWriter writer = new StreamWriter(bookmarks_file_path))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            writer.WriteLine(Guid.NewGuid().ToString());
                        }
                    }

                    File.WriteAllText(bookmarks_file_path, string.Empty);
                }

                bookmarks_combo_box.Items.Clear();
                LoadBookmarks();
            }
        }
        void ExitPrompt(FormClosingEventArgs e, bool form_closing_event = true)
        {
            if (form_closing_event)
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Browser.ActiveForm.Dispose();
                    tabPage1.Dispose();
                }
            }
            else
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    tabPage1.Dispose();
                    Browser.ActiveForm.Dispose();
                    Environment.Exit(0);
                }
            }
        }
        void Browser_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitPrompt(e);
        }

        void deleteAllBlockedSitesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(blocked_sites_file_path, string.Empty);

            UpdateBlockedSitesTextBox();

            MessageBox.Show("All blocked sites have been deleted.", "Blocked Sites Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}