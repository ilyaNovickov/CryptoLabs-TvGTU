namespace Lab4WF
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.addUserBtn = new System.Windows.Forms.Button();
            this.deleteUserBtn = new System.Windows.Forms.Button();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.blockChBox = new System.Windows.Forms.CheckBox();
            this.limitPasswChBox = new System.Windows.Forms.CheckBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.опрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.addUserToolBtn = new System.Windows.Forms.ToolStripButton();
            this.deleteUserToolBtn = new System.Windows.Forms.ToolStripButton();
            this.changePasswToolBtn = new System.Windows.Forms.ToolStripButton();
            this.saveToolBtn = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.ItemHeight = 16;
            this.listBoxUsers.Location = new System.Drawing.Point(21, 75);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(346, 308);
            this.listBoxUsers.TabIndex = 0;
            this.listBoxUsers.SelectedIndexChanged += new System.EventHandler(this.listBoxUsers_SelectedIndexChanged);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Location = new System.Drawing.Point(402, 224);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(222, 63);
            this.btnChangePassword.TabIndex = 1;
            this.btnChangePassword.Text = "Сменить пароль";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // addUserBtn
            // 
            this.addUserBtn.Location = new System.Drawing.Point(402, 64);
            this.addUserBtn.Name = "addUserBtn";
            this.addUserBtn.Size = new System.Drawing.Size(222, 55);
            this.addUserBtn.TabIndex = 2;
            this.addUserBtn.Text = "Добавить пользователя";
            this.addUserBtn.UseVisualStyleBackColor = true;
            this.addUserBtn.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // deleteUserBtn
            // 
            this.deleteUserBtn.Location = new System.Drawing.Point(402, 137);
            this.deleteUserBtn.Name = "deleteUserBtn";
            this.deleteUserBtn.Size = new System.Drawing.Size(222, 65);
            this.deleteUserBtn.TabIndex = 3;
            this.deleteUserBtn.Text = "Удалить пользователя";
            this.deleteUserBtn.UseVisualStyleBackColor = true;
            this.deleteUserBtn.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.Location = new System.Drawing.Point(402, 308);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(222, 64);
            this.btnViewLogs.TabIndex = 4;
            this.btnViewLogs.Text = "Просмотр логов";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            this.btnViewLogs.Click += new System.EventHandler(this.btnViewLogs_Click);
            // 
            // blockChBox
            // 
            this.blockChBox.AutoSize = true;
            this.blockChBox.Location = new System.Drawing.Point(655, 75);
            this.blockChBox.Name = "blockChBox";
            this.blockChBox.Size = new System.Drawing.Size(125, 20);
            this.blockChBox.TabIndex = 5;
            this.blockChBox.Text = "Заблокирован";
            this.blockChBox.UseVisualStyleBackColor = true;
            // 
            // limitPasswChBox
            // 
            this.limitPasswChBox.AutoSize = true;
            this.limitPasswChBox.Location = new System.Drawing.Point(655, 124);
            this.limitPasswChBox.Name = "limitPasswChBox";
            this.limitPasswChBox.Size = new System.Drawing.Size(167, 20);
            this.limitPasswChBox.TabIndex = 6;
            this.limitPasswChBox.Text = "Ограничение пароля";
            this.limitPasswChBox.UseVisualStyleBackColor = true;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(654, 174);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(167, 50);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.опрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.справкаToolStripMenuItem.Text = "Спра&вка";
            // 
            // опрограммеToolStripMenuItem
            // 
            this.опрограммеToolStripMenuItem.Name = "опрограммеToolStripMenuItem";
            this.опрограммеToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.опрограммеToolStripMenuItem.Text = "&О программе...";
            this.опрограммеToolStripMenuItem.Click += new System.EventHandler(this.опрограммеToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolBtn,
            this.deleteUserToolBtn,
            this.changePasswToolBtn,
            this.saveToolBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(151, 20);
            this.statusLabel.Text = "toolStripStatusLabel1";
            // 
            // addUserToolBtn
            // 
            this.addUserToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addUserToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("addUserToolBtn.Image")));
            this.addUserToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addUserToolBtn.Name = "addUserToolBtn";
            this.addUserToolBtn.Size = new System.Drawing.Size(180, 24);
            this.addUserToolBtn.Text = "Добавить пользователя";
            this.addUserToolBtn.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // deleteUserToolBtn
            // 
            this.deleteUserToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteUserToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("deleteUserToolBtn.Image")));
            this.deleteUserToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteUserToolBtn.Name = "deleteUserToolBtn";
            this.deleteUserToolBtn.Size = new System.Drawing.Size(169, 24);
            this.deleteUserToolBtn.Text = "Удалить пользователя";
            this.deleteUserToolBtn.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // changePasswToolBtn
            // 
            this.changePasswToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.changePasswToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("changePasswToolBtn.Image")));
            this.changePasswToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.changePasswToolBtn.Name = "changePasswToolBtn";
            this.changePasswToolBtn.Size = new System.Drawing.Size(137, 24);
            this.changePasswToolBtn.Text = "Изменить пароль";
            this.changePasswToolBtn.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // saveToolBtn
            // 
            this.saveToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveToolBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveToolBtn.Image")));
            this.saveToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolBtn.Name = "saveToolBtn";
            this.saveToolBtn.Size = new System.Drawing.Size(164, 24);
            this.saveToolBtn.Text = "Сохранить настройки";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.limitPasswChBox);
            this.Controls.Add(this.blockChBox);
            this.Controls.Add(this.btnViewLogs);
            this.Controls.Add(this.deleteUserBtn);
            this.Controls.Add(this.addUserBtn);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Программа";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button addUserBtn;
        private System.Windows.Forms.Button deleteUserBtn;
        private System.Windows.Forms.Button btnViewLogs;
        private System.Windows.Forms.CheckBox blockChBox;
        private System.Windows.Forms.CheckBox limitPasswChBox;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem опрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripButton addUserToolBtn;
        private System.Windows.Forms.ToolStripButton deleteUserToolBtn;
        private System.Windows.Forms.ToolStripButton changePasswToolBtn;
        private System.Windows.Forms.ToolStripButton saveToolBtn;
    }
}