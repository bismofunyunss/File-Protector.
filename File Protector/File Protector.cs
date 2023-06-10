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
        {
            bool _userExists = AuthenticateUser.UserExists(Userinpt_Text.Text);
            if (saveLoginCheckBox.Checked)
            {
                Properties.Settings.Default.userName = Userinpt_Text.Text;
                Properties.Settings.Default.Save();
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
                        MessageBox.Show("Log in successful! Redirecting to homepage...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserPasswrd_Inpt.Text = string.Empty;
                        this.Hide();
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
                UserPasswrd_Inpt.UseSystemPasswordChar = false;
                return;
            }
            UserPasswrd_Inpt.UseSystemPasswordChar = true;
        }
        private void File_Protector_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.userName != string.Empty)
            {
                Userinpt_Text.Text = Properties.Settings.Default.userName;
                saveLoginCheckBox.Checked = true;
            }
        }
    }
}
#pragma warning restore