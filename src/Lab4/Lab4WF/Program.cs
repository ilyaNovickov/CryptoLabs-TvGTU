using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4WF
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UserManager userManager = new UserManager();

            LoginForm loginForm = new LoginForm(userManager);
            if (loginForm.ShowDialog() == DialogResult.OK) // Ожидаем закрытие LoginForm
            {
                User loggedInUser = loginForm.GetLoggedInUser(); // Получаем авторизованного пользователя
                Application.Run(new MainForm(loggedInUser, userManager)); // Запускаем приложение с MainForm
            }
        }
    }
}
