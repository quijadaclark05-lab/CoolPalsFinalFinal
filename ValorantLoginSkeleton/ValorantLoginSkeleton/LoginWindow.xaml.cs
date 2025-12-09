using System;
using System.IO;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace ValorantLoginApp
{
    public partial class LoginWindow : Window
    {
        private const string DbFile = "users.db";

        public LoginWindow()
        {
            InitializeComponent();
            EnsureDatabase();
        }

        private void EnsureDatabase()
        {
            if (!File.Exists(DbFile))
            {
                using var conn = new SqliteConnection($"Data Source={DbFile}");
                conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "CREATE TABLE Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT UNIQUE, PasswordHash TEXT)";
                cmd.ExecuteNonQuery();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text?.Trim() ?? string.Empty;
            string password = PasswordBox.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var conn = new SqliteConnection($"Data Source={DbFile}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT PasswordHash FROM Users WHERE Username = @u LIMIT 1";
            cmd.Parameters.AddWithValue("@u", username);
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                string storedHash = (string)result;
                bool ok = BCrypt.Net.BCrypt.Verify(password, storedHash);
                if (ok)
                {
                    MessageBox.Show("Login successful!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                    // TODO: open main dashboard window
                }
                else
                {
                    MessageBox.Show("Invalid password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("User not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var w = new SignUpWindow();
            w.Show();
            this.Close();
        }
    }
}
