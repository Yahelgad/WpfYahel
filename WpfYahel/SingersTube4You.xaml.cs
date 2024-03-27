using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for SingersTube4You.xaml
    /// </summary>
    public partial class SingersTube4You : Page
    {
        SongsList songList = new SongsList();
        List<Songs> sList = new List<Songs>();
        PlaylistsList playlistList = new PlaylistsList();
        List<Playlists> pList = new List<Playlists>();
        SongsOnPlaylist sop = new SongsOnPlaylist();
        private Songs songSelected;
        List<SongsOnPlaylist> lSOP = new List<SongsOnPlaylist>();
        List<Artists> aList = new List<Artists>();
        private Playlists GetPlaylists { get; set; }
        private Artists GetArtists { get; set; }
        public SingersTube4You()
        {
            InitializeComponent();
            sSingers();
        }
        private async void sSingers()
        {
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            aList = await apiS.GetArtists() as List<Artists>;
            if (Login.LoggedInUsers.IsManager)
            {
                addArtistBtn.Visibility = Visibility.Visible;
                deleteArtistBtn.Visibility = Visibility.Visible;
            }
            else
            {
                addArtistBtn.Visibility = Visibility.Collapsed;
                deleteArtistBtn.Visibility = Visibility.Collapsed;
            }
            ListViewSingers.ItemsSource = aList;

        }
        private async void showSongsOfArtist(object sender, RoutedEventArgs e)
        {
            ListViewSingers.Visibility = Visibility.Collapsed;
            Artists ArtistSelected = (Artists)this.ListViewSingers.SelectedItem;


            sSongs(ArtistSelected);



        }
        private async void sSongs(Artists ArtistSelected)
        {
            int id = (int)ArtistSelected.Id;

            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            songList = (await apiSrv.GetSongs());
            sList = songList.FindAll(x => x.ArtistName.Id == id);


            listviewsongs.Visibility = Visibility.Visible;
            listviewsongs.ItemsSource = sList;
        }

        private async void addSong(object sender, RoutedEventArgs e)
        {
            songSelected = (Songs)this.listviewsongs.SelectedItem;

            listviewsongs.Visibility = Visibility.Collapsed;

            userPlaylists();

        }
        private async void userPlaylists()
        {
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            playlistList = (await apiS.GetPlaylists());
            pList = playlistList.FindAll(x => x.Owner1.Id == Login.LoggedInUsers.Id);


            ListViewPlaylists.Visibility = Visibility.Visible;
            ListViewPlaylists.ItemsSource = pList;

        }


        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchSingersTxtBox.Text.ToLower();
            var list = aList.Where(x => x.FullName.ToLower().StartsWith(keyword)).ToList();
            ListViewSingers.ItemsSource = list;

        }

        public async Task<bool> GetAllSOP()
        {
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            lSOP = await apiS.GetSongsOnPlaylist() as List<SongsOnPlaylist>;
            List<SongsOnPlaylist> lS = lSOP.FindAll(x => x.PlaylistName.Id == GetPlaylists.Id);
            if (lS.Count() == Login.LoggedInUsers.Subscription1.NumbOfSongs1)
            {
                return false;
            }
            else
                return true;
        }
        private async void addToPlaylist(object sender, RoutedEventArgs e)
        {
            Playlists playlistSelected = (Playlists)this.ListViewPlaylists.SelectedItem;
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            sop.PlaylistName.PlaylistName = playlistSelected.PlaylistName;
            sop.SongName.NameSong = songSelected.NameSong;
            bool NumbOfSongs = await GetAllSOP();
            if (NumbOfSongs)
            {

                apiS.InsertSongOnPlaylist(sop);
            }
            else
            {
                MessageBox.Show("You dont have another place");
            }

            ListViewPlaylists.Visibility = Visibility.Collapsed;
            ListViewSingers.Visibility = Visibility.Visible;

            sSingers();
        }

        private void ListViewPlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPlaylists = ListViewPlaylists.SelectedItem as Playlists;
        }

        private void DeleteArtist(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            if (Login.LoggedInUsers.IsManager)
            {
                var x = (ListViewSingers.SelectedItem as Artists);
                Artists a = ListViewSingers.Items.Cast<Artists>().FirstOrDefault(Item => Item.Id == x.Id);
                if (a == null)
                {
                    return;
                }
                else
                {
                    apiSrv.DeleteArtist(a.Id);
                }
            }


        }

        private void ListViewSingers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetArtists = ListViewSingers.SelectedItem as Artists;
        }

        private void Addartist(object sender, RoutedEventArgs e)
        {
            if (Login.LoggedInUsers.IsManager)
            {
                NavigationService.Navigate(new AddArtist());
            }
        }

        private void GenreFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
