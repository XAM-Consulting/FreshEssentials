using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FreshEssentials;

namespace FreshEssentialSamples
{
    public partial class AdvancedFrameSample : ContentPage
    {
        public AdvancedFrameSample()
        {
            InitializeComponent();
            this.LeftFrame.Corners = RoundedCorners.Left;
        }
    }
}

