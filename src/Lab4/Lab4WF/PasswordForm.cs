using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4WF
{
    public partial class PasswordForm : Form
    {
        private User currentUser = null;
        private User targetUser = null;
        private UserManager userManager  = null;

        public PasswordForm(User currentUser, User targetUser, UserManager userManager)
        {
            InitializeComponent();
            this.currentUser = currentUser;
            this.userManager = userManager;
            this.targetUser = targetUser;
        }

        private void cancalBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (! (currentUser.Password == oldPasswordTxtBox.Text) )
            {
                MessageBox.Show("Пароль не был изменен (неверный пароль для потверждения)", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.Log("Изменение пароля", currentUser.Username, "Пароль не изменён");
                //DialogResult = DialogResult.Cancel;
                return;
            }

            string newPassword = newPasswordTxtBox.Text;
            if (!string.IsNullOrEmpty(newPassword))
            {
                if (targetUser.IsLimitationForPassword && !Regex.IsMatch(newPassword, userManager.PasswordPattern))
                {
                    MessageBox.Show("Пароль не корректен", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

                userManager.ChangePassword(targetUser.Username, newPassword);
                Logger.Log("Изменение пароля", currentUser.Username, $"Пароль успешно изменён для пользователя {targetUser.Username}");
                MessageBox.Show("Пароль изменен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                return;
            }
        }
    }
}
