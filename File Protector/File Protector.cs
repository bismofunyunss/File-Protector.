namespace File_Protector
{
    public partial class FileProtector : Form
    {
        public FileProtector()
        {
            InitializeComponent();
        }
#pragma warning disable
        private static bool isAnimating;

        private async void Login_Btn_Click(object sender, EventArgs e)
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
                    StartAnimation();
                    AuthenticateUser.GetUserInfo(Userinpt_Text.Text);
                    string _hashedInput = await Task.Run(() => Crypto.HashPasswordV2Async(UserPasswrd_Inpt.Text, Convert.FromBase64String(Crypto.Salt)));
                    bool _LoginSuccessful = (bool)await Task.Run(() => Crypto.ComparePassword(_hashedInput));
                    GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
                    if (_LoginSuccessful)
                    {
                        UserLog.LogUser(Userinpt_Text.Text);
                        Userinpt_Text = null;
                        _hashedInput = string.Empty;
                        Crypto.Hash = string.Empty;
                        isAnimating = false;
                        MessageBox.Show("Log in successful! Redirecting to homepage...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserPasswrd_Inpt.Text = string.Empty;
                        Login_Btn.Enabled = true;
                        Reg_Btn.Enabled = true;
                        this.Hide();
                        using Homepage _Form = new();
                        _Form.ShowDialog();
                        this.Close();
                    }
                    else if (!_LoginSuccessful)
                    {
                        isAnimating = false;
                        MessageBox.Show("Log in failed! Please recheck your login credentials and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!_userExists)
                {
                    isAnimating = false;
                    throw new ArgumentException("Username does not exist.", nameof(Userinpt_Text));
                }
            }
            catch (ArgumentException ex)
            {
                isAnimating = false;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogging.ErrorLog(ex);
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

        private async void StartAnimation()
        {
            isAnimating = true;
            await AnimateLabel();
        }

        private async Task AnimateLabel()
        {
            while (isAnimating)
            {
                statusOutputLbl.Text = "Logging in";

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