using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SnowVillage
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 화면에 주기적으로 렌더링을 하기위해 사용할 타이머
        /// </summary>
        private Timer renderTimer = null;

        /// <summary>
        /// 이 프로젝트의 메인 클래스
        /// </summary>
        private SnowVillage snowVillage = null;

        /// <summary>
        /// 렌더링 함수에서 사용할 그래픽 객체
        /// </summary>
        private Graphics drawCanvas = null;

        public MainForm()
        {
            InitializeComponent();

            //코드 순서가 중요하기 때문에 주의해야함
            ClientSize = GlobalConsts.CanvasSize;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            drawCanvas = CreateGraphics();

            snowVillage = new SnowVillage();
            
            renderTimer = new Timer();
            renderTimer.Interval = GlobalConsts.RenderTimerInterval;
            renderTimer.Tick += RenderTimer_Tick;
            renderTimer.Start();
        }

        /// <summary>
        /// 렌더링 타이머가 주기적으로 호출하는 콜백함수. 동기로 호출된다.
        /// </summary>
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            snowVillage.Render(drawCanvas);
        }
    }
}
