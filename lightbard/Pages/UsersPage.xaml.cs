using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class UsersPage : Page
  {
    public long? UserId { get; set; }
    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();
    public ViewModels.TweetPageViewModel ViewModel2 { get; } = new ViewModels.TweetPageViewModel();
    public ViewModels.UserPageViewModel ViewModel3 { get; } = new ViewModels.UserPageViewModel();
    Models.TweetInfo item;
    Models.UserInfo item_user;

    public UsersPage()
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
      UserId = (long?)e.Parameter;
      ViewModel3.UserIdSet(UserId);
      ViewModel3.GetUserInfos();
      ViewModel3.GetUserTweetLists();
      ViewModel3.GetUserLikeLists();
      ViewModel3.GetUserFollowerLists();
      ViewModel3.GetUserFriendLists();
      ViewModel3.GetUserInfos();

      UserLoad();

      //this.tweetFrame.Navigate(typeof(Home));
    }

    private async void UserLoad()
    {
      await Task.Delay(1000);
      profView.Header = ViewModel3.UserInfos;
      profView.DataContext = ViewModel3.UserInfos;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      ViewModel3.GetUserInfos();
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
      this.Frame.Navigate(typeof(UsersPage), item.UserId);
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
      try
      {
        item = this.listView.SelectedItem as Models.TweetInfo;
        ViewModel.TweetIdSet(item.Id);
        ViewModel2.TweetIdSet(item.Id);
        //itemStock();
      }
      catch(Exception ex)
      {
        
      }
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    public void itemStock()
    {
      var item_test = this.listView.SelectedItem as Models.TweetInfo;
      if (item_test == null)
      {
        return;
      }
      else
      {
        //item = this.listView.SelectedItem as TweetClass.TweetInfo;
        item = this.listView.SelectedItem as Models.TweetInfo;
      }
    }

    private void conveItem_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ConvePage), item.Id);
    }

    private void TweetsList_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      item = this.listView.SelectedItem as Models.TweetInfo;
      //var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      //ViewModel.TweetIdSet(item.Id);
      ViewModel2.TweetIdSet(item.Id);
      itemStock();
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }


    private void likeList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      try
      {
        item = this.likeView.SelectedItem as Models.TweetInfo;
        //ViewModel.TweetIdSet(item.Id);
        ViewModel3.UserIdSet(item.UserId);
        //ViewModel2.TweetIdSet(item.Id);
        //itemStock();
      }
      catch (Exception ex)
      {

      }
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private void friendList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      try
      {
        item_user = this.friendView.SelectedItem as Models.UserInfo;
        //ViewModel.TweetIdSet(item.Id);
        ViewModel3.UserIdSet(item_user.UserId);
        //itemStock();
      }
      catch (Exception ex)
      {

      }
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private void followList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      try
      {
        item_user = this.followView.SelectedItem as Models.UserInfo;
        //ViewModel.TweetIdSet(item.Id);
        ViewModel3.UserIdSet(item_user.UserId);
        //itemStock();
      }
      catch (Exception ex)
      {

      }
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private void userInfoItem2_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(UsersPage), item_user.UserId);

    }

  }
}
