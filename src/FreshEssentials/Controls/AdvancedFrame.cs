using System;
using Xamarin.Forms;

namespace FreshEssentials
{
    public enum RoundedCorners
    {
        None,
        Left,
        Right,
        All
    }

    public class AdvancedFrame : Frame
    {
        public static readonly BindableProperty InnerBackgroundProperty = BindableProperty.Create("InnerBackground", typeof(Color), typeof(AdvancedFrame), default(Color));

        public Color InnerBackground
        {
            get { return (Color)GetValue(InnerBackgroundProperty); }
            set { SetValue(InnerBackgroundProperty, value); }
        }

        public static readonly BindableProperty CornersProperty = BindableProperty.Create("Corners", typeof(RoundedCorners), typeof(AdvancedFrame), RoundedCorners.None);
        public RoundedCorners Corners
        {
            get { return (RoundedCorners)GetValue(CornersProperty); }
            set { SetValue(CornersProperty, value); }
        }

        public AdvancedFrame()
        {
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            this.Padding = new Thickness(0, 0, 0, 0);
        }
    }
}

