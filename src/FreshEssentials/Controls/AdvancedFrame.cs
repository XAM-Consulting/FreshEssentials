using System;
using Xamarin.Forms;

namespace FreshEssentials
{
    public enum RoundedCorners
    {
        left,
        right,
        all,
        none
    }

    public class AdvancedFrame: Frame
    {
        public static readonly BindableProperty InnerBackgroundProperty = BindableProperty.Create("InnerBackground", typeof(Color), typeof(AdvancedFrame), default(Color));

        public Color InnerBackground
        {
            get { return (Color)GetValue(InnerBackgroundProperty); }
            set { SetValue(InnerBackgroundProperty, value); }
        }

        public RoundedCorners Corners { get; set; }

        public int CornerRadius { get; set; }

        public AdvancedFrame()
        {       
            BackgroundColor = Color.Transparent;
            HasShadow = false;
            Corners = RoundedCorners.none;
            this.Padding = new Thickness(0, 0, 0, 0);
        }
    }
}

