using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Windows.Storage;
using System.Collections.ObjectModel;
using Windows.UI.Notifications;
using Windows.UI.Core;
using Windows.UI.Xaml;
using System.Diagnostics;
using System.Reactive.Subjects;
using CoreTweet.Streaming;
using System.Reactive.Linq;
using Windows.UI.Xaml.Media.Imaging;

namespace lightbard.Class
{
  class Tweets
  {
    
    internal Tokens tokens;
    ObservableCollection<Models.TweetInfo> tweet;
    ObservableCollection<TweetClass.TweetInfo> reply;
    List<TweetClass.UserInfo> userPro;
    IConnectableObservable<StreamingMessage> sm_stream;
    IDisposable disposable;
    bool x = false;
    int tw_count;

    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();

    public Tweets()
    {
      SaveKey();
      //ViewModel.Load();
    }

    private void SaveKey()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      settings.Values["ApiKey"] = "rXBbSNc4nf01jY2tYYhcQlLdQ";
      settings.Values["ApiSecret"] = "nSaEdTdvVuAHbv8torT1RzvdHRLdF0b6XiUCO5n5Ciq43gv3vs";
    }

    public CoreTweet.Tokens getToken()
    {
      var value = ApplicationData.Current.RoamingSettings;

      if (!string.IsNullOrEmpty((string)value.Values["AccessToken"])

&& !string.IsNullOrEmpty((string)value.Values["AccessTokenSecret"]))

      {

        tokens = Tokens.Create(

            (string)value.Values["ApiKey"]

            , (string)value.Values["ApiSecret"]

            , (string)value.Values["AccessToken"]

            , (string)value.Values["AccessTokenSecret"]
            );
      }
      return tokens;
    }

