using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Android.App;
using Android.Widget;
using FreshEssentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AButton = Android.Widget.Button;
using Object = Java.Lang.Object;


[assembly: ExportRenderer(typeof(BindablePicker), typeof(FreshEssentials.Droid.BindablePickerRendererDroid))]
namespace FreshEssentials.Droid
{
    public class BindablePickerRendererDroid : ViewRenderer<BindablePicker, AButton>
    {
        AlertDialog _dialog;

        bool _isDisposed;

        public BindablePickerRendererDroid()
        {
            AutoPackage = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_isDisposed)
            {
                _isDisposed = true;
                ((INotifyCollectionChanged)Element.Items).CollectionChanged -= RowsCollectionChanged;
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BindablePicker> e)
        {
            if (e.OldElement != null)
                ((INotifyCollectionChanged)e.OldElement.Items).CollectionChanged -= RowsCollectionChanged;

            if (e.NewElement != null)
            {
                ((INotifyCollectionChanged)e.NewElement.Items).CollectionChanged += RowsCollectionChanged;
                if (Control == null)
                {
                    var button = new AButton(Context) { Focusable = false, Clickable = true, Tag = this, Text = e.NewElement.Title };
                    button.SetOnClickListener(PickerListener.Instance);
                    SetNativeControl(button);
                }
                UpdatePicker();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Picker.TitleProperty.PropertyName)
                UpdatePicker();
            if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
                UpdatePicker();
        }

        void OnClick()
        {
            Picker model = Element;

            var picker = new NumberPicker(Context);
            if (model.Items != null && model.Items.Any())
            {
                picker.MaxValue = model.Items.Count - 1;
                picker.MinValue = 0;
                picker.SetDisplayedValues(model.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.Value = model.SelectedIndex;
                picker.DescendantFocusability = Android.Views.DescendantFocusability.BlockDescendants;
            }

            var layout = new LinearLayout(Context) { Orientation = Orientation.Vertical };
            layout.AddView(picker);

            var builder = new AlertDialog.Builder(Context);
            builder.SetView(layout);
            builder.SetTitle(model.Title ?? "");
            builder.SetNegativeButton(global::Android.Resource.String.Cancel, (s, a) =>
            {
                // It is possible for the Content of the Page to be changed when Focus is changed.
                // In this case, we'll lose our Control.
                Control?.ClearFocus();
                _dialog = null;
            });
            builder.SetPositiveButton(global::Android.Resource.String.Ok, (s, a) =>
            {
                ((IElementController)Element).SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged. 
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                {
                    // It is also possible for the Content of the Page to be changed when Focus is changed.
                    // In this case, we'll lose our Control.
                    Control?.ClearFocus();
                }
                _dialog = null;
            });

            (_dialog = builder.Create()).Show();
        }

        void RowsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdatePicker();
        }

        void UpdatePicker()
        {
            if (Element.SelectedIndex < 0)
                Control.Text = Element.Title;
            else
                Control.Text = Element.Items[Element.SelectedIndex];
        }

        class PickerListener : Object, IOnClickListener
        {
            public static readonly PickerListener Instance = new PickerListener();

            public void OnClick(global::Android.Views.View v)
            {
                var renderer = v.Tag as BindablePickerRendererDroid;
                if (renderer == null)
                    return;

                renderer.OnClick();
            }
        }
    }
}