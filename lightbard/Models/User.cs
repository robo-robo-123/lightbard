using CoreTweet;
using lightbard.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lightbard.Models
{
  public class UserInfo : Common.BindableBase
  {
    public string UserName { get; set; }
    public long? UserId { get; set; }
    public string Text { get; set; }
    public string ScreenName { get; set; }
    public long Id { get; set; }
    public string ProfileImageUrl { get; set; }
    public string ProfileBackgroundImageUrl { get; set; }
    public string Prof { get; set; }
    public string Date { get; set; }
    public string Via { get; set; }
    public int FollowCount { get; set; }
    public int FavCount { get; set; }
    public int TweetCount { get; set; }
    public int FollowerCount { get; set; }
    public string Place { get; set; }

    //public string ProfileImageUrl { get; set; }
  }

  public class UserInfoManager : Common.BindableBase
  {
    public static UserInfoManager Instance { get; } = new UserInfoManager();
    Tweets data = new Tweets();
    internal Tokens tokens;
    public UserInfoManager()
    {
      tokens = data.getToken();
      // data.tweetload2(this.TweetInfos);
      // getTweet();
    }

    private long? userId;

    public long? UserId
    {
      get { return this.userId; }
      set { this.SetProperty(ref this.userId, value); }
    }

    public async void getUser()
    {
      try
      {
        var status = await tokens.Users.ShowAsync(id => this.UserId);
        data.userinfo(status, this.UserInfos);
      }
      catch (Exception ex)
      {
        this.UserInfos.Text = ex.Message;
      }
    }

    public void getUserList()
    {
      try
      {
        //var status = await tokens.Statuses.ShowAsync(id => this.TweetId);
        // data.replytweetinfo3(status, this.TweetInfos);
        //data.tweetload2(this.TweetTimeline);
      }
      catch (Exception ex)
      {
        this.UserLists.Add(new Models.UserInfo
        {
          Text = ex.Message
        }
                );
      }
    }

    public void getUserTweet()
    {
      try
      {
        this.UserTweetLists.Clear();
        data.usertweetload(this.UserTweetLists, this.UserId);
      }
      catch (Exception ex)
      {
        this.UserTweetLists.Add(new Models.TweetInfo
        {
          Text = ex.Message + this.UserId.ToString()
        }
                );
      }
    }

    public void getLikeList()
    {
      try
      {
        this.LikeLists.Clear();
        data.likeload(this.LikeLists, this.UserId);
      }
      catch (Exception ex)
      {
        this.LikeLists.Add(new Models.TweetInfo
        {
          Text = ex.Message
        }
                );
      }
    }

    public void getFollowerList()
    {
      try
      {
        this.UserFollowers.Clear();
        data.userfollowerload(this.UserFollowers, this.UserId);
      }
      catch (Exception ex)
      {
        this.UserFollowers.Add(new Models.UserInfo
        {
          Text = ex.Message
        }
                );
      }
    }

    public void getFriendList()
    {
      try
      {
        this.UserFriends.Clear();
        data.userfriendload(this.UserFriends, this.UserId);
        //        data.likeload(this.UserFriends);
      }
      catch (Exception ex)
      {
        this.UserFriends.Add(new Models.UserInfo
        {
          Text = ex.Message
        }
                );
      }
    }

    public ObservableCollection<UserInfo> getUserLists()
    {
      getUserList();
      return this.UserLists;
    }

    public UserInfo getUserInfos()
    {
      getUser();
      return this.UserInfos;
    }

    public ObservableCollection<TweetInfo> getUserTweetLists()
    {
      getUserTweet();
      return this.UserTweetLists;
    }

    public ObservableCollection<TweetInfo> getLikeLists()
    {
      getLikeList();
      return this.LikeLists;
    }

    public ObservableCollection<UserInfo> getFllowerLists()
    {
      getFollowerList();
      return this.UserFollowers;
    }

    public ObservableCollection<UserInfo> getFriendLists()
    {
      getFriendList();
      return this.UserFriends;
    }

    //public ObservableCollection<UserInfo> UserInfos { get; set; } = new ObservableCollection<UserInfo>()
    public UserInfo UserInfos { get; set; } = new UserInfo();

    public ObservableCollection<UserInfo> UserLists { get; set; } = new ObservableCollection<UserInfo>()
    {
      //   new TweetInfo {Text="aaaaaaaaaaaaaaaaaaaa" }
    };

    public ObservableCollection<TweetInfo> UserTweetLists { get; set; } = new ObservableCollection<TweetInfo>()
    {
         new TweetInfo {Text="aaaaaaaaaaaaaaaaaaaa" }
    };

    public ObservableCollection<TweetInfo> LikeLists { get; set; } = new ObservableCollection<TweetInfo>()
    {
         new TweetInfo {Text="aaaaaaaaaaaaaaaaaaaa" }
    };

    public ObservableCollection<UserInfo> UserFollowers { get; set; } = new ObservableCollection<UserInfo>()
    {
         new UserInfo {Text="aaaaaaaaaaaaaaaaaaaa" }
    };

    public ObservableCollection<UserInfo> UserFriends { get; set; } = new ObservableCollection<UserInfo>()
    {
         new UserInfo {Text="aaaaaaaaaaaaaaaaaaaa" }
    };

  }



  public class UserLoad : Common.BindableBase
  {
    public static UserLoad Instance { get; } = new UserLoad();

    public UserInfoManager UserInfoManager { get; } = new UserInfoManager();
  }
}
