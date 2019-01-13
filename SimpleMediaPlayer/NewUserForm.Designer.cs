namespace SimpleMediaPlayer
{
    partial class NewUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewUserForm));
            this.btnYes = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.txtAgain = new System.Windows.Forms.TextBox();
            this.pbMinForm = new System.Windows.Forms.PictureBox();
            this.pbMaxForm = new System.Windows.Forms.PictureBox();
            this.pbCloseForm = new System.Windows.Forms.PictureBox();
            this.btnUsername = new System.Windows.Forms.Button();
            this.btnPassword = new System.Windows.Forms.Button();
            this.btnAgain = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaxForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseForm)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.yes;
            this.btnYes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Location = new System.Drawing.Point(316, 402);
            this.btnYes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(157, 52);
            this.btnYes.TabIndex = 0;
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.back;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Location = new System.Drawing.Point(612, 402);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(129, 52);
            this.btnBack.TabIndex = 1;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(612, 130);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(153, 42);
            this.txtName.TabIndex = 5;
            // 
            // txtPassWord
            // 
            this.txtPassWord.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassWord.Location = new System.Drawing.Point(612, 228);
            this.txtPassWord.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(153, 42);
            this.txtPassWord.TabIndex = 6;
            // 
            // txtAgain
            // 
            this.txtAgain.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAgain.Location = new System.Drawing.Point(612, 324);
            this.txtAgain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAgain.Name = "txtAgain";
            this.txtAgain.PasswordChar = '*';
            this.txtAgain.Size = new System.Drawing.Size(153, 42);
            this.txtAgain.TabIndex = 7;
            // 
            // pbMinForm
            // 
            this.pbMinForm.BackColor = System.Drawing.Color.Black;
            this.pbMinForm.Image = global::SimpleMediaPlayer.Properties.Resources.最小化;
            this.pbMinForm.Location = new System.Drawing.Point(883, 1);
            this.pbMinForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbMinForm.Name = "pbMinForm";
            this.pbMinForm.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pbMinForm.Size = new System.Drawing.Size(32, 30);
            this.pbMinForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMinForm.TabIndex = 8;
            this.pbMinForm.TabStop = false;
            this.pbMinForm.Click += new System.EventHandler(this.pbMinForm_Click);
            // 
            // pbMaxForm
            // 
            this.pbMaxForm.BackColor = System.Drawing.Color.Black;
            this.pbMaxForm.Image = global::SimpleMediaPlayer.Properties.Resources.最大化;
            this.pbMaxForm.Location = new System.Drawing.Point(915, 1);
            this.pbMaxForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbMaxForm.Name = "pbMaxForm";
            this.pbMaxForm.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pbMaxForm.Size = new System.Drawing.Size(32, 30);
            this.pbMaxForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMaxForm.TabIndex = 9;
            this.pbMaxForm.TabStop = false;
            this.pbMaxForm.Click += new System.EventHandler(this.pbMaxForm_Click);
            // 
            // pbCloseForm
            // 
            this.pbCloseForm.BackColor = System.Drawing.Color.Black;
            this.pbCloseForm.Image = global::SimpleMediaPlayer.Properties.Resources.关闭;
            this.pbCloseForm.Location = new System.Drawing.Point(947, 1);
            this.pbCloseForm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbCloseForm.Name = "pbCloseForm";
            this.pbCloseForm.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.pbCloseForm.Size = new System.Drawing.Size(32, 30);
            this.pbCloseForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCloseForm.TabIndex = 10;
            this.pbCloseForm.TabStop = false;
            this.pbCloseForm.Click += new System.EventHandler(this.pbCloseForm_Click);
            // 
            // btnUsername
            // 
            this.btnUsername.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.usernameLast;
            this.btnUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsername.Location = new System.Drawing.Point(216, 119);
            this.btnUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUsername.Name = "btnUsername";
            this.btnUsername.Size = new System.Drawing.Size(289, 61);
            this.btnUsername.TabIndex = 11;
            this.btnUsername.UseVisualStyleBackColor = true;
            // 
            // btnPassword
            // 
            this.btnPassword.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.passwordLast;
            this.btnPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPassword.Location = new System.Drawing.Point(246, 207);
            this.btnPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(259, 62);
            this.btnPassword.TabIndex = 12;
            this.btnPassword.UseVisualStyleBackColor = true;
            // 
            // btnAgain
            // 
            this.btnAgain.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.confirmLast;
            this.btnAgain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgain.Location = new System.Drawing.Point(26, 296);
            this.btnAgain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgain.Name = "btnAgain";
            this.btnAgain.Size = new System.Drawing.Size(479, 65);
            this.btnAgain.TabIndex = 13;
            this.btnAgain.UseVisualStyleBackColor = true;
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SimpleMediaPlayer.Properties.Resources.zhuceLast;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(979, 528);
            this.Controls.Add(this.btnAgain);
            this.Controls.Add(this.btnPassword);
            this.Controls.Add(this.btnUsername);
            this.Controls.Add(this.pbCloseForm);
            this.Controls.Add(this.pbMaxForm);
            this.Controls.Add(this.pbMinForm);
            this.Controls.Add(this.txtAgain);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnYes);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "NewUserForm";
            this.RightToLeftLayout = true;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "NewUser";
            ((System.ComponentModel.ISupportInitialize)(this.pbMinForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaxForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCloseForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.TextBox txtAgain;
        private System.Windows.Forms.PictureBox pbMinForm;
        private System.Windows.Forms.PictureBox pbMaxForm;
        private System.Windows.Forms.PictureBox pbCloseForm;
        private System.Windows.Forms.Button btnUsername;
        private System.Windows.Forms.Button btnPassword;
        private System.Windows.Forms.Button btnAgain;
    }
}