using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class Moon : IRenderable
    {
        /// <summary>
        /// 위치
        /// </summary>
        private static Point pos = new Point(0, 0);

        /// <summary>
        /// 이미지
        /// </summary>
        private static Bitmap image = Properties.Resources.Moon;

        #region 싱글톤 코드

        private static Moon instance;

        private Moon()
        {
            //달을 중앙 정렬 시킨다. 그냥 멋을 위해서
            pos.X = (GlobalConsts.CanvasSize.Width - image.Width) / 2 ;
            pos.Y = (GlobalConsts.CanvasSize.Height - image.Height) / 2;
        }

        public static Moon Instance()
        {
            if (instance == null)
                instance = new Moon();

            return instance;
        }

        #endregion

        public void Render(Graphics canvas)
        {
            canvas.DrawImage(Image, pos.X, pos.Y, Image.Width, Image.Height);
        }

        public static Point Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }

        public static Bitmap Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }
    }
}
