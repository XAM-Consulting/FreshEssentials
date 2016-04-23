using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using FreshEssentials;
using FreshEssentials.iOS;

[assembly: ExportRenderer(typeof(AccessorizedTextCell), typeof(AccessorizedTextCellRenderer))]
[assembly: ExportRenderer(typeof(AccessorizedImageCell), typeof(AccessorizedImageCellRenderer))]
[assembly: ExportRenderer(typeof(AccessorizedViewCell), typeof(AccessorizedViewCellRenderer))]

namespace FreshEssentials.iOS
{
    public class AccessorizedTextCellRenderer : TextCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            var accessorizedCell = item as AccessorizedTextCell;

            if (accessorizedCell != null)
                cell.ApplyAccessory(item, accessorizedCell.Accessory);

            return cell;
        }
    }

    public class AccessorizedImageCellRenderer : ImageCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            var accessorizedCell = item as AccessorizedImageCell;

            if (accessorizedCell != null)
                cell.ApplyAccessory(item, accessorizedCell.Accessory);

            return cell;
        }
    }

    public class AccessorizedViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            var accessorizedCell = item as AccessorizedViewCell;

            if (accessorizedCell != null)
                cell.ApplyAccessory(item, accessorizedCell.Accessory);

            return cell;
        }
    }

    public static class AccessoryExtensions
    {
        public static void ApplyAccessory(this UITableViewCell nativeCell, Cell cell, Accessory accessory)
        {
            if (nativeCell == null)
                throw new ArgumentNullException("nativeCell");
            
            if (cell == null)
                throw new ArgumentNullException("cell");

            switch (accessory)
            {
                case Accessory.Checkmark:
                    nativeCell.Accessory = UITableViewCellAccessory.Checkmark;
                    break;

                case Accessory.DetailButton:
                    nativeCell.Accessory = UITableViewCellAccessory.DetailButton;
                    break;

                case Accessory.DetailDisclosureButton:
                    nativeCell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
                    break;

                case Accessory.DisclosureIndicator:
                    nativeCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    break;

                case Accessory.None:
                default:
                    nativeCell.Accessory = UITableViewCellAccessory.None;
                    break;
            }
        }
    }
}