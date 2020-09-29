using System;
using System.Collections.Generic;
using System.IO;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace BasicTable {
	public class TableSource : UITableViewSource {
		
		protected string[] tableItems;
		protected string cellIdentifier = "TableCell";
		HomeScreen owner;

		public TableSource (string[] items, HomeScreen owner)
		{
			tableItems = items;
			this.owner = owner;

		}
		
		public override nint NumberOfSections (UITableView tableView)
		{
			return 2;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{

            if (section==1)
            {
				return 2;
            }

			return 1;
		}

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 50;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			string item = tableItems[indexPath.Row];
			
			if (cell == null)
			{ cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier); }

			cell.BackgroundColor = UIColor.Clear;


			this.addCellWithIndexPath(cell,indexPath);



			return cell;
		}
		
		public void addCellWithIndexPath(UIView myView, NSIndexPath indexPath)
        {

			UILabel cell = new UILabel();
			cell.BackgroundColor = UIColor.Blue;
			cell.Text = "test";

			UIView shadowView = new UIView(new CoreGraphics.CGRect(50, 0, UIScreen.MainScreen.Bounds.Size.Width-100, 50));
			shadowView.Layer.ShadowColor = UIColor.Black.CGColor;
			shadowView.Layer.ShadowRadius = 8.0f;
			shadowView.Layer.ShadowOffset = new CGSize(13.0, 13.0);
			shadowView.Layer.ShadowOpacity = 0.5f;

			cell.Frame = shadowView.Bounds;

			//Top Left Right Corners
			var maskPathTop = UIBezierPath.FromRoundedRect(cell.Bounds, UIRectCorner.TopLeft | UIRectCorner.TopRight, new CoreGraphics.CGSize(8, 8));
			var shapeLayerTop = new CAShapeLayer();
			shapeLayerTop.Frame = cell.Bounds;
			//shapeLayerTop.BorderWidth = 3;
			//shapeLayerTop.BorderColor = VM.RedColor.ToNativeColor().CGColor;
			shapeLayerTop.Path = maskPathTop.CGPath;

			//Bottom Left Right Corners
			var maskPathBottom = UIBezierPath.FromRoundedRect(cell.Bounds, UIRectCorner.BottomLeft | UIRectCorner.BottomRight, new CoreGraphics.CGSize(8, 8));
			var shapeLayerBottom = new CAShapeLayer();
			shapeLayerBottom.Frame = cell.Bounds;
			//shapeLayerBottom.BorderColor = VM.RedColor.ToNativeColor().CGColor;
			//shapeLayerBottom.BorderWidth = 3;
			shapeLayerBottom.Path = maskPathBottom.CGPath;

			//All Corners
			var maskPathAll = UIBezierPath.FromRoundedRect(cell.Bounds, UIRectCorner.BottomLeft | UIRectCorner.BottomRight | UIRectCorner.TopLeft | UIRectCorner.TopRight, new CoreGraphics.CGSize(8, 8));
			var shapeLayerAll = new CAShapeLayer();
			shapeLayerAll.Frame = cell.Bounds;
			//shapeLayerAll.BorderWidth = 3;
			//shapeLayerAll.BorderColor = VM.RedColor.ToNativeColor().CGColor;
			shapeLayerAll.Path = maskPathAll.CGPath;



            if (indexPath.Section == 1)
            {
                if (indexPath.Row == 0)
                {
					cell.Layer.Mask = shapeLayerTop;
				}
				else if (indexPath.Row == 1)
                {
					cell.Layer.Mask = shapeLayerBottom;
				}
			}
            else
            {
				cell.Layer.Mask = shapeLayerAll;
			}

			shadowView.AddSubview(cell);
			myView.Add(shadowView);
		}
	}
}