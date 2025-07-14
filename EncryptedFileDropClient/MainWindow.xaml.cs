using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EncryptedFileDropClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Username_Text_Changed(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {

        }

        // Method to test out API calls from client.
        private async void Button_ApiTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://localhost:7090/api/user");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                ApiTestTextBox.Text = responseBody;
            }
            catch (HttpRequestException ex)
            {
                ApiTestTextBox.Text = $"Error: {ex.Message}";
            }
        }
    }
}