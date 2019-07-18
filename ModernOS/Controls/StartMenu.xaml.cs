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
using ModernOS.Controls;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI;
using System.Numerics;
using Windows.UI.Xaml.Hosting;
// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ModernOS.Controls
{
    public sealed partial class StartMenu : UserControl
    {
        public StartMenu()
        {
            this.InitializeComponent();

            AvatarShadow.Receivers.Add(ShadowGrid);
            AvatarShadow.Receivers.Add(NameGrid);
            DialogShadow.Receivers.Add(ContentShadowGrid);
            SetAnimationsForAppTiles();
        }

        public void OpenMenu()
        {
            //foreach (AppTile Tile in AppGrid.Children.OfType<AppTile>())
            //{
            //    Tile.Visibility = Visibility.Visible;
            //}
        }

        public void CloseMenu()
        {
            //foreach (AppTile Tile in AppGrid.Children.OfType<AppTile>())
            //{
            //    Tile.Visibility = Visibility.Collapsed;
            //}
        }


        private void SetAnimationsForAppTiles()
        {
            int Counter = 1;
            foreach (AppTile Tile in AppGrid.Children.OfType<AppTile>())
            {
                int Duration = Counter * 80;

                AnimationCollection Col = new AnimationCollection();
                OpacityAnimation O = new OpacityAnimation
                {
                    Duration = new TimeSpan(0, 0, 0, 0, Duration),
                    From = 0,
                    To = 1.0
                };

                TranslationAnimation T = new TranslationAnimation
                {
                    Duration = new TimeSpan(0, 0, 0, 0, Duration),
                    From = "0, 80, 0",
                    To = "0"
                };

                Col.Add(O);
                Col.Add(T);
                Implicit.SetShowAnimations(Tile, Col);
                Counter++;
            }
        }

        private void PersonPicture_Loaded(object sender, RoutedEventArgs e)
        {
            SetAnimationsForAppTiles();
        }

        private void AutoSuggestBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Translation = new Vector3(0, 0, 16);
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchBox.Translation = new Vector3(0, 0, 4);
        }

        private void NameGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AnimateOut = true;
            var imageVisual = ElementCompositionPreview.GetElementVisual(Avatar);
            imageVisual.Scale = new Vector3(0.5f, 0.5f, 0);
            NameText.Visibility = Visibility.Visible;
             MailText.Visibility = Visibility.Visible;
            LockButton.Visibility = Visibility.Visible;
            PowerButton.Visibility = Visibility.Visible;
        }

        private void NameGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (AnimateOut)
            {
                var imageVisual = ElementCompositionPreview.GetElementVisual(Avatar);
                imageVisual.Scale = new Vector3(1f, 1f, 0);
                NameText.Visibility = Visibility.Collapsed;
                MailText.Visibility = Visibility.Collapsed;
                LockButton.Visibility = Visibility.Collapsed;
                PowerButton.Visibility = Visibility.Collapsed;
            }
        }

        bool AnimateOut = true;
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            AnimateOut = false;
        }
    }
}
