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
using lightbard.Class;
using Windows.UI.Core;
using System.Collections.ObjectModel;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class ConvePage : Page
  {
    internal Tokens tokens;
    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();

    ObservableCollection<TweetClass.TweetInfo> tweet;
    Tweets data = new Tweets();
    TweetClass.TweetInfo item;
    public Status status { get; set; }
    public ConvePage()
    {
      this.InitializeComponent();
      tokens = data.getToken();

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
      var id  = (long?)e.Parameter;
      tweet = new ObservableCollection<TweetClass.TweetInfo>();
      conv(id);
    }

    private async void conv(long? Id)
    {
      status = await tokens.Statuses.ShowAsync(id => Id);
      data.Addtweet(tweet, status);
      try
      {
        //if (status.ExtendedEntities.UserMentions[0].Id != null)
        //{
        if (status.InReplyToStatusId != null)
        { 
          var rep_status = status.InReplyToStatusId;
          //checkBlock.Text = rep_status.ToString();
        conv(rep_status);
        }
        else
        {
          conveView.ItemsSource = tweet;
        }
      }
      catch(Exception ex) {
        var tes = ex.Message;
        data.toast("2" + tes);
        return;
      }

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
      var item = this.conveView.SelectedItem as TweetClass.TweetInfo;
      ViewModel.TweetIdSet(item.Id);
      itemStock();
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    public void itemStock()
    {
      var item_test = this.conveView.SelectedItem as TweetClass.TweetInfo;
      if (item_test == null)
      {
        return;
      }
      else
      {
        item = this.conveView.SelectedItem as TweetClass.TweetInfo;
      }
    }

    private void conveItem_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ConvePage), item.Id);
    }

    private void TweetsList_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      var item = this.conveView.SelectedItem as TweetClass.TweetInfo;
      ViewModel.TweetIdSet(item.Id);
      itemStock();
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }
  }
}
