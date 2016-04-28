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
        private static Size canvasSize = new Size(960, 600);

        /// <summary>
        /// 렌더타이머 콜백함수 호출주기 miilisecond
        /// </summary>
        private const int renderTimerInterval = 50;

        public static Size CanvasSize
        {
            get
            {
                return canvasSize;
            }
        }

        public static int RenderTimerInterval
        {
            get
            {
                return renderTimerInterval;
            }
        }
    }
}
