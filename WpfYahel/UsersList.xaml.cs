using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for UsersList.xaml
    /// </summary>
    public partial class UsersList : Page
    {
        List<Users> uList = new List<Users>();
        List<Playlists> pList = new List<Playlists>();
        List<SongsOnPlaylist> sList = new List<SongsOnPlaylist>();
        PlaylistsList playlistList = new PlaylistsList();
        SongsOnPlaylistList songList = new SongsOnPlaylistList();
        List<Playlists> pls = new List<Playlists>();
        SongsOnPlaylist sop = new SongsOnPlaylist();
        List<SongsOnPlaylist> song = new List<SongsOnPlaylist>();
        private Users GetUsers { get; set; }
        private Playlists GetPlaylists { get; set; }
        public UsersList()
        {
            InitializeComponent();
            Users();
        }
        private async void Users()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            uList = await apiSrv.GetUsers() as List<Users>;
            ListViewUsers.ItemsSource = uList;

        }
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            var x = (ListViewUsers.SelectedItem as Users);
            Users u = ListViewUsers.Items.Cast<Users>().FirstOrDefault(Item => Item.Id == x.Id);
            if (u == null)
            {
                return;
            }
            else
            {
                apiSrv.DeleteUser(u.Id);
            }
        }



        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchUsersTxtBox.Text.ToLower();
            var list = uList.Where(x => x.FirstName1.ToLower().StartsWith(keyword)).ToList();
            ListViewUsers.ItemsSource = list;
            SearchUsersTxtBox.Text = "";
        }

        private void UpdateUser(object sender, RoutedEventArgs e)
        {

        }

        private void ManagerBtn(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            Users u = (Users)ListViewUsers.SelectedItem as Users;
            if (u.IsManager)
            {
                if (u.Id != Login.LoggedInUsers.Id)
                {
                    u.IsManager = false;
                    apiSrv.UpdateUser(u);
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (u.Id != Login.LoggedInUsers.Id)
                {
                    u.IsManager = true;
                    apiSrv.UpdateUser(u);
                }
            }
        }

        private void MoveToPlaylists(object sender, RoutedEventArgs e)
        {
            ListViewUsers.Visibility = Visibility.Collapsed;
            SearchUsersTxtBox.Visibility = Visibility.Collapsed;

            pPlaylist();
        }

        private async void pPlaylist()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            playlistList = await apiSrv.GetPlaylists();
            pls = playlistList.FindAll(x => x.Owner1.Id == GetUsers.Id);
            SearchPlaylistsTxtBox.Visibility = Visibility.Visible;
            ListViewPlaylists.ItemsSource = pls;
            ListViewPlaylists.Visibility = Visibility.Visible;

        }
        private void moveToSongsOnPlaylist(object sender, RoutedEventArgs e)
        {
            ListViewPlaylists.Visibility = Visibility.Collapsed;
            SearchPlaylistsTxtBox.Visibility = Visibility.Collapsed;
            MoveToSongsOnPlaylist();
        }
        private async Task MoveToSongsOnPlaylist()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            songList = await apiSrv.GetSongsOnPlaylist();
            song = songList.FindAll(x => x.PlaylistName.PlaylistName == GetPlaylists.PlaylistName);
            SearchSongsOnPlaylistTxtBox.Visibility = Visibility.Visible;
            ListViewSongs.ItemsSource = song;
            ListViewSongs.Visibility = Visibility.Visible;

        }
        private void ListViewUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetUsers = ListViewUsers.SelectedItem as Users;
        }

        private void SearchPlaylistsTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchPlaylistsTxtBox.Text.ToLower();
            var list = pList.Where(x => x.PlaylistName.ToLower().StartsWith(keyword)).ToList(); /*&&*/
            ListViewPlaylists.ItemsSource = list;
            SearchUsersTxtBox.Text = "";
        }

        private void SearchSongsOnPlaylistTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchSongsOnPlaylistTxtBox.Text.ToLower();
            var list = sList.Where(x => x.SongName.NameSong.ToLower().StartsWith(keyword)).ToList(); /*&&*/
            ListViewSongs.ItemsSource = list;
            SearchUsersTxtBox.Text = "";
        }

        private void ListViewPlaylists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetPlaylists = ListViewPlaylists.SelectedItem as Playlists;
        }
    }
}
