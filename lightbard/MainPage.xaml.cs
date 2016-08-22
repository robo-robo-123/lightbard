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
using Windows.UI.Core;
using Windows.Storage;
using CoreTweet;
using lightbard.Class;
using Windows.UI.Popups;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace lightbard
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {

    public ViewModels.UserPageViewModel ViewModel3 { get; } = new ViewModels.UserPageViewModel();
    internal Tokens tokens;
    Tweets data = new Tweets();
    public MainPage()
        {
            this.InitializeComponent();
      data.LoadKey();
      var reg = new lightbard.Class.ClientData();
      tokens = data.getToken();
      nameChange();
      //this.testFrame.Navigate(typeof(Pages.HomeFrame));
      this.testFrame.Navigate(typeof(Pages.MainFrame));


      SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (testFrame.CanGoBack)
        {
          testFrame.GoBack();
          args.Handled = true;
        }
      };/**/
      //UpdateBackButtonState();
    }

    private void UpdateBackButtonState()
    {
      var rootFrame = (Frame)Window.Current.Content;
      SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = rootFrame.CanGoBack ?
          AppViewBackButtonVisibility.Visible :
          AppViewBackButtonVisibility.Collapsed;

    }

    private async void nameChange()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      try
      {
        titleBlock.Text = (string)settings.Values["ScreenName"];
      }
      catch
      {
        titleBlock.Text = "";
        var dlg = new MessageDialog("ログインされていません．ログインしますか？「はい」で設定ページへ移動します．", "ログインの確認");
        dlg.Commands.Add(new UICommand("はい", (cmd) => { this.testFrame.Navigate(typeof(SettingsFrame)); }));
        dlg.Commands.Add(new UICommand("いいえ", (cmd) => {  }));
        await dlg.ShowAsync();

      }
    }



    private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
    {
      // 適切な移動先のページに移動し、新しいページを構成します。
      // このとき、必要な情報をナビゲーション・パラメータとして渡します
      //this.testFrame.Navigate(typeof(Pages.HomeFrame));
      this.testFrame.Navigate(typeof(Pages.MainFrame));
    }

    private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
    {
      //this.testFrame.Navigate(typeof(Pages.HomeFrame));
      this.testFrame.Navigate(typeof(Pages.MainFrame));

    }

    private void Grid_Tapped_2(object sender, TappedRoutedEventArgs e)
    {

      this.testFrame.Navigate(typeof(SettingsFrame));

    }

    private void Grid_Tapped_3(object sender, TappedRoutedEventArgs e)
    {
      var settings = ApplicationData.Current.RoamingSettings;
      var UserId = (long?)settings.Values["UserId"];
      ViewModel3.UserIdSet(UserId);
      ViewModel3.GetUserInfos();

      this.testFrame.Navigate(typeof(Pages.UsersPage), (long?)settings.Values["UserId"]);

    }

    private void Grid_Tapped_4(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.MentionPage));
    }

    private void Grid_Tapped_5(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.SearchPage));
    }

    private void Grid_Tapped_6(object sender, TappedRoutedEventArgs e)
    {
      this.testFrame.Navigate(typeof(Pages.StreamingPage));
    }
  }
}
