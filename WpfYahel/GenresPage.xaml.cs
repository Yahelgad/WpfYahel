using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for GenresPage.xaml
    /// </summary>
    public partial class GenresPage : Page
    {
        JenreList jList = new JenreList();
        private Jenre GetGenre { get; set; }
        public GenresPage()
        {
            InitializeComponent();
            gGenres();

        }
        private async void gGenres()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            jList = await apiSrv.GetJenres();
            ListViewGenre.ItemsSource = jList;
        }

        private void ListViewGenres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetGenre = ListViewGenre.SelectedItem as Jenre;
        }

        private void DeleteThisGenre(object sender, RoutedEventArgs e)
        {
            List<Jenre> JenresList = ListViewGenre.ItemsSource as List<Jenre>;
            JenresList.Remove(GetGenre);

            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            apiSrv.DeleteJenre(GetGenre.Id);
            ListViewGenre.ItemsSource = null;
            ListViewGenre.ItemsSource = JenresList;


        }

        private void SearchGenreTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchGenreTxtBox.Text.ToLower();
            var list = jList.Where(x => x.JenreName1.ToLower().StartsWith(keyword)).ToList();
            ListViewGenre.ItemsSource = list;
            SearchGenreTxtBox.Text = "";
        }

        private void AddGenrePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddGenre());
        }
    }
}