    //Tweet投稿関連
    public async Task<ObservableCollection<Models.TweetInfo>> tweetload()
    {
      tweet = new ObservableCollection<Models.TweetInfo>();
      try
      {
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("1" + tes);
      }
      try
      {
        foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => tw_count))
        {
          Addtweet(tweet, status);
        }
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("2" + tes);
      }
      return tweet;
    }

    public async void tweetload2(ObservableCollection<Models.TweetInfo> tweet)
    {
      try
      {
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
        toast(tw_count.ToString()+"timeline");
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("1"+tes);
      }
      try
      {
        foreach (var status in await tokens.Statuses.HomeTimelineAsync(count => tw_count))
      {
        Addtweet2(tweet, status);
      }
      }
      catch(Exception ex)
      {
        var tes = ex.Message;
        toast("2"+tes);
      }
    }


    public async Task<ObservableCollection<Models.TweetInfo>> mentionload()
    {
      tweet = new ObservableCollection<Models.TweetInfo>();
      //var value = ApplicationData.Current.RoamingSettings;
      try
      {
        // tw_count = (int)value.Values["tweetsCount"];
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
        toast(tw_count.ToString());
      }
      catch(Exception ex)
      {
        var tes = ex.Message;
        toast("1" + tes);
      }
      foreach (var status in await tokens.Statuses.MentionsTimelineAsync(count => tw_count))
      {
        Addtweet(tweet, status);
      }
      return tweet;
    }


    public async void likeload(ObservableCollection<Models.TweetInfo> tweet, long? UserId)
    {
      try
      {
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
        toast(tw_count.ToString()+"like");
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("1" + tes);
      }
      try
      {
        foreach (var status in await tokens.Favorites.ListAsync(count => tw_count))
        {
          Addtweet2(tweet, status);
        }
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("2" + tes);
      }
    }

    public async void usertweetload(ObservableCollection<Models.TweetInfo> tweet, long? UserId)
    {
      try
      {
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
        toast(tw_count.ToString()+"user timeline");
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("1" + tes);
      }
      try
      {
        foreach (var status in await tokens.Statuses.UserTimelineAsync(user_id => UserId, count => tw_count))
        {
          Addtweet2(tweet, status);
        }
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("2" + tes);
      }
    }

    public async void userfollowerload(ObservableCollection<Models.UserInfo> tweet, long? UserId)
    {
      try
      {
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
        toast(tw_count.ToString()+"fllower");
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("1" + tes);
      }
      try
      {
        foreach (var status in await tokens.Followers.ListAsync(user_id => UserId, count => tw_count))
        {
          AddInfo2(tweet, status);
        }
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("2" + tes);
      }
    }

    public async void userfriendload(ObservableCollection<Models.UserInfo> tweet, long? UserId)
    {
      try
      {
        var x = ViewModel.tweetCount();
        tw_count = int.Parse(x);
        toast(tw_count.ToString()+"friend");
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("1" + tes);
      }
      try
      {
        foreach (var status in await tokens.Friends.ListAsync(user_id => UserId, count => tw_count))
        {
          AddInfo2(tweet, status);
        }
      }
      catch (Exception ex)
      {
        var tes = ex.Message;
        toast("2" + tes);
      }
    }

    //ストリーミング
    public async void streamingtest(ObservableCollection<TweetClass.TweetInfo> tweet)
    {
      try
      {
        sm_stream = tokens.Streaming.UserAsObservable().Publish();
        sm_stream.OfType<StatusMessage>().Subscribe(x => streamLoad(x, tweet));

        disposable = sm_stream.Connect();
        //testBlock.Text = "接続中です";
        var xx = ViewModel.streamCount();
        int tw_count = int.Parse(xx);
        toast(tw_count.ToString());
        await Task.Delay(tw_count * 600);
        disposable.Dispose();
        //streamButton.IsChecked = false;
        //testBlock.Text = "接続終了";
      }
      catch (Exception ex)
      { }
    }

    private async void streamLoad(StatusMessage x, ObservableCollection<TweetClass.TweetInfo> tweet)
    {
      Status status = x.Status;
      Inserttweet(tweet, status);
      //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
      //{
      //  this.listView.ItemsSource = tweet;
      //});
    }

    public async void Inserttweet(ObservableCollection<TweetClass.TweetInfo> tweet2, Status status)
    {
      //string con = status.Text;
     // await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
     //{
        if (status.RetweetedStatus != null)
        {
          tweet2.Insert(0, new TweetClass.TweetInfo
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

          tweet2.Insert(0, new TweetClass.TweetInfo
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
     // });
    }

    public bool xxx(Status status)
    {
      if (status.ExtendedEntities.Media == null)
      { return false; }
      else
      { return true; }
    }

    public async void Addtweet(ObservableCollection<Models.TweetInfo> tweet, Status status)
    {
     // string con = status.Text;
      //BitmapImage tweetImage1 = new BitmapImage();

/*
      if (status.ExtendedEntities.Media == null)
      { x = false; }
      else
      { x = true; }
  */    
  //status.ExtendedEntities.

      /*
      int media_num = status.ExtendedEntities.Media.Length;
      for (int n = 0; n < media_num; n++)
      {
        var mediaurl = new Uri(status.ExtendedEntities.Media[n].MediaUrl);

        BitmapImage imageSource = new BitmapImage(mediaurl);

        switch (n)
        {
          case 0:
            tweetImage1 = imageSource;
            break;
          case 1:
            var tweetImage2 = imageSource;
            break;
          case 2:
            var tweetImage3 = imageSource;
            break;
          case 3:
            var tweetImage4 = imageSource;
            break;

        }
      }
      */

      if (status.RetweetedStatus != null)
      {

        tweet.Add(new Models.TweetInfo
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
          //image = tweetImage1
          //image_check = xxx(status)
          //media = status.RetweetedStatus.ExtendedEntities.Media
          //arrayB.CopyTo(arrayA, 0)
          //media = status.RetweetedStatus.Entities.Media.CopyTo(media, 0)

        }
          );
      }
      else
      {

        tweet.Add(new Models.TweetInfo
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

      

    }

    public void Addtweet2(ObservableCollection<Models.TweetInfo> tweet, Status status)
    {
      string con = status.Text;
      if (status.RetweetedStatus != null)
      {

        tweet.Add(new Models.TweetInfo
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

        tweet.Add(new Models.TweetInfo
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
    }

    //通知関連
    public void toast(string text1)
    {
      // テンプレートのタイプを取得
      var template = ToastTemplateType.ToastText01;
      // テンプレートを取得（XMLDocument"
      var toastXml = ToastNotificationManager.GetTemplateContent(template);
      // textタグを取得（ここに文字列が入る）
      var textTag = toastXml.GetElementsByTagName("text").First();
      // 子要素に文字列を追加
      textTag.AppendChild(toastXml.CreateTextNode(text1));

      // Notifierを作成してShowメソッドで通知
      var notifier = ToastNotificationManager.CreateToastNotifier();
      notifier.Show(new ToastNotification(toastXml));
    }

    //Like関連
    private async void favasync(long itemid)
    {
      try
      {
        await tokens.Favorites.CreateAsync(id => itemid);
        var tes = "'いいね'しました";
        toast(itemid.ToString() + tes);
      }
      catch(Exception ex)
      {
        toast(itemid.ToString() +  ex.Message);
      }
    }

    public void like(long id)
    {
      favasync(id);

    }

    //リツイート
    private async void retweetasync(long itemid)
    {
      try
      {
        await tokens.Statuses.RetweetAsync(id => itemid);
      }
      catch (Exception ex)
      {
      }

    }

    public void retweet(long id)
    {
      retweetasync(id);
      var tes = "リツイートしました";
      toast(tes);
    }


    public void AddInfo(List<TweetClass.UserInfo> userPro, CoreTweet.User user/*=null, CoreTweet.*/)
    {
      userPro.Add(new TweetClass.UserInfo
      {
        UserName = user.Name,
        UserId = user.Id,
        ScreenName = "@" + user.ScreenName,
        ProfileImageUrl = user.ProfileImageUrlHttps,
        FollowCount = user.FollowersCount,
        FavCount = user.FavouritesCount,
        FollowerCount = user.FriendsCount,
        Prof = user.Description

      }
);
    }

    public void AddInfo2(ObservableCollection<Models.UserInfo> userPro, CoreTweet.User user)
    {
      userPro.Add(new Models.UserInfo
      {
        UserName = user.Name,
        UserId = user.Id,
        ScreenName = "@" + user.ScreenName,
        ProfileImageUrl = user.ProfileImageUrlHttps,
        FollowCount = user.FollowersCount,
        FavCount = user.FavouritesCount,
        FollowerCount = user.FriendsCount,
        Prof = user.Description

      }
);
    }

    public ObservableCollection<TweetClass.TweetInfo> replytweetinfo(TweetClass.TweetInfo item)
    {
      //情報を引き出し、ここで画像を取得しよう。
      reply = new ObservableCollection<TweetClass.TweetInfo>();

      reply.Add(new TweetClass.TweetInfo
      {

        UserName = item.UserName,
        UserId = item.UserId,
        ScreenName = item.ScreenName,
        ProfileImageUrl = item.ProfileImageUrl,
        Text = System.Net.WebUtility.HtmlDecode(item.Text),
        Date = item.Date,
        Via = item.Via,
        FavoriteCount = item.FavoriteCount.ToString(),
        RetweetCount = item.RetweetCount.ToString()

      }
      );

      return reply;

    }

    public ObservableCollection<TweetClass.TweetInfo> replytweetinfo2(Status item)
    {
      //情報を引き出し、ここで画像を取得しよう。
      reply = new ObservableCollection<TweetClass.TweetInfo>();
      try
      {
        reply.Add(new TweetClass.TweetInfo
        {

          UserName = item.User.Name,
          UserId = item.User.Id,
          ScreenName = item.User.ScreenName,
          ProfileImageUrl = item.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(item.Text),
          Date = item.CreatedAt.LocalDateTime.ToString(),
          Via = item.Source,
          FavoriteCount = item.FavoriteCount.ToString(),
          RetweetCount = item.RetweetCount.ToString(),
          //urls = item.Entities.Urls,
          //media = item.ExtendedEntities.Media
        }
        );
      }
      catch
      { }
        return reply;

    }

    public void replytweetinfo3(Status item, ObservableCollection<Models.TweetInfo> reply)
    {
      //情報を引き出し、ここで画像を取得しよう。
      try
      {
        reply.Add(new Models.TweetInfo
        {

          UserName = item.User.Name,
          UserId = item.User.Id,
          ScreenName = item.User.ScreenName,
          ProfileImageUrl = item.User.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(item.Text),
          Date = item.CreatedAt.LocalDateTime.ToString(),
          Via = item.Source,
          FavoriteCount = item.FavoriteCount.ToString(),
          RetweetCount = item.RetweetCount.ToString(),
          //urls = item.Entities.Urls,
          //media = item.ExtendedEntities.Media
        }
        );
      }
      catch
      { }

    }

    public void tweetinfo(UserResponse status, ObservableCollection<Models.TweetInfo> reply)
    {
      //情報を引き出し、ここで画像を取得しよう。
      try
      {
        reply.Add(new Models.TweetInfo
        {
          UserName = status.Name + " ",
          UserId = status.Id,
          ScreenName = "@" + status.ScreenName,
          ProfileImageUrl = status.ProfileImageUrlHttps,
          Text = System.Net.WebUtility.HtmlDecode(status.Status.Text),
          Date = status.CreatedAt.LocalDateTime.ToString(),
          //Date = status.RetweetedStatus.CreatedAt.ToString(),
          Id = status.Status.Id,
          //retUser = status.RetweetedStatus,
          //Url = m.Value.ToString(),
          FavoriteCount = ", Like: " + status.Status.FavoriteCount.ToString(),
          RetweetCount = "Retweet: " + status.Status.RetweetCount.ToString(),
          Via = status.Status.Source,
          RetweetUser = null,
          urls = status.Status.Entities.Urls,
          //ReplyId = status.InReplyToStatusId
        }
        );
      }
      catch(Exception ex)
      {
        toast(ex.Message);
      }

    }

    /*
    public void userinfo(UserResponse showedUser, ObservableCollection<Models.UserInfo> user)
    {
      //情報を引き出し、ここで画像を取得しよう。
      try
      {
        user.Add(new Models.UserInfo
        {
          UserName = showedUser.Name,
          UserId = showedUser.Id,
          ScreenName = "@" + showedUser.ScreenName,
          ProfileImageUrl = showedUser.ProfileImageUrlHttps,
          ProfileBackgroundImageUrl = showedUser.ProfileBackgroundImageUrlHttps,
          FollowCount = showedUser.FollowersCount,
          FavCount = showedUser.FavouritesCount,
          FollowerCount = showedUser.FriendsCount,
          Prof = showedUser.Description

        }
);
      }
      catch
      { }

    }
*/
    public void userinfo(UserResponse showedUser, Models.UserInfo user)
    {
      //情報を引き出し、ここで画像を取得しよう。
      try
      {
        user.UserName = showedUser.Name;
        user.UserId = showedUser.Id;
        user.ScreenName = "@" + showedUser.ScreenName;
        user.ProfileImageUrl = showedUser.ProfileImageUrlHttps;
        user.ProfileBackgroundImageUrl = showedUser.ProfileBackgroundImageUrlHttps;
        user.FollowCount = showedUser.FollowersCount;
        user.FavCount = showedUser.FavouritesCount;
        user.FollowerCount = showedUser.FriendsCount;
        user.Prof = showedUser.Description;
        user.Place = showedUser.ProfileLocation.Name;
      }
      catch
      { }

    }

  }
}
