using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for SongPlay.xaml
    /// </summary>
    public partial class SongPlay : Page
    {
        private Songs GetSong { get; set; }

        public SongPlay(Model.Songs song)
        {
            InitializeComponent();
            this.txtSongName.Text = song.NameSong;
            GetSong = song;

        }

        private TimeSpan playbackPosition;

        private void PlaySongBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.LoadedBehavior = MediaState.Manual;
            string pathSong = GetSong.AudioFilePath1;
            mediaElement.Source = new Uri(pathSong, UriKind.RelativeOrAbsolute);
            mediaElement.Play();
        }

        private void StopSongBtn_Click(object sender, RoutedEventArgs e)
        {
            playbackPosition = mediaElement.Position;
            mediaElement.Pause();
        }

        private void ContinueSongBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = playbackPosition;
            mediaElement.Play();
        }

        private void AddSongBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
