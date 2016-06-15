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

    internal Tokens tokens;
    Tweets data = new Tweets();
    ObservableCollection<TweetClass.TweetInfo> tweet;

    IConnectableObservable<StreamingMessage> sm_stream;
    IDisposable disposable;

    public long? UserId { get; set; }
    TweetClass.TweetInfo item;
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
        tweetLoad();

      var settings = ApplicationData.Current.RoamingSettings;
      tweet = new ObservableCollection<TweetClass.TweetInfo>();

    }

    //page読み込み時
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      item = (TweetClass.TweetInfo)e.Parameter;
    }

    //tweetをロードするのに使います
    private async void tweetLoad()
    {
      Task<ObservableCollection<TweetClass.TweetInfo>> tweetload = data.tweetload();
      try {
        this.listView.ItemsSource = await tweetload;
        tweet = await tweetload;
      }
      catch(Exception ex)
      { }
    }

    //ストリーミング
    private async void streamingtest()
    {
      sm_stream = tokens.Streaming.UserAsObservable().Publish();
      sm_stream.OfType<StatusMessage>().Subscribe(x => streamLoad(x));

      disposable = sm_stream.Connect();
      //testBlock.Text = "接続中です";

      await Task.Delay(300 * 1000);
      disposable.Dispose();
      //testBlock.Text = "接続終了";
    }

    private async void streamLoad(StatusMessage x)
    {
      Status status = x.Status;
      Inserttweet(tweet, status);
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      {
        this.listView.ItemsSource = tweet;
      });
    }

    private async void Inserttweet(ObservableCollection<TweetClass.TweetInfo> tweet2, Status status)
    {
      //string con = status.Text;
      await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      {
      if (status.RetweetedStatus != null)
      {
        tweet.Insert(0, new TweetClass.TweetInfo
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

        tweet.Insert(0, new TweetClass.TweetInfo
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


    //ロードボタンです．
    private void reloadButton_Click(object sender, RoutedEventArgs e)
    {
        tweetLoad();
     // streamingtest();
    }

    //リプライページに飛びます．※ツイートに関しては，下のツイートと共通化させたい．
    private void replyButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void tweetButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(TweetPage));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    //お気に入り．
    private void favoriteButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        data.like(item.Id);
      }
    }

    //リツイート
    private void retweetButton_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        data.retweet(item.Id);
      }
    }
    
    ///

    //userinfo
    private void userInfoCommand_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void profileImage_Tapped(object sender, TappedRoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
  }


    private void userInfoItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(UserPage), item_send.UserId);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void TweetsList_Tapped(object sender, TappedRoutedEventArgs e)
    {
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private void conveItem_Click(object sender, RoutedEventArgs e)
    {
      var item = this.listView.SelectedItem as TweetClass.TweetInfo;
      if (item == null)
      {
        return;
      }
      else
      {
        var item_send = this.listView.SelectedItem as TweetClass.TweetInfo;
        this.Frame.Navigate(typeof(ConvePage), item_send.Id);
        //rootFrame.Navigate(typeof(ReplayPage), item_send);
      }
    }

    private void TweetsList_RightTapped(object sender, RightTappedRoutedEventArgs e)
    {
      FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private void streamButton_Checked(object sender, RoutedEventArgs e)
    {
      streamingtest();
    }

    private void streamButton_Unchecked(object sender, RoutedEventArgs e)
    {
      disposable.Dispose();
    }
  }

}
