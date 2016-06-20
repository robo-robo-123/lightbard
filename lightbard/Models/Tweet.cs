using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Collections.ObjectModel;
using lightbard.Common;
using lightbard.Class;

namespace lightbard.Models
{

  public class TweetInfo : Common.BindableBase
  {
    public string UserName { get; set; }
    public long? UserId { get; set; }
    public string Text { get; set; }
    public string ScreenName { get; set; }
    public long Id { get; set; }
    public long? ReplyId { get; set; }
    public string ProfileImageUrl { get; set; }
    public string Date { get; set; }
    public string Via { get; set; }
    // public Status retUser { get; set; }
    public string Url { get; set; }
    public string RetweetUser { get; set; }
    public string RetweetUserProfileImageUrl { get; set; }
    public string RetweetCount { get; set; }
    public string FavoriteCount { get; set; }
    public MediaEntity[] media { get; set; }
    public UrlEntity[] urls { get; set; }
    public int media_number { get; set; }
    public int RelativeTime { get; set; }

  }

  public class TweetInfoManager : Common.BindableBase
  {
    public static TweetInfoManager Instance { get; } = new TweetInfoManager();
    Tweets data = new Tweets();
    internal Tokens tokens;
    public TweetInfoManager()
    {
      tokens = data.getToken();
     // data.tweetload2(this.TweetInfos);
    }

    
    public ObservableCollection<TweetInfo> getTweetInfos()
    {
      //this.TweetInfos = new ObservableCollection<TweetInfo>();
    //  data.tweetload2(this.TweetInfos);
      return this.TweetInfos;
    }

    private ObservableCollection<TweetInfo> tweetInfos;
    public ObservableCollection<TweetInfo> TweetInfos
    {
      get { return this.tweetInfos; }
      set { this.SetProperty(ref this.tweetInfos, value); }
    }

    /*
        public ObservableCollection<TweetInfo> TweetInfos { get; set; } = new ObservableCollection<TweetInfo>()
        {

        };
        */
  }

  public class TweetLoad
  {
    public static TweetLoad Instance { get; } = new TweetLoad();

    public TweetInfoManager TweetInfoManager { get; } = new TweetInfoManager();
}

  public class UserInfo : Common.BindableBase
  {
    public string UserName { get; set; }
    public long? UserId { get; set; }
    public string Text { get; set; }
    public string ScreenName { get; set; }
    public long Id { get; set; }
    public string ProfileImageUrl { get; set; }
    public string Prof { get; set; }
    public string Date { get; set; }
    public string Via { get; set; }
    public int FollowCount { get; set; }
    public int FavCount { get; set; }
    public int TweetCount { get; set; }
    public int FollowerCount { get; set; }
    // public string ProfileImageUrl { get; set; }
  }
}
