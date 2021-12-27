using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.KeyboardButton
{
   public class RMSGenericButton:Button
    {
        private Image bgImageOnMouseDown;
        private Image bgImageOnMouseUp;
        private Color foreColorOnMouseUp = Color.White;
        private Color foreColorOnMouseDown = Color.White;

        public RMSGenericButton()
        {
            //base.BackColor = Color.Transparent;
            //base.BackgroundImageLayout = ImageLayout.Tile;
            //base.FlatAppearance.MouseDownBackColor = Color.Transparent;
            //base.FlatAppearance.MouseOverBackColor = Color.Transparent;
            //base.FlatStyle = FlatStyle.Flat;
            //base.FlatAppearance.BorderSize = 0;

        }


        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            base.ForeColor = foreColorOnMouseDown;
            if (bgImageOnMouseDown != null)
            {
                base.BackgroundImage = bgImageOnMouseDown;
            }

        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            base.ForeColor = foreColorOnMouseUp;
            if (bgImageOnMouseUp != null)
            {
                base.BackgroundImage = bgImageOnMouseUp;

            }
        }

        public Image BgImageOnMouseDown
        {
            get { return this.bgImageOnMouseDown; }
            set { this.bgImageOnMouseDown = value; }
        }
        public Image BgImageOnMouseUp
        {
            get { return this.bgImageOnMouseUp; }
            set { this.bgImageOnMouseUp = value; }
        }
        public Color ForeColorOnMouseDown
        {
            get { return this.foreColorOnMouseDown; }
            set { this.foreColorOnMouseDown = value; }
        }
        public Color ForeColorOnMouseUp
        {
            get { return this.foreColorOnMouseUp; }
            set { this.foreColorOnMouseUp = value; }
        }
    }
}
