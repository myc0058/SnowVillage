using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    /// <summary>
    /// 렌더링이 필요한 객체들은 이 Interface를 상속받는다.
    /// </summary>
    public interface IRenderable
    {
        void Render(Graphics canvas);
    }
}
