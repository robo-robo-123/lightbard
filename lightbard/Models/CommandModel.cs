using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace lightbard.Models
{
  public class CommandModel : Common.BindableBase
  {
    public static CommandModel Instance { get; } = new CommandModel();

    public void RetweetToast()
    {


    }

    // private const bool PivotToggleSwtichDefault = false;
    private bool pivotToggleSwtich = false;
   public bool PivotToggleSwtich
    {
      //get { if (pivotToggleSwtich == false) { return false; } else { return true; } }
      get { return this.pivotToggleSwtich; }
      set { this.SetProperty(ref this.pivotToggleSwtich, value); }
    }


    private long tweetId;

    public long TweetId
    {
      get { return this.tweetId; }
      set { this.SetProperty(ref this.tweetId, value); }
    }

    private int tweetCountIndex;

    public int TweetCountIndex
    {
      get { return this.tweetCountIndex; }
      set { this.SetProperty(ref this.tweetCountIndex, value); }
    }

    private string tweetCount;

    public string TweetCount
    {
      get { return this.tweetCount;  }
      set { this.SetProperty(ref this.tweetCount, value);  }
    }

    private int streamCountIndex;

    public int StreamCountIndex
    {
      get { return this.streamCountIndex; }
      set { this.SetProperty(ref this.streamCountIndex, value); }
    }

    private string streamCount;

    public string StreamCount
    {
      get { return this.streamCount; }
      set { this.SetProperty(ref this.streamCount, value); }
    }

    public void SaveCount()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      settings.Values["tweet_count"] = this.TweetCount;
      settings.Values["tweet_countindex"] = this.TweetCountIndex;
      settings.Values["stream_count"] = this.StreamCount;
      settings.Values["stream_countindex"] = this.StreamCountIndex;
      settings.Values["pivot_switch"] = this.PivotToggleSwtich;
    }

    public void LoadCount()
    {
      var settings = ApplicationData.Current.RoamingSettings;
      var temp = default(object);
      if (settings.Values.TryGetValue("tweet_count", out temp))
      {
        this.TweetCount = (string)temp;
      }
      //settings.Values["tweet_count"] = this.TweetCount;
      if (settings.Values.TryGetValue("tweet_countindex", out temp))
      {
        this.TweetCountIndex = (int)temp;
      }
      if (settings.Values.TryGetValue("stream_count", out temp))
      {
        this.StreamCount = (string)temp;
      }
      //settings.Values["tweet_count"] = this.TweetCount;
      if (settings.Values.TryGetValue("stream_countindex", out temp))
      {
        this.StreamCountIndex = (int)temp;
      }
      if (settings.Values.TryGetValue("pivot_switch", out temp))
      {
        this.PivotToggleSwtich = (bool)temp;
      }
    }

  }
}
