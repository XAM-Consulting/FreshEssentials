using Xamarin.Forms;

namespace FreshEssentials
{
    public enum Accessory
    {
        None,
        Checkmark,
        DetailButton,
        DetailDisclosureButton,
        DisclosureIndicator
    }

    public class AccessorizedViewCell : ViewCell
    {
        public Accessory Accessory
        {
            get;
            set;
        }
    }

    public class AccessorizedImageCell : ImageCell
    {
        public Accessory Accessory
        {
            get;
            set;
        }
    }

    public class AccessorizedTextCell : TextCell
    {
        public Accessory Accessory
        {
            get;
            set;
        }
    }
}