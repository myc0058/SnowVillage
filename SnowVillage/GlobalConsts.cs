using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class GlobalConsts
    {
        private static Size stageSize = new Size(800, 600);

        //millisecond
        private static int renderTimerInterval = 50;

        public static Size StageSize
        {
            get
            {
                return stageSize;
            }

            set
            {
                stageSize = value;
            }
        }

        public static int RenderTimerInterval
        {
            get
            {
                return renderTimerInterval;
            }

            set
            {
                renderTimerInterval = value;
            }
        }
    }
}
