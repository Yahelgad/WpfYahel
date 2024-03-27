using Microsoft.Win32;
using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModelYahel;
using YahelApiService;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AddSong.xaml
    /// </summary>
    public partial class AddSong : Page
    {
        private string GetFileName { get; set; }

        List<string> GenreStrings = new List<string>();
        //private DateTime selectedDate = new DateTime();

        JenreList gList = new JenreList();
        List<string> ArtistStrings = new List<string>();
        ArtistsList aList = new ArtistsList();
        Jenre Gen = null;
        Artists Art = null;
        SubscriptionType sub = null;
        public AddSong()
        {
            InitializeComponent();
            GetGenre();
        }

        public async Task GetGenre()
        {
            await GetAllGenres();
            this.GenreComboBox.ItemsSource = null;
            this.GenreComboBox.ItemsSource = gList.Select(x => x.JenreName1);
            GetArtists();
        }
        public async Task GetAllGenres()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            gList = await yApi.GetJenres();

        }
        public async Task GetArtists()
        {
            await GetAllArtists();
            this.ArtistComboBox.ItemsSource = null;
            this.ArtistComboBox.ItemsSource = aList.Select(x => x.FullName);

        }
        public async Task GetAllArtists()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            aList = await yApi.GetArtists();


        }



        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (GetFileName != "")
            {

                YahelApiService.YahelApiSrv Y = new YahelApiService.YahelApiSrv();

                Songs song = new Songs();
                song.NameSong = SongNameTextBox.Text.ToString();
                DateTime selectedDate = (DateTime)DateTextBox.SelectedDate;
                song.CreationDate = DateOnly.FromDateTime(selectedDate);
                song.Time = TimeOnly.Parse(timeTxtBox.Text);

                //user. = AgeTextBox.Text.ToString();
                //song.Link = linkTxtBox.Text.ToString();
                song.AudioFilePath1 = "aa";
                song.Link = System.IO.Path.GetFileName(GetFileName);
                SongsDB.SaveAudioFile(GetFileName);

                song.Jenre = gList.Find(x => x.JenreName1 == GenreComboBox.SelectedItem);
                Artists s = aList.Find(x => x.FullName == ArtistComboBox.SelectedItem as string);
                MessageBox.Show(s.ToString());
                if (s != null)
                {
                    song.ArtistName = s;
                }

                Y.InsertSong(song);
            }
        }



        private void UplodSongAction(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dig = new OpenFileDialog
            {
                Filter = "Mp3 Files (*.mp3)|*.mp3",
                Title = "Choose song"
            };

            if (dig.ShowDialog() == true)
            {
                string fileName = dig.FileName;
                GetFileName = fileName;
                MessageBox.Show("קובץ השמע נבחר בהצלחה");

            }


        }
    }

}
