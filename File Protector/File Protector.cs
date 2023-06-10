namespace File_Protector
{
    public partial class FileProtector : Form
    {
        public FileProtector()
        {
            InitializeComponent();
        }
#pragma warning disable
        private void Login_Btn_Click(object sender, EventArgs e)
                Properties.Settings.Default.userName = Userinpt_Text.Text;
                Properties.Settings.Default.Save();
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
                Properties.Settings.Default.Save();
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
            try
            {
                if (_userExists)
                {
                    AuthenticateUser.GetUserInfo(Userinpt_Text.Text);
                    var _hashedInput = Crypto.HashPasswordV2(UserPasswrd_Inpt.Text, Convert.FromBase64String(Crypto.Salt));
                    var _LoginSuccessful = Crypto.ComparePassword(_hashedInput);
                    if (_LoginSuccessful)
                    {
                        UserLog.LogUser(Userinpt_Text.Text);
                        Userinpt_Text = null;
                        _hashedInput = string.Empty;
                        Crypto.Hash = string.Empty;
        private void File_Protector_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.userName != string.Empty)
            {
                Userinpt_Text.Text = Properties.Settings.Default.userName;
                saveLoginCheckBox.Checked = true;
            }
        }
                        MessageBox.Show("Log in successful! Redirecting to homepage...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
}
#pragma warning restore                        this.Hide();
                        using Homepage _Form = new();
                        _Form.ShowDialog();
                        this.Close();
                    }
                    else if (!_LoginSuccessful)
                    {
                        MessageBox.Show("Log in failed! Please recheck your login credentials and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!_userExists)
                {
}                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reg_Btn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
}#pragma warning restore
        private void unmaskPass_CheckedChanged(object sender, EventArgs e)
        {
            if (unmaskPass.Checked)
            {
                UserPasswrd_Inpt.UseSystemPasswordChar = false;
                return;
            }
            UserPasswrd_Inpt.UseSystemPasswordChar = true;
        }
    }
}