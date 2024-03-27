using Model;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using YahelApiService;
namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private readonly Regex phoneRegex = new Regex(@"05\d{8}$");
        private readonly Regex gmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@gmail\.com$");
        List<string> subscriptionStrings = new List<string>();
        SubscriptionTypeList stList = new SubscriptionTypeList();
        CountryList cList = new CountryList();
        List<string> countryStrings = new List<string>();
        List<string> languageStrings = new List<string>();
        LanguageList lList = new LanguageList();
        Language lan = null;
        Country con = null;
        SubscriptionType sub = null;
        public Register()
        {
            InitializeComponent();
            GetCountry();




        }
        public async Task GetCountry()
        {
            await GetAllCountries();
            this.countryList.ItemsSource = null;
            this.countryList.ItemsSource = cList.Select(x => x.CountryName);

            GetSubscription();
        }
        public async Task GetAllCountries()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            cList = await yApi.GetCountries();

        }
        public async Task GetSubscription()
        {
            await GetAllSubscriptions();
            this.subscriptionList.ItemsSource = null;
            this.subscriptionList.ItemsSource = stList.Select(x => x.SubscriptionName);
            GetLanguages();
        }
        public async Task GetAllSubscriptions()
        {

            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            stList = await yApi.GetSubscriptionsTypes();


        }
        public async Task GetLanguages()
        {
            await GetAllLanguages();
            this.languageList.ItemsSource = null;
            this.languageList.ItemsSource = lList.Select(x => x.LanguageName);

        }
        public async Task GetAllLanguages()
        {
            try
            {
                YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
                lList = await yApi.GetLanguages();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void movetoLoginClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }

        private async void signUpClick(object sender, RoutedEventArgs e)
        {
            if (!phoneRegex.IsMatch(PhoneTextBox.Text.ToString()))
            {
                MessageBox.Show("Phone number is not correct");
                return;
            }
            if (!gmailRegex.IsMatch(GmailTextBox.Text.ToString()))
            {
                MessageBox.Show("Gmail is not correct");
                return;
            }

            YahelApiService.YahelApiSrv Y = new YahelApiService.YahelApiSrv();

            Users user = new Users();
            user.FirstName1 = FirstNameTextBox.Text.ToString();
            user.LastName1 = LastNameTextBox.Text.ToString();
            //user. = AgeTextBox.Text.ToString();
            user.Phone1 = PhoneTextBox.Text.ToString();
            user.Gmail1 = GmailTextBox.Text.ToString();
            user.BirthYear = BirthYearTextBox.Text.ToString();
            user.Language1 = lList.Find(x => x.LanguageName == languageList.SelectedItem);
            user.Country1 = cList.Find(x => x.CountryName == countryList.SelectedItem);
            user.IsManager = false;
            user.Subscription1 = stList.Find(x => x.SubscriptionName == subscriptionList.SelectedItem);
            Y.InsertUser(user);

        }

        private void languageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
