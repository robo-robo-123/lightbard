using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
