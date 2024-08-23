using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;

namespace MultiSrceenApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = this;
    }

    private Screen[] _screens;
    public Screen[] Screens
    {
        get
        {
            return _screens;
        }
        set
        {
            _screens = value;
            OnPropertyChanged();
        }
    }


    private void btnKlick_Click(object sender, RoutedEventArgs e)
    {
        this.Screens = Screen.AllScreens;
    }

    private async void btnMessage_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.Controls.Button? button = sender as System.Windows.Controls.Button;
        if (button == null)
        {
            return;
        }

        if (button.DataContext is Screen screen)
        {
            //string messageText = "Hallo Welt!";
            string messageText = await GetSampleTextAsync();

            MessageWindow messageWindow = new MessageWindow(messageText)
            {
                Title = "Message",
                WindowStartupLocation = WindowStartupLocation.Manual,
                Topmost = true
            };

            messageWindow.Left = screen.Bounds.Left + screen.WorkingArea.Width / 2 - messageWindow.Width / 2;
            messageWindow.Top = screen.Bounds.Top + screen.WorkingArea.Height / 2 - messageWindow.Height / 2;

            messageWindow.Show();
        }
    }

    private async Task<string> GetSampleTextAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "https://loripsum.net/api/3/long/plaintext";
            return await client.GetStringAsync(url);
        }
    }

}
