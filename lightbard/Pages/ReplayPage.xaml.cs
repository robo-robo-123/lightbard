﻿using System;
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
using lightbard.Class;
using CoreTweet;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Core;
using System.Collections.ObjectModel;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace lightbard.Pages
{
  /// <summary>
  /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
  /// </summary>
  public sealed partial class ReplayPage : Page
  {

    internal Tokens tokens;
    //TweetClass.TweetInfo item;
    Models.TweetInfo item;
    Tweets data = new Tweets();
    MediaEntity[] image_entity;
    public long? ReplyId { get; set; }
    public Status status { get; set; }
    public Status status2 { get; set; }
    ObservableCollection<TweetClass.TweetInfo> convetweet;
    public ViewModels.TweetPageViewModel ViewModel2 { get; } = new ViewModels.TweetPageViewModel();

    public ReplayPage()
    {
      this.InitializeComponent();
      tokens = data.getToken();

    SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
      {
        if (Frame.CanGoBack)
        {
          Frame.GoBack();
          args.Handled = true;
        }
      };

    }

      //画像表示
    private void show(Status status)
    {
      reptweetState.Text = "";

      tweetImage1.Source = null;
      tweetImage2.Source = null;
      tweetImage3.Source = null;
      tweetImage4.Source = null;
      try
      {
        if (status.Entities.Urls != null)
        {
          urlView.ItemsSource = status.Entities.Urls;
        }
      }
      catch { }

      try
      {
        if (status.ExtendedEntities.Media != null)
        {
          image_entity = status.ExtendedEntities.Media;
          testBlock.Text = "";
          int media_num = status.ExtendedEntities.Media.Length;
          for (int n = 0; n < media_num; n++)
          {
            var mediaurl = new Uri(status.ExtendedEntities.Media[n].MediaUrl);

            BitmapImage imageSource = new BitmapImage(mediaurl);

            switch (n)
            {
              case 0:
                tweetImage1.Source = imageSource;
                break;
              case 1:
                tweetImage2.Source = imageSource;
                break;
              case 2:
                tweetImage3.Source = imageSource;
                break;
              case 3:
                tweetImage4.Source = imageSource;
                break;

            }
          }
        }
      }
      catch { }

    }

    /*
    private async void replyTweetAsync(string text, long? replyid)
    {
      try
      {
        await tokens.Statuses.UpdateAsync(status => text, in_reply_to_status_id => replyid);
        reptweetState.Text = "ツイート成功";
        ReplyId = 0;
      }
      catch
      {
        reptweetState.Text = "ツイート失敗";
      }
    }
    */

    private void replyTweetButtom_Click(object sender, RoutedEventArgs e)
    {
      this.tweetFrane.Visibility = Visibility.Visible;
      this.tweetFrane.Navigate(typeof(TweetPage), item);
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
      //item = (TweetClass.TweetInfo)e.Parameter;
      item = (Models.TweetInfo)e.Parameter;
      //data.toast(item.Id.ToString());

      //loadTweet(item.Id);
      status = await tokens.Statuses.ShowAsync(id => item.Id);
      show(status);


      ViewModel2.GetTweetInfos();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      base.OnNavigatedFrom(e);
      this.tweetFrane.Visibility = Visibility.Collapsed;

    }

    /*
    private async void loadTweet(long? Id)
    {

      status = await tokens.Statuses.ShowAsync(id => Id);
      //var tweet = new ObservableCollection<TweetClass.TweetInfo>();
      //tweet = data.replytweetinfo2(status);
      //replyView.ItemsSource = tweet;
      show(status);
    }
    */

    private void HyperLinkButton_Click(object sender, RoutedEventArgs e)
    {
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
      if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
    }

    private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
    {
      var run = e.OriginalSource;
    }


    private async void webOpen(string url)
    {
      var uri = new Uri(url);

      var success = await Windows.System.Launcher.LaunchUriAsync(uri);

      if (success)
      {
      }
      else
      {
      }
    }

    private /*async*/ void tweetImage1_Tapped(object sender, TappedRoutedEventArgs e)
    {

      this.Frame.Navigate(typeof(ImageView), status.ExtendedEntities.Media[0]);
      //this.Frame.Navigate(typeof(ImageView), tweetImage1.Source);

      /*
            var result = await this.webDlg.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
              //reptweetState.Text = status.ExtendedEntities.Media[0].Url.ToString();
              webOpen(status.ExtendedEntities.Media[0].Url.ToString());
            }
            else if (result == ContentDialogResult.Secondary)
            {
              System.Diagnostics.Debug.WriteLine("Secondary");
            }
            else
            {
              System.Diagnostics.Debug.WriteLine("None");
            }
      */
    }

    private async void tweetImage3_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ImageView), status.ExtendedEntities.Media[2]);

      /*
      var result = await this.webDlg.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        webOpen(status.ExtendedEntities.Media[2].Url.ToString());
      }
      else if (result == ContentDialogResult.Secondary)
      {
        System.Diagnostics.Debug.WriteLine("Secondary");
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("None");
      }
      */
      /*
      var uri = new Uri(status.ExtendedEntities.Media[2].Url.ToString());

      var success = await Windows.System.Launcher.LaunchUriAsync(uri);

      if (success)
      {
        // 起動に成功した場合の処理。
        // ブラウザは起動するがアプリも裏で動く
        reptweetState.Text = status.ExtendedEntities.Media[2].Url.ToString();
      }
      else
      {

      }
      */
    }

    private async void tweetImage2_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ImageView), status.ExtendedEntities.Media[1]);

      /*
      var result = await this.webDlg.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        webOpen(status.ExtendedEntities.Media[1].Url.ToString());
      }
      else if (result == ContentDialogResult.Secondary)
      {
        System.Diagnostics.Debug.WriteLine("Secondary");
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("None");
      }
      */
      /*
      var uri = new Uri(status.ExtendedEntities.Media[1].Url.ToString());

      var success = await Windows.System.Launcher.LaunchUriAsync(uri);

      if (success)
      {
        // 起動に成功した場合の処理。
        reptweetState.Text = status.ExtendedEntities.Media[1].Url.ToString();
      }
      else
      {

      }
      */
    }

    private async void tweetImage4_Tapped(object sender, TappedRoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ImageView), status.ExtendedEntities.Media[3]);

      /*
      var result = await this.webDlg.ShowAsync();
      if (result == ContentDialogResult.Primary)
      {
        webOpen(status.ExtendedEntities.Media[3].Url.ToString());
      }
      else if (result == ContentDialogResult.Secondary)
      {
        System.Diagnostics.Debug.WriteLine("Secondary");
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("None");
      }
      */
      /*
      var uri = new Uri(status.ExtendedEntities.Media[3].Url.ToString());

      var success = await Windows.System.Launcher.LaunchUriAsync(uri);

      if (success)
      {
        // 起動に成功した場合の処理。
        // ブラウザは起動するがアプリも裏で動く
        reptweetState.Text = status.ExtendedEntities.Media[3].Url.ToString();
      }
      else
      {

      }
      */
    }

    private void conveButton_Click(object sender, RoutedEventArgs e)
    {
      this.Frame.Navigate(typeof(ConvePage), item.Id);
    }
      /*
    private async void conv(long? Id)
    {
      status2 = await tokens.Statuses.ShowAsync(id => Id);
      data.Addtweet(convetweet, status2);
      try
      {
        //if (status.ExtendedEntities.UserMentions[0].Id != null)
        //{
        if (status2.InReplyToStatusId != null)
        {
          var rep_status = status2.InReplyToStatusId;
          conv(rep_status);
        }
        else
        {
          conveView.ItemsSource = convetweet;
        }
      }
      catch (Exception ex)
      {
        testBlock.Text = ex.Message;
        return;
      }
    }


      private void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
      {
        var item = (TweetClass.TweetInfo)navigationParameter;
        replyBox.Text = item.ToString();
      }




      private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
      {

        //e.NavigationParameter.ToString();
      }
      */

    }
}
