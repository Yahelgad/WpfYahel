using Model;
using System.Windows;
using System.Windows.Controls;
using YahelApiService;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for CreatePlaylists.xaml
    /// </summary>

    public partial class CreatePlaylists : Page
    {
        List<string> GenreStrings = new List<string>();
        JenreList gList = new JenreList();
        Jenre Gen = null;
        public CreatePlaylists()
        {
            InitializeComponent();
            GetGenre();
        }
        public async Task GetGenre()
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

        private void AddButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv Y = new YahelApiService.YahelApiSrv();
            Playlists p = new Playlists();
            p.PlaylistName = PlaylistNameTextBox.Text.ToString();
            p.PlaylistDateCreation = DateOnly.FromDateTime(DateTime.Now.Date);
            p.Jenre = gList.Find(x => x.JenreName1 == GenreComboBox.SelectedItem);
            p.Owner1 = Login.LoggedInUsers;
            Y.InsertPlaylist(p);
        }

    }
}
