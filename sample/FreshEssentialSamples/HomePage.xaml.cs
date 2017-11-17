using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FreshEssentialSamples
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command OpenPage
        {
            get
            {
                return new Command((param) =>
                    {
                        var pageName = param as string;
                        if (pageName == "Picker")
                        {
                            var pickerPage = new BindablePickerExample();
                            pickerPage.BindingContext = new BindableViewModel();
                            Navigation.PushAsync(pickerPage);
                        }
                        else if (pageName == "Frame")
                        {
                            Navigation.PushAsync(new AdvancedFrameSample());
                        }
                        else if (pageName == "SegmentedButton")
                        {
                            var segmentdPage = new SegmentedButtonGroupSample();
                            segmentdPage.BindingContext = new BindableViewModel();
                            Navigation.PushAsync(segmentdPage);
                        }
                        else if (pageName == "InverseBool")
                        {
                            var inversePage = new InverseBooleanSample();
                            inversePage.BindingContext = new BindableViewModel();
                            Navigation.PushAsync(inversePage);
                        }
                        else if (pageName == "TappedAttached")
                        {
                            var itemTapPage = new ItemTappedTappedAttachedSample();
                            itemTapPage.BindingContext = new BindableViewModel();
                            Navigation.PushAsync(itemTapPage);
                        }
                        else if (pageName == "TappedGesture")
                        {
                            var tapPage = new TappedGestureAttachedSample();
                            tapPage.BindingContext = new BindableViewModel();
                            Navigation.PushAsync(tapPage);
                        }
                        else if (pageName == "AutoGrid")
                        {
                            var autoGridPage = new AutoGridSample();
                            Navigation.PushAsync(autoGridPage);
                        }

                    });
            }
        }
    }
}

