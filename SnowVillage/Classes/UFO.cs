using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class UFO : BaseRenderable, ILogicRenderable, IDestroyable
    {
        /// <summary>
        /// 이미지
        /// </summary>
        private static Bitmap image = Properties.Resources.UFO;

        /// <summary>
        /// 속도
        /// </summary>
        private static int speed = 5;

        /// <summary>
        /// UFO 생성주기 nanosecond
        /// </summary>
        private static long createUFOCycleTime = 150000000;

        /// <summary>
        /// UFO가 생성되는 시작위치
        /// </summary>
        private static Point initPos = new Point(-20, 200);

        public UFO()
        {
            pos.X = initPos.X;
            pos.Y = initPos.Y;
        }

        public void LogicRender()
        {
            if (isDead) return;

            pos.X += speed;
        }

        public override void Render(Graphics canvas)
        {
            if (isDead) return;

            Point worldPoint = GetWorldPoint();
            canvas.DrawImage(image, worldPoint.X, worldPoint.Y, image.Width, image.Height);

            base.Render(canvas);
        }

        public static Bitmap Image
        {
            get
            {
                return image;
            }
        }

        public static long CreateUFOCycleTime
        {
            get
            {
                return createUFOCycleTime;
            }

            set
            {
                createUFOCycleTime = value;
            }
        }
    }
}
