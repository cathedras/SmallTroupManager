using System;
using System.Windows;
using MediaPlayer = VisioForge.Controls.UI.WPF.MediaPlayer;


namespace SmallTroupManager.View
{
    /// <summary>
    /// PlayMusicWin.xaml 的交互逻辑
    /// </summary>
    public partial class PlayMusicWin : Window
    {
        public PlayMusicWin()
        {
            InitializeComponent();
        }


        private void PlayMusicWin_OnClosed(object sender, EventArgs e)
        {
            
            //App.Locator.PlayM.CloseWindow();
        }
    }
}
