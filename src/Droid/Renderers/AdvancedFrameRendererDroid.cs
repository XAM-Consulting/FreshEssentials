using System;
using Xamarin.Forms;
using FreshEssentials;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AdvancedFrame), typeof(FreshEssentials.Droid.AdvancedFrameRendererDroid))]
namespace FreshEssentials.Droid
{
    public class AdvancedFrameRendererDroid  : VisualElementRenderer<AdvancedFrame>
    {
        Paint paint = new Paint ();
        AdvancedFrame myFrame;
        protected override void OnElementChanged (ElementChangedEventArgs<AdvancedFrame> e)
        {
            base.OnElementChanged (e);
            if (e.NewElement != null) {
                myFrame = (e.NewElement as AdvancedFrame);
            }
        }            

        protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged (sender, e);
            if (e.PropertyName == "InnerBackground" || e.PropertyName == "OutlineColor") {
                this.RequestLayout ();
            }
        }

        protected override void OnDraw (Canvas canvas)
        {           
            // The Quad function is a Bezier curve.
            if (myFrame == null) // Error
                return;
            float h = (float)this.Height; 
            float w = (float)this.Width;
            float left = this.PivotX - w / 2; 
            float top = this.PivotY - h / 2; 
            float right = left + Width; 
            float bottom = top + h; 
            float r = Forms.Context.ToPixels(myFrame.CornerRadius);
            Path path = new Path (); 

            switch (myFrame.Corners) {
                //
                case RoundedCorners.right:                 
                    path.MoveTo (left, top);
                    path.LineTo (right - r, top);
                    path.QuadTo (right, top, right, top + r);
                    path.LineTo(right, bottom - r);
                    path.QuadTo (right, bottom, right - r, bottom);
                    path.LineTo (left, bottom);
                    path.LineTo (left, top);
                    path.Close ();
                    break;
                case RoundedCorners.left:
                    path.MoveTo (right, top);
                    path.LineTo (left + r, top);
                    path.QuadTo (left, top, left, top + r);
                    path.LineTo (left, bottom - r);
                    path.QuadTo (left, bottom, left + r, bottom);
                    path.LineTo (right, bottom);
                    path.LineTo (right, top);
                    path.Close ();
                    break;
                case RoundedCorners.all:                   
                    path.MoveTo (left + r, top);
                    path.LineTo (right - r, top);
                    path.QuadTo (right, top, right, top + r);
                    path.LineTo(right, bottom - r);
                    path.QuadTo (right, bottom, right - r, bottom);
                    path.LineTo (left + r, bottom);
                    path.QuadTo (left, bottom, left, bottom - r);
                    path.LineTo (left, top + r);
                    path.QuadTo (left, top, left + r, top);
                    path.Close ();
                    break;
                case RoundedCorners.none:
                    path.MoveTo (left, top);
                    path.LineTo (right, top);
                    path.LineTo (right, bottom);
                    path.LineTo (left, bottom);
                    path.LineTo (left, bottom);
                    path.LineTo (left, top);
                    path.Close ();
                    break;
            }
            //
            // Fill path 
            var fillColor = myFrame.InnerBackground.ToAndroid ();
            paint.SetARGB (fillColor.A, fillColor.R, fillColor.G, fillColor.B);
            paint.SetStyle (Paint.Style.FillAndStroke);
            path.SetFillType (Path.FillType.EvenOdd);
            canvas.DrawPath (path, paint);
            //
            // Draw outline
            var outlineColor = myFrame.OutlineColor.ToAndroid ();
            paint.SetARGB (outlineColor.A, outlineColor.R, outlineColor.G, outlineColor.B);
            paint.StrokeWidth = 2.0f; // Fix value
            paint.SetStyle (Paint.Style.Stroke);
            canvas.DrawPath (path, paint);
        }

    }
}

