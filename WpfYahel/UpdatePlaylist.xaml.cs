using Model;
using System.Windows;
using System.Windows.Controls;
using YahelApiService;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for UpdatePlaylist.xaml
    /// </summary>
    public partial class UpdatePlaylist : Page
    {
        private Playlists playlist;
        JenreList gList = new JenreList();

        public UpdatePlaylist(Playlists p)
        {
            InitializeComponent();
            GetData(p);
            this.playlist = p;
        }

        public async Task GetData(Playlists p)
        {
            YahelApiService.YahelApiSrv api = new YahelApiService.YahelApiSrv();
            this.PlaylistNameTextBox.Text = p.PlaylistName;
            this.GenreComboBox.Text = p.Jenre.JenreName1;


        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            playlist.PlaylistName = this.PlaylistNameTextBox.Text.ToString();
            playlist.Jenre = gList.Find(x => x.JenreName1 == GenreComboBox.SelectedItem);
            s.UpdatePlaylist(playlist);
        }
        public async Task GetGenres()
        {
            await GetAllGenres();
            this.GenreComboBox.ItemsSource = null;
            this.GenreComboBox.ItemsSource = gList.Select(x => x.JenreName1);

        }
        public async Task GetAllGenres()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            gList = await yApi.GetJenres();

        }

    }

}
