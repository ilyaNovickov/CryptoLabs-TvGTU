using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab4WF
{
    public class UserManager
    {
        private const string FilePath = "users.txt";
        private List<User> users = new List<User>();

        public UserManager()
        {
            LoadUsers();
        }

        public string PasswordPattern
        {
            get => @"^[\p{L}\p{P}\d]*$";
        }

        // Загрузка пользователей из файла
        private void LoadUsers()
        {
            if (!File.Exists(FilePath))
            {
                // Если файла нет, создаём администратора
                users.Add(new User("admin", "", true));
                SaveUsers();
                return;
            }

            users.Clear();
            foreach (var line in File.ReadAllLines(FilePath))
            {
                var parts = line.Split(',');
                if (parts.Length == 5)
                {
                    users.Add(new User(parts[0], parts[1], bool.Parse(parts[2]), bool.Parse(parts[3]), bool.Parse(parts[4])));
                }
            }
        }

        // Сохранение пользователей в файл
        public void SaveUsers()
        {
            File.WriteAllLines(FilePath, users.Select(u => $"{u.Username},{u.Password},{u.IsAdmin},{u.IsBlocked},{u.IsLimitationForPassword}"));
        }

        // Поиск пользователя
        public User GetUser(string username)
        {
            return users.FirstOrDefault(u => u.Username == username);
        }

        // Проверка логина и пароля
        public User ValidateUser(string username, string password)
        {
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        // Изменение пароля
        public bool ChangePassword(string username, string newPassword)
        {
            var user = GetUser(username);
            if (user != null)
            {
                user.Password = newPassword;
                SaveUsers();
                return true;
            }
            return false;
        }

        // Получение всех пользователей
        public List<User> GetAllUsers()
        {
            return users;
        }

        // Добавление пользователя
        public bool AddUser(string username, string password, bool isAdmin = false)
        {
            if (GetUser(username) == null)
            {
                users.Add(new User(username, password, isAdmin));
                SaveUsers();
                return true;
            }
            return false;
        }

        // Удаление пользователя
        public bool RemoveUser(string username)
        {
            var user = GetUser(username);
            if (user != null && user.Username != "admin") // Админа нельзя удалить
            {
                users.Remove(user);
                SaveUsers();
                return true;
            }
            return false;
        }
    }
}
