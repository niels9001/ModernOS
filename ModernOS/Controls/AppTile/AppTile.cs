using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Hosting;

namespace ModernOS.Controls
{
    public sealed class AppTile : ContentControl
    {
        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(nameof(AppName), typeof(string), typeof(AppTile), new PropertyMetadata(""));

        public string AppName
        {
            get { return (string)GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }

        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(nameof(IconPath), typeof(string), typeof(AppTile), new PropertyMetadata(""));

        public string IconPath
        {
            get { return (string)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        public static readonly DependencyProperty TileColorProperty = DependencyProperty.Register(nameof(TileColor), typeof(Color), typeof(AppTile), new PropertyMetadata(""));

        public Color TileColor
        {
            get { return (Color)GetValue(TileColorProperty); }
            set { SetValue(TileColorProperty, value); }
        }


        AppTile _AppTile;
        ThemeShadow _backgroundShadow;
        ThemeShadow _iconShadow;
        TextBlock _appTitle;
        Grid _backgroundShadowReceiverGrid;
        Grid _tileGrid;
        Grid _rootGrid;
        Image _appIcon;

        public AppTile()
        {
            DefaultStyleKey = typeof(AppTile);
        }

        protected override void OnApplyTemplate()
        {

            _AppTile = (AppTile)this;
            _backgroundShadowReceiverGrid = (Grid)_AppTile.GetTemplateChild("BackgroundShadowReceiverGrid");

            _rootGrid = (Grid)_AppTile.GetTemplateChild("RootGrid");
            _backgroundShadow = (ThemeShadow)_rootGrid.Resources["BackgroundShadow"];
            _iconShadow = (ThemeShadow)_rootGrid.Resources["IconShadow"];

            _appTitle = (TextBlock)_AppTile.GetTemplateChild("AppTitle");
            _appIcon = (Image)_AppTile.GetTemplateChild("AppIcon");
            _tileGrid = (Grid)_AppTile.GetTemplateChild("TileGrid");


            _backgroundShadow.Receivers.Add(_backgroundShadowReceiverGrid);

            _tileGrid.Background = new AcrylicBrush() { BackgroundSource = AcrylicBackgroundSource.Backdrop, FallbackColor = TileColor, TintColor = TileColor, TintOpacity = 0.6, TintTransitionDuration = new TimeSpan(0, 0, 0, 0, 400) };

            _appIcon.Source = new BitmapImage() { UriSource = new Uri(IconPath) };
            base.OnApplyTemplate();

            Loaded -= AppTile_Loaded;
            Loaded += AppTile_Loaded;

            _tileGrid.PointerEntered += AppTile_PointerEntered;
            _tileGrid.PointerExited += AppTile_PointerExited;
        }

        private void AppTile_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
         
            _tileGrid.Translation = new Vector3(0, 0, 4);
            var imageVisual = ElementCompositionPreview.GetElementVisual(_appTitle);
            imageVisual.Offset = new Vector3(imageVisual.Offset.X, imageVisual.Offset.Y + 8, imageVisual.Offset.Z);

            //_appTitle.Offset(offsetX: 0f, offsetY: 0, duration: 150, delay: 0, easingMode: EasingMode.EaseInOut).Start();
        }

        private void AppTile_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var imageVisual = ElementCompositionPreview.GetElementVisual(_appTitle);
            imageVisual.Offset = new Vector3(imageVisual.Offset.X, imageVisual.Offset.Y - 8, imageVisual.Offset.Z);
            //   RootElement.(RevealBrush.State)
         
          //  _appTitle.Offset(offsetX: 0f, offsetY: -8f, duration: 150, delay: 0, easingMode: EasingMode.EaseInOut).Start();
            _tileGrid.Translation = new Vector3(0, 0, 40);
        }

        private void AppTile_Loaded(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }
    }
}
