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
    /*
    public void GetTweetInfos()
    {
      //this.TweetInfos = new ObservableCollection<TweetInfo>(this.Model.TweetInfoManager.TweetInfos);
     // this.TweetInfos = this.Model.TweetInfoManager.getTweetInfos();
    }
    */


  }
}
