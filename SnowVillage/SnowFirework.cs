using SnowVillage.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SnowVillage
{
    public class SnowVillage : IRenderable, IDisposable
    {
        private Graphics drawCanvas = null;
        private Brush snowBrush = new SolidBrush(Color.White);

        public SnowVillage()
        {
        }

        #region IRenderable Implementation

        public void Render()
        {
            //바탕색을 칠함.
            drawCanvas.Clear(Color.Black);


        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                snowBrush.Dispose();
                snowBrush = null;
            }
        }            

        #endregion

        public Graphics DrawCanvas
        {
            get
            {
                return drawCanvas;
            }

            set
            {
                drawCanvas = value;
            }
        }
    }
}
