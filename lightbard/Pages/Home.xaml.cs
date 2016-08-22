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
using CoreTweet.Streaming;
using Windows.UI.Popups;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Windows.UI.Core;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class Home : Page
  {
    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();
    public ViewModels.TweetPageViewModel ViewModel2 { get; } = new ViewModels.TweetPageViewModel();

    internal Tokens tokens;
    Tweets data = new Tweets();
    ObservableCollection<Models.TweetInfo> tweet;
    ObservableCollection<Models.TweetInfo> tweet2;
    //ObservableCollection<TweetClass.TweetInfo> tweetInfo;

    IConnectableObservable<StreamingMessage> sm_stream;
    IDisposable disposable;

    public long? UserId { get; set; }
    //TweetClass.TweetInfo item;
    Models.TweetInfo item;
    public long? ReplyId { get; set; }

    public string userId { get; set; }
    public string ScreenName { get; set; }
    public string filename { get; set; }
    public FileInfo fileinfo { get; set; }

    FileOpenPicker openPicker = new FileOpenPicker();
    StorageFile file;
    public Home()
    {
      this.InitializeComponent();

      tokens = data.getToken();
      //tweetLoad();
      ViewModel2.GetTimelineInfos();

      var settings = ApplicationData.Current.RoamingSettings;
      tweet = new ObservableCollection<Models.TweetInfo>();
      //tweetInfo = new ObservableCollection<TweetClass.TweetInfo>();

    }

    //page読み込み時
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (Models.TweetInfo)e.Parameter;
      //item = (TweetClass.TweetInfo)e.Parameter;
    }


    //ストリーミング
    private async void streamingtest()
    {
      try {
        tweet2 = ViewModel2.TweetTimeline;
      sm_stream = tokens.Streaming.UserAsObservable().Publish();
      sm_stream.OfType<StatusMessage>().Subscribe(x => streamLoad(x));

      disposable = sm_stream.Connect();
        //testBlock.Text = "接続中です";
        var xx = ViewModel.streamCount();
        int tw_count = int.Parse(xx);
        await Task.Delay(tw_count * 60000);
      disposable.Dispose();
      streamButton.IsChecked = false;
        //testBlock.Text = "接続終了";
      }
      catch(Exception ex)
      { }
    }

    private async void streamLoad(StatusMessage x)
    {
      Status status = x.Status;
      Inserttweet(ViewModel2.TweetTimeline, status);
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      {
        this.listView.ItemsSource = tweet2;
      });
    }

    //private async void Inserttweet(ObservableCollection<TweetClass.TweetInfo> tweet2, Status status)
    private async void Inserttweet(ObservableCollection<Models.TweetInfo> tweet2, Status status)
    {
      //string con = status.Text;
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      {
      if (status.RetweetedStatus != null)
      {
        tweet2.Insert(0, new Models.TweetInfo
        {
          UserName = status.RetweetedStatus.User.Name + " ",
          UserId = status.RetweetedStatus.User.Id,
          ScreenName = "@" + status.RetweetedStatus.User.ScreenName,
          ProfileImageUrl = status.RetweetedStatus.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(status.RetweetedStatus.Text),
          Date = status.RetweetedStatus.CreatedAt.LocalDateTime.ToString(),
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.RetweetedStatus.Id,
          //retUser = status.RetweetedStatus,
          Via = status.RetweetedStatus.Source,
          FavoriteCount = ", Like: " + status.FavoriteCount.ToString(),
          RetweetCount = "Retweet: " + status.RetweetCount.ToString(),

          //Url = m.Value.ToString(),
          RetweetUser = "Retweeted by @" + status.User.ScreenName,
          RetweetUserProfileImageUrl = status.User.ProfileImageUrlHttps,

          urls = status.RetweetedStatus.Entities.Urls,
          //ReplyId = status.RetweetedStatus.InReplyToStatusId

          //media = status.RetweetedStatus.ExtendedEntities.Media
          //arrayB.CopyTo(arrayA, 0)
          //media = status.RetweetedStatus.Entities.Media.CopyTo(media, 0)

        }
          );
      }
      else
      {

        tweet2.Insert(0, new Models.TweetInfo
        {
          UserName = status.User.Name + " ",
          UserId = status.User.Id,
          ScreenName = "@" + status.User.ScreenName,
          ProfileImageUrl = status.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(status.Text),
          Date = status.CreatedAt.LocalDateTime.ToString(),
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.Id,
          //retUser = status.RetweetedStatus,
          //Url = m.Value.ToString(),
          FavoriteCount = ", Like: " + status.FavoriteCount.ToString(),
          RetweetCount = "Retweet: " + status.RetweetCount.ToString(),
          Via = status.Source,
          RetweetUser = null,
          urls = status.Entities.Urls,
          //ReplyId = status.InReplyToStatusId

        }
        );
      }
      });
    }

    private void streamButton_Checked(object sender, RoutedEventArgs e)
    {
      //data.streamingtest(tweet);
      streamingtest();
    }

    private void streamButton_Unchecked(object sender, RoutedEventArgs e)
    {
      try
      {
        disposable.Dispose();
      }
      catch (Exception ex)
      { }
    }


/*
    //tweetをロードするのに使います
    private async void tweetLoad()
    {
      Task<ObservableCollection<Models.TweetInfo>> tweetload = data.tweetload();
      try
      {
        this.listView.ItemsSource = await tweetload;
        tweet = await tweetload;
      }
      catch (Exception ex)
      { }
    }

    //ロードボタンです．
    private void reloadButton_Click(object sender, RoutedEventArgs e)
    {
     // ViewModel2.GetTimelineInfos();

         tweetLoad();
      // streamingtest();
    }
    */

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
      this.Frame.Navigate(typeof(UsersPage), item.UserId);
  }


    private void userInfoItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(UsersPage), item.UserId);
    }

    private void TweetsList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      //var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      item = this.listView.SelectedItem as Models.TweetInfo;
      ViewModel.TweetIdSet(item.Id);
      ViewModel2.TweetIdSet(item.Id);
      itemStock();
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
      ViewModel.TweetIdSet(item.Id);
      ViewModel2.TweetIdSet(item.Id);
      itemStock();
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }


  }

}
