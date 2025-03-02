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
    public partial class LoginForm  : Form
    {
        private UserManager userManager = null;
        private User loggedInUser = null; // Хранит авторизованного пользователя
        private int countofRetry = 0;
        private const int maxRetry = 3;

        public LoginForm(UserManager userManager)
        {
            InitializeComponent();

            this.userManager = userManager;
        }

        public User GetLoggedInUser()
        {
            return loggedInUser; // Возвращаем авторизованного пользователя
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (countofRetry == maxRetry && userManager.GetUser(username) != null)
            {
                User userToBlock = userManager.GetUser(username);
                userToBlock.IsBlocked = true;
                userManager.SaveUsers();
                Application.Exit();
            }            

            User user = userManager.ValidateUser(username, password);
            if (user != null)
            {
                if (user.IsBlocked)
                {
                    MessageBox.Show("Пользователь заблокирован. Связитесь с администратором.", "Блокировка учётной записи", 
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Logger.Log("Авторизация", username, "Попытка входа в заблокированную учётную запись");
                    //this.DialogResult = DialogResult.Cancel;
                    return;
                }

                Logger.Log("Авторизация", username, "Успешный вход в систему");

                loggedInUser = user;  // Сохраняем пользователя
                this.DialogResult = DialogResult.OK; // Закрываем форму с успешным результатом
                this.Close(); // Закрываем LoginForm
            }
            else
            {
                Logger.Log("Авторизация", username, "Ошибка входа (неверный логин или пароль)");
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (userManager.GetUser(username) != null)
                    countofRetry++;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Устанавливаем результат отмены
            this.Close(); // Закрываем форму
        }
    }
}
