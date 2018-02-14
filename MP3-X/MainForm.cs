using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace MP3_X
{
    public interface IMainForm
    {

        int Volume { get; }
        int TimeChange { set; get; }
        int TimeDuration { set; get; }
        int PlayListSelect { get; set; }
        string PlayList { set; }
        string FilePathProperty { get; set; }
     
       
        void TimerStart();
        void TimerStop();
        void PlayListClear();
        

        event EventHandler AudioOpenClick;
        event EventHandler PlayClick;
        event EventHandler PauseClick;
        event EventHandler StopClick;
        event EventHandler BackClick;
        event EventHandler NextClick;
        event EventHandler VolumeChanged;
        event EventHandler Tick;
        event EventHandler TimeBarChange;
        event EventHandler PlayListChek;
        event EventHandler OpenPlayer;
        event EventHandler ClosePlayer;
        event EventHandler DeletePlayListSong;
    }


    public partial class MainForm : Form, IMainForm
    {
        
        string _FilePath;
        OpenFileDialog open = new OpenFileDialog();
        public MainForm()
        {

            InitializeComponent();
            btnOpenFile.Click += BtnOpenFile_Click;
            btnPlay.Click += BtnPlay_Click;
            btnPause.Click += BtnPause_Click;
            btnStop.Click += BtnStop_Click;
            btnNext.Click += BtnNext_Click;
            btnBack.Click += BtnBack_Click;
            tbVolume.ValueChanged += TbVolume_ValueChanged;
            timeTimer.Tick += TimeTimer_Tick;
            tbTime.MouseUp += TbTime_MouseUp;
            lbPlayList.DoubleClick += LbPlayList_DoubleClick;
            btnDelete.Click += BtnDelete_Click;
            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (DeletePlayListSong != null && lbPlayList.SelectedItem!=null) DeletePlayListSong(this, EventArgs.Empty);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ClosePlayer != null) ClosePlayer(this, EventArgs.Empty);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (OpenPlayer != null) OpenPlayer(this, EventArgs.Empty);
        }
        

        private void LbPlayList_DoubleClick(object sender, EventArgs e)
        {
            if (PlayListChek != null) PlayListChek(this, EventArgs.Empty);
        }

        private void TbTime_MouseUp(object sender, MouseEventArgs e)
        {
            if (TimeBarChange != null) TimeBarChange(this, EventArgs.Empty);
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            if (Tick != null) Tick(this, EventArgs.Empty);
        }

        private void TbVolume_ValueChanged(object sender, EventArgs e)
        {
            if (VolumeChanged != null) VolumeChanged(this, EventArgs.Empty);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (BackClick != null) BackClick(this, EventArgs.Empty);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (NextClick != null) NextClick(this, EventArgs.Empty);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (StopClick != null) StopClick(this, EventArgs.Empty);
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (PauseClick != null) PauseClick(this, EventArgs.Empty);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (PlayClick != null) PlayClick(this, EventArgs.Empty);


        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {           
            open.Filter = "MP3|*.mp3";
            if (open.ShowDialog() == DialogResult.OK)
            {
                _FilePath = open.FileName;
                if (AudioOpenClick != null) AudioOpenClick(this, EventArgs.Empty);
            }  
            
        }





        #region IMainForm;
        public int Volume { get { return tbVolume.Value; } }
        public event EventHandler AudioOpenClick;
        public event EventHandler PlayClick;
        public event EventHandler PauseClick;
        public event EventHandler StopClick;
        public event EventHandler BackClick;
        public event EventHandler NextClick;
        public event EventHandler VolumeChanged;
        public event EventHandler Tick;
        public event EventHandler TimeBarChange;
        public event EventHandler PlayListChek;
        public event EventHandler OpenPlayer;
        public event EventHandler ClosePlayer;
        public event EventHandler DeletePlayListSong;


        public void PlayListClear()
        {            
            lbPlayList.Items.Clear();
        }

        public int TimeChange {
            set { tbTime.Value = value; }
            get { return tbTime.Value; }
        }

        public int TimeDuration { set { tbTime.Maximum = value; } get { return tbTime.Maximum; } }

        public void TimerStart()
        {
            timeTimer.Start();
        }

        public void TimerStop()
        {
            timeTimer.Stop();
        }

        public string FilePathProperty
        {
            get {
                if (_FilePath == null) BtnOpenFile_Click(this, EventArgs.Empty);
                return _FilePath;
            }
        set { _FilePath = value;}
        }


        public string PlayList { set { lbPlayList.Items.Add(value); } }



        public int PlayListSelect { get { return lbPlayList.SelectedIndex; } set { lbPlayList.SelectedIndex = value; } }
       
    }
       #endregion
    

}
