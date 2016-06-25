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
using Windows.Storage;
using lightbard.Class;
using CoreTweet;
using Windows.UI.Popups;
using Windows.Storage.Pickers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MentionPage : Page
  {

    internal Tokens tokens;
    Tweets data = new Tweets();
    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();

    ObservableCollection<TweetClass.TweetInfo> tweet;
    public long? UserId { get; set; }
    TweetClass.TweetInfo item;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }
    public string filename { get; set; }
    public FileInfo fileinfo { get; set; }

    FileOpenPicker openPicker = new FileOpenPicker();

    public MentionPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();

      var settings = ApplicationData.Current.RoamingSettings;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (TweetClass.TweetInfo)e.Parameter;
      mentionLoad();
    }

    //tweetをロードするのに使います
    private async void mentionLoad()
    {
      Task<ObservableCollection<TweetClass.TweetInfo>> mentionload = data.mentionload();
      try {
        this.listView.ItemsSource = await mentionload;
      }
      catch(Exception ex)
      {
        
      }
    }

    //ロードボタンです．
    private void reloadButton_Click(object sender, RoutedEventArgs e)
    {
      mentionLoad();
    }

    //リプライページに飛びます．
    private void replyButton_Click(object sender, RoutedEventArgs e)
    {

      this.Frame.Navigate(typeof(ReplayPage), item);
    }

    private void tweetButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(TweetPage));
    }

    //userinfo
    private void userInfoCommand_Click(object sender, RoutedEventArgs e)
    {

      this.Frame.Navigate(typeof(UserPage), item.UserId);
    }

    private void profileImage_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(UserPage), item.UserId);
    }


    private void userInfoItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(UserPage), item.UserId);
    }

    private void TweetsList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      ViewModel.TweetIdSet(item.Id);
      itemStock();
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    public void itemStock()
    {
      var item_test = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item_test == null)
      {
        return;
      }
      else
      {
        item = this.listView.SelectedItem as TweetClass.TweetInfo;
      }
    }

    private void conveItem_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ConvePage), item.Id);
    }

    private void TweetsList_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      ViewModel.TweetIdSet(item.Id);
      itemStock();
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }
  }
}
