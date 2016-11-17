using FreshEssentials;
using System;
using Windows.UI.Xaml;
using Xamarin.Forms.Platform.WinRT;
using Xamarin.Forms;
using Windows.UI.Xaml.Media;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AdvancedFrame), typeof(FreshEssentials.WinRT.AdvancedFrameRendererWinRT))]
namespace FreshEssentials.WinRT
{
    public class AdvancedFrameRendererWinRT : FrameRenderer
    {
        AdvancedFrame myFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                myFrame = (e.NewElement as AdvancedFrame);

                int topLeft = myFrame.Corners == RoundedCorners.left || myFrame.Corners == RoundedCorners.all ? myFrame.CornerRadius : 0;
                int topRight = myFrame.Corners == RoundedCorners.right || myFrame.Corners == RoundedCorners.all ? myFrame.CornerRadius : 0;
                int bottomRight = myFrame.Corners == RoundedCorners.right || myFrame.Corners == RoundedCorners.all ? myFrame.CornerRadius : 0;
                int bottomLeft = myFrame.Corners == RoundedCorners.left || myFrame.Corners == RoundedCorners.all ? myFrame.CornerRadius : 0;

                this.Control.CornerRadius = new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);

                if (myFrame.InnerBackground != null)
                    this.Control.Background = new SolidColorBrush(
                        Windows.UI.Color.FromArgb(
                            (byte)Math.Round(myFrame.InnerBackground.A * 255),
                            (byte)Math.Round(myFrame.InnerBackground.R * 255),
                            (byte)Math.Round(myFrame.InnerBackground.G * 255),
                            (byte)Math.Round(myFrame.InnerBackground.B * 255)
                            ));
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "InnerBackground" || e.PropertyName == "OutlineColor")
            {
                this.Control.Background = new SolidColorBrush(
                       Windows.UI.Color.FromArgb(
                           (byte)Math.Round(myFrame.InnerBackground.A * 255),
                           (byte)Math.Round(myFrame.InnerBackground.R * 255),
                           (byte)Math.Round(myFrame.InnerBackground.G * 255),
                           (byte)Math.Round(myFrame.InnerBackground.B * 255)
                           ));
            }
        }

    }
}