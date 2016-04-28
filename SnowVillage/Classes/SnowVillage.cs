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
        /// <summary>
        /// 화면에 그릴수 있는 객체 모음
        /// </summary>
        private List<IRenderable> renderableObjList = new List<IRenderable>();

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

            renderableObjList.Add(Moon.Instance());
            renderableObjList.Add(VillageSkyLine.Instance());
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

            //눈 생성
            for (int i = 0; i < Snow.CreateSnowCount; i++)
            {
                Snow snow = new Snow();
                renderableObjList.Add(snow);
                logicRenderableObjList.Add(snow);
            }

            //UFO 생성
            if (DateTime.Now.Ticks - createUFOTime.Ticks > UFO.CreateUFOCycleTime)
            {
                UFO ufo = new UFO();
                renderableObjList.Add(ufo);
                logicRenderableObjList.Add(ufo);
                createUFOTime = DateTime.Now;
            }

            /*
             * 수명이 다한 눈은 지워준다.
             * Loop안에서 리스트의 객체를 삭제해야 하기때문에 역순으로 찾음
             */
            for (int i = renderableObjList.Count-1; i >= 0; i--)
            {
                if (renderableObjList[i] is IDestroyable)
                {
                    if ((renderableObjList[i] as IDestroyable).IsDead)
                        renderableObjList.RemoveAt(i);
                }
            }

            //로직 적용이 필요한 객체들 로직 처리해 주기
            foreach (ILogicRenderable obj in logicRenderableObjList)
            {
                obj.LogicRender();
            }

            #endregion


            #region Rendering

            //화면 깜빡임 때문에 버퍼에 먼저 그리고 마지막에 한방에 Window에 그린다.
            Graphics temp = Graphics.FromImage(backBuffer);

            //바탕색을 칠함.
            temp.Clear(Color.Gray);

            //화면에 그려야될 객체들 그려주기
            foreach (IRenderable obj in renderableObjList)
            {
                obj.Render(temp);
            }
            #endregion

            //마지막으로 Window에 그림
            canvas.DrawImage(backBuffer, 0, 0);
        }
    }
}
