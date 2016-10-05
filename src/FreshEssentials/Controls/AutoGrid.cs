using System;
using Xamarin.Forms;

namespace FreshEssentials
{
    public enum AutoStepType
    {
        None,
        Begin,
        Step,
        Hold
    }
    public class AutoGrid : Grid
    {
        static int RowNumber = 0;
        static int ColumnNumber = 0;
        public AutoGrid() : base()
        {
        }

        public static readonly BindableProperty AutoRowProperty = BindableProperty.CreateAttached(
            "AutoRow",
            typeof(AutoStepType),
            typeof(AutoGrid),
            AutoStepType.None,
            validateValue: (bindable, value) => Enum.IsDefined(typeof(AutoStepType), value),
            propertyChanged: OnAutoRowChanged
        );

        public static readonly BindableProperty AutoColumnProperty = BindableProperty.CreateAttached(
            "AutoColumn",
            typeof(AutoStepType),
            typeof(AutoGrid),
            AutoStepType.None,
            validateValue: (bindable, value) => Enum.IsDefined(typeof(AutoStepType), value),
            propertyChanged: OnAutoColumnChanged
        );

        public static AutoStepType GetAutoColumn(BindableObject bindable)
        {
            return (AutoStepType)bindable.GetValue(AutoColumnProperty);
        }

        public static AutoStepType GetAutoRow(BindableObject bindable)
        {
            return (AutoStepType)bindable.GetValue(AutoRowProperty);
        }

        public static void SetAutoColumn(BindableObject bindable, AutoStepType value)
        {
            bindable.SetValue(AutoColumnProperty, value);
        }

        public static void SetAutoRow(BindableObject bindable, AutoStepType value)
        {
            bindable.SetValue(AutoRowProperty, value);
        }

        public static void OnAutoColumnChanged(BindableObject bindable, object oldValue, object newValue)
        {
            switch ((AutoStepType)newValue)
            {
                case AutoStepType.Begin:
                    ColumnNumber = 0;
                    break;
                case AutoStepType.Step:
                    ColumnNumber++;
                    break;
                case AutoStepType.Hold:
                    break;
            }

            bindable.SetValue(ColumnProperty, ColumnNumber);
        }

        public static void OnAutoRowChanged(BindableObject bindable, object oldValue, object newValue)
        {
            switch ((AutoStepType)newValue)
            {
                case AutoStepType.Begin:
                    RowNumber = 0;
                    break;
                case AutoStepType.Step:
                    RowNumber++;
                    break;
                case AutoStepType.Hold:
                    break;
            }

            bindable.SetValue(RowProperty, RowNumber);
        }
    }
}

