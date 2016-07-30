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
        public static readonly BindableProperty InnerBackgroundProperty =
           BindableProperty.Create("InnerBackground", typeof(Color), typeof(AdvancedFrame),
               defaultValue: default(Color),
               defaultBindingMode: BindingMode.TwoWay);
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
            HorizontalOptions = LayoutOptions.Start;
            WidthRequest = 100;
            HasShadow = false;
            OutlineColor = Color.Transparent;
            Corners = RoundedCorners.none;
            CornerRadius = 30;
            HeightRequest = 2 * CornerRadius;
            this.Padding = new Thickness(0, 0, 0, 0);
        }
    }
}

