using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleMediaPlayer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //改变按钮颜色
            //this.btnLogin.BackgroundImage = SimpleMediaPlayer.Properties.
            //    Resources.login2;
            string userName = this.txtUserName.Text;
            string userPassWord = this.txtPassword.Text;

            if(userName.Equals("")||userPassWord.Equals(""))
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            else
            {
                //如果用户名和密码正确，则跳转至播放器界面
                if (isExist(userName,userPassWord)) 
                {
                    this.Hide();
                    mainForm mf = new mainForm();
                    mf.lblUser.Text = userName;
                    mf.Show();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.btnRegister.BackgroundImage = SimpleMediaPlayer.Properties.
                Resources.new2;
            this.Hide();
            NewUserForm nuf = new NewUserForm();
            nuf.Show();
        }

        //public bool isExist(string username,string password)
        //{
        //    bool exist = false;
        //    string MyConString = "DRIVER={MySQL ODBC 8.0 Driver(*.MDB)};" +
        //       "integrated security=SSPI; " +
        //       "persist security info=False; initial catalog=users";
        //       //@"Data sourse=..\users\userinfo.ibd";
        //    OdbcConnection MyConn = new OdbcConnection(MyConString);
        //    MyConn.Open();
        //    OdbcCommand mycm = new OdbcCommand("SELECT * FROM userinfo FROM users" +
        //        $"WHERE userName={username} AND passWord={password}");
        //    //检索数据
        //    OdbcDataReader msdr = mycm.ExecuteReader();
        //    while (msdr.Read())
        //    {
        //        if (msdr.HasRows)
        //        {
        //            Console.WriteLine(msdr.GetString(0));
        //            exist = true;
        //        }
        //    }
        //    msdr.Close();
        //    MyConn.Close();
        //    return exist;
        //}
        public bool isExist(string username, string password)
        {
            bool exit = false;
            try
            {
                string filename = @"..\..\data\users.txt";
                StreamReader sr = File.OpenText(filename);
                string s = sr.ReadLine();
                while (s != null)
                {
                    string[] strs = s.Split('#');
                    if ((strs[0] == username)&&(strs[1]==password))
                    {
                        exit= true;
                    }
                    s = sr.ReadLine();
                }
                sr.Close();
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return exit;
        }

        private void pbMinForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbMaxForm_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void pbCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void pbUsername_Click(object sender, EventArgs e)
        {

        }
    }
}
