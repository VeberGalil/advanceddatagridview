#region License
// Advanced DataGridView
//
// Copyright (c), 2020 Vladimir Bershadsky <vladimir@galileng.com>
// Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion

using System.Drawing;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal class TreeNodeStateImages
    {
        public const string KeyUnchecked = "uncheck";
        public const string KeyChecked = "check";
        public const string KeyIndeterminate = "mixed";
        public const string KeyCheckedRtl = "checkRtl";

        /// <summary>
        /// Create list of checkbox images, including indeterminate state (which standard TreeNode.Checked does not support)
        /// </summary>
        /// <returns>ImageList for TreeView.StateImageList</returns>
        public static ImageList GetCheckListStateImages()
        {
            ImageList images = new System.Windows.Forms.ImageList();
            Bitmap unCheckImg = new Bitmap(16, 16);
            Bitmap checkImg = new Bitmap(16, 16);
            Bitmap checkImgRtl = new Bitmap(16, 16);
            Bitmap mixedImg = new Bitmap(16, 16);

            using (Bitmap img = new Bitmap(16, 16))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
                    unCheckImg = (Bitmap)img.Clone();
                    CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
                    checkImg = (Bitmap)img.Clone();
                    CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), System.Windows.Forms.VisualStyles.CheckBoxState.MixedNormal);
                    mixedImg = (Bitmap)img.Clone();
                    g.Clear(Color.White);
                    CheckBoxRenderer.DrawCheckBox(g, new Point(3, 1), System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal);
                    checkImgRtl = (Bitmap)img.Clone();
                    checkImgRtl.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }

            images.Images.Add(KeyUnchecked, unCheckImg);
            images.Images.Add(KeyChecked, checkImg);
            images.Images.Add(KeyIndeterminate, mixedImg);
            images.Images.Add(KeyCheckedRtl, checkImgRtl);

            return images;
        }



    }
}
