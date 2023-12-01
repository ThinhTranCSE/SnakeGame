namespace SnakeGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            PtbGamePlay = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PtbGamePlay).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(PtbGamePlay);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1010, 1010);
            panel1.TabIndex = 0;
            // 
            // PtbGamePlay
            // 
            PtbGamePlay.BackColor = SystemColors.GradientActiveCaption;
            PtbGamePlay.Location = new Point(3, 3);
            PtbGamePlay.Name = "PtbGamePlay";
            PtbGamePlay.Size = new Size(1000, 1000);
            PtbGamePlay.SizeMode = PictureBoxSizeMode.StretchImage;
            PtbGamePlay.TabIndex = 0;
            PtbGamePlay.TabStop = false;
            PtbGamePlay.Paint += UpdateGameFrame;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 1061);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            FormClosed += OnFormClosed;
            KeyPress += OnKeyPress;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PtbGamePlay).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox PtbGamePlay;
    }
}