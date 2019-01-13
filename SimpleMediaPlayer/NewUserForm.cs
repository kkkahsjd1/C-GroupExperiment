using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimpleMediaPlayer
{
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text;
            string password = this.txtPassWord.Text;
            string again = this.txtAgain.Text;
            if(password.Equals(again))
            {
                if(IsRepeat(name))
                {
                    MessageBox.Show("该用户已存在！");
                }
                else
                {
                    AddUser(name, password);
                    this.Hide();
                    LoginForm lgf = new LoginForm();
                    lgf.Show();
                }
            }
            else
            {
                MessageBox.Show("密码不匹配！");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lgf1 = new LoginForm();
            lgf1.Show();
        }

        //public bool Connect_Odbc_IsRepeat(string name)
        //{
        //    bool isRepeat = false;
        //    string MyConString = "DRIVER={MySQL ODBC 8.0 Driver};" +
        //        "integrated security=SSPI; " +
        //        "persist security info=False; initial catalog=users";
        //        //@"Data sourse=..\users\userinfo.ibd";
        //    OdbcConnection MyConn = new OdbcConnection(MyConString);
        //    //打开链接
        //    MyConn.Open();
        //    //定义OdbcCommand
        //    OdbcCommand mycm = new OdbcCommand("SELECT userName FROM userinfo FROM users");
        //    //调用ExecuteReader方法检索数据
        //    OdbcDataReader msdr = mycm.ExecuteReader();
        //    while(msdr.Read())
        //    {
        //        if(msdr.HasRows)
        //        {
        //            if(msdr.GetString(0)==name)
        //            {
        //                isRepeat = true;
        //            }
        //        }
        //    }
        //    msdr.Close();
        //    MyConn.Close();
        //    return isRepeat;

        //public void AddUser(string username,string password)
        //{
        //    string MyConString = "DRIVER={MySQL ODBC 8.0 Driver};" +
        //       "integrated security=SSPI; " +
        //       "persist security info=False; initial catalog=users";
        //       //@"Data sourse=..\users\userinfo.ibd";
        //    OdbcConnection MyConn = new OdbcConnection(MyConString);
        //    //打开链接
        //    MyConn.Open();
        //    //定义OdbcCommand
        //    OdbcCommand mycm = new OdbcCommand("INSERT INTO userinfo(userName,passWord)" +
        //       $"VALUES ({username},{password})");
        //    //添加行
        //    int cntInsert = mycm.ExecuteNonQuery();
        //    Console.WriteLine(cntInsert);
        //    //msdr.Close();
        //    MyConn.Close();
        //}


        public bool IsRepeat(string name)
        {
            bool repeat = false;
            //FileStream f = new FileStream(@"..\data\users.txt",);
            try
            {
                string filename = @"..\..\data\users.txt";
                StreamReader sr = File.OpenText(filename);
                string s = sr.ReadLine();
                while(s!=null)
                {
                    string[] strs=s.Split('#');
                    if(strs[0]==name)
                    {
                        repeat= true;
                    }
                    s = sr.ReadLine();
                }
                sr.Close();
            }catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return repeat;
        }

        public void AddUser(string username, string password)
        {
            try
            {
                string filename = @"..\..\data\users.txt";
                StreamWriter sw = File.AppendText(filename);
                string s = username + "#" + password+"#"+$@"..\..\data\usersSongs\{username}.txt";
                //转到下一行
                //sw.WriteLine();
                sw.WriteLine(s);
                sw.Close();
                FileStream fs = new FileStream($@"..\..\data\usersSongs\{username}.txt",
                    FileMode.Create, FileAccess.ReadWrite);
                fs.Close();
            }
            catch(IOException e)
            {
                Console.WriteLine(e.Message);
            }
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
    }
}
