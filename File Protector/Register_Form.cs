﻿using System.IO;

#pragma warning disable
namespace File_Protector
{
    public partial class Register_Form : Form
    {
        public Register_Form()
        {
            InitializeComponent();
        }
        private static bool isAnimating;
        public static bool UserExists(string userName)
        {
            string path = GetUserInfoFilePath();
            if (!File.Exists(path))
                throw new IOException("File does not exist.");

            return File.ReadAllLines(path).Contains(userName);
        }

        private static bool CheckPasswordValidity(string password, string password2)
        {
            if (password.Length < 8 || password.Length > 64)
                return false;

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
                return false;

            if (password.Contains(' ') || password != password2)
                return false;

            return password.Any(char.IsSymbol) || password.Any(char.IsPunctuation);
        }

        private async void RegisterUser()
        {
            string rootFolder = CreateDirectoryIfNotExists("User Data");
            string userInfoFolder = CreateDirectoryIfNotExists(Path.Combine(rootFolder, "User Data"));
            string userFilesFolder = CreateDirectoryIfNotExists(Path.Combine(rootFolder, "User Files"));
            string userDirectory = CreateDirectoryIfNotExists(Path.Combine(userFilesFolder, userTxt.Text));
            string filePath = Path.Combine(userInfoFolder, "UserInfo.txt");

            string header = @"=========////(DO NOT modify this file as doing so may cause a loss of data.)\\\\=========";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, header + "\n");
            }

            bool exists = AuthenticateUser.UserExists(userTxt.Text);
            try
            {
                if (!exists)
                {
                    StartAnimation();
                    if (!CheckPasswordValidity(passTxt.Text, confirmPassTxt.Text))
                        throw new ArgumentException("Password must contain between 8 and 64 characters. " +
                         "It also must include:\n1.) At least one uppercase letter.\n2.) At least one lowercase letter.\n" +
                         "3.) At least one number.\n4.) At least one special character.\n5.) Must not contain any spaces.\n" +
                         "6.) Both passwords must match.\n", nameof (passTxt));
                    byte[] salt = Crypto.RndByteSized(Crypto.SaltSize);
                    string hashPassword = await Crypto.HashAsync(passTxt.Text, salt);
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
                    string saltString = DataConversionHelpers.ByteArrayToBase64String(salt);
                    File.AppendAllText(filePath, $"\nUser:\n{userTxt.Text}\nSalt:\n{saltString.Trim()}\nHash:\n{hashPassword.Trim()}\n");
                    isAnimating = false;
                    DialogResult dialogResult = MessageBox.Show("Registration successful! Make sure you do NOT forget your password or you will lose access " +
                        "to all of your files.", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dialogResult == DialogResult.OK)
                    {
                        this.Hide();
                        using FileProtector form = new();
                        form.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    isAnimating = false;
                    throw new ArgumentException("Username already exists", userTxt.Text);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isAnimating = false;
                ErrorLogging.ErrorLog(ex);
            }
        }

        private static string CreateDirectoryIfNotExists(string directoryPath)
        {
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), directoryPath);
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            return fullPath;
        }

        private static string GetUserInfoFilePath()
        {
            return Path.Combine(CreateDirectoryIfNotExists("User Data"), "User Data", "UserInfo.txt");
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!userTxt.Text.All(c => char.IsLetterOrDigit(c) || c == '_' || c == ' '))
                    throw new ArgumentException("Value contains illegal characters. Valid characters are letters, digits, underscores, and spaces.", nameof(userTxt));

                if (string.IsNullOrEmpty(userTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(userTxt));

                if (userTxt.Text.Length > 20)
                    throw new ArgumentException("Value was too long.", nameof(userTxt));

                if (string.IsNullOrEmpty(passTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(passTxt));

                if (!CheckPasswordValidity(passTxt.Text, confirmPassTxt.Text))
                {
                    throw new ArgumentException("Password must contain between 8 and 64 characters. " +
                        "It also must include:\n1.) At least one uppercase letter.\n2.) At least one lowercase letter.\n" +
                        "3.) At least one number.\n4.) At least one special character.\n5.) Must not contain any spaces.\n" +
                        "6.) Both passwords must match.\n", nameof(passTxt));
                }

                RegisterUser();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogging.ErrorLog(ex);
            }
        }

        private void unmaskPass_CheckedChanged(object sender, EventArgs e)
        {
            passTxt.UseSystemPasswordChar = !unmaskPass.Checked;
            confirmPassTxt.UseSystemPasswordChar = !unmaskPass.Checked;
        }

        private void Register_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private async void StartAnimation()
        {
            isAnimating = true;
            await AnimateLabel();
        }

        private async Task AnimateLabel()
        {
            while (isAnimating)
            {
                statusOutputLbl.Text = "Creating account";

                // Add animated periods
                for (int i = 0; i < 4; i++)
                {
                    statusOutputLbl.Text += ".";
                    await Task.Delay(400); // Delay between each period
                }
            }
        }
    }
}
#pragma warning restore
