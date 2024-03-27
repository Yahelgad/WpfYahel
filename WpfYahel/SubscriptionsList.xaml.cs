using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for SubscriptionsList.xaml
    /// </summary>
    public partial class SubscriptionsList : Page
    {
        List<SubscriptionType> stList = new List<SubscriptionType>();
        private SubscriptionType GetSubscription { get; set; }
        public SubscriptionsList()
        {
            InitializeComponent();
            SubscriptionView();
        }
        private async void SubscriptionView()
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            stList = await apiSrv.GetSubscriptionsTypes() as List<SubscriptionType>;
            ListViewSubscriptions.ItemsSource = null;
            ListViewSubscriptions.ItemsSource = stList;
        }



        private void DeleteSubscriptionType(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv apiSrv = new YahelApiService.YahelApiSrv();
            apiSrv.DeleteSubscriptionType(GetSubscription.Id);
            stList.Remove(GetSubscription);
            ListViewSubscriptions.ItemsSource = null;
            ListViewSubscriptions.ItemsSource = stList;

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = SearchSubscriptionTxtBox.Text.ToLower();
            var list = stList.Where(x => x.SubscriptionName.ToLower().StartsWith(keyword)).ToList();
            ListViewSubscriptions.ItemsSource = list;
            SearchSubscriptionTxtBox.Text = "";
        }

        private void AddSubscriptionType(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSubscription());
        }

        private void UpdateSubscriptionType(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpdateSubscription(GetSubscription));

        }

        private void ListViewSubscriptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetSubscription = ListViewSubscriptions.SelectedItem as SubscriptionType;
        }
    }
}
