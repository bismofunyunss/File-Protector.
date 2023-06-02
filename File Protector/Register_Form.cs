using System.IO;

namespace File_Protector
{
    public partial class Register_Form : Form
    {
        public Register_Form()
        {
            InitializeComponent();
        }
        public static bool UserExists(string userName)
        {
            var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var _rootFolder = Path.Combine(_appData, "User Data");
            var path = Path.Combine(_appData, _rootFolder, "User Data", "UserInfo.txt");
            if (!File.Exists(path))
                throw new IOException("File does not exist.");


            using var sr = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None));
            for (int i = 0; i < path.Length; i++)
            {
                string? _line = sr.ReadLine();
                if (_line == userName)
                    return true;
            }
            return false;
        }
        private static bool checkPasswordValidity(string password, string password2)
        {
            if (password.Length < 8 || password.Length > 32)
                return false;
            if (!password.Any(char.IsUpper))
                return false;
            if (!password.Any(char.IsLower))
                return false;
            if (!password.Any(char.IsDigit))
                return false;
            if (password.Contains(' ', StringComparison.CurrentCulture))
                return false;
            if (password != password2)
                return false;
            if (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation))
                return true;
            return false;
        }
        private void registerUser()
        {
            var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var _rootFolder = Path.Combine(_appData, "User Data");
            if (!Directory.Exists(_rootFolder))
                Directory.CreateDirectory(_rootFolder);
            var _userInfo = Path.Combine(_rootFolder, "User Data");
            if (!Directory.Exists(_userInfo))
                Directory.CreateDirectory(_userInfo);
            var _userFiles = Path.Combine(_rootFolder, "User Files");
            if (!Directory.Exists(_userFiles))
                Directory.CreateDirectory(_userFiles);
            var _userDirectory = Path.Combine(_userFiles, userTxt.Text);
            if (!Directory.Exists(_userDirectory))
                Directory.CreateDirectory(_userDirectory);
            var _file = "UserInfo.txt";
            var _filePath = Path.Combine(_userInfo, _file);

            string header = @"=========////(DO NOT modify this file as doing so may cause a loss of data.)\\\\=========" + "\n";

            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                using var _sw = new StreamWriter(_filePath, false);
                _sw.Write(header);
                _sw.Flush();
                _sw.Close();
            }

            bool _Exists = AuthenticateUser.UserExists(userTxt.Text);
            try
            {
                if (!_Exists)
                {
                    byte[] _Salt = Crypto.RndByteSized(512 / 8);
                    string _HashPassword = Crypto.HashPassword(passTxt.Text, _Salt);
                    string _SaltString = Convert.ToBase64String(_Salt);

                    using (var sw = new StreamWriter(_filePath, true))
                        sw.Write("\n" + "User:" + "\n" + userTxt.Text + "\n" +
                               "Salt:" + "\n" + _SaltString.Trim() +
                                "\n" + "Hash:" + "\n" + _HashPassword.Trim() + "\n");
                    DialogResult dialogResult = MessageBox.Show("Registration successful! Make sure you do NOT forget your password or you will lose access " +
                       "to all of your files.", "Registration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        this.Hide();
                        using File_Protector _Form = new();
                        _Form.ShowDialog();
                        this.Close();
                    }
                }
                if (_Exists)
                {
                    throw new ArgumentException("Username already exists", userTxt.Text);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
#pragma warning disable
        private void registerBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool _result = userTxt.Text.All(c => char.IsLetterOrDigit(c) || c == '_' || c == ' ');
                if (!_result)
                    throw new ArgumentException("Value contains illegal characters. Valid characters are letters, digits, underscores" +
                        "and spaces.", nameof(userTxt));
                if (string.IsNullOrEmpty(userTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(userTxt));
                if (userTxt.Text.Length > 20)
                    throw new ArgumentException("Value was too long.", nameof(userTxt));



                if (string.IsNullOrEmpty(passTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(passTxt));
                bool _passwordResult = checkPasswordValidity(passTxt.Text, confirmPassTxt.Text);
                if (!_passwordResult)
                {
                    throw new ArgumentException("Password must contain between 8 and 32 characters. " +
                        "It also must include:" + "\n" + "1.) At least one uppercase letter." + "\n" +
                        "2.) At least one lowercase letter." + "\n" + "3.) At least one number." + "\n" +
                        "4.) At least one special character." + "\n" + "5.) Must not contain any spaces." + "\n"
                        + "6.) Both passwords must match." + "\n", nameof(passTxt));
                }
                registerUser();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
#pragma warning restore
        private void unmaskPass_CheckedChanged(object sender, EventArgs e)
        {
            if (unmaskPass.Checked)
            {
                passTxt.UseSystemPasswordChar = false;
                confirmPassTxt.UseSystemPasswordChar = false;
                return;
            }
            passTxt.UseSystemPasswordChar = true;
            confirmPassTxt.UseSystemPasswordChar = true;
        }
        private void Register_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
