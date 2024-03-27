using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Page
    {
        public AdminHomePage()
        {
            InitializeComponent();

        }

        private void MoveToAddSongPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSong());
        }

        private void MoveToAddGenresPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddGenre());
        }

        private void MoveToAddCountryPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCountry());
        }

        private void MoveToAddLanguagePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddLanguage());
        }

        private void MoveToAddSubscriptionPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSubscription());
        }



        private void MoveToMyPlaylist(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyPlaylists());
        }

        private void MoveToUsersPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersList());
        }

        private void MoveToProfile(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());
        }

        private void MoveToPlaylistsPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MyPlaylists());
        }

        private void MoveToGenresPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenresPage());
        }

        private void MoveToSubscriptionPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SubscriptionsList());
        }

        private void MoveToLanguagesPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LanguagesPAge());
        }

        private void MoveToCountriesPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CountriesPage());
        }

        private void MoveToSongsPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SongsTube4You());
        }

        private void MoveToArtistPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingersTube4You());
        }

        private void MoveToAddArtistPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddArtist());
        }
    }
}
