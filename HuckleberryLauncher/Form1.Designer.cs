namespace HuckleberryLauncher
{
    partial class MainForm
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
            this.exitButton = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.player_head = new System.Windows.Forms.PictureBox();
            this.focus_holder = new System.Windows.Forms.Label();
            this.auth_loading_label = new System.Windows.Forms.Label();
            this.logged_in_label = new System.Windows.Forms.Label();
            this.play_button = new System.Windows.Forms.Button();
            this.logout_button = new System.Windows.Forms.Button();
            this.loadbar = new System.Windows.Forms.ProgressBar();
            this.updates_panel = new HuckleberryLauncher.TransparentRTF();
            this.logo = new HuckleberryLauncher.ClickThroughPictureBox();
            this.player_splash = new HuckleberryLauncher.SplashPlayerImage();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player_head)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_splash)).BeginInit();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackgroundImage = global::HuckleberryLauncher.Properties.Resources.exit_button_normal;
            this.exitButton.Location = new System.Drawing.Point(923, 9);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 5;
            this.exitButton.TabStop = false;
            this.exitButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.exitButton_MouseClick);
            this.exitButton.MouseEnter += new System.EventHandler(this.exitButton_MouseEnter);
            this.exitButton.MouseLeave += new System.EventHandler(this.exitButton_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::HuckleberryLauncher.Properties.Resources.transparent_bg;
            this.panel1.Controls.Add(this.logout_button);
            this.panel1.Controls.Add(this.play_button);
            this.panel1.Controls.Add(this.logged_in_label);
            this.panel1.Controls.Add(this.auth_loading_label);
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.password);
            this.panel1.Controls.Add(this.player_head);
            this.panel1.Location = new System.Drawing.Point(613, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 76);
            this.panel1.TabIndex = 4;
            // 
            // username
            // 
            this.username.ForeColor = System.Drawing.Color.Gray;
            this.username.Location = new System.Drawing.Point(18, 14);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(236, 20);
            this.username.TabIndex = 2;
            this.username.Tag = "Username...";
            this.username.Text = "Username...";
            this.username.Enter += new System.EventHandler(this.fieldFocus);
            this.username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.username_KeyPress);
            this.username.Leave += new System.EventHandler(this.fieldBlur);
            // 
            // password
            // 
            this.password.ForeColor = System.Drawing.Color.Gray;
            this.password.Location = new System.Drawing.Point(18, 40);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(236, 20);
            this.password.TabIndex = 2;
            this.password.Tag = "Password...";
            this.password.Text = "Password...";
            this.password.Enter += new System.EventHandler(this.fieldFocus);
            this.password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.password_KeyPress);
            this.password.Leave += new System.EventHandler(this.fieldBlur);
            // 
            // player_head
            // 
            this.player_head.Image = global::HuckleberryLauncher.Properties.Resources.player_unknown1;
            this.player_head.Location = new System.Drawing.Point(272, 6);
            this.player_head.Name = "player_head";
            this.player_head.Size = new System.Drawing.Size(64, 64);
            this.player_head.TabIndex = 1;
            this.player_head.TabStop = false;
            // 
            // focus_holder
            // 
            this.focus_holder.AutoSize = true;
            this.focus_holder.BackColor = System.Drawing.Color.Transparent;
            this.focus_holder.Location = new System.Drawing.Point(12, -19);
            this.focus_holder.Name = "focus_holder";
            this.focus_holder.Size = new System.Drawing.Size(0, 13);
            this.focus_holder.TabIndex = 1;
            // 
            // auth_loading_label
            // 
            this.auth_loading_label.AutoSize = true;
            this.auth_loading_label.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.auth_loading_label.Location = new System.Drawing.Point(65, 25);
            this.auth_loading_label.Name = "auth_loading_label";
            this.auth_loading_label.Size = new System.Drawing.Size(145, 25);
            this.auth_loading_label.TabIndex = 3;
            this.auth_loading_label.Text = "Logging in...";
            this.auth_loading_label.Visible = false;
            // 
            // logged_in_label
            // 
            this.logged_in_label.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logged_in_label.Location = new System.Drawing.Point(9, 12);
            this.logged_in_label.Name = "logged_in_label";
            this.logged_in_label.Size = new System.Drawing.Size(257, 20);
            this.logged_in_label.TabIndex = 3;
            this.logged_in_label.Text = "Logged in as: Someone";
            this.logged_in_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.logged_in_label.Visible = false;
            // 
            // play_button
            // 
            this.play_button.Location = new System.Drawing.Point(26, 40);
            this.play_button.Name = "play_button";
            this.play_button.Size = new System.Drawing.Size(104, 23);
            this.play_button.TabIndex = 4;
            this.play_button.Text = "Play!";
            this.play_button.UseVisualStyleBackColor = true;
            this.play_button.Visible = false;
            this.play_button.Click += new System.EventHandler(this.play_button_Click);
            // 
            // logout_button
            // 
            this.logout_button.Location = new System.Drawing.Point(142, 40);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(104, 23);
            this.logout_button.TabIndex = 4;
            this.logout_button.Text = "Logout";
            this.logout_button.UseVisualStyleBackColor = true;
            this.logout_button.Visible = false;
            this.logout_button.Click += new System.EventHandler(this.logout_button_Click);
            // 
            // loadbar
            // 
            this.loadbar.ForeColor = System.Drawing.Color.Yellow;
            this.loadbar.Location = new System.Drawing.Point(13, 515);
            this.loadbar.Maximum = 0;
            this.loadbar.Name = "loadbar";
            this.loadbar.Size = new System.Drawing.Size(588, 23);
            this.loadbar.TabIndex = 8;
            this.loadbar.Visible = false;
            // 
            // updates_panel
            // 
            this.updates_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.updates_panel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updates_panel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.updates_panel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updates_panel.Location = new System.Drawing.Point(25, 141);
            this.updates_panel.Name = "updates_panel";
            this.updates_panel.ReadOnly = true;
            this.updates_panel.Size = new System.Drawing.Size(494, 353);
            this.updates_panel.TabIndex = 7;
            this.updates_panel.Text = "Loading latest updates...";
            this.updates_panel.Enter += new System.EventHandler(this.updates_panel_Enter);
            // 
            // logo
            // 
            this.logo.BackColor = System.Drawing.Color.Transparent;
            this.logo.Image = global::HuckleberryLauncher.Properties.Resources.logo_small;
            this.logo.Location = new System.Drawing.Point(25, 24);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(432, 99);
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // player_splash
            // 
            this.player_splash.BackColor = System.Drawing.Color.Transparent;
            this.player_splash.Location = new System.Drawing.Point(545, 7);
            this.player_splash.Name = "player_splash";
            this.player_splash.Size = new System.Drawing.Size(423, 541);
            this.player_splash.TabIndex = 6;
            this.player_splash.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BackgroundImage = global::HuckleberryLauncher.Properties.Resources.background_green;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(967, 548);
            this.Controls.Add(this.loadbar);
            this.Controls.Add(this.focus_holder);
            this.Controls.Add(this.updates_panel);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.player_splash);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Huckleberry - Runsafe Blend Minecraft";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player_head)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_splash)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox player_head;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.PictureBox exitButton;
        private SplashPlayerImage player_splash;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ClickThroughPictureBox logo;
        private TransparentRTF updates_panel;
        private System.Windows.Forms.Label focus_holder;
        private System.Windows.Forms.Label auth_loading_label;
        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.Button play_button;
        private System.Windows.Forms.Label logged_in_label;
        private System.Windows.Forms.ProgressBar loadbar;

    }
}

