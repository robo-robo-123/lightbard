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
using CoreTweet;
using Windows.Storage;
//using MyToolkit;
using Windows.UI.Core;
using Windows.UI.Popups;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class SettingsFrame : Page
  {
    //OAuth.OAuthSession session;
    internal Tokens tokens;

    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();

    public SettingsFrame()
    {
      this.InitializeComponent();
      version();

      this.blockFrame.Navigate(typeof(Pages.BlockPage));
      release();
      

    }



private void BackButton_Click(object sender, RoutedEventArgs e)
    {
      if (rootPivot.SelectedIndex > 0)
      {
        // If not at the first item, go back to the previous one.
        rootPivot.SelectedIndex -= 1;
      }
      else
      {
        // The first PivotItem is selected, so loop around to the last item.
        rootPivot.SelectedIndex = rootPivot.Items.Count - 1;
      }
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
      if (rootPivot.SelectedIndex < rootPivot.Items.Count - 1)
      {
        // If not at the last item, go to the next one.
        rootPivot.SelectedIndex += 1;
      }
      else
      {
        // The last PivotItem is selected, so loop around to the first item.
        rootPivot.SelectedIndex = 0;
      }
    }

    private void PivotItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      ///this.testFrame.Navigate(typeof(Settings.AccountPage));

    }

    private void registButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(Settings.AccountPage));
    }

    private void version()
    {
      var versionInfo = Windows.ApplicationModel.Package.Current.Id.Version;
      string version = string.Format(
                         "{0}.{1}.{2}.{3}",
                         versionInfo.Major, versionInfo.Minor,
                         versionInfo.Build, versionInfo.Revision);
      versionBlock.Text = version;
    }
    private void release()
    {
      var str = "2015/12/02にVer.2を公開。\n2015/12/05に会話機能を追加";
      releaseBlock.Text = str;
    }

    private void testButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void viewToggle_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      //testBlock.Text = "changedtest";
    }

    private async void viewToggle_Toggled(object sender, RoutedEventArgs e)
    {
     // var dlg = new MessageDialog("反映するには再起動してください", "完了");
     // await dlg.ShowAsync();
    }



    private async void cancelAccount()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      settings.Values["AccessToken"] = null;
      settings.Values["AccessTokenSecret"] = null;
      settings.Values["ScreenName"] = null;
      settings.Values["UserId"] = null;

      var dlg = new MessageDialog("アカウントを削除しました。再度ログインするか、このアプリを再起動してください。", "ログインの確認");
      dlg.Commands.Add(new UICommand("はい", (cmd) => { }));
      await dlg.ShowAsync();


    }

    private async void deletButton_Click(object sender, RoutedEventArgs e)
    {
      var dlg = new MessageDialog("アカウントを削除しますか？", "ログインの確認");
      dlg.Commands.Add(new UICommand("はい", (cmd) => { cancelAccount(); }));
      dlg.Commands.Add(new UICommand("いいえ", (cmd) => {  }));
      await dlg.ShowAsync();
    }

    private async void tweetsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var x = ViewModel.streamCount();
      //int tw_count = int.Parse(x);
      //tw_count = 100;
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      {
        test.Text = x;
      });
    }
  }
}
