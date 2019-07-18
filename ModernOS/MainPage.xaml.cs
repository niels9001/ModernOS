using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ModernOS
{
    public sealed partial class MainPage : Page
    {
        DispatcherTimer LoadingTimer;
        public MainPage()
        {
            this.InitializeComponent();
            ShellBarShadow.Receivers.Add(BackgroundGrid);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ShellBar.Visibility = Visibility.Collapsed;


            LoadingTimer = new DispatcherTimer();
            LoadingTimer.Interval = new TimeSpan(0, 0, 2);
            LoadingTimer.Tick += LoadingTimer_Tick;
            LoadingTimer.Start();


            DispatcherTimer TimeTimer = new DispatcherTimer();
            TimeTimer.Interval = new TimeSpan(0, 0, 10);
            TimeTimer.Tick += TimeTimer_Tick;
            TimeTimer.Start();

            Background.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/Images/Waves.mp4"));
            Background.MediaPlayer.Play();
            Background.MediaPlayer.PlaybackSession.PlaybackRate = 0.6;
            Background.MediaPlayer.IsLoopingEnabled = true;
        }

        private void LoadingTimer_Tick(object sender, object e)
        {
            LoadingOverlay.Visibility = Visibility.Collapsed;
            ShellBar.Visibility = Visibility.Visible;
            LoadingTimer.Stop();
        }


        private void TimeTimer_Tick(object sender, object e)
        {
            TimeTextBlock.Text = DateTime.Now.ToShortTimeString();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (StartMenu.Visibility == Visibility.Visible)
            {
                StartMenu.CloseMenu();
                StartMenu.Visibility = Visibility.Collapsed;
            }
            else
            {
                StartMenu.OpenMenu();
                StartMenu.Visibility = Visibility.Visible;
            }
            MusicControl2.Visibility = Visibility.Collapsed;
        }

        private void AudioControl_CloseButtonClicked(object sender, EventArgs e)
        {
            AudioControl.Visibility = Visibility.Collapsed;
        }

        private void AudioButton_Click(object sender, RoutedEventArgs e)
        {
            StartMenu.Visibility = Visibility.Collapsed;
            if (MusicControl2.Visibility == Visibility.Visible)
            {
                MusicControl2.Visibility = Visibility.Collapsed;
            }
           
            else
            {
                MusicControl2.Visibility = Visibility.Visible;
            }
           
         
        }

        private void Background_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
 
        }

        private void BackgroundGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            StartMenu.Visibility = Visibility.Collapsed;
            MusicControl2.Visibility = Visibility.Collapsed;
        }

 
    }
}