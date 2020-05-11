using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using SmallTroupManager.Model;
using SmallTroupManager.View;
using Vlc.DotNet.Core;
//using VisioForge.Tools;
//using VisioForge.Controls.UI.WPF;
//using VisioForge.Types;
using Vlc.DotNet.Wpf;

namespace SmallTroupManager.ViewModel
{
    public class PlayMusicViewModel:ViewModelBase
    {
        private ILog _log = LogManager.GetLogger("logfile");
        private VlcMediaPlayer _curMediaPlayer;
        private VlcControl _player;
        private ObservableCollection<string> _curList;
        private string _playTime = string.Empty;
        private string _videoPath = string.Empty;
        public ObservableCollection<string> CurList
        {
            get => _curList ?? (_curList = new ObservableCollection<string>());
        }
        private ICommand _startingCommand;
        public ICommand StartingCommand => _startingCommand ?? (_startingCommand = new RelayCommand(() => OnPlay()));

        public string PlayTime { get => _playTime; set => _playTime = value; }
        public VlcControl Player { get => _player; set => _player = value; }


        public void OnPlay()
        {
            _curMediaPlayer.Play(_videoPath);
            //_curMediaPlayer = CreatePlayer(Player, AppDomain.CurrentDomain.BaseDirectory);



        }

        public void InitShow(List<string> bList)
        {
            var play = new PlayMusicWin();
           // _curMediaPlayer = play.VideoPlayer;
            CurList.Clear();
            bList.ForEach(b =>
            {
                CurList.Add(Path.GetFileName(b));
            });
            //_curMediaPlayer.OnError += CurMediaPlayerOnOnError;
            //_curMediaPlayer.FilenamesOrURL.AddRange(bList);
            //_curMediaPlayer.Audio_Play = true;
           
            ////_curMediaPlayer.Audio_OutputDevice = "";
            var isClose = play.ShowDialog();
            //_curMediaPlayer = null;
        }

        public void NoWindowPlayMusic(string srcPath,Action<string> act)
        {  
            _curMediaPlayer = CreatePlayer(new VlcControl(), AppDomain.CurrentDomain.BaseDirectory);
            //_curMediaPlayer.FilenamesOrURL.Clear();
            //_curMediaPlayer.FilenamesOrURL.AddRange(bList);
            //_curMediaPlayer.OnError += CurMediaPlayerOnOnError;
            //_curMediaPlayer.OnLicenseRequired += CurMediaPlayer_OnLicenseRequired;
            //_curMediaPlayer.Audio_Play = true;

            //_log.Debug(player.Position_Get_Time());
            Task.Factory.StartNew(() =>
            {
                while (GetTimeCount(out _playTime))
                {
                    act?.Invoke(_playTime);
                    Thread.Sleep(1000);
                }
            });
            _curMediaPlayer.Play(new FileInfo(srcPath));
        }

        public void StopPlay()
        {
            _curMediaPlayer.Stop();
        }

        public bool GetTimeCount(out string timeFormat)
        {
            
            //var max = (int)(_curMediaPlayer.Duration_Time() / 1000);
            //var maxStr = MediaPlayer.Helpful_SecondsToTimeFormatted(max);
            //int cur = (int)(_curMediaPlayer.Position_Get_Time() / 1000);
            //var curStr = MediaPlayer.Helpful_SecondsToTimeFormatted(cur);
            var time = _curMediaPlayer.Time/1000;
            var max = _curMediaPlayer.Length/1000;

            timeFormat = $"{time}/{max}";//$"{curStr} / {maxStr}";
           // if (max > 0 && max == cur)
            {
                //return false;
            }
            return true;
        }


        //private void CurMediaPlayerOnOnError(object sender, ErrorsEventArgs e)
        //{
        //    _log.Debug(e.Message);
        //}
        //private void CurMediaPlayer_OnLicenseRequired(object sender, LicenseEventArgs e)
        //{
        //    if (e.Level == LicenseLevel.Standard)
        //    {
        //        _log.Debug($"{e.Message},{e.Level}");
        //    }
        //}
        public VlcMediaPlayer CreatePlayer(VlcControl vlcPlayer, string path)
        {
            var vlcLibDirectory = new DirectoryInfo(Path.Combine(path, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            var options = new string[]
            {
                // VLC options can be given here. Please refer to the VLC command line documentation.
                "--file-logging", "-vvv", "--logfile=Logs.log"
            };

            vlcPlayer.SourceProvider.CreatePlayer(vlcLibDirectory, options);
            //var play = vlcPlayer.SourceProvider.MediaPlayer.Manager.CreateMediaPlayer();
            // Load libvlc libraries and initializes stuff. It is important that the options (if you want to pass any) and lib directory are given before calling this method.
             //vlcPlayer.SourceProvider.MediaPlayer.Play(new FileInfo("../../TestData/Video/Rescue Emergency.mp4"));
            // vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri("rtmp://media3.sinovision.net:1935/live/livestream"));
            
            return vlcPlayer.SourceProvider.MediaPlayer;
        }


        public void CloseWindow()
        {
            _curMediaPlayer.Stop();
        }

    }
}
