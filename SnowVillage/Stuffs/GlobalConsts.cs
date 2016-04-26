using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    static class GlobalConsts
    {
        /// <summary>
        /// 렌더링될 화면 사이즈
        /// </summary>
        private static Size stageSize = new Size(960, 600);

        /// <summary>
        /// 렌더타이머 콜백함수 호출주기 miilisecond
        /// </summary>
        private const int renderTimerInterval = 50;

        /// <summary>
        /// 눈을 그릴때 사용할 Brush
        /// </summary>
        private static Brush snowBrush = new SolidBrush(Color.White);

        /// <summary>
        /// 눈이 떨어지는 최소 속도
        /// </summary>
        private const int minDropSpeed = 3;

        /// <summary>
        /// 눈이 떨어지는 최대 속도
        /// </summary>
        private const int maxDropSpeed = 7;

        /// <summary>
        /// 한번에 눈이 생성되는 수
        /// </summary>
        private const int createSnowCount = 5;

        /// <summary>
        /// 스카이 라인 배경을 그릴 위치
        /// </summary>
        private static Point initPosVillage = new Point(0, 287);

        private static Bitmap bgVillage = Properties.Resources.Village;

        public static Size CanvasSize
        {
            get
            {
                return stageSize;
            }
        }

        public static int RenderTimerInterval
        {
            get
            {
                return renderTimerInterval;
            }
        }

        public static Brush SnowBrush
        {
            get
            {
                return snowBrush;
            }
        }

        public static int CreateSnowCount
        {
            get
            {
                return createSnowCount;
            }
        }

        public static int MinDropSpeed
        {
            get
            {
                return minDropSpeed;
            }
        }

        public static int MaxDropSpeed
        {
            get
            {
                return maxDropSpeed;
            }
        }

        public static Point InitPosVillage
        {
            get
            {
                return initPosVillage;
            }

            set
            {
                initPosVillage = value;
            }
        }

        public static Bitmap BgVillage
        {
            get
            {
                return bgVillage;
            }

            set
            {
                bgVillage = value;
            }
        }
    }
}
