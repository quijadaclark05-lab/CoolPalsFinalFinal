using System;
using Microsoft.Data.Sqlite;
using System.Windows;

namespace ValorantLoginApp
{
    public partial class SignUpWindow : Window
    {
        private const string DbFile = "users.db";

        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text?.Trim() ?? string.Empty;
            string password = PasswordBox.Password ?? string.Empty;
            string confirm = ConfirmPasswordBox.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username/password cannot be empty.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string hashed = BCrypt.Net.BCrypt.HashPassword(password);

            using var conn = new SqliteConnection($"Data Source={DbFile}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Username, PasswordHash) VALUES (@u, @p)";
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", hashed);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19)
            {
                MessageBox.Show("Username already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            login.Show();
            this.Close();
        }
    }
}
