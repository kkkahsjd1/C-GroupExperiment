namespace SimpleMediaPlayer
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pbMaxForm = new System.Windows.Forms.PictureBox();
            this.pbCloseForm = new System.Windows.Forms.PictureBox();
            this.pbMinForm = new System.Windows.Forms.PictureBox();
            this.pbUsername = new System.Windows.Forms.PictureBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaxForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUsername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.login;
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLogin.Location = new System.Drawing.Point(612, 552);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(180, 84);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.register;
            this.btnRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegister.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRegister.Location = new System.Drawing.Point(827, 552);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(224, 84);
            this.btnRegister.TabIndex = 1;
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.Location = new System.Drawing.Point(648, 442);
            this.txtPassword.MinimumSize = new System.Drawing.Size(100, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(340, 60);
            this.txtPassword.TabIndex = 6;
            // 
            // pbMaxForm
            // 
            this.pbMaxForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMaxForm.BackColor = System.Drawing.Color.Black;
            this.pbMaxForm.Image = global::SimpleMediaPlayer.Properties.Resources.最大化;
            this.pbMaxForm.Location = new System.Drawing.Point(1044, 1);
            this.pbMaxForm.Name = "pbMaxForm";
            this.pbMaxForm.Padding = new System.Windows.Forms.Padding(6);
            this.pbMaxForm.Size = new System.Drawing.Size(36, 36);
            this.pbMaxForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMaxForm.TabIndex = 7;
            this.pbMaxForm.TabStop = false;
            this.pbMaxForm.Click += new System.EventHandler(this.pbMaxForm_Click);
            // 
            // pbCloseForm
            // 
            this.pbCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCloseForm.BackColor = System.Drawing.Color.Black;
            this.pbCloseForm.Image = global::SimpleMediaPlayer.Properties.Resources.关闭;
            this.pbCloseForm.Location = new System.Drawing.Point(1086, 1);
            this.pbCloseForm.Name = "pbCloseForm";
            this.pbCloseForm.Padding = new System.Windows.Forms.Padding(8);
            this.pbCloseForm.Size = new System.Drawing.Size(36, 36);
            this.pbCloseForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCloseForm.TabIndex = 8;
            this.pbCloseForm.TabStop = false;
            this.pbCloseForm.Click += new System.EventHandler(this.pbCloseForm_Click);
            // 
            // pbMinForm
            // 
            this.pbMinForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMinForm.BackColor = System.Drawing.Color.Black;
            this.pbMinForm.Image = global::SimpleMediaPlayer.Properties.Resources.最小化;
            this.pbMinForm.Location = new System.Drawing.Point(1002, 1);
            this.pbMinForm.Name = "pbMinForm";
            this.pbMinForm.Padding = new System.Windows.Forms.Padding(6);
            this.pbMinForm.Size = new System.Drawing.Size(36, 36);
            this.pbMinForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMinForm.TabIndex = 9;
            this.pbMinForm.TabStop = false;
            this.pbMinForm.Click += new System.EventHandler(this.pbMinForm_Click);
            // 
            // pbUsername
            // 
            this.pbUsername.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.username;
            this.pbUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbUsername.Location = new System.Drawing.Point(648, 82);
            this.pbUsername.Name = "pbUsername";
            this.pbUsername.Size = new System.Drawing.Size(340, 80);
            this.pbUsername.TabIndex = 10;
            this.pbUsername.TabStop = false;
            this.pbUsername.Click += new System.EventHandler(this.pbUsername_Click);
            // 
            // pbPassword
            // 
            this.pbPassword.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.password;
            this.pbPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbPassword.Location = new System.Drawing.Point(648, 319);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(340, 80);
            this.pbPassword.TabIndex = 11;
            this.pbPassword.TabStop = false;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtUserName.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUserName.Location = new System.Drawing.Point(648, 208);
            this.txtUserName.MinimumSize = new System.Drawing.Size(100, 60);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(340, 60);
            this.txtUserName.TabIndex = 5;
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.begin;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1123, 690);
            this.Controls.Add(this.pbPassword);
            this.Controls.Add(this.pbUsername);
            this.Controls.Add(this.pbMinForm);
            this.Controls.Add(this.pbCloseForm);
            this.Controls.Add(this.pbMaxForm);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbMaxForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUsername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pbCloseForm;
        private System.Windows.Forms.PictureBox pbMinForm;
        private System.Windows.Forms.PictureBox pbMaxForm;
        private System.Windows.Forms.PictureBox pbUsername;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.TextBox txtUserName;
    }
}