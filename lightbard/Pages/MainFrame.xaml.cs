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

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class MainFrame : Page
  {
    public ViewModels.CommandViewModel ViewModel { get; } = new ViewModels.CommandViewModel();

    public MainFrame()
    {
      this.InitializeComponent();
      
      this.timelineFrame.Navigate(typeof(Pages.Home));
      //this.tweetFrame.Navigate(typeof(Pages.TweetPage));
      this.mentionFrame.Navigate(typeof(Pages.MentionPage));
      this.searchFrame.Navigate(typeof(Pages.SearchPage));

    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      //this.timelineFrame.Navigate(typeof(Pages.Home));
      if (e.Parameter != null)
      {
      var check = (bool)e.Parameter;

      if (check==true)
      {
        var tp = this.rootPivot.Items.Where(i => (string)(((PivotItem)i).Tag) == (string)mentionItem.Tag).FirstOrDefault();
        this.rootPivot.SelectedItem = tp;
      }
      }


    }


    private void timelineItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      //this.timelineFrame.Navigate(typeof(Pages.Home));
    }

    private void tweetItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      //this.tweetFrame.Navigate(typeof(Pages.TweetPage));
      //  this.tweetFrame.Navigate(typeof(Pages.TweetPage));
      //this.testFrame..Navigate.InitializeComponent();
    }

    private void mentionFrame_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }

    private void mentionItem_Tapped(object sender, TappedRoutedEventArgs e)
    {
      //this.mentionFrame.Navigate(typeof(Pages.MentionPage));
    }
  }
}
