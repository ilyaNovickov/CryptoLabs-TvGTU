using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Lab4WF
{
    public partial class MainForm : Form
    {
        private Stopwatch sw = new Stopwatch();

        private User currentUser;
        private UserManager userManager = null;

        public MainForm(User user, UserManager userManager)
        {
            InitializeComponent();
            currentUser = user;
            this.userManager = userManager;
            LoadUsers();

            statusLabel.Text = currentUser.IsAdmin ? "Вы админ" : "Вы не админ";

            #region Disable Admin tools
            if (!currentUser.IsAdmin)
            {
                addUserToolBtn.Enabled = false;
                addUserBtn.Enabled = false;
                deleteUserBtn.Enabled = false;
                deleteUserToolBtn.Enabled = false;
                btnViewLogs.Enabled = false;
                saveBtn.Enabled = false;
                saveToolBtn.Enabled = false;
                blockChBox.Enabled = false;
                limitPasswChBox.Enabled = false;
            }
            #endregion

            sw.Start();
        }
        

        private void LoadUsers()
        {
            listBoxUsers.Items.Clear();
            if (currentUser.IsAdmin)
            {
                foreach (var user in userManager.GetAllUsers())
                {
                    listBoxUsers.Items.Add(user.Username);
                }
            }
            else
            {
                listBoxUsers.Items.Add(currentUser.Username);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (listBoxUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Нет выбранного пользователя для изменения пароля", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PasswordForm passwordForm = new PasswordForm(currentUser, userManager.GetUser(listBoxUsers.SelectedItem.ToString()), userManager);
            passwordForm.ShowDialog();
            passwordForm.Dispose();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (currentUser.IsAdmin)
            {
                AddUserForm addUserForm = new AddUserForm(currentUser, userManager);
                if (addUserForm.ShowDialog() == DialogResult.OK)
                    LoadUsers();
                addUserForm.Dispose();
            }
            else
            {
                MessageBox.Show("Нового пользователя может добавить только администратор", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (currentUser.IsAdmin)
            {
                int index = listBoxUsers.SelectedIndex;

                if (index == -1)
                {
                    MessageBox.Show("Не выбран пользователь на удаление", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                User selectedUser = userManager.GetAllUsers()[index]; 

                if (selectedUser != null && selectedUser.Username != "admin")
                {
                    userManager.RemoveUser(selectedUser.Username);
                    Logger.Log("Удаление пользователя", currentUser.Username, $"Удалён пользователь: {selectedUser.Username}");
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Нельзя удалить администратора!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Пользователя может удалить только администратор",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            if (currentUser.IsAdmin)
            {
                if (File.Exists("log.txt"))
                {
                    string logs = File.ReadAllText("log.txt");
                    MessageBox.Show(logs, "Журнал событий", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Лог-файл пуст или отсутствует.", "Журнал событий", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Только администратор может просматривать лог файлы через приложение",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void listBoxUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            User user = userManager.GetUser(listBoxUsers.SelectedItem.ToString());

            if (user == null)
                return;

            blockChBox.Checked = user.IsBlocked;
            limitPasswChBox.Checked = user.IsLimitationForPassword;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (!currentUser.IsAdmin)
            {
                MessageBox.Show("Только администратор может изменять ограничения пароля и блокировать/разблокировать пользователей", 
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                blockChBox.Checked = !blockChBox.Checked;
                limitPasswChBox.Checked = !limitPasswChBox.Checked;
                return;
            }

            if (listBoxUsers.SelectedItem == null)
            {
                MessageBox.Show("Нет выбранного пользователя",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User user = userManager.GetUser(listBoxUsers.SelectedItem.ToString());

            if (user == null)
            {
                MessageBox.Show("Нет выбранного пользователя", "Ошибка");
                return;
            }

            if (user.IsAdmin && blockChBox.Checked)
            {
                MessageBox.Show("Нельзя заблокировать учётную запись администратора\nИзменение этого свойства не произошло", 
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                blockChBox.Checked = false;
            }
            else
                user.IsBlocked = blockChBox.Checked;

            user.IsLimitationForPassword = limitPasswChBox.Checked;

            userManager.SaveUsers();

            Logger.Log("Изменение свойст", currentUser.Username, $"Изменены свойства IsBlocked : {user.IsBlocked} и IsLimitedPassword : {user.IsLimitationForPassword}");

            MessageBox.Show("Свойства изменены", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void опрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Log("Завершение программы", currentUser.Username, $"Программа завершена по прицине : {e.CloseReason}; длительность сессии : {sw.Elapsed.ToString(@"hh\:mm\:ss")} ");
        }
    }
}
