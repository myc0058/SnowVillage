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
        private Timer renderTimer = null;
        private SnowVillage snowVillage = null;
        private Graphics g = null;

        public MainForm()
        {
            InitializeComponent();

            //코드 순서가 중요하기 때문에 주의해야함
            ClientSize = GlobalConsts.StageSize;

            g = CreateGraphics();

            snowVillage = new SnowVillage();
            snowVillage.DrawCanvas = g;

            renderTimer = new Timer();
            renderTimer.Interval = GlobalConsts.RenderTimerInterval;
            renderTimer.Tick += RenderTimer_Tick;
            renderTimer.Start();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            snowVillage.Render();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
