using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for UpdateSong.xaml
    /// </summary>
    public partial class UpdateSong : Page
    {
        private Songs song;
        JenreList gList = new JenreList();
        ArtistsList artistsList = new ArtistsList();


        public UpdateSong(Songs s)
        {
            InitializeComponent();
            GetData(s);
            this.song = s;
        }
        public async Task GetData(Songs s)
        {
            YahelApiService.YahelApiSrv api = new YahelApiService.YahelApiSrv();
            this.SongNameTextBox.Text = s.NameSong;
            this.GenreComboBox.Text = s.Jenre.JenreName1;
            this.timeTxtBox.Text = s.Time.ToString();



        }
        private void UpdateButton(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
