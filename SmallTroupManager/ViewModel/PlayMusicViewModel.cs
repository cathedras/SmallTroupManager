using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SmallTroupManager.View;
using VisioForge.Tools;
using VisioForge.Controls.UI.WPF;

namespace SmallTroupManager.ViewModel
{
    public class PlayMusicViewModel:ViewModelBase
    {
        private MediaPlayer _curMediaPlayer;
        private ObservableCollection<string> _curList;

        public ObservableCollection<string> CurList
        {
            get => _curList ?? (_curList = new ObservableCollection<string>());
        }
        private ICommand _startingCommand;
        public ICommand StartingCommand => _startingCommand ?? (_startingCommand = new RelayCommand(() => OnPlay()));

        private void OnPlay()
        {
            _curMediaPlayer.Play();
        }

        public void InitShow(List<string> bList)
        {
            var play = new PlayMusicWin();
            _curMediaPlayer = play.VideoPlayer;
            bList.ForEach(b =>
            {
                CurList.Add(Path.GetFileName(b));
            });
            _curMediaPlayer.FilenamesOrURL.AddRange(bList);
            _curMediaPlayer.Audio_Play = true;
            //_curMediaPlayer.Audio_OutputDevice = "";
            var isClose = play.ShowDialog();
            _curMediaPlayer = null;
        }
    }
}
