using App1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MyToolkit.Multimedia;
using Windows.UI.Popups;

namespace App1
{
    public sealed partial class VideoPage : Page
    {
        Video video;
        public VideoPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    video = e.Parameter as Video;
                    var Url = await YouTube.GetVideoUriAsync(video.Id, YouTubeQuality.Quality1080P);
                    Player.Source = Url.Uri;
                }
                else
                {
                    MessageDialog message = new MessageDialog("You are not connected to the Internet");
                    await message.ShowAsync();
                    this.Frame.GoBack();
                }
            }
            catch { }
            base.OnNavigatedTo(e);
        }


        private void btnHomePage_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), new object());
        }
    }
}
