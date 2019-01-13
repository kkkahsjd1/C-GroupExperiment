using AxWMPLib;
using SimpleMediaPlayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Collections;

namespace SimpleMediaPlayer
{
    #region new 初始化弹幕状态
    public enum MessageFlag { Unpressed, Pressed }//new
    public enum ImageFlag { Unpressed, Pressed }
    #endregion
    public partial class mainForm : Form
    {
        AxWindowsMediaPlayer testAWM = new AxWMPLib.AxWindowsMediaPlayer();
        SongsInfo currPlaySong = new SongsInfo(null);
        SongsInfo currSelectedSong = new SongsInfo(null);       //用于查看详情
        private ImageFlag imageflag = new ImageFlag();
        private string defaultSongsFilePath = @"C:\Users\Rhine\Music";
        private string localSongsFilePath = Application.StartupPath + "\\songListHistory.txt";
        private string favoriteSongsFilePath = Application.StartupPath + "\\favoriteSongsHistory.txt";
        #region new
        //private string albumSongFilePath = Application.StartupPath + "\\albumSongsHistory.txt";
        private string recommendSongFilePath = Application.StartupPath + "\\recommendSongsHistory.txt";
        #endregion
        private List<SongsInfo> oringinListSong;                //用于搜索功能
        private List<SongsInfo> localSongsList = new List<SongsInfo>();                 //本地音乐List
        private List<SongsInfo> favoriteSongsList = new List<SongsInfo>();              //收藏音乐List
        #region new
        private List<SongsInfo> AlbumList = new List<SongsInfo>();
        private List<SongsInfo> recommendSongsList = new List<SongsInfo>();//推荐音乐list
        
        
        #endregion
        //随机0，单曲循环1，列表循环2
        public enum PlayMode { Shuffle = 0, SingleLoop, ListLoop };
        public PlayMode currPlayMode = PlayMode.Shuffle;
        private int[] randomList;           //随机播放序列
        private int randomListIndex = 0;    //序列索引
        private int jumpSongIndex;          //手动选中需要在随机过程中跳过的歌曲

        private List<Label> lrcLabels = new List<Label>();  //歌词呈现地形式
        private string[] lrcList;

        private ThumbnailToolbarButton ttbbtnPlayPause;
        private ThumbnailToolbarButton ttbbtnPre;
        private ThumbnailToolbarButton ttbbtnNext;
        List<MenuItem> menuItemList;

        #region new 初始化弹幕状态
        static int Which_Message { get; set; }
        static int Height_Message { get; set; }
        static int Speed_Message { get; set; }
        static int Lines { get; set; }//弹幕行数
        static bool NewMessage { get; set; }
        static List<Label> MyLabels;
        #endregion

        public mainForm()
        {
            InitializeComponent();
            //检测MediaPlayer控件是否有安装
            //if (testAWM == null)
            //{
            //    throw new Exception();
            //}
            //else
            //{
            //    testAWM.Dispose();
            //}
            testAWM.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(AxWmp_PlayStateChange);

            MenuItem item1 = new MenuItem(Resources.list, "Local");
            MenuItem item2 = new MenuItem(Resources.favorite, "Favorite");
            MenuItem item3 = new MenuItem(Resources.album, "Album");
            MenuItem item4 = new MenuItem(Resources.musicLibrary, "Recommend");
            MenuItem item5 = new MenuItem(Resources.star, "Setting");
            this.menuItemList = new List<MenuItem>();
            menuItemList.Add(item1);
            menuItemList.Add(item2);
            menuItemList.Add(item3);
            menuItemList.Add(item4);
            menuItemList.Add(item5);

            


            lbMenu.Items.Add("List");
            lbMenu.Items.Add("Favorite");
            lbMenu.Items.Add("Album");
            lbMenu.Items.Add("Recommend");
            lbMenu.Items.Add("Setting");

            lrcLabels.Add(lrcLabel1);
            lrcLabels.Add(lrcLabel2);
            lrcLabels.Add(lrcLabel3);
            lrcLabels.Add(lrcLabel4);
            lrcLabels.Add(lrcLabel5);
            lrcLabels.Add(lrcLabel6);
            lrcLabels.Add(lrcLabel7);
            lrcLabels.Add(lrcLabel8);
            lrcLabels.Add(lrcLabel9);
            lrcLabels.Add(lrcLabel10);
            lrcLabels.Add(lrcLabel11);
            lrcLabels.Add(lrcLabel12);

            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 30);    //分别是宽和高
            lvSongList.SmallImageList = imgList;

            MyColorTable myColorTable = new MyColorTable();
            cmsSongListMenu.Renderer = new ToolStripProfessionalRenderer(myColorTable);
            cmsSongListMenu.ForeColor = Color.White;
            cmsSongListMenu.BackColor = Color.FromArgb(48, 47, 51);

            pbAddSong.Visible = false;

            #region new 初始化弹幕状态
            MyMessages.Flag = MessageFlag.Unpressed;
            SongsName.Import("myMessage.xml");
            Height_Message = 56;
            Speed_Message = 2;
            Lines = this._Message_panel.Size.Height / 4 * 3 / 30;
            MyLabels = new List<Label>();
            MyMessages.Flag = MessageFlag.Unpressed;
            this._Message.ReadOnly = true;
            imageflag = ImageFlag.Unpressed;
            #endregion
        }

        #region 窗体Load、Shown、Closed事件
        /*
         * 设置各种初始状态
         */
        private void Form1_Load(object sender, EventArgs e)
        {
            //设置文件打开窗口（添加音乐）可多选
            this.odlgFile.Multiselect = true;
            //重置播放器状态信息
            ReloadStatus();
            //读取播放器列表历史记录
            localSongsList = ReadHistorySongsList(localSongsFilePath);
            favoriteSongsList = ReadHistorySongsList(favoriteSongsFilePath);
            recommendSongsList = ReadHistorySongsList(recommendSongFilePath);
            //设置专辑图片控件到顶部页面（z-index)
            pbAlbumImage.BringToFront();
            //设置开机自启
            StarUp("0");
        }

        /*
         * 设置任务栏缩略图的属性与绑定事件 
         */
        private void Form1_Shown(object sender, EventArgs e)
        {
            //暂停按钮
            ttbbtnPlayPause = new ThumbnailToolbarButton(Properties.Resources.播放icon, "播放");
            ttbbtnPlayPause.Enabled = true;
            ttbbtnPlayPause.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(btnPlay_Click);

            //上一首按钮
            ttbbtnPre = new ThumbnailToolbarButton(Properties.Resources.上一首icon, "上一首");
            ttbbtnPre.Enabled = true;
            ttbbtnPre.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(btnBack_Click);

            //下一首按钮
            ttbbtnNext = new ThumbnailToolbarButton(Properties.Resources.下一首icon, "下一首");
            ttbbtnNext.Enabled = true;
            ttbbtnNext.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(btnNext_Click);
            TaskbarManager.Instance.ThumbnailToolbars.AddButtons(this.Handle, ttbbtnPre, ttbbtnPlayPause, ttbbtnNext);

            //裁剪显示略缩图
            //坐标值为多个父容器相对的位置坐标累加所得
            Point p = new Point(4, 558);
            TaskbarManager.Instance.TabbedThumbnail.SetThumbnailClip(this.Handle, new Rectangle(p, pbSmallAlbum.Size));
        }

