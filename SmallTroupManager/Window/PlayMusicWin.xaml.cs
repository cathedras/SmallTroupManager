using System;
using System.Windows;
using SmallTroupManager.Window;
using MediaPlayer = VisioForge.Controls.UI.WPF.MediaPlayer;


namespace SmallTroupManager.View
{
    /// <summary>
    /// PlayMusicWin.xaml 的交互逻辑
    /// </summary>
    public partial class PlayMusicWin : WindowBase
    {
        public PlayMusicWin()
        {
            InitializeComponent();
            App.Locator.PlayM.Player = this.VlcControl;
        }


        private void PlayMusicWin_OnClosed(object sender, EventArgs e)
        {
            
            //App.Locator.PlayM.CloseWindow();
        }
    }
}
