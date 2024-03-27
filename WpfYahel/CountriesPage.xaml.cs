using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for CountriesPage.xaml
    /// </summary>
    public partial class CountriesPage : Page
    {
        private Country GetCountry { get; set; }
        List<Country> cList = new List<Country>();
        public CountriesPage()
        {
            InitializeComponent();
            cCountries();
        }
        private async void cCountries()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            cList = await apiSrv.GetCountries();
            ListViewCountries.ItemsSource = cList;
        }

        private void SearchCountriesTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchCountriesTxtBox.Text.ToLower();
            var list = cList.Where(x => x.CountryName.ToLower().StartsWith(keyword)).ToList();
            ListViewCountries.ItemsSource = list;
            SearchCountriesTxtBox.Text = "";
        }

        private void ListViewCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetCountry = ListViewCountries.SelectedItem as Country;
        }

        private void DeleteThisCountry(object sender, RoutedEventArgs e)
        {
            List<Country> CountriesList = ListViewCountries.ItemsSource as List<Country>;
            CountriesList.Remove(GetCountry);
            ListViewCountries.ItemsSource = null;
            ListViewCountries.ItemsSource = cList;
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            apiSrv.DeleteCountry(GetCountry.Id);
        }

        private void AddCountry(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCountry());
        }
    }
}
