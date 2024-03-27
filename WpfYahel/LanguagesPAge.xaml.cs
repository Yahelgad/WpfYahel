using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for LanguagesPAge.xaml
    /// </summary>
    public partial class LanguagesPAge : Page
    {
        private Language GetLanguage { get; set; }
        LanguageList lList = new LanguageList();

        public LanguagesPAge()
        {
            InitializeComponent();
            lLanguges();
        }

        private async void lLanguges()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            lList = await apiSrv.GetLanguages();
            ListViewLanguage.ItemsSource = lList;
        }
        private void AddLanguage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddLanguage());
        }

        private void DeleteThisLanguage(object sender, RoutedEventArgs e)
        {
            List<Language> LanguagesList = ListViewLanguage.ItemsSource as List<Language>;
            LanguagesList.Remove(GetLanguage);
            ListViewLanguage.ItemsSource = null;
            ListViewLanguage.ItemsSource = lList;
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            apiSrv.DeleteLanguage(GetLanguage.Id);
        }

        private void ListViewLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetLanguage = ListViewLanguage.SelectedItem as Language;
        }

        private void SearchLanguageTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchLanguageTxtBox.Text.ToLower();
            var list = lList.Where(x => x.LanguageName.ToLower().StartsWith(keyword)).ToList();
            ListViewLanguage.ItemsSource = list;
            SearchLanguageTxtBox.Text = "";
        }
    }
}
