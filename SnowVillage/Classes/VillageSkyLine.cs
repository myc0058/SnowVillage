using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class VillageSkyLine : IRenderable
    {
        /// <summary>
        /// 스카이 라인 배경을 그릴 위치
        /// </summary>
        private static Point pos = new Point(0, 287);

        /// <summary>
        /// 마을 스카이 라인 이미지
        /// </summary>
        private static Bitmap image = Properties.Resources.Village;

        #region 싱글톤 코드

        private static VillageSkyLine instance;

        private VillageSkyLine() { }

        public static VillageSkyLine Instance()
        {
            if (instance == null)
                instance = new VillageSkyLine();

            return instance;
        }

        #endregion

        public void Render(Graphics canvas)
        {
            //스카이라인 그리기
            canvas.DrawImage(Image, Pos);
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
