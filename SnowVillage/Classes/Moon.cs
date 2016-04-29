using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class Moon : BaseRenderable
    {
        /// <summary>
        /// 위치
        /// </summary>
        private static Point initPos = new Point(0, 0);

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

        public override void Render(Graphics canvas)
        {
            Point worldPoint = GetWorldPoint();
            canvas.DrawImage(Image, worldPoint.X, worldPoint.Y, Image.Width, Image.Height);

            base.Render(canvas);
        }

        public static Point InitPos
        {
            get
            {
                return initPos;
            }

            set
            {
                initPos = value;
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
