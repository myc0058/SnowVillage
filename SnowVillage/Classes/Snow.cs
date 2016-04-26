using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    /// <summary>
    /// 눈은 왼쪽 오른쪽으로 조금씩 흔들리면서 떨어지면 모든 눈은 떨어지는 속도가 같지 않습니다.
    /// </summary>
    public class Snow : IRenderable, ILogicRenderable
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
        private const int maxInitTopValue = 0;

        /// <summary>
        /// 눈 생성시 최초 Y좌표 최소값
        /// </summary>
        private const int minInitTopValue = -10;

        /// <summary>
        /// 눈의 크기
        /// </summary>
        private static Size size = new Size(3, 3);

        /// <summary>
        /// 눈이 바닥에 닿았을때부터 살아 있는 시간 nanosecond
        /// </summary>
        private static int TimeToLive = 90000000;

        /// <summary>
        /// 눈이 바닥에 닿았을때 시간
        /// </summary>
        private DateTime timeGrounded = DateTime.MinValue;

        /// <summary>
        /// 자기 자신의 좌표
        /// </summary>
        private Point point = Point.Empty;

        /// <summary>
        /// 어딘가에 부딪쳐서 더이상 움직일수 없을때 True, 아니면 False
        /// </summary>
        private bool isGrounded = false;

        /// <summary>
        /// 눈이 수명을 다하면 true
        /// </summary>
        private bool isDead = false;

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

            point = new Point(xPos, yPos);
            dropSpeed = snowPosMaker.Next(GlobalConsts.MinDropSpeed, GlobalConsts.MaxDropSpeed);
        }

        /// <summary>
        /// IRenderable 구현 함수
        /// </summary>
        /// <param name="canvas">렌더링할 타겟</param>
        public void Render(Graphics canvas)
        {
            canvas.FillRectangle(GlobalConsts.SnowBrush, point.X, point.Y,
                Snow.Size.Width, Snow.Size.Height);
        }

        /// <summary>
        /// ILogicRenderable 구현 함수
        /// </summary>
        public void LogicRender()
        {
            if (isGrounded)
            {
                if (DateTime.Now.Ticks - timeGrounded.Ticks > TimeToLive)
                {
                    IsDead = true;
                }
            }
            else
            {
                /*
                 * 바닥에 닿았을때 처리
                 * 스카이라인에 닿으면 멈추도록 하기위해 주석처리 한다. 
                if (point.Y + dropSpeed > GlobalConsts.CanvasSize.Height)
                { 
                    IsGrounded = true;
                    timeGrounded = DateTime.Now;
                    return;
                }
                */

                bool noWind = Wind.Instance().Force == 0;
                if (noWind)
                {
                    int swingValue = snowPosMaker.Next(minSwingValue, maxSwingValue);
                    point.X += swingValue;
                }
                else
                {
                    point.X += Wind.Instance().Force;
                }

                point.Y += dropSpeed;


                /*
                 * 스카이라인에 닿으면 눈을 멈춘다.
                 */

                //스카이라인 영역안에 있는지 검사
                if ((point.X >= GlobalConsts.InitPosVillage.X) && 
                    (point.Y >= GlobalConsts.InitPosVillage.Y) &&
                    (point.X <= GlobalConsts.InitPosVillage.X + GlobalConsts.BgVillage.Width-1) &&
                    (point.Y <= GlobalConsts.InitPosVillage.Y + GlobalConsts.BgVillage.Height-1))
                {
                    //눈의 위치의 픽셀을 가지고 온다.
                    Color villageColorSnowPlaced = GlobalConsts.BgVillage.GetPixel(
                                                        point.X - GlobalConsts.InitPosVillage.X,
                                                        point.Y - GlobalConsts.InitPosVillage.Y);

                    //픽셀에 알파값이 있다면 스카이라인영역이니 멈춘다.
                    if (villageColorSnowPlaced.A > 0)
                    {
                        IsGrounded = true;
                        timeGrounded = DateTime.Now;
                        return;
                    }
                }
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
    }
}
