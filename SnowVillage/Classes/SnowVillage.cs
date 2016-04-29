using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    /// <summary>
    /// 이 프로젝트의 Root Class
    /// </summary>
    public class SnowVillage : IRenderable, IDisposable
    {
        private static class LayerLevel
        {
            public static readonly int BaseLayer = 0;
            public static readonly int UFOLayer = 1;
            public static readonly int SnowLayer = 2;
        }

        /// <summary>
        /// 화면에 그릴수 있는 객체 모음
        /// </summary>
        private List<List<BaseRenderable>> renderableRootObjList = new List<List<BaseRenderable>>();

        /// <summary>
        /// 로직을 처리해야되는 객체 모음
        /// </summary>
        private List<ILogicRenderable> logicRenderableObjList = new List<ILogicRenderable>();


        /// <summary>
        /// 더블 버퍼링을 위해 사용할 객체
        /// </summary>
        private Bitmap backBuffer = new Bitmap(GlobalConsts.CanvasSize.Width, GlobalConsts.CanvasSize.Height);

        /// <summary>
        /// UFO를 생성한 시간
        /// </summary>
        private DateTime createUFOTime = DateTime.MinValue;

        public SnowVillage()
        {
            //리스트에 바람을 제일 먼저 넣어야 문제가 없다.
            logicRenderableObjList.Add(Wind.Instance());

            //지금은 Layer가 두개만 필요하기 때문에 두개만 만든다.
            renderableRootObjList.Add(new List<BaseRenderable>());
            renderableRootObjList.Add(new List<BaseRenderable>());
            renderableRootObjList.Add(new List<BaseRenderable>());

            renderableRootObjList[LayerLevel.BaseLayer].Add(Moon.Instance());
            renderableRootObjList[LayerLevel.BaseLayer].Add(VillageSkyLine.Instance());
        }

        /// <summary>
        /// IDisposable 구현 함수
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 직접 이 클래스가 가지고 있는 자원을 해제하기 위해 MSDN에서 권장하는 스타일로 만듦
        /// </summary>
        /// <param name="disposing">true이면 managed 리소스들도 같이 free한다., false 이면 나머지만 free한다.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                backBuffer.Dispose();
            }

            // free native resources if there are any.
        }

        /// <summary>
        /// IRenderable 구현 함수
        /// </summary>
        /// <param name="canvas">렌더링할 타겟</param>
        public void Render(Graphics canvas)
        {
            #region Game Logic

            //UFO 생성
            if (DateTime.Now.Ticks - createUFOTime.Ticks > UFO.CreateUFOCycleTime)
            {
                UFO ufo = new UFO();
                renderableRootObjList[LayerLevel.UFOLayer].Add(ufo);
                logicRenderableObjList.Add(ufo);
                createUFOTime = DateTime.Now;
            }

            //눈 생성
            for (int i = 0; i < Snow.CreateSnowCount; i++)
            {
                Snow snow = new Snow();
                renderableRootObjList[LayerLevel.SnowLayer].Add(snow);
                logicRenderableObjList.Add(snow);
            }

            for (int i = 0; i < renderableRootObjList.Count; i++)
                for (int j = renderableRootObjList[i].Count - 1; j >= 0; j--)
                {
                    /*
                    * 수명이 다한 객체는 지워준다.
                    * Loop안에서 리스트의 객체를 삭제해야 하기때문에 역순으로 찾음
                    */

                    if (renderableRootObjList[i][j].IsDead)
                        if (renderableRootObjList[i][j].TryDestroy() == true)
                            renderableRootObjList[i].RemoveAt(j);
                }

            //로직 적용이 필요한 객체들 로직 처리해 주기
            foreach (ILogicRenderable obj in logicRenderableObjList)
            {
                obj.LogicRender();
            }


            //눈 충돌 처리
            for (int i = renderableRootObjList[LayerLevel.SnowLayer].Count - 1; i >= 0; i--)
            {
                Snow snow = renderableRootObjList[LayerLevel.SnowLayer][i] as Snow;

                //스카이라인 충돌 체크
                //스카이라인 영역안에 있는지 검사
                if ((snow.Pos.X >= VillageSkyLine.Instance().Pos.X) &&
                    (snow.Pos.Y >= VillageSkyLine.Instance().Pos.Y) &&
                    (snow.Pos.X <= VillageSkyLine.Instance().Pos.X + VillageSkyLine.Image.Width - 1) &&
                    (snow.Pos.Y <= VillageSkyLine.Instance().Pos.Y + VillageSkyLine.Image.Height - 1))
                {
                    //눈의 위치의 픽셀을 가지고 온다.
                    Color villageColorSnowPlaced = VillageSkyLine.Image.GetPixel(
                                                        snow.Pos.X - VillageSkyLine.Instance().Pos.X,
                                                        snow.Pos.Y - VillageSkyLine.Instance().Pos.Y);

                    //픽셀에 알파값이 있다면 스카이라인영역이니 멈춘다.
                    if (villageColorSnowPlaced.A > 0)
                    {
                        snow.IsGrounded = true;
                        snow.TimeGrounded = DateTime.Now;
                        VillageSkyLine.Instance().AddChild(snow);
                        renderableRootObjList[LayerLevel.SnowLayer].RemoveAt(i);
                    }
                }


                // UFO와 충돌 체크
                //UFO 영역안에 있는지 검사
                for (int j = 0; j < renderableRootObjList[LayerLevel.UFOLayer].Count; j++)
                {
                    if ((snow.Pos.X >= renderableRootObjList[LayerLevel.UFOLayer][j].Pos.X) &&
                    (snow.Pos.Y >= renderableRootObjList[LayerLevel.UFOLayer][j].Pos.Y) &&
                    (snow.Pos.X <= renderableRootObjList[LayerLevel.UFOLayer][j].Pos.X + UFO.Image.Width - 1) &&
                    (snow.Pos.Y <= renderableRootObjList[LayerLevel.UFOLayer][j].Pos.Y + UFO.Image.Height - 1))
                    {
                        //눈의 위치의 픽셀을 가지고 온다.
                        Color ufoColorSnowPlaced = UFO.Image.GetPixel(
                                                            snow.Pos.X - renderableRootObjList[LayerLevel.UFOLayer][j].Pos.X,
                                                            snow.Pos.Y - renderableRootObjList[LayerLevel.UFOLayer][j].Pos.Y);

                        //픽셀에 알파값이 있다면 UFO영억이니 멈춘다.
                        if (ufoColorSnowPlaced.A > 0)
                        {
                            snow.IsGrounded = true;
                            snow.TimeGrounded = DateTime.Now;
                            renderableRootObjList[LayerLevel.UFOLayer][j].AddChild(snow);
                            renderableRootObjList[LayerLevel.SnowLayer].RemoveAt(i);
                        }
                    }
                }
            }
            #endregion


            #region Rendering

            //화면 깜빡임 때문에 버퍼에 먼저 그리고 마지막에 한방에 Window에 그린다.
            Graphics temp = Graphics.FromImage(backBuffer);

            //바탕색을 칠함.
            temp.Clear(Color.Gray);

            //화면에 그려야될 객체들 그려주기
            for (int i = 0; i < renderableRootObjList.Count; i++)
                for (int j = 0; j < renderableRootObjList[i].Count; j++)
                {
                    renderableRootObjList[i][j].Render(temp);
                }
            #endregion

            //마지막으로 Window에 그림
            canvas.DrawImage(backBuffer, 0, 0);
        }
    }
}
