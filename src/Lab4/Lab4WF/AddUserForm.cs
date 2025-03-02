using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4WF
{
    public partial class AddUserForm : Form
    {
        private User currentUser = null;
        private UserManager userManager = null;

        public AddUserForm(User currentUser ,UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            this.currentUser = currentUser;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            string newUser = nameTxtBox.Text;

            if (userManager.AddUser(newUser, ""))
            {
                Logger.Log("Добавление пользователя", currentUser.Username, $"Добавлен новый пользователь: {newUser}");
                MessageBox.Show("Пользователь добавлен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("Такой пользователь уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
        }
    }
}
