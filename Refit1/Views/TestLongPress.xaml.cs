using Plugin.SimpleAudioPlayer;
using Refit1.Effects;
using System.IO;
using System.Reflection;
using System.Threading;
using Xamarin.Forms;

namespace Refit1.Views
{
    public partial class TestLongPress : ContentPage
    {
        public TestLongPress()
        {
            InitializeComponent();
        }

        public void onButttond(object sender, object args)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("Refit1.Files.sound1.mp3");
            var player = CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            player.Play();
            Thread.Sleep(300);
            System.Diagnostics.Debug.WriteLine("Pressed");
        }

        public void onButttonReleasedss(object sender, object args)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("Refit1.Files.light.mp3");
            var player = CrossSimpleAudioPlayer.Current;
            player.Load(audioStream);
            player.Play();
            
            System.Diagnostics.Debug.WriteLine("Releaaased");
        }

        protected override void OnAppearing()
        {
            string _html = "<table>" +
  "<thead>" +
    "<tr>" +
      "<th>Header 1</th>" +
      "<th>Header 2</th>" +
      "<th>Header 3</th>" +
      "<th>Header 4</th>" +
    "</tr>" +
  "</thead>" +
  "<tbody>" +
    "<tr>" +
      "<td>f C</td>" +
      "<td>S C</td>" +
      "<td>T C</td>" +
      "<td>F C</td>" +
    "</tr>" +
     "<tr>" +
      "<td>f C</td>" +
      "<td>S C</td>" +
      "<td>T C</td>" +
      "<td>F C</td>" +
    "</tr>" +
     "<tr>" +
      "<td>f C</td>" +
      "<td>S C</td>" +
      "<td>T C</td>" +
      "<td>F C</td>" +
    "</tr>" +
  "</tbody>" +
"</table>";
            base.OnAppearing();
            string htmlText = "<ul><li>Lorem ipsum dolor sit amet, consectetuer adipiscing elit.</li><li>Aliquam tincidunt mauris eu risus.</li><li>Vestibulum auctor dapibus neque.</li></ul>".Replace(@"\", string.Empty);
           // var browser = new WebView();
            var html = new HtmlWebViewSource
            {
                Html = _html
            };
            wView.Source = html;

            //stck.Children.Add(browser);
        }
    }
}
