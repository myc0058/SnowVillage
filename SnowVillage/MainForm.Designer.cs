namespace SnowVillage
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            drawCanvas.Dispose();
            snowVillage.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbVillage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbVillage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbVillage
            // 
            this.pbVillage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbVillage.Image = global::SnowVillage.Properties.Resources.Village;
            this.pbVillage.InitialImage = null;
            this.pbVillage.Location = new System.Drawing.Point(0, 287);
            this.pbVillage.Name = "pbVillage";
            this.pbVillage.Size = new System.Drawing.Size(960, 480);
            this.pbVillage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbVillage.TabIndex = 0;
            this.pbVillage.TabStop = false;
            this.pbVillage.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.pbVillage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "SnowVillage";
            ((System.ComponentModel.ISupportInitialize)(this.pbVillage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVillage;
    }
}

