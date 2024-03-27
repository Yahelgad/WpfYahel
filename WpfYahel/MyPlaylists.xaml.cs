using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for MyPlaylists.xaml
    /// </summary>
    public partial class MyPlaylists : Page
    {
        PlaylistsList playlistList = new PlaylistsList();
        List<Playlists> pls = new List<Playlists>();
        private Playlists playlistSelected;
        SongsOnPlaylistList songList = new SongsOnPlaylistList();
        List<SongsOnPlaylist> song = new List<SongsOnPlaylist>();
        List<Songs> Songs = new List<Songs>();
        SongsList AllTheSongs = new SongsList();
        SongsOnPlaylist sop = new SongsOnPlaylist();
        private Playlists GetPlaylist { get; set; }
        public MyPlaylists()
        {
            InitializeComponent();
            GetPlaylistsByUser();

        }
        private async Task GetPlaylistsByUser()
        {
            if (Login.LoggedInUsers.IsManager)
            {
                YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
                playlistList = await apiSrv.GetPlaylists();
                ListViewPlaylists.ItemsSource = playlistList;
            }
            else
            {
                YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
                playlistList = await apiSrv.GetPlaylists();
                pls = playlistList.FindAll(x => x.Owner1.Id == Login.LoggedInUsers.Id);
                ListViewPlaylists.ItemsSource = pls;
            }
            //להמשיך פה עם התצוגה
        }

        private async void showsongsfromthisplaylist(object sender, RoutedEventArgs e)
        {
            ListViewPlaylists.Visibility = Visibility.Collapsed;
            playlistSelected = (Playlists)this.ListViewPlaylists.SelectedItem;
            ShowSongsFromPlaylist(playlistSelected);
        }
        private async Task ShowSongsFromPlaylist(Playlists playlistSelected)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            songList = await apiSrv.GetSongsOnPlaylist();
            ListViewSongs.Visibility = Visibility.Visible;

            ListViewSongs.ItemsSource = null;

            song = songList.FindAll(x => x.PlaylistName.PlaylistName == playlistSelected.PlaylistName);
           
            ListViewSongs.ItemsSource = song;


        }

        private void deleteThisSongClk(object sender, RoutedEventArgs e)
        {
            if ((Login.LoggedInUsers.IsManager && playlistSelected.Owner1.Id == Login.LoggedInUsers.Id) || (!Login.LoggedInUsers.IsManager))
            {
                YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
                var x = (ListViewSongs.SelectedItem as SongsOnPlaylist).Id;
                SongsOnPlaylist sop = ListViewSongs.Items.Cast<SongsOnPlaylist>().FirstOrDefault(item => item.Id == x);
                if (sop == null)
                {
                    ListViewSongs.Visibility = Visibility.Collapsed;
                    ListViewPlaylists.Visibility = Visibility.Visible;
                    GetPlaylistsByUser();
                }
                else
                {
                    apiSrv.DeleteSongOnPlaylist(sop.Id);
                    ListViewSongs.Visibility = Visibility.Collapsed;
                    ListViewPlaylists.Visibility = Visibility.Visible;
                    GetPlaylistsByUser();
                }
            }
            else
                MessageBox.Show("You cant do that");
        }

        private void addSongToaPlaylist(object sender, RoutedEventArgs e)
        {
            if ((Login.LoggedInUsers.IsManager && playlistSelected.Owner1.Id == Login.LoggedInUsers.Id) || (!Login.LoggedInUsers.IsManager))
            {
                YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
                Songs song = (Songs)this.ListViewSongs.SelectedItem;
                sop.PlaylistName.PlaylistName = playlistSelected.PlaylistName;
                sop.SongName.NameSong = song.NameSong;
                apiSrv.InsertSongOnPlaylist(sop);
                ListViewSongs.Visibility = Visibility.Collapsed;
                ListViewPlaylists.Visibility = Visibility.Visible;
                GetPlaylistsByUser();
            }
            else
                MessageBox.Show("you cant do that");
        }

        private void ListViewPlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPlaylist = ListViewPlaylists.SelectedItem as Playlists;
        }

        private void DeleteThisPlaylist(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            if (Login.LoggedInUsers.IsManager && playlistSelected.Owner1.Id == Login.LoggedInUsers.Id || !Login.LoggedInUsers.IsManager)
            {
                var x = (ListViewPlaylists.SelectedItem as Playlists);
                Playlists p = ListViewPlaylists.Items.Cast<Playlists>().FirstOrDefault(Item => Item.Id == x.Id);
                if (p == null)
                {
                    return;
                }
                else
                {
                    apiSrv.DeletePlaylist(p.Id);
                }
            }
            else
                MessageBox.Show("error");
        }

        private void SearchPlaylistTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Login.LoggedInUsers.IsManager)
            {
                string keyword = SearchPlaylistTxtBox.Text.ToLower();
                var list = playlistList.Where(x => x.PlaylistName.ToLower().StartsWith(keyword)).ToList();
                ListViewPlaylists.ItemsSource = list;
                SearchPlaylistTxtBox.Text = "";
            }
            else
            {
                string keyword = SearchPlaylistTxtBox.Text.ToLower();
                var list = (playlistList.Where(x => x.PlaylistName.ToLower().StartsWith(keyword) && x.Owner1.Id == Login.LoggedInUsers.Id));
                ListViewPlaylists.ItemsSource = list;
                SearchPlaylistTxtBox.Text = "";
            }
        }

        private void addPlaylist(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePlaylists());
        }

        private void updatePlaylist(object sender, RoutedEventArgs e)
        {
            if (Login.LoggedInUsers.IsManager && playlistSelected.Owner1.Id == Login.LoggedInUsers.Id || !Login.LoggedInUsers.IsManager)
            {
                NavigationService.Navigate(new UpdatePlaylist(GetPlaylist));
            }
            else
                MessageBox.Show("error");
        }
    }
}
