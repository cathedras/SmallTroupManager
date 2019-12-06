using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using log4net;
using SmallTroupManager.Model;
using SmallTroupManager.View;
using VisioForge.Tools;
using VisioForge.Controls.UI.WPF;
using VisioForge.Types;

namespace SmallTroupManager.ViewModel
{
    public class PlayMusicViewModel:ViewModelBase
    {
        private ILog _log = LogManager.GetLogger("logfile");
        private MediaPlayer _curMediaPlayer;
        private ObservableCollection<string> _curList;
        private string _playTime = string.Empty;

        public ObservableCollection<string> CurList
        {
            get => _curList ?? (_curList = new ObservableCollection<string>());
        }
        private ICommand _startingCommand;
        public ICommand StartingCommand => _startingCommand ?? (_startingCommand = new RelayCommand(() => OnPlay()));

        public string PlayTime { get => _playTime; set => _playTime = value; }

        public void OnPlay()
        {
            _curMediaPlayer.Play();
        }

        public void InitShow(List<string> bList)
        {
            var play = new PlayMusicWin();
            _curMediaPlayer = play.VideoPlayer;
            CurList.Clear();
            bList.ForEach(b =>
            {
                CurList.Add(Path.GetFileName(b));
            });
            _curMediaPlayer.OnError += CurMediaPlayerOnOnError;
            _curMediaPlayer.FilenamesOrURL.AddRange(bList);
            _curMediaPlayer.Audio_Play = true;
           
            //_curMediaPlayer.Audio_OutputDevice = "";
            var isClose = play.ShowDialog();
            _curMediaPlayer = null;
        }

        public void NoWindowPlayMusic(List<string> bList,Action<string> act)
        {  
            _curMediaPlayer = new MediaPlayer();
            _curMediaPlayer.FilenamesOrURL.Clear();
            _curMediaPlayer.FilenamesOrURL.AddRange(bList);
            _curMediaPlayer.OnError += CurMediaPlayerOnOnError;
            _curMediaPlayer.OnLicenseRequired += CurMediaPlayer_OnLicenseRequired;
            _curMediaPlayer.Audio_Play = true;
            //_log.Debug(player.Position_Get_Time());
            Task.Factory.StartNew(() =>
            {
                while (GetTimeCount(out _playTime))
                {
                    act?.Invoke(_playTime);
                    Thread.Sleep(1000);
                }
            });


           
            _curMediaPlayer.Play(true);
        }

        public void StopPlay()
        {
            _curMediaPlayer.Stop();
        }

        public bool GetTimeCount(out string timeFormat)
        {
            var max = (int)(_curMediaPlayer.Duration_Time() / 1000);
            var maxStr = MediaPlayer.Helpful_SecondsToTimeFormatted(max);
            int cur = (int)(_curMediaPlayer.Position_Get_Time() / 1000);
            var curStr = MediaPlayer.Helpful_SecondsToTimeFormatted(cur);
            timeFormat = $"{curStr} / {maxStr}";
            if ( max > 0 && max == cur)
            {
                return false;
            }
            return true;
        }


        private void CurMediaPlayerOnOnError(object sender, ErrorsEventArgs e)
        {
            _log.Debug(e.Message);
        }
        private void CurMediaPlayer_OnLicenseRequired(object sender, LicenseEventArgs e)
        {
            if (e.Level == LicenseLevel.Standard)
            {
                _log.Debug($"{e.Message},{e.Level}");
            }
        }

        public void CloseWindow()
        {
            _curMediaPlayer.Stop();
        }

    }
}
