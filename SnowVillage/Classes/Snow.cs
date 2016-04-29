using SnowVillage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    /// <summary>
    /// 눈은 왼쪽 오른쪽으로 조금씩 흔들리면서 떨어지면 모든 눈은 떨어지는 속도가 같지 않습니다.
    /// </summary>
    public class Snow : BaseRenderable, ILogicRenderable, IDestroyable
    {

        /// <summary>
        /// 눈이 흔들리는 범위
        /// </summary>
        private const int minSwingValue = -2;

        /// <summary>
        /// 눈이 흔들리는 범위
        /// </summary>
        private const int maxSwingValue = 2;

        /// <summary>
        /// 눈과 관련된 각종 랜덤값을 생성해주는 랜덤객체
        /// </summary>
        private static Random snowPosMaker = new Random();

        /// <summary>
        /// 눈 생성시 최초 Y좌표 최대값
        /// </summary>
        private static int maxInitTopValue = 0;

        /// <summary>
        /// 눈 생성시 최초 Y좌표 최소값
        /// </summary>
        private static int minInitTopValue = -10;

        /// <summary>
        /// 눈의 크기
        /// </summary>
        private static Size size = new Size(3, 3);

        /// <summary>
        /// 눈이 바닥에 닿았을때부터 살아 있는 시간 nanosecond
        /// </summary>
        private static long timeToLive = 150000000;

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
        /// 눈이 바닥에 닿았을때 시간
        /// </summary>
        private DateTime timeGrounded = DateTime.MinValue;

        /// <summary>
        /// 어딘가에 부딪쳐서 더이상 움직일수 없을때 True, 아니면 False
        /// </summary>
        private bool isGrounded = false;

        /// <summary>
        /// 떨어지는 속도. 눈마다 다르다.
        /// </summary>
        private int dropSpeed = 0;

        /// <summary>
        /// 기본 생성자는 숨긴다.
        /// </summary>
        public Snow()
        {
            //X좌표를 랜덤으로 정해준다. 최대값은 그릴 캔버스의 넓이.
            int xPos = snowPosMaker.Next(0, GlobalConsts.CanvasSize.Width);
            int yPos = snowPosMaker.Next(minInitTopValue, maxInitTopValue);

            pos = new Point(xPos, yPos);
            dropSpeed = snowPosMaker.Next(minDropSpeed, maxDropSpeed);
        }

        /// <summary>
        /// IRenderable 구현 함수
        /// </summary>
        /// <param name="canvas">렌더링할 타겟</param>
        public override void Render(Graphics canvas)
        {
            if (isDead) return;

            Point worldPoint = GetWorldPoint();
            canvas.FillRectangle(snowBrush, worldPoint.X, worldPoint.Y,
                Snow.Size.Width, Snow.Size.Height);

            base.Render(canvas);
        }

        /// <summary>
        /// ILogicRenderable 구현 함수
        /// </summary>
        public void LogicRender()
        {
            if (isDead) return;

            if (isGrounded)
            {
                if (DateTime.Now.Ticks - TimeGrounded.Ticks > timeToLive)
                {
                    IsDead = true;
                }
            }
            else
            {
                bool noWind = Wind.Instance().Force == 0;
                if (noWind)
                {
                    int swingValue = snowPosMaker.Next(minSwingValue, maxSwingValue);
                    pos.X += swingValue;
                }
                else
                {
                    pos.X += Wind.Instance().Force;
                }

                pos.Y += dropSpeed;
            }
        }

        public static Size Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public bool IsGrounded
        {
            get
            {
                return isGrounded;
            }

            set
            {
                isGrounded = value;
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

        public DateTime TimeGrounded
        {
            get
            {
                return timeGrounded;
            }

            set
            {
                timeGrounded = value;
            }
        }
    }
}
