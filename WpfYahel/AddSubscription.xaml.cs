using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AddSubscription.xaml
    /// </summary>
    public partial class AddSubscription : Page
    {
        public AddSubscription()
        {
            InitializeComponent();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            SubscriptionType subscription = new SubscriptionType();
            subscription.SubscriptionName = SubscriptionNameTextBox.Text.ToString();
            subscription.Price1 = int.Parse(PriceTextBox.Text);
            subscription.Period1 = PeriodTxtBox.Text.ToString();
            subscription.NumbOfSongs1 = int.Parse(NumOfSongsTxtBox.Text);
            s.InsertSubscriptionType(subscription);
        }
    }
}
