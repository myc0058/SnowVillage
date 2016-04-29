using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class VillageSkyLine : BaseRenderable
    {
        /// <summary>
        /// 스카이 라인 배경을 그릴 위치
        /// </summary>
        private static Point initPos = new Point(0, 287);

        /// <summary>
        /// 마을 스카이 라인 이미지
        /// </summary>
        private static Bitmap image = Properties.Resources.Village;

        #region 싱글톤 코드

        private static VillageSkyLine instance;

        private VillageSkyLine()
        {
            pos.X = initPos.X;
            pos.Y = initPos.Y;
        }

        public static VillageSkyLine Instance()
        {
            if (instance == null)
                instance = new VillageSkyLine();

            return instance;
        }

        #endregion

        public override void Render(Graphics canvas)
        {
            //스카이라인 그리기
            Point worldPoint = GetWorldPoint();
            canvas.DrawImage(Image, worldPoint.X, worldPoint.Y, image.Width, image.Height);

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
