using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for UpdateSubscription.xaml
    /// </summary>
    public partial class UpdateSubscription : Page
    {
        SubscriptionsList subscriptionsList = new SubscriptionsList();
        private SubscriptionType subscriptionType;
        public UpdateSubscription(SubscriptionType subscription)
        {
            InitializeComponent();
            GetData(subscription);
            this.subscriptionType = subscription;
        }
        public async Task GetData(SubscriptionType subscription)
        {
            YahelApiService.YahelApiSrv api = new YahelApiService.YahelApiSrv();
            this.SubscriptionNameTextBox.Text = subscription.SubscriptionName;
            this.PriceTextBox.Text = subscription.Price1.ToString();
            this.PeriodTxtBox.Text = subscription.Period1;
            this.NumOfSongsTxtBox.Text = subscription.NumbOfSongs1.ToString();


        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            //לבדוק שזה נכון לכתוב ככה
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            subscriptionType.SubscriptionName = this.SubscriptionNameTextBox.Text.ToString();
            subscriptionType.Price1 = int.Parse(this.PriceTextBox.Text.ToString());
            subscriptionType.Period1 = this.PeriodTxtBox.Text.ToString();
            subscriptionType.NumbOfSongs1 = int.Parse(this.NumOfSongsTxtBox.Text.ToString());
            s.UpdateSubscriptionType(subscriptionType);
        }
    }
}
