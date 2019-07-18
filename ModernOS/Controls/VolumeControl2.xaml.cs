using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ModernOS.Controls
{
    public sealed partial class VolumeControl2 : UserControl
    {
        public VolumeControl2()
        {
            this.InitializeComponent();
            CoverArtShadow.Receivers.Add(CoverArtBackgroundShadowGrid);
            VolumeGridShadow.Receivers.Add(BackgroundShadowGrid);
            MusicGridShadow.Receivers.Add(BackgroundShadowGrid);
           


            SetSong();
        }

        int MusicCounter = 0;
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            OnCloseButtonClicked();
        }


        List<Song> Songs = new List<Song>() {
            new Song() { Artist = "Ryan Leslie ft. Fabolous & Cassie", Title = "Addiction", CoverArtURL = "ms-appx:///Assets/Images/CoverArt1.png" },
             new Song() { Artist = "Jason Aldean", Title = "Rearview Town", CoverArtURL = "ms-appx:///Assets/Images/CoverArt2.png" },
              new Song() { Artist = "Ariana Grande ft. Pharrell Williams", Title = "Blazed", CoverArtURL = "ms-appx:///Assets/Images/CoverArt3.png" },
        };


        private async void SetSong()
        {
            if (MusicCounter < 0)
            {
                MusicCounter = Songs.Count -1;
            }

            if (MusicCounter > Songs.Count -1)
            {
                MusicCounter = 0;
            }

            Song SelectedSong = Songs[MusicCounter];
            Artist.Text = SelectedSong.Artist;
            Title.Text = SelectedSong.Title;
            CoverArt.SelectedIndex = MusicCounter;
        }
 
        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            MusicCounter--;
            SetSong();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
      
            MusicCounter++;
            SetSong();

        }


        public event EventHandler CloseButtonClicked;

        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke(this, new PropertyChangedEventArgs("CloseButtonClicked"));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            VolumeSlider.ValueChanged += VolumeSlider_ValueChanged;
        }

        private void VolumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            double Volume = VolumeSlider.Value;

            if (Volume == 0)
            {
                GlyphIcon.Glyph = "\uE74F";
            }
            else if (Volume > 0 && Volume < 32)
            {
                GlyphIcon.Glyph = "\uE993";
            }
            else if (Volume > 65)
            {
                GlyphIcon.Glyph = "\uE995";
            }
            else
            {
                GlyphIcon.Glyph = "\uE994";
            }
        }
    }
}