using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Diagnostics;

namespace HuckleberryLauncher
{
    public partial class MainForm : Form
    {
        private static String host = "https://huckleberry.runsafe.no/";
        private static String folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.huckleberry\";
        private static String profile = folder + "profile";
        private String loggedIn = null;
        private String accessToken = null;
        private Boolean assetSyncStarted = false;
        private Boolean libSyncStarted = false;
        private List<String> libCollection = new List<String>();

        public MainForm()
        {
            InitializeComponent();

            Directory.CreateDirectory(folder);

            player_splash.Image = (Image) Properties.Resources.ResourceManager.GetObject("player_shot_" + new Random().Next(1, 8));
            //new Thread(() => queryLatest()).Start();
        }

        public void queryLatest()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    setLatestContent(e.Result);
                };
                client.DownloadStringAsync(new Uri(MainForm.host + "latest.txt"));
                client.Dispose();
            }
        }

        public void setLatestContent(String content)
        {
            //Action action = delegate { updates_panel.Rtf = content; };
            //updates_panel.Invoke(action);
        }

        public void authenticateUser(String username, String password)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    handleAuthenticationResponse(e.Result);
                };
                client.DownloadStringAsync(new Uri(MainForm.host + "auth.php?username=" + username + "&password=" + password));
                client.Dispose();
            }
        }

        public void handleAuthenticationResponse(String response)
        {
            if (response == "invalid")
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    username.Show();
                    password.Show();
                    auth_loading_label.Hide();
                    MessageBox.Show("Invalid username/password!", "Great Scott!");
                });
            }
            else if (response == "noinvite")
            {
                this.Invoke((MethodInvoker)delegate()
                {
                    username.Show();
                    password.Show();
                    auth_loading_label.Hide();
                    MessageBox.Show("Your account hasn't been invited to Huckleberry yet!", "Great Scott!");
                });
            }
            else if (response.StartsWith("USER"))
            {
                loginPlayer(response);

                using (StreamWriter writer = new StreamWriter(MainForm.profile))
                {
                    writer.Write(response);
                    writer.Dispose();
                }
            }
        }

        public void loginPlayer(String loginKey)
        {
            String[] split = loginKey.Substring(4).Split(',');

            this.loggedIn = split[0];
            this.accessToken = split[1];

            String skinFolder = MainForm.folder + "skin_cache/";
            String skinPath = skinFolder + this.loggedIn + ".png";
            Directory.CreateDirectory(skinFolder);

            this.Invoke((MethodInvoker)delegate()
            {
                username.Hide();
                password.Hide();

                logged_in_label.Text = "Logged in as: " + this.loggedIn;
                logged_in_label.Show();
                play_button.Show();
                logout_button.Show();
                auth_loading_label.Hide();
            });

            applySkin(skinPath);
            using (WebClient client = new WebClient())
            {
                client.Headers.Set("User-Agent", "Runsafe Huckleberry");
                client.DownloadFileCompleted += (sender, e) =>
                {
                    applySkin(skinPath);
                };
                client.DownloadFileAsync(new Uri("http://skins.runsafe.no/" + this.loggedIn + ".png"), skinPath);
                client.Dispose();
            }
        }

        public void applySkin(String skinFile)
        {
            if (File.Exists(skinFile))
            {
                FileInfo info = new FileInfo(skinFile);

                if (info.Length > 0)
                {
                    Bitmap bmpImage = new Bitmap(Image.FromFile(skinFile));
                    Bitmap bmpCrop = bmpImage.Clone(new Rectangle(8, 8, 8, 8), bmpImage.PixelFormat);

                    using (bmpCrop)
                    {
                        var bmp2 = new Bitmap(player_head.Width, player_head.Height);
                        using (var g = Graphics.FromImage(bmp2))
                        {
                            g.InterpolationMode = InterpolationMode.NearestNeighbor;
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.DrawImage(bmpCrop, new Rectangle(Point.Empty, bmp2.Size));
                            this.Invoke((MethodInvoker)delegate()
                            {
                                player_head.Image = (Image)bmp2;
                            });
                        }
                    }
                }
            }
        }

        private void fieldBlur(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 0)
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.ForeColor = Color.Gray;

                if (textBox.Name == "password")
                    textBox.PasswordChar = '\0';
            }
        }

        private void fieldFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.ForeColor = Color.Black;
                textBox.Text = String.Empty;

                if (textBox.Name == "password")
                    textBox.PasswordChar = '*';
            }
        }

        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = global::HuckleberryLauncher.Properties.Resources.exit_button_hover;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = global::HuckleberryLauncher.Properties.Resources.exit_button_normal;
        }

        private void exitButton_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void updates_panel_Enter(object sender, EventArgs e)
        {
            focus_holder.Focus();
        }

        private void username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                password.Focus();
                e.Handled = true;
            }
        }

        private void password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                String username_value = username.Text.Trim();
                String password_value = password.Text.Trim();

                if (username_value.Length > 0 && password_value.Length > 0)
                {
                    username.Hide();
                    password.Hide();
                    auth_loading_label.Show();

                    new Thread(() => authenticateUser(username_value, password_value)).Start();
                }

                e.Handled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(MainForm.profile))
            {
                String loginKey = File.ReadAllText(MainForm.profile);
                new Thread(() => loginPlayer(loginKey)).Start();
            }
        }

        private void logout_button_Click(object sender, EventArgs e)
        {
            File.Delete(MainForm.profile);
            this.loggedIn = null;
            this.accessToken = null;

            username.Show();
            password.Show();
            logged_in_label.Hide();
            play_button.Hide();
            logout_button.Hide();

            player_head.Image = Properties.Resources.player_unknown1;
        }

        private void play_button_Click(object sender, EventArgs e)
        {
            play_button.Enabled = false;
            logout_button.Enabled = false;

            loadbar.Show();

            new Thread(() => loadLibs()).Start();
            new Thread(() => loadAssets()).Start();
        }

        public void loadAssets()
        {
            WebClient client = new WebClient();
            String assetList = client.DownloadString(new Uri(MainForm.host + "assets.dat"));
            client.Dispose();

            String[] asset_split = assetList.Split((char) 31);
            String[] asset_dir_mappings = asset_split[0].Split((char) 30);

            foreach (String mapping in asset_dir_mappings)
                Directory.CreateDirectory(MainForm.folder + @"assets\" + mapping);

            String[] assets = asset_split[1].Split((char)30);

            this.Invoke((MethodInvoker)delegate()
            {
                assetSyncStarted = true;
                loadbar.Maximum += assets.Length;
            });

            String assetFolder = MainForm.folder + @"assets\";
            foreach (String asset in assets)
            {
                String[] asset_parts = asset.Split(':');
                String assetPath = assetFolder + asset_parts[0];

                if (File.Exists(assetPath))
                {
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = File.OpenRead(assetPath))
                        {
                            String hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                            stream.Close();
                            md5.Dispose();

                            if (asset_parts[1] == hash)
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    libComplete();
                                });
                            }
                            else
                            {
                                startAssetDownload(asset_parts[0], assetPath);
                            }
                        }
                    }
                }
                else
                {
                    startAssetDownload(asset_parts[0], assetPath);
                }
            }
        }

        public void loadLibs()
        {
            WebClient client = new WebClient();
            String lib_list = client.DownloadString(new Uri(MainForm.host + "lib_list.php"));
            client.Dispose();

            String[] libs = lib_list.Split(',');

            this.Invoke((MethodInvoker)delegate()
            {
                libSyncStarted = true;
                loadbar.Maximum += libs.Length;
            });

            String libFolder = MainForm.folder + @"libs\";
            Directory.CreateDirectory(libFolder);

            foreach (String lib in libs)
            {
                String[] lib_parts = lib.Split(':');
                String libPath = libFolder + lib_parts[0];

                this.Invoke((MethodInvoker)delegate()
                {
                    this.libCollection.Add(libPath);
                });

                if (File.Exists(libPath))
                {
                    using (var md5 = MD5.Create())
                    {
                        using (var stream = File.OpenRead(libPath))
                        {
                            String hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                            stream.Close();
                            md5.Dispose();

                            if (lib_parts[1] == hash)
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    libComplete();
                                });
                            }
                            else
                            {
                                startLibDownload(lib_parts[0], libPath);
                            }
                        }
                    }
                }
                else
                {
                    startLibDownload(lib_parts[0], libPath);
                }
            }
        }

        public void startLibDownload(String lib, String path)
        {
            Console.Write("Lib: " + lib);
            Console.Write("Path: " + path);
            using (WebClient client = new WebClient())
            {
                client.DownloadFileCompleted += (sender, e) =>
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        libComplete();
                    });
                };
                client.DownloadFileAsync(new Uri(MainForm.host + "client/libs/" + lib), path);
            }
        }

        public void startAssetDownload(String asset, String path)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFileCompleted += (sender, e) =>
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        libComplete();
                    });
                };
                client.DownloadFileAsync(new Uri(MainForm.host + "client/assets/" + asset), path);
            }
        }

        public void libComplete()
        {
            loadbar.Value += 1;
            checkSyncStatus();
        }

        public void checkSyncStatus()
        {
            if (libSyncStarted && assetSyncStarted && loadbar.Value == loadbar.Maximum)
            {
                try
                {
                    String path = null;

                    String[] dirs = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + Path.DirectorySeparatorChar + "Java");

                    foreach (String subDir in dirs)
                    {
                        String[] subDirs = Directory.GetDirectories(subDir);

                        foreach (String subSubDir in subDirs)
                        {
                            if (Path.GetFileName(subSubDir) == "bin")
                            {
                                String[] files = Directory.GetFiles(subSubDir);

                                foreach (String file in files)
                                {
                                    if (Path.GetFileName(file) == "java.exe")
                                    {
                                        path = file;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (path != null)
                    {
                        Process process = new System.Diagnostics.Process();
                        ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = path;
                        startInfo.Arguments = "-Djava.library.path=\"" + MainForm.folder + @"libs\ -cp " + String.Join(";", this.libCollection) + "\" net.minecraft.client.main.Main --username " + this.loggedIn + " --session " + this.accessToken + " --version 1.6.4 --gameDir " + MainForm.folder + " --assetsDir " + MainForm.folder + "assets";
                        process.StartInfo = startInfo;
                        process.Start();

                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Could not find Java. :(");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}