        /*
         * 窗体关闭事件 
         */
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }
        #endregion

        #region 公共模块

        #region 播放核心
        /*
         * 播放指定的歌曲
         * index：播放列表中，歌曲的序号
         */
        private void Play(int index)
        {
            //设置被播放音乐项的状态
            lvSongList.Items[index].Focused = true;
            lvSongList.Items[index].EnsureVisible();
            lvSongList.Items[index].Selected = true;

            //Console.WriteLine(lvSongList.Items[index].SubItems[6].Text);
            if (AxWmp.playState.ToString() == "wmppsPlaying")       //播放->其他状态
            {
                AxWmp.Ctlcontrols.pause();
                pbPlay.BackgroundImage = Resources.播放hoover;
                ttbbtnPlayPause.Icon = Resources.播放icon;
               
                return;
            } else if (AxWmp.playState.ToString() != "wmppsPaused")      //更改播放路径并播放
              {
                if (MyMessages.Flag == MessageFlag.Pressed)
                {
                    for (int i = this._Message_panel.Controls.Count - 1; i >= 0; i--)
                    {
                        if (this._Message_panel.Controls[i] is Label)
                        {
                            this._Message_panel.Controls[i].Dispose();
                        }
                        New_Message.Dispose();
                        Which_Message = 0;
                        New_Message.Start();
                    }
                    
                       
                }
               
                //生成随机序列
                BuildRandomList(lvSongList.Items.Count);
                jumpSongIndex = index;
                currPlaySong = new SongsInfo(lvSongList.SelectedItems[0].SubItems[6].Text);
                AxWmp.URL = currPlaySong.FilePath;
                AxWmp.Ctlcontrols.play();
                lrcLabels.ForEach(a => a.Text = "");
                if(LrcPanel.Visible == true) {
                    lrcList = LRC_Lyric("../../../lyric/" + AxWmp.URL.Substring(AxWmp.URL.LastIndexOf(Convert.ToChar("\\")) + 1, AxWmp.URL.LastIndexOf(Convert.ToChar(".")) - AxWmp.URL.LastIndexOf(Convert.ToChar("\\"))) + "lrc");
                    if (lrcList == null)
                    {
                        lrcLabel6.Text = "No lyric file";
                    }
                }
                return;
            } else                            //暂停->播放
                AxWmp.Ctlcontrols.play();

            pbPlay.BackgroundImage = Resources.暂停hoover;
            ttbbtnPlayPause.Icon = Resources.暂停icon;
            ttbbtnPlayPause.Tooltip = "暂停";
        }

        /*
         * 计时器函数
         */

        int currentLine = 0;
        private void timerPlay_Tick(object sender, EventArgs e)
        {
            //歌曲总时间 
            double total = AxWmp.currentMedia.duration;
            //歌曲当前时间
            double pos = AxWmp.Ctlcontrols.currentPosition;

            //设置当前播放时间
            lbcurrTime.Text = AxWmp.Ctlcontrols.currentPositionString;
            lbEndTime.Text = currPlaySong.Duration.Remove(0, 3);

            //设置滑动条值
            tkbMove.Value = (int)AxWmp.Ctlcontrols.currentPosition;

            if(this.LrcPanel.Visible == true && lrcList != null) {
                //若进度条移动，计算 curLyricLine
                for (int i = 0; i < lrcList.Count(); i++) {
                    if (pos >= Double.Parse(lrcList[i].Substring(0, lrcList[i].LastIndexOf(Convert.ToChar("|"))))) {
                        currentLine = i;
                    } else if (pos == 0) {
                        currentLine = 0;
                    }
                }
                if (currentLine < lrcList.Count() && lrcList.Count() > 0) {
                    for (int i = 0; i < 12; i++) {
                        if (currentLine + i - 5 < 0 || currentLine + i - 5 > lrcList.Count() - 1) {
                            lrcLabels[i].Text = "";
                        } else {
                            lrcLabels[i].Text = lrcList[currentLine + i - 5].Substring(lrcList[currentLine + i - 5].LastIndexOf(Convert.ToChar("|")) + 1);
                        }
                    }
                }

                //将动态显示的歌词label居中显示
                for (int i = 0; i < 12; i++) {
                    lrcLabels[i].Location = new Point(this.LrcPanel.Width / 2 - lrcLabels[i].Width / 2, lrcLabels[i].Location.Y);
                }
            }
           

        }

        /*
         * 播放器控件状态改变事件
         */
        private void AxWmp_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            switch (e.newState) {
                case 0:    // Stopped 未知状态
                    break;

                case 1:    // Stopped 停止
                    timerPlay.Stop();
                    ReloadStatus();
                    break;

                case 2:    // Paused 暂停
                    timerPlay.Stop();
                    break;

                case 3:    // Playing 正在播放
                    timerPlay.Start();
                    //显示专辑图片
                    pbAlbumImage.Image = currPlaySong.AlbumImage;
                    pbSmallAlbum.BackgroundImage = currPlaySong.SmallAblum;
                    lbSmallAlbumSingerName.Text = currPlaySong.Artist;

                    //显示歌曲标题名字
                    lbMusicName.Text = currPlaySong.FileName;
                    if (currPlaySong.FileName.Length > 18)
                        lbSmallAlbumSongName.Text = currPlaySong.FileName.Substring(0, 18) + "...";
                    else
                        lbSmallAlbumSongName.Text = currPlaySong.FileName;

                    tkbMove.Maximum = (int)AxWmp.currentMedia.duration;

                    int currIndex = lvSongList.SelectedItems[0].Index;
                    lvSongList.SelectedItems.Clear();
                    lvSongList.Items[currIndex].Selected = true;    //设定选中            
                    lvSongList.Items[currIndex].EnsureVisible();    //保证可见
                    lvSongList.Items[currIndex].Focused = true;
                    lvSongList.Select();
                    break;

                case 4:    // ScanForward
                    //tsslCurrentPlayState.Text = "ScanForward";
                    break;

                case 5:    // ScanReverse
                    //tsslCurrentPlayState.Text = "ScanReverse";
                    break;
                case 6:    // Buffering
                    //tsslCurrentPlayState.Text = "正在缓冲";
                    break;

                case 7:    // Waiting
                    //tsslCurrentPlayState.Text = "Waiting";
                    break;

                case 8:    // MediaEnded
                    //tsslCurrentPlayState.Text = "MediaEnded";
                    break;

                case 9:    // Transitioning
                    //tsslCurrentPlayState.Text = "正在连接";
                    break;

                case 10:   // Ready
                    //tsslCurrentPlayState.Text = "准备就绪";
                    break;

                case 11:   // Reconnecting
                    //tsslCurrentPlayState.Text = "Reconnecting";
                    break;

                case 12:   // Last
                    //tsslCurrentPlayState.Text = "Last";
                    break;
                default:
                    //tsslCurrentPlayState.Text = ("Unknown State: " + e.newState.ToString());
                    break;
            }

            if (AxWmp.playState.ToString() == "wmppsMediaEnded") {
                //获取音乐播放文件路径，并添加到播放控件
                string path = GetPath();
                WMPLib.IWMPMedia media = AxWmp.newMedia(path);
                AxWmp.currentPlaylist.appendItem(media);
            }
        }

        /*
         * 重置播放器状态信息
         */
        private void ReloadStatus()
        {
            //设置专辑封面为默认
            pbAlbumImage.Image = Properties.Resources.defaultAlbum;
            lbMusicName.Text = "SimpleMediaPlayer";
            lbcurrTime.Text = "00:00";
            lbEndTime.Text = "00:00";
            tkbVol.Value = tkbVol.Maximum / 2;
            tkbMove.Value = 0;
            if (lvSongList.Items.Count > 0 && lvSongList.SelectedItems.Count == 0) {
                lvSongList.Items[0].Selected = true;//设定选中            
                lvSongList.Items[0].EnsureVisible();//保证可见
                lvSongList.Items[0].Focused = true;
            }
            lrcList = null;
        }
        #endregion

        #region 播放模式

        private string GetPath()
        {
            int currIndex = lvSongList.SelectedItems[0].Index;
            switch (currPlayMode) {
                case PlayMode.ListLoop:
                    if (currIndex != lvSongList.Items.Count - 1)
                        currIndex += 1;
                    else
                        currIndex = 0;

                    break;
                case PlayMode.SingleLoop:
                    Console.WriteLine("SingleLoop");
                    //do nothing
                    break;
                case PlayMode.Shuffle:
                    //当局结束
                    if (randomListIndex > randomList.Length - 1)
                        StarNewRound();

                    //匹配到需要跳过的歌曲
                    if (randomList[randomListIndex] == jumpSongIndex)
                        if (randomListIndex == randomList.Length - 1)   //当局结束
                            StarNewRound();
                        else
                            randomListIndex++;

                    currIndex = randomList[randomListIndex++];

                    break;
            }

            lvSongList.Items[currIndex].Selected = true;//设定选中            
            lvSongList.Items[currIndex].EnsureVisible();//保证可见
            lvSongList.Items[currIndex].Focused = true;
            currPlaySong = new SongsInfo(lvSongList.SelectedItems[0].SubItems[6].Text);

            return currPlaySong.FilePath;
        }

        private void StarNewRound()
        {
            //重新生成随机序列
            BuildRandomList(lvSongList.Items.Count);
            //第二轮开始 播放所有歌曲 不跳过
            jumpSongIndex = -1;
        }

        private void BuildRandomList(int songListCount)
        {
            randomListIndex = 0;
            randomList = new int[songListCount];

            //初始化序列
            for (int i = 0; i < songListCount; i++) {
                randomList[i] = i;
            }

            //随机序列
            for (int i = songListCount - 1; i >= 0; i--) {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int j = r.Next(0, songListCount - 1);
                swap(randomList, i, j);
            }

        }

        private void swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
        #endregion

        #region 开机自启
        private void StarUp(string flag)
        {
            string path = Application.StartupPath;
            string keyName = path.Substring(path.LastIndexOf("//") + 1);
            Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (flag.Equals("1")) {
                if (Rkey == null) {
                    Rkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                }
                Rkey.SetValue(keyName, path + @"\SimpleMediaPlayer.exe");
            } else {
                if (Rkey != null) {
                    Rkey.DeleteValue(keyName, false);
                }
            }
        }
        #endregion

        #region 系统托盘
        /*
         * 窗体大小改变事件
         */
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) {
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        /*
         * 系统托盘双击事件
         */
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        /*
         * 系统托盘菜单——打开
         */
        private void tsmiOpenForm_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false;
            this.WindowState = FormWindowState.Normal;
        }

        /*
         * 系统托盘菜单——退出
         */
        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();
            this.Close();
        }
        #endregion

        #region 读写播放列表历史记录
        /*
         * 保存历史记录到本地文件
         */
        private void SaveSongsListHistory(string savePath, List<SongsInfo> songsList)
        {
            string saveString = "";
            for (int i = 0; i < songsList.Count; i++) {
                saveString += songsList[i].FilePath + "};{";
            }
            File.WriteAllText(savePath, saveString);
        }

        /*
         * 读取历史记录到本地文件
         */
        private List<SongsInfo> ReadHistorySongsList(string filePath)
        {
            List<SongsInfo> resSongList = new List<SongsInfo>();
            string readString = "";
            if (File.Exists(filePath)) {
                readString = File.ReadAllText(filePath);
                if (readString != "") {
                    string[] arr = readString.Split(new string[] { "};{" }, StringSplitOptions.None);
                    foreach (string path in arr) {
                        if (path != null && path != "" && File.Exists(path)) {
                            resSongList.Add(new SongsInfo(path));
                        }
                    }
                }
            } else
                File.Create(filePath);

            return resSongList;
        }
        #endregion

        #region 按钮交互体验
        /*
         * 鼠标移入事件集合
         *     鼠标移入改变按钮图标
         */
        private void MoveEnter_ChangeIconToHooverStyle(object sender, EventArgs e)
        {
            PictureBox currPicBox = (PictureBox)sender;
            if (currPicBox.Name == "pbPlay") {
                if (AxWmp.playState.ToString() == "wmppsPlaying")       //播放->其他状态
                {
                    pbPlay.BackgroundImage = Resources.暂停hoover;
                } else {
                    pbPlay.BackgroundImage = Resources.播放hoover;
                }
            } else if (currPicBox.Name == "pbBack") {
                pbBack.BackgroundImage = Resources.上一首hoover;
            } else if (currPicBox.Name == "pbNext") {
                pbNext.BackgroundImage = Resources.下一首hoover;
            } else if (currPicBox.Name == "pbCloseForm") {
                currPicBox.Image = Resources.关闭hoover;
            } else if (currPicBox.Name == "pbMaxForm") {
                currPicBox.Image = Resources.最大化hoover;
            } else if (currPicBox.Name == "pbMinForm") {
                currPicBox.Image = Resources.最小化hoover;
            } else if (currPicBox.Name == "pbSmallAlbum") {
                currPicBox.Image = Resources.展开;
            } else if (currPicBox.Name == "pbAddSong") {
                currPicBox.Image = Resources.添加hoover;
            }
            else if (currPicBox.Name == "MessageOnBoard")
            {
                if (MyMessages.Flag == MessageFlag.Unpressed)
                {
                    MessageOnBoard.Image = Resources.DMhoover;
                }


            }
        }

        /*
         * 鼠标移出事件集合
         *     鼠标移入改变按钮图标
         */
        private void MoveLeave_ChangeIconToNormalStyle(object sender, EventArgs e)
        {
            PictureBox currPicBox = (PictureBox)sender;
            if (currPicBox.Name == "pbPlay") {
                if (AxWmp.playState.ToString() == "wmppsPlaying")       //播放->其他状态
                {
                    pbPlay.BackgroundImage = Resources.暂停;
                } else {
                    pbPlay.BackgroundImage = Resources.播放;
                }
            } else if (currPicBox.Name == "pbBack") {
                pbBack.BackgroundImage = Resources.上一首;
            } else if (currPicBox.Name == "pbNext") {
                pbNext.BackgroundImage = Resources.下一首;
            } else if (currPicBox.Name == "pbCloseForm") {
                currPicBox.Image = Resources.关闭;
            } else if (currPicBox.Name == "pbMaxForm") {
                currPicBox.Image = Resources.最大化;
            } else if (currPicBox.Name == "pbMinForm") {
                currPicBox.Image = Resources.最小化;
            } else if (currPicBox.Name == "pbSmallAlbum") {
                currPicBox.Image = null;
            } else if (currPicBox.Name == "pbAddSong") {
                currPicBox.Image = Resources.添加音乐;
            }
            else if (currPicBox.Name == "MessageOnBoard")
            {
                if (MyMessages.Flag == MessageFlag.Unpressed)
                {
                    MessageOnBoard.Image = Resources.DMUnpressed1;
                }
                
                   
            }

        }
        #endregion

        #region  获取MP3的歌词
        /// <summary>
        /// 获取MP3的歌词
        /// </summary>
        /// <param FileName="string">歌词路径</param>
        public string[] LRC_Lyric(string FileName)
        {
            try {
                ArrayList ArrLyric = new ArrayList();//声明一个使用大小可动态增加的数组对象
                Console.WriteLine(FileName);
                FileStream fs = new FileStream(@FileName, FileMode.Open, FileAccess.Read, FileShare.None);//定义一个公开以文件为主的 Stream，既支持同步读写操作，也支持异步读写操作的对象。
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);//声明一个读取数据的对象
                sr.BaseStream.Seek(0, SeekOrigin.Begin);//从指定的起始点开始读取数据
                string Tem_strLine = sr.ReadLine();//读取当前流中的一行数据
                string Tem_Str = "";//定义一个string型临时变量Tem_Str
                string sp = "";//定义一个string型临时变量sp
                int Tem_p = 0;//记录当前[的位置
                int Tem_q = 0;//记录当前]的位置
                string Tem_Time = "";//记录播放时间
                string Tem_Slyrec = "";//记录歌词
                bool Tem_bool = true;//循环条件

                while (Tem_strLine != null) //只要当前流中还存在数据就继续循环
                {
                    Tem_bool = true; //设定Tem_bool的值为true
                    Tem_Str = Tem_strLine;//为变量Tem_Str赋值
                    sp = Tem_strLine;//为变量sp赋值
                    if (sp.IndexOf(Convert.ToChar("[")) == -1 || sp.Trim() == "")//当"["字符在此字符串中的第一个匹配项的索引为-1或当从当前 String 对象的开始和末尾移除所有空白字符后保留的字符串为空值时
                    {
                        Tem_strLine = sr.ReadLine();//继续从流中读取数据
                        continue;//跳出此层循环
                    }

                    sp = sp.Substring(sp.IndexOf(Convert.ToChar("[")) + 1, 1);//从此实例检索子字符串。子字符串从指定的字符位置开始且具有指定的长度
                    Tem_Slyrec = Tem_Str.Substring(Tem_Str.LastIndexOf(Convert.ToChar("]")) + 1, Tem_Str.Length - (Tem_Str.LastIndexOf(Convert.ToChar("]")) + 1));//截取歌词中“[”和“]”后的内容
                    if (EstimateFig(sp))//当字符串中存在数字时
                    {
                        while (Tem_bool)//只要Tem_bool为真就不断的循环
                        {
                            Tem_p = Tem_Str.IndexOf(Convert.ToChar("["));//获取字符“[”的索引
                            Tem_q = Tem_Str.IndexOf(Convert.ToChar("]"));//获取字符“]”的索引
                            Tem_Time = Tem_Str.Substring(Tem_p + 1, Tem_q - Tem_p - 1);//获取当前行的播放时间
                            ArrLyric.Add(Tem_Time + "|" + Tem_Slyrec); //在播放时间和歌词之间添加“|”
                            if (Tem_q != Tem_Str.LastIndexOf(Convert.ToChar("]"))) //当没有循环到字符串的结尾时
                                Tem_Str = Tem_Str.Substring(Tem_q + 1, Tem_Str.Length - Tem_q - 1);//保存字符串的值
                            else                  //当循环到结尾时
                                Tem_bool = false;//设置Tem_bool的值为false
                        }
                    }
                    Tem_strLine = sr.ReadLine();//从当前流中读取一行数据
                }
                sr.Dispose(); //释放由sr对象使用的所有资源
                fs.Dispose(); //释放由fs对象使用的所有资源

                string Tem_taxisTime = "0";//记当截取后的时间字符串
                string[] ArrayStr = new string[ArrLyric.Count];//定义一个string型的数组ArrayStr
                for (int i = ArrayStr.Length - 1; i >= 0; i--)//循环遍历数组ArrayStr中的每一个元素
                {
                    Tem_Str = ArrLyric[i].ToString();//记录数组ArrayStr中的元素
                                                     //Tem_Str.Substring(0, Tem_Str.LastIndexOf(Convert.ToChar("|")))截取歌词的播放时间
                    Tem_taxisTime = (Double.Parse(Tem_taxisTime) + Double.Parse(BuildMillisecond(Tem_Str.Substring(0, Tem_Str.LastIndexOf(Convert.ToChar("|")))))).ToString();//保存歌词的播放时间总数
                    if (Tem_Str.Substring(Tem_Str.LastIndexOf(Convert.ToChar("|"))) == "") {
                        i--;
                        continue;
                    }
                    ArrayStr[i] = Tem_taxisTime + "|" + Tem_Str.Substring(Tem_Str.LastIndexOf(Convert.ToChar("|")) + 1, Tem_Str.Length - Tem_Str.LastIndexOf(Convert.ToChar("|")) - 1);//将时间和歌词内容赋值给字符数组ArrayStr
                    Tem_taxisTime = "0";
                }

                return ArrayStr;//返回数组ArrayStr
            }catch(Exception ex) {
                return null;
            }
            }
            
        #endregion

        #region  在字符串中截取数字
        /// <summary>
        /// 在字符串中截取数字
        /// </summary>
        /// <param Istr="string">包含数字的字符串</param>
        public bool EstimateFig(string Istr)
        {
            string Tem_Sint = "";//定义一个string型变量Tem_Sint
            bool Tem_Bool = false;//定一个Tem_Bool型变量Tem_Bool
            CharEnumerator Tem_CharEnum = Istr.GetEnumerator();//定义一个支持循环访问 String 对象并读取它的各个字符的对象
            while (Tem_CharEnum.MoveNext())//循环访问字符串中的每一个字符
            {
                byte[] Tem_byte = new byte[1];//定义一个字节型数组
                Tem_byte = System.Text.Encoding.ASCII.GetBytes(Tem_CharEnum.Current.ToString());//保存当前字符串的编码类型
                int Tem_ASCII_Code = (short)(Tem_byte[0]);//将字节数组中的字符转化为int型的
                if (Tem_ASCII_Code >= 48 && Tem_ASCII_Code <= 57)//当该字符为数字时
                    Tem_Sint += Tem_CharEnum.Current.ToString();//保存该字符的值
            }
            if (Tem_Sint != "")//当变量Tem_Sint的值不为空时
                Tem_Bool = true;//设定Tem_Bool的值为true
            return Tem_Bool;//返回Tem_Bool的值
        }
        #endregion

        #region  计算时间的毫秒数
        /// <summary>
        /// 计算时间的毫秒数
        /// </summary>
        /// <param Istr = "string" > 时间 </ param >
        public string BuildMillisecond(string Istr)
        {
            string Tem_Value = ""; //定义一个string型的变量Tem_Value
            double Tem_Cent = 0;//定义一个int型的变量Tem_Cent
            int Tem_Sec = 0;//定义一个int型的变量Tem_Sec
            int Tem_Mill = 0;//定义一个int型的变量Tem_Mill
            Tem_Cent = Convert.ToInt32(Istr.Substring(0, Istr.IndexOf(Convert.ToChar(":"))));//获取歌词播放多少分钟
            Tem_Sec = Convert.ToInt32(Istr.Substring(Istr.IndexOf(Convert.ToChar(":")) + 1, Istr.IndexOf(Convert.ToChar(".")) - Istr.IndexOf(Convert.ToChar(":")) - 1));//获取歌词播放的秒数
            Tem_Mill = Convert.ToInt32(Istr.Substring(Istr.IndexOf(Convert.ToChar(".")) + 1, Istr.Length - Istr.IndexOf(Convert.ToChar(".")) - 1));//获取歌词的播放毫秒数
            Tem_Cent = (Tem_Cent * 60000 + Tem_Sec * 1000 + Tem_Mill) / 1000;//计算歌词播放的总时间数
            Tem_Value = Tem_Cent.ToString();//保存歌词的播放时间
            return Tem_Value;//返回歌词的播放时间
        }
        #endregion

        #endregion

        #region 窗体顶部
        /*
         * 拖动窗口，标题文字拖动变色（默认gray、拖动white）
         */
        Point downPoint;
        private void lbTitle_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
            lbTitle.ForeColor = Color.White;
        }
        private void lbTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                this.Location = new Point(this.Location.X + e.X - downPoint.X, this.Location.Y + e.Y - downPoint.Y);
            }
        }
        private void lbTitle_MouseUp(object sender, MouseEventArgs e)
        {
            lbTitle.ForeColor = Color.Gray;
        }

        /*
         * 关闭、最大化、最小化按钮点击事件
         */
        private void FormControlButton_Click(object sender, EventArgs e)
        {
            PictureBox currPicBox = (PictureBox)sender;
            if (currPicBox.Name == "pbCloseForm") {
                this.Close();
            } else if (currPicBox.Name == "pbMaxForm") {
                this.WindowState = FormWindowState.Maximized;
            } else if (currPicBox.Name == "pbMinForm") {
                this.WindowState = FormWindowState.Minimized;
            }
        }
        #endregion

        #region 窗体左部菜单
        /*
         * 绘制单元项
         */
        private void lbMenu_DrawItem(object sender, DrawItemEventArgs e)
        {
            Bitmap bitmap = new Bitmap(e.Bounds.Width, e.Bounds.Height);

            int index = e.Index;                                //获取当前要进行绘制的行的序号，从0开始。
            Graphics g = e.Graphics;                            //获取Graphics对象。

            Graphics tempG = Graphics.FromImage(bitmap);

            tempG.SmoothingMode = SmoothingMode.AntiAlias;          //使绘图质量最高，即消除锯齿
            tempG.InterpolationMode = InterpolationMode.HighQualityBicubic;
            tempG.CompositingQuality = CompositingQuality.HighQuality;

            Rectangle bound = e.Bounds;                         //获取当前要绘制的行的一个矩形范围。
            string text = this.menuItemList[index].Text.ToString();     //获取当前要绘制的行的显示文本。

            //绘制选中时的背景，要注意绘制的顺序，后面的会覆盖前面的
            //绘制底色
            Color backgroundColor = Color.FromArgb(34, 35, 39);             //背景色
            Color guideTagColor = Color.FromArgb(183, 218, 114);            //高亮指示色
            Color selectedBackgroundColor = Color.FromArgb(46, 47, 51);     //选中背景色
            Color fontColor = Color.Gray;                                   //字体颜色
            Color selectedFontColor = Color.White;                          //选中字体颜色
            Font textFont = new Font("微软雅黑", 9, FontStyle.Bold);        //文字
            //图标
            Image itmeImage = this.menuItemList[index].Img;

            //矩形大小
            Rectangle backgroundRect = new Rectangle(0, 0, bound.Width, bound.Height);
            Rectangle guideRect = new Rectangle(0, 4, 5, bound.Height - 8);
            Rectangle textRect = new Rectangle(55, 0, bound.Width, bound.Height);
            Rectangle imgRect = new Rectangle(20, 4, 22, bound.Height - 8);

            //当前选中行
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {
                backgroundColor = selectedBackgroundColor;
                fontColor = selectedFontColor;
            } else {
                guideTagColor = backgroundColor;
            }
            //绘制背景色
            tempG.FillRectangle(new SolidBrush(backgroundColor), backgroundRect);
            //绘制左前高亮指示
            tempG.FillRectangle(new SolidBrush(guideTagColor), guideRect);
            //绘制显示文本
            TextRenderer.DrawText(tempG, text, textFont, textRect, fontColor,
                                  TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            //绘制图标
            tempG.DrawImage(itmeImage, imgRect);

            g.DrawImage(bitmap, bound.X, bound.Y, bitmap.Width, bitmap.Height);
            tempG.Dispose();
        }

        /*
         * 设置行高
         */
        private void lbMenu_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 30;
        }

        /*
         * 菜单按钮选中事件
         */
        private void lbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lbMenu.SelectedIndex) {
                case 0:     //选中的是本地列表
                    lvSongList.Items.Clear();
                    AddSongsToListView(localSongsList);
                    tsmiRemoveSongFromList.Visible = true;
                    lvSongList.BringToFront();
                    lvSongList.Visible = true;
                    tsmiFavorite.Visible = true;
                    pbAddSong.Visible = true;
                    lbMusicName.Visible = false;
                    lbCategory.Text = "本地音乐";
                    lbCategory.Visible = true;
                    lbSongCount.Visible = true;
                    imageflag = ImageFlag.Unpressed;

                    break;
                case 1:     //选中的是收藏音乐
                    lvSongList.Items.Clear();
                    AddSongsToListView(favoriteSongsList);
                    tsmiRemoveSongFromList.Visible = true;
                    lvSongList.BringToFront();
                    lvSongList.Visible = true;
                    tsmiFavorite.Visible = false;
                    pbAddSong.Visible = false;
                    lbMusicName.Visible = false;
                    lbCategory.Text = "收藏音乐";
                    lbCategory.Visible = true;
                    lbSongCount.Visible = true;
                    imageflag = ImageFlag.Unpressed;
                    break;

                case 2:     //选中的是音乐分类
                    lvSongList.Items.Clear();
                    AddSongsToListView(AlbumList);
                    tsmiRemoveSongFromList.Visible = false;
                    lvSongList.BringToFront();
                    lvSongList.Visible = true;
                    tsmiFavorite.Visible = true;
                    pbAddSong.Visible = false;
                    lbMusicName.Visible = false;
                    lbCategory.Text = "音乐分类";
                    lbCategory.Visible = true;
                    lbSongCount.Visible = true;
                    imageflag = ImageFlag.Unpressed;
                    break;

                case 3://
                    lvSongList.Items.Clear();
                    AddSongsToListView(recommendSongsList);
                    tsmiRemoveSongFromList.Visible = false;
                    lvSongList.BringToFront();
                    lvSongList.Visible = true;
                    tsmiFavorite.Visible = true;
                    pbAddSong.Visible = false;
                    lbMusicName.Visible = false;
                    lbCategory.Text = "推荐音乐";
                    lbCategory.Visible = true;
                    lbSongCount.Visible = true;
                    imageflag = ImageFlag.Unpressed;
                    break;
            }

            int songsCount = lvSongList.Items.Count;
            lbSongCount.Text = songsCount + "首音乐";
        }


        /*
         * 专辑小图标点击事件
         */
        private void pbSmallAlbum_Click(object sender, EventArgs e)
        {
            if (imageflag == ImageFlag.Unpressed)
            {
                pbAlbumImage.BringToFront();
                pbAlbumImage.Visible = true;
                pbAddSong.Visible = false;
                lbSongCount.Visible = false;
                lbCategory.Visible = false;
                lbMusicName.Visible = true;
                LrcPanel.Visible = false;
                imageflag = ImageFlag.Pressed;
            }
            else if (imageflag == ImageFlag.Pressed)
            {
                pbAlbumImage.SendToBack();
                pbAlbumImage.Visible = false;
                pbAddSong.Visible = false;
                lbSongCount.Visible = false;
                lbCategory.Visible = false;
                lbMusicName.Visible = true;
                LrcPanel.Visible = false;
                imageflag = ImageFlag.Unpressed;
            }
            //pbAlbumImage.BringToFront();
            //pbAlbumImage.Visible = true;
            //pbAddSong.Visible = false;
            //lbSongCount.Visible = false;
            //lbCategory.Visible = false;
            
            //lbMusicName.Visible = false;
            //Console.WriteLine("it's me");
        }
        #endregion

        #region 窗体右部
        #region 播放列表
        /*
         * 播放列表重绘
         */
        private void lvSongList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            int index = e.ColumnIndex;

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(27, 27, 25)), e.Bounds);
            TextRenderer.DrawText(e.Graphics, lvSongList.Columns[index].Text, new Font("微软雅黑", 9, FontStyle.Regular), e.Bounds, Color.Gray, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

            Pen pen = new Pen(Color.FromArgb(34, 35, 39), 2);
            Point p = new Point(e.Bounds.Left - 1, e.Bounds.Top + 1);
            Size s = new Size(e.Bounds.Width, e.Bounds.Height - 2);
            Rectangle r = new Rectangle(p, s);
            e.Graphics.DrawRectangle(pen, r);
        }
        private void lvSongList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Console.WriteLine("selectedCount:" + lvSongList.SelectedItems.Count);
            if (e.ItemIndex == -1) {
                return;
            }

            if (e.ItemIndex % 2 == 0) {
                e.SubItem.BackColor = Color.FromArgb(27, 29, 32);
                e.DrawBackground();
            }

            if (e.ColumnIndex == 1) {
                e.SubItem.ForeColor = Color.White;
            } else {
                e.SubItem.ForeColor = Color.Gray;
            }

            if ((e.ItemState & ListViewItemStates.Selected) == ListViewItemStates.Selected) {
                using (SolidBrush brush = new SolidBrush(Color.Blue)) {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(46, 47, 51)), e.Bounds);
                }
            }

            if (!string.IsNullOrEmpty(e.SubItem.Text)) {
                this.DrawText(e, e.Graphics, e.Bounds, 2);
            }
        }
        private void DrawText(DrawListViewSubItemEventArgs e, Graphics g, Rectangle r, int paddingLeft)
        {
            TextFormatFlags flags = GetFormatFlags(e.Header.TextAlign);

            r.X += 1 + paddingLeft;//重绘图标时，文本右移
            TextRenderer.DrawText(
                g,
                e.SubItem.Text,
                e.SubItem.Font,
                r,
                e.SubItem.ForeColor,
                flags);
        }
        private TextFormatFlags GetFormatFlags(HorizontalAlignment align)
        {
            TextFormatFlags flags =
                    TextFormatFlags.EndEllipsis |
                    TextFormatFlags.VerticalCenter;

            switch (align) {
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.Right;
                    break;
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.Left;
                    break;
            }

            return flags;
        }

        /*
         * 播放列表双击事件
         */
        private void lvSongList_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine(lvSongList.SelectedItems[0].Index);

            int currIndex = lvSongList.SelectedItems[0].Index;
            string songFilePath = lvSongList.Items[currIndex].SubItems[6].Text;
            if (currPlaySong.FilePath == songFilePath) {
                //选中歌曲为正在播放的歌曲
                if (AxWmp.playState.ToString() == "wmppsPlaying") {
                    AxWmp.Ctlcontrols.pause();
                    pbPlay.BackgroundImage = Resources.播放;
                    ttbbtnPlayPause.Icon = Resources.播放icon;
                } else if (AxWmp.playState.ToString() == "wmppsPaused") {
                    AxWmp.Ctlcontrols.play();
                    pbPlay.BackgroundImage = Resources.暂停;
                    ttbbtnPlayPause.Icon = Resources.暂停icon;
                }
            } else {
                //选中的为其他歌曲
                BuildRandomList(lvSongList.Items.Count);
                jumpSongIndex = currIndex;
                currPlaySong = new SongsInfo(songFilePath);
                AxWmp.URL = songFilePath;
                AxWmp.Ctlcontrols.play();
                pbPlay.BackgroundImage = Resources.暂停;
                ttbbtnPlayPause.Icon = Resources.暂停icon;
            }
            lvSongList.Items[currIndex].Focused = true;
        }

        private void lvSongList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                ListViewItem lvi = lvSongList.GetItemAt(e.X, e.Y);
                if (lvi != null) {
                    cmsSongListMenu.Visible = true;
                    currSelectedSong = new SongsInfo(lvi.SubItems[6].Text);
                    cmsSongListMenu.Show(Cursor.Position);
                } else
                    cmsSongListMenu.Close();
            }
        }

        #region 播放列表右键菜单(收藏音乐、删除音乐、打开文件位置)
        /*
         * 菜单——收藏音乐
         */
        private void tsmiFavorite_Click(object sender, EventArgs e)
        {
            foreach (SongsInfo song in favoriteSongsList) {
                if (currSelectedSong.FilePath == song.FilePath)
                    return;
            }

            favoriteSongsList.Add(new SongsInfo(currSelectedSong.FilePath));
            SaveSongsListHistory(favoriteSongsFilePath, favoriteSongsList);
        }

        /*
         * 菜单——删除音乐
         */
        private void tsmiRemoveSongFromList_Click(object sender, EventArgs e)
        {
            DeleteSongFormList deleteSongFormList = new DeleteSongFormList(currSelectedSong.FilePath);
            if (deleteSongFormList.ShowDialog() == DialogResult.OK) {
                int removeIndex = lvSongList.SelectedItems[0].Index;
                if (lbMenu.SelectedIndex == 0) {
                    localSongsList.RemoveAt(removeIndex);
                    SaveSongsListHistory(localSongsFilePath, localSongsList);
                    AddSongsToListView(localSongsList);
                } else if (lbMenu.SelectedIndex == 1) {
                    favoriteSongsList.RemoveAt(removeIndex);
                    SaveSongsListHistory(favoriteSongsFilePath, favoriteSongsList);
                    AddSongsToListView(favoriteSongsList);
                }

                UpdataOringinSongList();
            }
            int songsCount = lvSongList.Items.Count;
            lbSongCount.Text = songsCount + "首音乐";
        }

        /*
         * 菜单——打开文件位置
         */
        private void tsmiOpenFilePath_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Explorer.exe", "/select,\"" + currSelectedSong.FilePath + "\"");
        }
        #endregion
        #endregion

        #region 添加音乐
        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            this.odlgFile.InitialDirectory = defaultSongsFilePath;
            this.odlgFile.Filter = "媒体文件|*.mp3;*.wav;*.wma;*.avi;*.mpg;*.asf;*.wmv";
            if (odlgFile.ShowDialog() == DialogResult.OK) {
                for (int i = 0; i < odlgFile.FileNames.Length; i++) {
                    string path = odlgFile.FileNames[i];
                    if (!IsExistInList(path))
                        localSongsList.Add(new SongsInfo(path));
                }
            }

            AddSongsToListView(localSongsList);
            SaveSongsListHistory(localSongsFilePath, localSongsList);

            UpdataOringinSongList();
            int songsCount = lvSongList.Items.Count;
            lbSongCount.Text = songsCount + "首音乐";
        }

        private bool IsExistInList(string path)
        {
            for (int i = 0; i < localSongsList.Count; i++) {
                if (path == localSongsList[i].FilePath)
                    return true;
            }
            return false;
        }

        private void AddSongsToListView(List<SongsInfo> songList)
        {
            lvSongList.BeginUpdate();
            lvSongList.Items.Clear();
            foreach (SongsInfo song in songList) {
                string[] songAry = new string[6];
                int currCount = lvSongList.Items.Count + 1;
                if (currCount < 10)
                    songAry[0] = "0" + currCount;
                else
                    songAry[0] = "" + currCount;

                songAry[1] = song.FileName;
                songAry[2] = song.Artist;
                songAry[3] = song.Album;
                songAry[4] = song.Duration;
                songAry[5] = song.Filesize;


                ListViewItem lvItem = new ListViewItem(songAry);
                lvItem.SubItems.Add(song.FilePath);
                lvSongList.Items.Add(lvItem);

                WMPLib.IWMPMedia media = AxWmp.newMedia(song.FilePath);
                AxWmp.currentPlaylist.appendItem(media);
            }
            lvSongList.EndUpdate();
        }

        #endregion

        #region 列表搜索
        private void txtSreachSongName_Enter(object sender, EventArgs e)
        {
            if (txtSreachSongName.Text == "输入要搜索的歌曲名")
                txtSreachSongName.Text = "";
        }

        private void txtSreachSongName_Leave(object sender, EventArgs e)
        {
            if (txtSreachSongName.Text == "")
                txtSreachSongName.Text = "输入要搜索的歌曲名";
        }

        private void txtSreachSongName_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine(txtSreachSongName.Text);
            lbNoResults.SendToBack();
            //初始化
            if (txtSreachSongName.Text == "") {
                switch (lbMenu.SelectedIndex) {
                    case 0:
                        AddSongsToListView(localSongsList);
                        break;
                    case 1:
                        AddSongsToListView(favoriteSongsList);
                        break;
                }
                return;
            } else {
                List<SongsInfo> resultList = new List<SongsInfo>();

                Dictionary<string, SongsInfo> resultDic = new Dictionary<string, SongsInfo>();
                string strSreach = txtSreachSongName.Text;
                Regex r = new Regex(Regex.Escape(strSreach), RegexOptions.IgnoreCase);

                for (int i = 0; i < localSongsList.Count; i++) {
                    Match m = r.Match(localSongsList[i].FileName);
                    if (m.Success) {
                        resultDic.Add(localSongsList[i].FilePath, localSongsList[i]);
                    }
                }

                for (int i = 0; i < favoriteSongsList.Count; i++) {
                    Match m = r.Match(favoriteSongsList[i].FileName);
                    if (m.Success && !resultDic.ContainsKey(localSongsList[i].FilePath)) {
                        resultDic.Add(localSongsList[i].FilePath, localSongsList[i]);
                    }
                }


                if (resultDic.Count > 0) {
                    List<SongsInfo> resList = new List<SongsInfo>();
                    foreach (SongsInfo song in resultDic.Values) {
                        resList.Add(song);
                    }
                    AddSongsToListView(resList);
                } else {
                    lvSongList.Items.Clear();
                    //没有搜索结果
                    lbNoResults.BringToFront();
                }
            }
        }

        private void UpdataOringinSongList()
        {
            oringinListSong = new List<SongsInfo>();
            for (int i = 0; i < lvSongList.Items.Count; i++) {
                oringinListSong.Add(new SongsInfo(lvSongList.Items[i].SubItems[6].Text));
            }
        }
        #endregion

        #region 歌词海报界面切换
        private void pbAlbumImage_Click(object sender, EventArgs e)
        {
            
            if (pbAlbumImage.Image.Flags == Properties.Resources.defaultAlbum.Flags && AxWmp.currentMedia == null) {
                return;
            }
            LrcPanel.BringToFront();
            pbAlbumImage.Visible = false;
            _Message_panel.Visible = false;
            LrcPanel.Visible = true;
            lrcLabels.ForEach(x => x.Text = "");
            lrcList = LRC_Lyric("../../../lyric/" + AxWmp.URL.Substring(AxWmp.URL.LastIndexOf(Convert.ToChar("\\")) + 1, AxWmp.URL.LastIndexOf(Convert.ToChar(".")) - AxWmp.URL.LastIndexOf(Convert.ToChar("\\"))) + "lrc");
            if (lrcList == null) {
                lrcLabel6.Text = "No lyric file";
            }
            Console.WriteLine("first");
        }

        private void LrcPanel_Click(object sender, EventArgs e)
        {
            
            if (pbAlbumImage.Image == Properties.Resources.defaultAlbum) {
                return;
            }
            pbAlbumImage.BringToFront();
            LrcPanel.Visible = false;
            _Message_panel.Visible = true;
            pbAlbumImage.Visible = true;
            Console.WriteLine("second");
        }

        #endregion

        #endregion

        #region 窗体底部
        #region 控制按钮单击事件
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (AxWmp.playState.ToString() == "wmppsPlaying")       //播放->暂停
            {
                AxWmp.Ctlcontrols.pause();
                pbPlay.BackgroundImage = Resources.播放hoover;
                ttbbtnPlayPause.Icon = Resources.播放icon;
                if (MyMessages.Flag == MessageFlag.Pressed)
                {
                    Move_Message_Tick.Stop();
                    New_Message.Stop();
                }
                return;
            } else if (AxWmp.playState.ToString() == "wmppsPaused")    //暂停->播放
              {
                AxWmp.Ctlcontrols.play();
                pbPlay.BackgroundImage = Resources.暂停hoover;
                ttbbtnPlayPause.Icon = Resources.暂停icon;
                if (MyMessages.Flag == MessageFlag.Pressed)
                {
                    Move_Message_Tick.Start();
                    New_Message.Start();
                }
                return;
            }

            if (lvSongList.SelectedItems.Count > 0)         //双击播放列表控制
            {
                Play(lvSongList.SelectedItems[0].Index);
            } else
                MessageBox.Show("请选择要播放的曲目");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lvSongList.Items.Count == 0) {
                MessageBox.Show("请先添加曲目至目录");
                return;
            }

            int currIndex = lvSongList.SelectedItems[0].Index;
            if (currIndex > 0) {
                AxWmp.Ctlcontrols.stop();
                currIndex -= 1;
            } else {
                AxWmp.Ctlcontrols.stop();
                currIndex = lvSongList.Items.Count - 1;
            }

            lvSongList.Items[currIndex].Focused = true;
            lvSongList.Items[currIndex].EnsureVisible();
            lvSongList.Items[currIndex].Selected = true;

            Play(currIndex);

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lvSongList.SelectedItems.Count == 0) {
                MessageBox.Show("请先添加曲目至目录");
                return;
            }

            int currIndex = lvSongList.SelectedItems[0].Index;
            if (currIndex < lvSongList.Items.Count - 1) {
                AxWmp.Ctlcontrols.stop();
                currIndex += 1;
            } else {
                AxWmp.Ctlcontrols.stop();
                currIndex = 0;
            }

            Play(currIndex);
        }

        private void btnPlayMode_Click(object sender, EventArgs e)
        {
            if (currPlayMode == PlayMode.ListLoop)
                currPlayMode = PlayMode.Shuffle;
            else
                currPlayMode += 1;

            if (currPlayMode == PlayMode.SingleLoop)
                btnPlayMode.BackgroundImage = Properties.Resources.单曲循环;
            else if (currPlayMode == PlayMode.ListLoop)
                btnPlayMode.BackgroundImage = Properties.Resources.列表循环;
            else
                btnPlayMode.BackgroundImage = Properties.Resources.随机播放;
        }
        #endregion

        #region 音量与进度条事件
        private void tkbVol_ValueChanged(object sender, EventArgs e)
        {
            AxWmp.settings.volume = tkbVol.Value;
            //lbVolumeVal.Text = tkbVol.Value.ToString() + "%";
        }

        private void tkbVol_MouseHover(object sender, EventArgs e)
        {
            //lbVolumeVal.Text = tkbVol.Value.ToString() + "%";
        }

        private void tkbVol_MouseLeave(object sender, EventArgs e)
        {
            //lbVolumeVal.Text = "音量：";
        }

        private void tkbMove_Scroll(object sender, EventArgs e)
        {
            AxWmp.Ctlcontrols.currentPosition = (double)this.tkbMove.Value;
        }


        #endregion

        #endregion

        #region 换肤
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            int a = ran.Next(0, 3);
            switch (a) {
                case 0:
                    this.mainWindowPanel.BackgroundImage = Properties.Resources.beijing1right;
                    this.menuPanel.BackgroundImage = Properties.Resources.beijing1left;
                    break;
                case 1:
                    this.mainWindowPanel.BackgroundImage = Properties.Resources.beijing2right;
                    this.menuPanel.BackgroundImage = Properties.Resources.beijing2left;
                    break;
                case 2:
                    this.mainWindowPanel.BackgroundImage = Properties.Resources.beijing3right;
                    this.menuPanel.BackgroundImage = Properties.Resources.beijing3left;
                    break;
                default:
                    break;
            }

        }
        #endregion


        #region 弹幕
        private void MessageOnBoard_Click(object sender, EventArgs e)
        {
            if (MyMessages.Flag == MessageFlag.Unpressed && AxWmp.playState.ToString() == "wmppsPlaying")
            {
               
                
                New_Message.Start();
                Move_Message_Tick.Start();

                //_Message_panel.Parent = pbAlbumImage;
                //_Message_panel.BackColor = Color.FromArgb(160, 0, 0, 0);
                _Message_panel.BringToFront();
                MessageOnBoard.Image = Resources.DMpressed;
                MyMessages.Flag = MessageFlag.Pressed;

                Which_Message = 0;
                this._Message_panel.BringToFront();
                this._Message_panel.Visible = true;
                this.pbAlbumImage.Visible = true;
                this._Message.ReadOnly = false;


                this.LrcPanel.Visible = false;
                lvSongList.Visible = false;

            }
            else if (MyMessages.Flag == MessageFlag.Pressed)
            {

                _Message_panel.Parent = WindowPanel;
                MessageOnBoard.Image = Resources.DMUnpressed1;
                MyMessages.Flag = MessageFlag.Unpressed;
                for (int i = this._Message_panel.Controls.Count - 1; i >= 0; i--)
                {
                    if (this._Message_panel.Controls[i] is Label)
                    {
                        this._Message_panel.Controls[i].Dispose();
                    }
                }

                _Message.Text = "";
                Move_Message_Tick.Dispose();
                New_Message.Dispose();
                this._Message.ReadOnly = true;
                this.pbAlbumImage.Visible = true;
                this.LrcPanel.Visible = true;
                this._Message_panel.Visible = false;
                this._Message_panel.SendToBack();
            }
            



        }


        private void lbEndTime_Click(object sender, EventArgs e)
        {

        }
        private void _Message_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                SongsName.songs.Find(a => a.Song == "aa").messages.Add(new MyMessages(_Message.Text));
                _Message.Text = "";
            }
        }
        private void New_Message_Tick(object sender, EventArgs e)
        {
            try
            {
                SongsName.Export("myMessage.xml");
                SongsName this_music = SongsName.songs.Find(s => s.Song == "aa"); //当前播放歌曲
                if (Which_Message < this_music.messages.Count)
                {
                    if (Which_Message % Lines == 0)
                    {

                        Label label2 = new Label();
                        label2.Text = this_music.messages[Which_Message].Message;
                        label2.BackColor = System.Drawing.Color.Transparent;
                        label2.Parent = _Message_panel;
                        //label2.BackColor = Color.FromArgb(0, 0, 0, 0);
                        label2.ForeColor = Color.BlueViolet;
                        label2.Font = new Font("楷书", 13, FontStyle.Bold);
                        label2.Location = new Point(this._Message_panel.Size.Width, Height_Message / 2);
                        label2.Visible = true;
                        label2.AutoSize = true;
                        //label2.Size = new Size(50,10);
                        MyLabels.Add(label2);
                        this._Message_panel.Controls.Add(label2);
                        label2.Show();

                    }
                    else
                    {
                        Label label2 = new Label();
                        label2.Text = this_music.messages[Which_Message].Message;
                        //label2.BackColor = System.Drawing.Color.Transparent;
                        label2.Parent = _Message_panel;
                        label2.BackColor = Color.FromArgb(0, 0, 0, 0);
                        label2.ForeColor = Color.BlueViolet;
                        label2.Font = new Font("楷书", 13, FontStyle.Bold);
                        label2.Location = new Point(this._Message_panel.Size.Width, Height_Message / 2 * (Which_Message % Lines + 1));
                        label2.Visible = true;
                        label2.AutoSize = true;
                        //label2.Size = new Size(50, 10);
                        MyLabels.Add(label2);
                        this._Message_panel.Controls.Add(label2);
                    }
                    Which_Message++;
                }
            }
            catch (NullReferenceException ex)
            {
                
                //TextBox NullLabel = new TextBox();
                //NullLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
                //NullLabel.Text = "墙上空空如也，快来发射你的弹幕叭";
                //_Message_panel.Controls.Add(NullLabel);
                //NullLabel.Font = new Font("楷书", 20, FontStyle.Bold);
                //NullLabel.BackColor = Color.FromArgb( 22, 24, 28); 
                //NullLabel.Location = new Point(this._Message_panel.Size.Width / 2, this._Message_panel.Size.Height / 3);
            }
            
            
        }

        private void Move_Message_Tick_Tick(object sender, EventArgs e)
        {
            //foreach (Label ctrl in MyLabels)
            //{
            //    if (ctrl.Location.X <= -ctrl.Size.Width)
            //    {
            //        ctrl.Dispose();
            //    }
            //    //else if (ctrl.Location.X >= this._Message_panel.Width - ctrl.Size.Width)
            //    //    ctrl.Left -= 1;
            //    else
            //        ctrl.Left -= Speed_Message;

            //}
            for (int i = this._Message_panel.Controls.Count - 1; i >= 0; i--)
            {
                if (this._Message_panel.Controls[i] is Label)
                {
                    if (this._Message_panel.Controls[i].Location.X <= -this._Message_panel.Controls[i].Size.Width)
                    {
                        this._Message_panel.Controls[i].Dispose();
                    }
                    //else if (this._Message_panel.Controls[i].Location.X >= this._Message_panel.Width - this._Message_panel.Controls[i].Size.Width)
                    //    this._Message_panel.Controls[i].Left -= 2;
                    else
                        this._Message_panel.Controls[i].Left -= Speed_Message;
                }
            }
        }

        #endregion


    }
}
