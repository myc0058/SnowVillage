using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    /// <summary>
    /// 월드 좌표와 로컬좌표를 개념을 추가하기 위해 생성된 렌더링 가능한 최상위 클래스
    /// </summary>
    public class BaseRenderable : IRenderable, IDestroyable
    {
        /// <summary>
        /// 자식 객체
        /// </summary>
        protected List<BaseRenderable> childs = new List<BaseRenderable>();

        /// <summary>
        /// 부모 객체
        /// </summary>
        protected BaseRenderable parent = null;
        
        /// <summary>
        /// 위치
        /// </summary>
        protected Point pos = new Point(0, 0);

        /// <summary>
        /// 수명을 다하면 true
        /// </summary>
        protected bool isDead = false;

        /// <summary>
        /// 월드 좌표 구하기
        /// </summary>
        /// <returns>월드 좌표</returns>
        protected Point GetWorldPoint()
        {
            //parent가 없을때까지(Root Object 인경우) 좌표를 누적시킨다.
            Point result = new Point(pos.X, pos.Y);
            BaseRenderable tempParent = parent;
            while (tempParent != null)
            {
                result.X += tempParent.Pos.X;
                result.Y += tempParent.Pos.Y;
                tempParent = tempParent.parent;
            }

            return result;
        }

        public virtual void Render(Graphics canvas)
        {
            foreach (IRenderable child in childs)
            {
                child.Render(canvas);
            }
        }

        public bool TryDestroy()
        {
            if (isDead)
            {
                foreach(BaseRenderable child in childs)
                {
                    child.isDead = true;
                }
                childs.Clear();
            }

            return isDead;
        }

        public void AddChild(BaseRenderable child)
        {
            this.childs.Add(child);
            //월드 좌표를 로컬 좌표로 변환
            child.Pos = new Point(child.Pos.X - this.Pos.X, child.Pos.Y - this.Pos.Y);
            child.parent = this;

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

        public Point Pos
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
    }
}
