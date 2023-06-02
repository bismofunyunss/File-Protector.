using System.IO;
using System.Text;

namespace File_Protector
{
    public partial class File_Protector : Form
    {
        public File_Protector()
        {
            InitializeComponent();
        }

        private static class AuthenticateUser
        {
            public static string _currentLoggedInUser = string.Empty;
            public static bool UserExists(string userName)
            {
                var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var _rootFolder = Path.Combine(_appData, "User Data");
                var path = Path.Combine(_appData, _rootFolder, "User Data", "UserInfo.txt");
                if (!File.Exists(path))
                    throw new IOException("File does not exist.");

                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                    if (lines[i] == userName)
                    {
                        AuthenticateUser._currentLoggedInUser = lines[i];
                        return true;

                    }
                return false;
            }
            public static void GetUserInfo(string userName)
            {
                try
                {
                    var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    var _rootFolder = Path.Combine(_appData, "User Data");
                    var path = Path.Combine(_appData, _rootFolder, "User Data", "UserInfo.txt");
                    if (!File.Exists(path))
                        throw new IOException("File does not exist.");

                    string[] lines = File.ReadAllLines(path);
                    for (int i = 0; i < lines.Length; i++)
                        if (lines[i] == userName)
                        {
                            AuthenticateUser._currentLoggedInUser = userName;
                            Crypto.Salt = lines[i + 2];
                            Crypto.Hash = lines[i + 4];
                            break;
                        }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Login_Btn_Click(object sender, EventArgs e)
        {
            bool _userExists = AuthenticateUser.UserExists(Userinpt_Text.Text);
            try
            {
                if (_userExists)
                {
                    AuthenticateUser.GetUserInfo(Userinpt_Text.Text);
                    var _hashedInput = Crypto.HashPassword(UserPasswrd_Inpt.Text, Convert.FromBase64String(Crypto.Salt));
                    var _LoginSuccessful = Crypto.ComparePassword(_hashedInput);

                    if (_LoginSuccessful)
                    {
                        _hashedInput = string.Empty;
                        Crypto.Hash = string.Empty;
                        MessageBox.Show("Log in successful! Redirecting to homepage...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserPasswrd_Inpt.Text = string.Empty;
                        AuthenticateUser._currentLoggedInUser = Userinpt_Text.Text;
                        UserLog.LogUser();
                        this.Hide();
                        using Homepage _Form = new();
                        _Form.ShowDialog();
                        this.Close();
                    }
                    else if (!_LoginSuccessful)
                    {
                        MessageBox.Show("Failure");
                    }

                }
                else if (!_userExists)
                {
                    throw new ArgumentException("Username does not exist.", nameof(Userinpt_Text));
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reg_Btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            using Register_Form _Form = new();
            _Form.ShowDialog();
            this.Close();
        }

        private void unmaskPass_CheckedChanged(object sender, EventArgs e)
        {
            if (unmaskPass.Checked)
            {
                
                return;
            }
            UserPasswrd_Inpt.UseSystemPasswordChar = true;
        }
    }
}