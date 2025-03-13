using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Musikverwaltung.Dtos;

using RestSharp;

namespace MusikverwaltungWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  public MainWindow() => InitializeComponent();
  RestClient _client = new("http://localhost:5000");

  private void Window_Loaded(object sender, RoutedEventArgs e)
  {
    DisplayArtists();
  }

  private void DisplayArtists()
  {
    var artists = _client.Get<List<ArtistDto>>("Artists");
    cbxArtists.ItemsSource = artists;
    cbxArtists.SelectedIndex = 0;
  }

  private void dgRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    DisplaySongs();
  }

  private void DisplaySongs()
  {
    var selectedRecord = dgRecords.SelectedItem as RecordDto;
    if (selectedRecord == null) return;
    var restRequest = new RestRequest("api/Songs");
    restRequest.AddQueryParameter("recordId", selectedRecord.Id);
    var songs = _client.Get<List<SongDto>>(restRequest);
    dgSongs.ItemsSource = songs;
    dgSongs.SelectedIndex = 0;
  }

  private void dgSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {

  }

  private void trvMusic_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
  {

  }

  private void cbxArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    DisplayRecords();
  }

  private void DisplayRecords()
  {
    ArtistDto? artist = cbxArtists.SelectedItem as ArtistDto;
    if (artist == null) return;
    var restRequest = new RestRequest("api/Records");
    restRequest.AddQueryParameter("artistId", artist.Id);
    var records = _client.Get<List<RecordDto>>(restRequest);
    dgRecords.ItemsSource = records;
    dgRecords.SelectedIndex = 0;
  }
}
