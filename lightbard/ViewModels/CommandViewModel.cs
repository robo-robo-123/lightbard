using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lightbard.Common;
using lightbard.Models;
using System.ComponentModel;

namespace lightbard.ViewModels
{
  public class CommandViewModel : Common.BindableBase
  {
    private Models.CommandModel Model { get; } = Models.CommandModel.Instance;

   // public void Load()
   // {
   //   this.Model.LoadCount();
   // }

    public long TweetId
    {
      get { return this.Model.TweetId; }
      set { this.Model.TweetId = value; }
    }

    public void TweetIdSet(long value)
    {
      this.Model.TweetId = value; 
    }

    public void Like()
    {
      this.Model.Like();
    }

    public void Retweet()
    {
      this.Model.Retweet();
    }

    public bool PivotToggleSwtich
    {
      get { return this.Model.PivotToggleSwtich; }
      set { this.Model.PivotToggleSwtich = value; }
    }

    public string TweetCount
    {
      get { return this.Model.TweetCount; }
      set { this.Model.TweetCount = value; }
    }

    public string tweetCount()
    {
       return this.Model.TweetCount;
    }

    public int TweetCountIndex
    {
      get { return this.Model.TweetCountIndex; }
      set { this.Model.TweetCountIndex = value; }
    }

    public int tweetCountIndex()
    {
      return this.Model.TweetCountIndex;
    }

    public string StreamCount
    {
      get { return this.Model.StreamCount; }
      set { this.Model.StreamCount = value; }
    }

    public string streamCount()
    {
      return this.Model.StreamCount;
    }

    public int StreamCountIndex
    {
      get { return this.Model.StreamCountIndex; }
      set { this.Model.StreamCountIndex = value; }
    }


  }

  public class TweetPageViewModel : Common.BindableBase//, IDisposable
  {
    private Models.TweetLoad Model { get; } = Models.TweetLoad.Instance;

    // public ReadOnlyObservableCollection<TweetInfo> TweetInfos { get; set; }
    // public ObservableCollection<TweetInfo> TweetInfos { get; set; }

    
  public ObservableCollection<TweetInfo> TweetInfos
  {
    get { return this.Model.TweetInfoManager.TweetInfos; }
    set { this.Model.TweetInfoManager.TweetInfos = value; }
  }

    public ObservableCollection<TweetInfo> TweetTimeline
    {
      get { return this.Model.TweetInfoManager.TweetTimeline; }
      set { this.Model.TweetInfoManager.TweetTimeline = value; }
    }


    public ObservableCollection<TweetInfo> SerachTweets
    {
      get { return this.Model.TweetInfoManager.SearchTweets; }
      set { this.Model.TweetInfoManager.SearchTweets = value; }
    }


    public string Word
    {
      get { return this.Model.TweetInfoManager.Word; }
      set { this.Model.TweetInfoManager.Word = value; }
    }

    public void TweetIdSet(long value)
    {
      this.Model.TweetInfoManager.TweetId = value;
    }


    public long TweetId
    {
      get { return this.TweetId; }
      set { this.Model.TweetInfoManager.TweetId = value; }
    }

    public TweetPageViewModel()
    {
      this.Model.PropertyChanged += this.TweetPageViewModel_PropatyChanged;
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      //this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }

    private void TweetPageViewModel_PropatyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged(e.PropertyName);
    }

    /*
    public void GetTweetInfos()
    {
      this.Model.TweetInfoManager.getTweetInfos();
    }
    */

    public void GetTweetInfos()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
      //this.Model.PropertyChanged += this.TweetPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    public void GetTimelineInfos()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.TweetTimeline.Clear();
      this.TweetTimeline = this.Model.TweetInfoManager.getTimelineInfos();
      this.Model.PropertyChanged += this.TweetPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }

    public void GetSearchTweets()
    {
      this.Model.TweetInfoManager.GetSearchTweets();
    }


    /*
    public void GetTweetInfos()
    {
      //this.TweetInfos = new ObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
     // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    */


  }

  public class UserPageViewModel : Common.BindableBase//, IDisposable
  {
    private Models.UserLoad Model { get; } = Models.UserLoad.Instance;

    // public ReadOnlyObservableCollection<TweetInfo> TweetInfos { get; set; }
    // public ObservableCollection<TweetInfo> TweetInfos { get; set; }


    //public ObservableCollection<UserInfo> UserInfos
    public UserInfo UserInfos
    {
      get { return this.Model.UserInfoManager.UserInfos; }
      set { this.Model.UserInfoManager.UserInfos = value; }
    }


    public ObservableCollection<TweetInfo> UserTweetLists
    {
      get { return this.Model.UserInfoManager.UserTweetLists; }
      set { this.Model.UserInfoManager.UserTweetLists = value; }
    }

    public ObservableCollection<UserInfo> UserLists
    {
      get { return this.Model.UserInfoManager.UserLists; }
      set { this.Model.UserInfoManager.UserLists = value; }
    }

    public ObservableCollection<TweetInfo> LikeLists
    {
      get { return this.Model.UserInfoManager.LikeLists; }
      set { this.Model.UserInfoManager.LikeLists = value; }
    }

    public ObservableCollection<UserInfo> UserFollowers
    {
      get { return this.Model.UserInfoManager.UserFollowers; }
      set { this.Model.UserInfoManager.UserFollowers = value; }
    }

    public ObservableCollection<UserInfo> UserFriends
    {
      get { return this.Model.UserInfoManager.UserFriends; }
      set { this.Model.UserInfoManager.UserFriends = value; }
    }

    public ObservableCollection<UserInfo> SerachUsers
    {
      get { return this.Model.UserInfoManager.SearchUsers; }
      set { this.Model.UserInfoManager.SearchUsers = value; }
    }


    public string User_word
    {
      get { return this.Model.UserInfoManager.User_word; }
      set { this.Model.UserInfoManager.User_word = value; }
    }

    public void UserIdSet(long? value)
    {
      this.Model.UserInfoManager.UserId = value;
    }


    public long? UserId
    {
      get { return this.UserId; }
      set { this.Model.UserInfoManager.UserId = value; }
    }

    public UserPageViewModel()
    {
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      //this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }

    private void UserPageViewModel_PropatyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged(e.PropertyName);
    }

    /*
    public void GetTweetInfos()
    {
      this.Model.TweetInfoManager.getTweetInfos();
    }
    */

    public void GetUserInfos()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.UserInfos = this.Model.UserInfoManager.getUserInfos();
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    public void GetUserLists()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.UserLists.Clear();
      this.UserLists = this.Model.UserInfoManager.getUserLists();
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }

    public void GetUserTweetLists()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.UserTweetLists.Clear();
      this.UserTweetLists = this.Model.UserInfoManager.getUserTweetLists();
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }

    public void GetUserLikeLists()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.LikeLists.Clear();
      this.LikeLists = this.Model.UserInfoManager.getLikeLists();
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }

    public void GetUserFollowerLists()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.UserFollowers.Clear();
      this.UserFollowers = this.Model.UserInfoManager.getFllowerLists();
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    public void GetUserFriendLists()
    {
      //this.TweetInfos = new ReadOnlyObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
      this.UserFriends.Clear();
      this.UserFriends = this.Model.UserInfoManager.getFriendLists();
      this.Model.PropertyChanged += this.UserPageViewModel_PropatyChanged;
      // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    public void GetSearchUsers()
    {
      this.Model.UserInfoManager.GetSearchUsers();
    }

    /*
    public void GetTweetInfos()
    {
      //this.TweetInfos = new ObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
     // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    */


  }

}
