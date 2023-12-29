namespace GameClient
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
            LoginBtn = new Button();
            UserNameTxb = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // LoginBtn
            // 
            LoginBtn.Location = new Point(175, 73);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(75, 23);
            LoginBtn.TabIndex = 0;
            LoginBtn.Text = "Bắt đầu";
            LoginBtn.UseVisualStyleBackColor = true;
            LoginBtn.Click += OnLoginBtnClick;
            // 
            // UserNameTxb
            // 
            UserNameTxb.Location = new Point(12, 73);
            UserNameTxb.Name = "UserNameTxb";
            UserNameTxb.Size = new Size(157, 23);
            UserNameTxb.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 55);
            label1.Name = "label1";
            label1.Size = new Size(74, 15);
            label1.TabIndex = 2;
            label1.Text = "Tên nhân vật";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(13, 18);
            label2.Name = "label2";
            label2.Size = new Size(237, 21);
            label2.TabIndex = 3;
            label2.Text = "Nhập tên nhân vật để bắt đầu";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 124);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(UserNameTxb);
            Controls.Add(LoginBtn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoginBtn;
        private TextBox UserNameTxb;
        private Label label1;
        private Label label2;
    }
}