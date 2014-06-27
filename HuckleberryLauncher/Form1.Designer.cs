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
            this.password = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.username = new System.Windows.Forms.TextBox();
            this.player_head = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player_head)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            // player_head
            // 
            this.player_head.Image = global::HuckleberryLauncher.Properties.Resources.player_unknown1;
            this.player_head.Location = new System.Drawing.Point(272, 6);
            this.player_head.Name = "player_head";
            this.player_head.Size = new System.Drawing.Size(64, 64);
            this.player_head.TabIndex = 1;
            this.player_head.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::HuckleberryLauncher.Properties.Resources.logo_small1;
            this.pictureBox1.Location = new System.Drawing.Point(5, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(432, 99);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BackgroundImage = global::HuckleberryLauncher.Properties.Resources.huckleberry_back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(967, 548);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Huckleberry - Runsafe Blend Minecraft";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player_head)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox player_head;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox username;

    }
}

