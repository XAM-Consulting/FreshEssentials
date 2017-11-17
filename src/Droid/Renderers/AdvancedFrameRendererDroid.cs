using System;
using Xamarin.Forms;
using FreshEssentials;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.ComponentModel;
using Android.Graphics.Drawables;
using AButton = Android.Widget.Button;
using ACanvas = Android.Graphics.Canvas;
using GlobalResource = Android.Resource;

[assembly: ExportRenderer(typeof(AdvancedFrame), typeof(FreshEssentials.Droid.AdvancedFrameRendererDroid))]
namespace FreshEssentials.Droid
{
    public class AdvancedFrameRendererDroid : FrameRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null && e.OldElement == null)
            {
                this.SetBackground(new FrameDrawable(Element as AdvancedFrame, Context.ToPixels));
            }
        }

        class FrameDrawable : Drawable
        {
            readonly AdvancedFrame _frame;
            readonly Func<double, float> _convertToPixels;

            bool _isDisposed;
            Bitmap _normalBitmap;

            public FrameDrawable(AdvancedFrame frame, Func<double, float> convertToPixels)
            {
                _frame = frame;
                _convertToPixels = convertToPixels;
                frame.PropertyChanged += FrameOnPropertyChanged;
            }

            public override bool IsStateful
            {
                get { return false; }
            }

            public override int Opacity
            {
                get { return 0; }
            }

            public override void Draw(ACanvas canvas)
            {
                int width = Bounds.Width();
                int height = Bounds.Height();

                if (width <= 0 || height <= 0)
                {
                    if (_normalBitmap != null)
                    {
                        _normalBitmap.Dispose();
                        _normalBitmap = null;
                    }
                    return;
                }

                if (_normalBitmap == null || _normalBitmap.Height != height || _normalBitmap.Width != width)
                {
                    // If the user changes the orientation of the screen, make sure to destroy reference before
                    // reassigning a new bitmap reference.
                    if (_normalBitmap != null)
                    {
                        _normalBitmap.Dispose();
                        _normalBitmap = null;
                    }

                    _normalBitmap = CreateBitmap(false, width, height);
                }
                Bitmap bitmap = _normalBitmap;
                using (var paint = new Paint())
                    canvas.DrawBitmap(bitmap, 0, 0, paint);
            }

            public override void SetAlpha(int alpha)
            {
            }

            public override void SetColorFilter(ColorFilter cf)
            {
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && !_isDisposed)
                {
                    if (_normalBitmap != null)
                    {
                        _normalBitmap.Dispose();
                        _normalBitmap = null;
                    }

                    _isDisposed = true;
                }

                base.Dispose(disposing);
            }

            protected override bool OnStateChange(int[] state)
            {
                return false;
            }

            Bitmap CreateBitmap(bool pressed, int width, int height)
            {
                Bitmap bitmap;
                using (Bitmap.Config config = Bitmap.Config.Argb8888)
                    bitmap = Bitmap.CreateBitmap(width, height, config);

                using (var canvas = new ACanvas(bitmap))
                {
                    DrawCanvas(canvas, width, height, pressed);
                }

                return bitmap;
            }

            void DrawBackground(ACanvas canvas, Path path, bool pressed)
            {
                using (var paint = new Paint { AntiAlias = true })
                using (Path.Direction direction = Path.Direction.Cw)
                using (Paint.Style style = Paint.Style.Fill)
                {

                    global::Android.Graphics.Color color = _frame.InnerBackground.ToAndroid();

                    paint.SetStyle(style);
                    paint.Color = color;

                    canvas.DrawPath(path, paint);
                }
            }

            void DrawOutline(ACanvas canvas, Path path)
            {
                using (var paint = new Paint { AntiAlias = true })
                using (Path.Direction direction = Path.Direction.Cw)
                using (Paint.Style style = Paint.Style.Stroke)
                {
                    paint.StrokeWidth = 1;
                    paint.SetStyle(style);
                    paint.Color = _frame.OutlineColor.ToAndroid();

                    canvas.DrawPath(path, paint);
                }
            }

            void FrameOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName ||
                    e.PropertyName == Frame.OutlineColorProperty.PropertyName ||
                    e.PropertyName == Frame.CornerRadiusProperty.PropertyName ||
                    e.PropertyName == AdvancedFrame.InnerBackgroundProperty.PropertyName)
                {
                    if (_normalBitmap == null)
                        return;

                    using (var canvas = new ACanvas(_normalBitmap))
                    {
                        int width = Bounds.Width();
                        int height = Bounds.Height();
                        canvas.DrawColor(global::Android.Graphics.Color.Black, PorterDuff.Mode.Clear);
                        DrawCanvas(canvas, width, height, false);
                    }
                    InvalidateSelf();
                }
            }

            void DrawCanvas(ACanvas canvas, int width, int height, bool pressed)
            {
                float cornerRadius = _frame.CornerRadius;

                if (cornerRadius == -1f)
                    cornerRadius = 5f; // default corner radius

                float h = (float)height;
                float w = (float)width;
                float left = 0;
                float top = 0;
                float right = left + width;
                float bottom = top + h;
                float r = _convertToPixels(cornerRadius);
                using (var path = new Path())
                {
                    switch (_frame.Corners)
                    {
                        case RoundedCorners.Right:
                            path.MoveTo(left, top);
                            path.LineTo(right - r, top);
                            path.QuadTo(right, top, right, top + r);
                            path.LineTo(right, bottom - r);
                            path.QuadTo(right, bottom, right - r, bottom);
                            path.LineTo(left, bottom);
                            path.LineTo(left, top);
                            path.Close();
                            break;
                        case RoundedCorners.Left:
                            path.MoveTo(right, top);
                            path.LineTo(left + r, top);
                            path.QuadTo(left, top, left, top + r);
                            path.LineTo(left, bottom - r);
                            path.QuadTo(left, bottom, left + r, bottom);
                            path.LineTo(right, bottom);
                            path.LineTo(right, top);
                            path.Close();
                            break;
                        case RoundedCorners.All:
                            path.MoveTo(left + r, top);
                            path.LineTo(right - r, top);
                            path.QuadTo(right, top, right, top + r);
                            path.LineTo(right, bottom - r);
                            path.QuadTo(right, bottom, right - r, bottom);
                            path.LineTo(left + r, bottom);
                            path.QuadTo(left, bottom, left, bottom - r);
                            path.LineTo(left, top + r);
                            path.QuadTo(left, top, left + r, top);
                            path.Close();
                            break;
                        case RoundedCorners.None:
                            path.MoveTo(left, top);
                            path.LineTo(right, top);
                            path.LineTo(right, bottom);
                            path.LineTo(left, bottom);
                            path.LineTo(left, bottom);
                            path.LineTo(left, top);
                            path.Close();
                            break;
                    }
                    DrawBackground(canvas, path, pressed);
                    DrawOutline(canvas, path);
                }

            }
        }
    }
}

