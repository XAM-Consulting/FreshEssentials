using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace FreshEssentials
{
    public class ListViewItemTappedAttached
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(ListView),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: OnItemTappedChanged);


        public static ICommand GetItemTapped(BindableObject bindable)
        {
            return (ICommand)bindable.GetValue(CommandProperty);
        }

        public static void SetItemTapped(BindableObject bindable, ICommand value)
        {
            bindable.SetValue(CommandProperty, value);
        }

        public static void OnItemTappedChanged(BindableObject bindable, object oldValue, object newValue)
        {			
            if (!(bindable is ListView listView))
                return;

            if (oldValue != null)
                listView.ItemTapped -= OnItemTapped;

            if (newValue != null)
                listView.ItemTapped += OnItemTapped;
        }

        private static void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var control = sender as ListView;
            var command = GetItemTapped(control);

            if (command != null && command.CanExecute(e.Item))
                command.Execute(e.Item);
        }
    }
}

