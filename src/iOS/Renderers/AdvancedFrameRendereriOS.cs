using System;
using Xamarin.Forms;
using FreshEssentials;
using Xamarin.Forms.Platform.iOS;
using System.Drawing;
using UIKit;

[assembly: ExportRenderer(typeof(AdvancedFrame), typeof(FreshEssentials.iOS.AdvancedFrameRendereriOS))]
namespace FreshEssentials.iOS
{
    public class AdvancedFrameRendereriOS : FrameRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AdvancedFrame.InnerBackgroundProperty.PropertyName
                || e.PropertyName == AdvancedFrame.OutlineColorProperty.PropertyName
                || e.PropertyName == AdvancedFrame.CornerRadiusProperty.PropertyName
                || e.PropertyName == AdvancedFrame.CornersProperty.PropertyName)
            {
                this.SetNeedsDisplay();
            }
        }

        public override void Draw(CoreGraphics.CGRect rect)
        {            
            CoreGraphics.CGRect r;  

            SizeF radius = new SizeF((float)Element.CornerRadius, (float)Element.CornerRadius);

            UIBezierPath path;
            switch (Element.Corners)
            {
                case RoundedCorners.Left:
                    r = new CoreGraphics.CGRect(rect.X + 4,
                        rect.Y + 2, rect.Width - 4, rect.Height - 4);
                    path = UIBezierPath.FromRoundedRect(r, 
                        (UIRectCorner.TopLeft | UIRectCorner.BottomLeft), radius);
                    break;
                case RoundedCorners.Right:
                    r = new CoreGraphics.CGRect(rect.X,
                        rect.Y + 2, rect.Width - 4, rect.Height - 4);
                    path = UIBezierPath.FromRoundedRect(r, 
                        (UIRectCorner.TopRight | UIRectCorner.BottomRight), radius);
                    break;
                case RoundedCorners.All:
                    r = new CoreGraphics.CGRect(rect.X + 2,
                        rect.Y + 2, rect.Width - 4, rect.Height - 4);
                    path = UIBezierPath.FromRoundedRect(r, radius.Width);
                    break;
                case RoundedCorners.None:
                    r = new CoreGraphics.CGRect(rect.X,
                        rect.Y + 2, rect.Width, rect.Height - 4);
                    path = UIBezierPath.FromRoundedRect(r, (float)0.0);                    
                    break;
                default:
                    r = new CoreGraphics.CGRect(rect.X + 2,
                        rect.Y + 2, rect.Width, rect.Height - 4);
                    path = UIBezierPath.FromRoundedRect(r, radius.Width);
                    break;
            }
            ;

            if (Element == null)
            {
                UIColor.FromRGB(245, 249, 252).SetFill();
                path.Fill();
                UIColor.FromRGB(186, 198, 210).SetStroke();
                path.Stroke();
            }
            else
            {
                Element.InnerBackground.ToUIColor().SetFill();
                path.Fill();
                Element.OutlineColor.ToUIColor().SetStroke();
                path.Stroke();
            }                

        }
    }
}

