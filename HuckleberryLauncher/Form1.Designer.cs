namespace HuckleberryLauncher
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
            this.logo = new ClickThroughPictureBox();
            this.exitButton = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.username = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.TextBox();
            this.player_head = new System.Windows.Forms.PictureBox();
            this.player_splash = new HuckleberryLauncher.SplashPlayerImage();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player_head)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_splash)).BeginInit();
            this.SuspendLayout();
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
            // exitButton
            // 
            this.exitButton.BackgroundImage = global::HuckleberryLauncher.Properties.Resources.exit_button_normal;
            this.exitButton.Location = new System.Drawing.Point(923, 7);
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
            // player_splash
            // 
            this.player_splash.BackColor = System.Drawing.Color.Transparent;
            this.player_splash.Location = new System.Drawing.Point(545, 7);
            this.player_splash.Name = "player_splash";
            this.player_splash.Size = new System.Drawing.Size(423, 541);
            this.player_splash.TabIndex = 6;
            this.player_splash.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BackgroundImage = global::HuckleberryLauncher.Properties.Resources.background_green;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(967, 548);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.player_splash);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Huckleberry - Runsafe Blend Minecraft";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player_head)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player_splash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.PictureBox player_head;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.PictureBox exitButton;
        private SplashPlayerImage player_splash;

    }
}

