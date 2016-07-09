using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using lightbard.Class;
using CoreTweet;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class ImageView : Page
  {
    MediaEntity item;
    public ImageView()
    {
      this.InitializeComponent();
      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (Frame.CanGoBack)
        {
          Frame.GoBack();
          args.Handled = true;
        }
      };
    }


    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (MediaEntity)e.Parameter;
      var mediaurl = new Uri(item.MediaUrl);
      BitmapImage imageSource = new BitmapImage(mediaurl);
      imageview.Source = imageSource;

      /*
                   var mediaurl = new Uri(status.ExtendedEntities.Media[n].MediaUrl);

            BitmapImage imageSource = new BitmapImage(mediaurl);
       */
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
      if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
    }

    private void saveButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private async void webOpen(string url)
    {
      var uri = new Uri(url);

      var success = await Windows.System.Launcher.LaunchUriAsync(uri);

    }

    private async void webButton_Click(object sender, RoutedEventArgs e)
    {
      var result = await this.webDlg.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        webOpen(item.Url.ToString());
      }
      else if (result == ContentDialogResult.Secondary)
      {
        System.Diagnostics.Debug.WriteLine("Secondary");
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("None");
      }
    }
  }
}
