using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            TextUser.Text += " " + Login.LoggedInUsers.FullName + " ";
            CurrentDateText.Text += " " + DateTime.Now.TimeOfDay.ToString();
        }
       
        public async Task GetName()
        {
            //YahelApiService.YahelApiSrv api = new YahelApiService.YahelApiSrv();
            //UsersList uList = await api.GetUsers();
            //Users usr = uList.Find(x => x.Id == Login.LoggedInUsers.Id);
            //this.FullName.Text = usr.FirstName1 + " " + usr.LastName1;
        }
        private void ProfileBtn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }

        private void SongsBtn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SongsTube4You());
        }

        private void PlaylistsBtn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyPlaylists());
        }

        private void ArtistsBtn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingersTube4You());
        }
    }
}
