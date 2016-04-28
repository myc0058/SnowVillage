using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class UFO : IRenderable, ILogicRenderable, IDestroyable
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

        /// <summary>
        /// 위치
        /// </summary>
        private Point pos = new Point(0, 0);

        /// <summary>
        /// UFO를 파괴해야 할때 true
        /// </summary>
        private bool isDead = false;

        public UFO()
        {
            pos.X = initPos.X;
            pos.Y = initPos.Y;
        }

        public void LogicRender()
        {
            pos.X += speed;
        }

        public void Render(Graphics canvas)
        {
            canvas.DrawImage(image, pos);
        }

        public Point Pos
        {
            get
            {
                return pos;
            }
        }

        public static Bitmap Image
        {
            get
            {
                return image;
            }
        }

        public bool IsDead
        {
            get
            {
                return isDead;
            }

            set
            {
                isDead = value;
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
