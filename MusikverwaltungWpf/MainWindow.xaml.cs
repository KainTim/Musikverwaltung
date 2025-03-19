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
    DisplayTreeView();
  }

  private void DisplayTreeView()
  {
    var initials = _client.Get<List<string>>("api/Initials")!;
    var root = new TreeViewItem()
    {
      Header = "Interpreten",
    };
    foreach (var initial in initials)
    {
      var initialsTreeview = new TreeViewItem()
      {
        Tag = initial,
        Header = initial,
      };
      root.Items.Add(initialsTreeview);
      var artistRequest = new RestRequest("Artists/initial");
      artistRequest.AddQueryParameter("initial", initial);
      var initialArtists = _client.Get<List<ArtistDto>>(artistRequest)!;
      foreach (var artist in initialArtists)
      {
        var artistTreeview = new TreeViewItem()
        {
          Tag = artist,
          Header = artist.ArtistName,
        };
        initialsTreeview.Items.Add(artistTreeview);

        var RecordRequest = new RestRequest("api/Records");
        RecordRequest.AddQueryParameter("artistId", artist.Id);
        var records = _client.Get<List<RecordDto>>(RecordRequest)!;
        foreach (var record in records)
        {
          var recordTreeview = new TreeViewItem()
          {
            Tag = record,
            Header = record.RecordTitle,
          };
          artistTreeview.Items.Add(recordTreeview);
        }
      }
    }
    trvMusic.Items.Add(root);
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
    var artistDto = trvMusic.SelectedValue as ArtistDto;
    if (artistDto == null) return;
    ArtistDto[] artistDtos = new ArtistDto[cbxArtists.Items.Count];
    cbxArtists.Items.CopyTo(artistDtos, 0);
    cbxArtists.SelectedItem = artistDtos.FirstOrDefault(x => x.Id == artistDto.Id);
    DisplayRecords();
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
    DisplayBarChart();
  }

  private void DisplayBarChart()
  {
    var recordTypeDtos = _client.Get<List<RecordTypeDto>>("api/RecordTypes")!;

    ArtistDto? artist = cbxArtists.SelectedItem as ArtistDto;
    if (artist == null) return;
    var restRequest = new RestRequest("api/Records");
    restRequest.AddQueryParameter("artistId", artist.Id);
    var records = _client.Get<List<RecordDto>>(restRequest)!;

    stkBarChart.Children.Clear();
    foreach (var type in recordTypeDtos)
    {
      stkBarChart.Children.Add(
        new ProgressBar()
        {
          Value = records.Count(x => x.RecordTypeId == type.Id),
          Margin = new Thickness(5),
          Height = 200,
          Width = 50,
          Maximum = records.Count,
          Minimum = 0,
          Orientation = Orientation.Vertical
        });
    }
  }
}
