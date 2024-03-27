using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using YahelApiService;
namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for SongsTube4You.xaml
    /// </summary>
    public partial class SongsTube4You : Page
    {
        PlaylistsList playlistList = new PlaylistsList();
        List<Playlists> pList = new List<Playlists>();
        private Songs songSelected;
        SongsOnPlaylist sop = new SongsOnPlaylist();
        List<SongsOnPlaylist> lSOP = new List<SongsOnPlaylist>();
        List<Songs> sList = new List<Songs>();
        JenreList gList = new JenreList();
        private Songs selectedSong;
        private Playlists GetPlaylist { get; set; }
        public SongsTube4You()
        {
            InitializeComponent();
            GetGenre();
            sSongs();

        }
        public async Task GetGenre()
        {

            this.GenreComboBoxFilter.ItemsSource = null;
            this.GenreComboBoxFilter.ItemsSource = gList.Select(x => x.JenreName1);

        }
        public async Task GetAllGenres()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            gList = await yApi.GetJenres();

        }
        private async void sSongs()
        {
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            sList = await apiS.GetSongs() as List<Songs>;
            if (Login.LoggedInUsers.IsManager)
            {
                deleteSongBtn.Visibility = Visibility.Visible;
                updateSongBtn.Visibility = Visibility.Visible;
                addSongBtn.Visibility = Visibility.Visible;

            }
            else
            {
                deleteSongBtn.Visibility = Visibility.Collapsed;
                updateSongBtn.Visibility = Visibility.Collapsed;
                addSongBtn.Visibility = Visibility.Collapsed;
            }
            ListViewSongs.ItemsSource = sList;
        }
        private async void addSongToaPlaylist(object sender, RoutedEventArgs e)
        {
            songSelected = (Songs)this.ListViewSongs.SelectedItem;
            ListViewSongs.Visibility = Visibility.Collapsed;
            SearchSongTxtBox.Visibility = Visibility.Collapsed;
            userPlaylists();



        }
        private async void userPlaylists()
        {
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            playlistList = (await apiS.GetPlaylists());
            pList = playlistList.FindAll(x => x.Owner1.Id == Login.LoggedInUsers.Id);

            SearchPlaylistTxtBox.Visibility = Visibility.Visible;
            ListViewPlaylists.Visibility = Visibility.Visible;
            ListViewPlaylists.ItemsSource = pList;

        }
        private void ListViewSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListViewSongs.SelectedItem != null)
            {
                selectedSong = ListViewSongs.SelectedItem as Songs;
            }
            else
            {
                selectedSong = null;
            }
        }

        private void ListViewSongs_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ListViewItem item)
            {
                selectedSong = item.DataContext as Songs;
            }
        }

        private void ListViewSongs_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ListViewItem item)
            {
                selectedSong = item.DataContext as Songs;
            }
        }

        private void ToPlaySong(object sender, RoutedEventArgs e)
        {
            if (selectedSong != null)
            {
                NavigationService.Navigate(new SongPlay(selectedSong));
            }
        }
        public async Task<bool> GetAllSOP()
        {
            YahelApiService.YahelApiSrv apiS = new YahelApiService.YahelApiSrv();
            lSOP = await apiS.GetSongsOnPlaylist() as List<SongsOnPlaylist>;
            List<SongsOnPlaylist> lS = lSOP.FindAll(x => x.PlaylistName.Id == GetPlaylist.Id);
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
            sop.PlaylistName = playlistSelected;
            sop.SongName = songSelected;
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
            ListViewSongs.Visibility = Visibility.Visible;

            sSongs();
        }

        private void deleteSong(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();

            if (Login.LoggedInUsers.IsManager)
            {
                var x = (ListViewSongs.SelectedItem as Songs);
                Songs s = ListViewSongs.Items.Cast<Songs>().FirstOrDefault(Item => Item.Id == x.Id);
                if (s == null)
                {
                    return;
                }
                else
                {
                    apiSrv.DeleteSong(s.Id);
                    List<Songs> SongsList = ListViewSongs.ItemsSource as List<Songs>;
                    SongsList.Remove(s);
                   
                    
                    ListViewSongs.ItemsSource = null;
                    ListViewSongs.ItemsSource = SongsList;
                }
            }
        }

        private void ListViewPlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPlaylist = ListViewPlaylists.SelectedItem as Playlists;
        }

        private void SearchSongTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchSongTxtBox.Text.ToLower();
            var list = sList.Where(x => x.NameSong.ToLower().StartsWith(keyword)).ToList();
            ListViewSongs.ItemsSource = list;
            SearchSongTxtBox.Text = "";
        }

        private void SearchPlaylistTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchPlaylistTxtBox.Text.ToLower();
            var list = playlistList.Where(x => x.PlaylistName.ToLower().StartsWith(keyword)).ToList();
            ListViewPlaylists.ItemsSource = list;
            SearchPlaylistTxtBox.Text = "";
        }

        private void updatesong(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpdateSong(selectedSong));
        }

        private void addsong(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSong());
        }

        private void GenreComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiSrv();
            var list = sList.Where(x => x.Jenre.JenreName1 == GenreComboBoxFilter.Text).ToList();
            if (list == null)
            {

                ListViewSongs.ItemsSource = sList;
            }
            else
            {
                ListViewSongs.ItemsSource = list;
            }
        }
    }
}
