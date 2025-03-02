namespace Lab4WF
{
    partial class PasswordForm
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.oldPasswordTxtBox = new System.Windows.Forms.MaskedTextBox();
            this.newPasswordTxtBox = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancalBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // oldPasswordTxtBox
            // 
            this.oldPasswordTxtBox.Location = new System.Drawing.Point(246, 73);
            this.oldPasswordTxtBox.Name = "oldPasswordTxtBox";
            this.oldPasswordTxtBox.PasswordChar = '*';
            this.oldPasswordTxtBox.Size = new System.Drawing.Size(245, 22);
            this.oldPasswordTxtBox.TabIndex = 0;
            // 
            // newPasswordTxtBox
            // 
            this.newPasswordTxtBox.Location = new System.Drawing.Point(246, 156);
            this.newPasswordTxtBox.Name = "newPasswordTxtBox";
            this.newPasswordTxtBox.PasswordChar = '*';
            this.newPasswordTxtBox.Size = new System.Drawing.Size(245, 22);
            this.newPasswordTxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Потверждение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Новый пароль";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(70, 257);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(227, 89);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancalBtn
            // 
            this.cancalBtn.Location = new System.Drawing.Point(379, 257);
            this.cancalBtn.Name = "cancalBtn";
            this.cancalBtn.Size = new System.Drawing.Size(241, 89);
            this.cancalBtn.TabIndex = 5;
            this.cancalBtn.Text = "Отмена";
            this.cancalBtn.UseVisualStyleBackColor = true;
            this.cancalBtn.Click += new System.EventHandler(this.cancalBtn_Click);
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancalBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPasswordTxtBox);
            this.Controls.Add(this.oldPasswordTxtBox);
            this.Name = "PasswordForm";
            this.Text = "Смена пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox oldPasswordTxtBox;
        private System.Windows.Forms.MaskedTextBox newPasswordTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancalBtn;
    }
}