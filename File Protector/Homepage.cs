using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#pragma warning disable
namespace File_Protector
{
    public partial class Homepage : Form
    {
        private readonly Random random = new Random();
        private bool fileOpened;
        private string? loadedFile = string.Empty;
        private string fileName = string.Empty;
        private static string userSalt = string.Empty;
        private static string userKey = string.Empty;
        private static string userEncryptedKey = string.Empty;
        public Homepage()
        {
            InitializeComponent();
        }

        private void rainbowLabel(Label label)
        {
            label.ForeColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
            Thread.Sleep(150);
        }

        public string? Encrypt(string data, byte[] key)
        {
            byte[]? plainText = DataConversionHelpers.StringToByteArray(data);
            try
            {
                var cipherText = Crypto.Encrypt(plainText, key);
                if (cipherText == null)
                    throw new CryptographicException("Value returned null.", nameof(cipherText));
                return DataConversionHelpers.ByteArrayToBase64String(cipherText);
            }
            catch (CryptographicException ex)
            {
                ShowErrorAndLog(ex, "File encryption error.");
            }
            return null;
        }

        public string? Decrypt(string data, byte[] key)
        {
            try
            {
                var inputText = DataConversionHelpers.Base64StringToByteArray(data);
                var result = Crypto.Decrypt(inputText, key);

                if (result == null)
                    throw new System.Exception("Decryption value returned empty or null.");

                return Encoding.UTF8.GetString(result);
            }
            catch (System.Exception ex)
            {
                ShowErrorAndLog(ex, "File decryption error.");
            }
            return null;
        }
        private void openFile_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt files(*.txt) | *.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowHiddenFiles = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var _rootFolder = System.IO.Path.Combine(_appData, "User Data");
            var path = System.IO.Path.Combine(_appData, _rootFolder, "User Files", AuthenticateUser.CurrentLoggedInUser);
            openFileDialog.RestoreDirectory = true;
            openFileDialog.InitialDirectory = path;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                try
                {
                    string compare = System.IO.Path.GetExtension(fileName);
                    if (compare != ".txt")
                        throw new ArgumentException("Invalid file type.", openFileDialog.FileName);
                    const int maxSize = 800_000_000;
                    FileInfo info = new FileInfo(fileName);

                    if (info.Length > maxSize)
                        throw new OutOfMemoryException("Value exceeded maximum value.");

                    byte[] buffer = new byte[info.Length];
                    StringBuilder sb = new StringBuilder();
                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        fs.Read(buffer, 0, buffer.Length);
                        loadedFile = sb.Append(Encoding.UTF8.GetString(buffer)).ToString();
                        fileOpened = true;
                    }
                    if (!fileOpened)
                    {
                        MessageBox.Show("Error while trying to open file.", "Open File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentStatusLbl.Text = "File read error.";
                        currentStatusLbl.ForeColor = Color.Red;
                    }
                    else
                    {
                        MessageBox.Show("File Opened Successfully!", "Open File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentStatusLbl.Text = "File Loaded! File size: " + info.Length.ToString() + " bytes!";
                        currentStatusLbl.ForeColor = Color.LimeGreen;
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorAndLog(ex, "File read error.");
                    throw;
                }
            }
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
            welcomeLbl.Text = "Welcome, " + AuthenticateUser.CurrentLoggedInUser + "!";
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                rainbowLabel(welcomeLbl);
            }
        }

        private void generateRnd32_Click(object sender, EventArgs e)
        {
            keyTxtBox.Text = Crypto.GenerateRndString(32);
        }

        private void encryptBtn_Click(object sender, EventArgs e)
        {
            string? inputString = loadedFile;
            string keyString = keyTxtBox.Text;
            byte[] Key = Encoding.UTF8.GetBytes(keyTxtBox.Text);
            try
            {
                if (string.IsNullOrEmpty(keyString))
                    throw new ArgumentException("Key value was empty or null.", nameof(Key));
                if (string.IsNullOrEmpty(inputString))
                    throw new ArgumentException("Input value was empty or null.", nameof(inputString));
                string? encryptData = Encrypt(inputString, Key);
                loadedFile = encryptData;
                if (string.IsNullOrEmpty(encryptData))
                    throw new System.Exception("Encryption value returned empty or null.");
                currentStatusLbl.Text = "File Encrypted! New byte size: " + loadedFile.Length + " bytes!";
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File encryption error.");
                throw;
            }
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            string inputData = loadedFile;
            byte[] Key = Encoding.UTF8.GetBytes(keyTxtBox.Text);
            try
            {
                if (Key == null)
                    throw new ArgumentException("Value was empty or null.", nameof(Key));
                if (Key.Length != 256 / 8)
                    throw new ArgumentException("Key must be 256 bits.", nameof(Key));
                string? Text = Decrypt(inputData, Key);
                if (string.IsNullOrEmpty(Text))
                    throw new Exception("Value was empty or null.");
                currentStatusLbl.Text = "File Decrypted! New byte size: " + loadedFile.Length + " bytes!";
                loadedFile = Text;
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File decryption error.");
                throw;
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileOpened)
                    throw new ArgumentException("No file is opened.", nameof(loadedFile));
                using var sf = new SaveFileDialog();
                var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var _rootFolder = System.IO.Path.Combine(_appData, "User Data");
                var path = System.IO.Path.Combine(_appData, _rootFolder, "User Files", AuthenticateUser.CurrentLoggedInUser);
                sf.FilterIndex = 1;
                sf.ShowHiddenFiles = true;
                sf.RestoreDirectory = true;
                sf.InitialDirectory = path;
                sf.Filter = "Txt files(*.txt) | *.txt";

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    fileName = sf.FileName;
                    if (!string.IsNullOrEmpty(sf.FileName))
                    {
                        using (StreamWriter sw = new StreamWriter(sf.FileName))
                            sw.Write(loadedFile);
                    }
                    else
                        throw new ArgumentException("Value returned null or empty.", nameof(sf.FileName));

                    MessageBox.Show("File Saved Successfully!", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    currentStatusLbl.Text = "Idle...";
                    currentStatusLbl.ForeColor = Color.WhiteSmoke;
                    loadedFile = null;
                    fileOpened = false;
                }
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File save error.");
                throw;
            }
        }
        private void ShowErrorAndLog(Exception ex, string errorMessage)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorLogging.ErrorLog(ex);
            currentStatusLbl.Text = errorMessage;
            currentStatusLbl.ForeColor = Color.Red;
        }
        private static bool CheckPasswordValidity(string password, string password2)
        {
            if (password.Length < 8 || password.Length > 32)
                return false;

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
                return false;

            if (password.Contains(' ') || password != password2)
                return false;

            return password.Any(char.IsSymbol) || password.Any(char.IsPunctuation);
        }
        private void createPassBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!createPassTxt.Text.All(c => char.IsLetterOrDigit(c) || c == '_' || c == ' '))
                    throw new ArgumentException("Value contains illegal characters. Valid characters are letters, digits, underscores, and spaces.", nameof(createPassTxt));

                if (string.IsNullOrEmpty(createPassTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(createPassTxt));

                if (createPassTxt.Text.Length > 20)
                    throw new ArgumentException("Value was too long.", nameof(createPassTxt));

                if (string.IsNullOrEmpty(createPassTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(createPassTxt));

                if (!CheckPasswordValidity(createPassTxt.Text, confirmPasswordTxt.Text))
                {
                    throw new ArgumentException("Password must contain between 8 and 32 characters. " +
                        "It also must include:\n1.) At least one uppercase letter.\n2.) At least one lowercase letter.\n" +
                        "3.) At least one number.\n4.) At least one special character.\n5.) Must not contain any spaces.\n" +
                        "6.) Both passwords must match.\n", nameof(createPassTxt));
                }


                var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var _rootFolder = System.IO.Path.Combine(_appData, "User Data");
                var path = System.IO.Path.Combine(_appData, _rootFolder, "User Data", "UserKeys.txt");


                string header = @"=========////(DO NOT modify this file as doing so may cause a loss of data.)\\\\=========";

                if (!File.Exists(path))
                    File.WriteAllText(path, header + "\n");

                userSalt = DataConversionHelpers.ByteArrayToBase64String(Crypto.RndByteSized(256 / 8));
                userKey = Crypto.deriveKey(createPassTxt.Text, Encoding.UTF8.GetBytes(userSalt), 256 / 8 / 2);
                userEncryptedKey = Crypto.deriveKey(userKey, Encoding.UTF8.GetBytes(userSalt), 256 / 8 / 2);

                string userName = AuthenticateUser.CurrentLoggedInUser;
                string[] lines = File.ReadAllLines(path);

                int index = Array.IndexOf(lines, userName);
                if (index != -1)
                {
                    MessageBox.Show("User already has a unique key assigned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.AppendAllText(path, "Username: " + "\n" + AuthenticateUser.CurrentLoggedInUser + "\n" + "Salt: " + "\n" + userSalt + "\n" + "Key: " + "\n" + userEncryptedKey + "\n");

                MessageBox.Show("Key was made successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLogging.ErrorLog(ex);
            }
        }

            private static string GetUserInfoFilePath()
        {
            try
            {
                var _appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var _rootFolder = System.IO.Path.Combine(_appData, "User Data");
                var path = System.IO.Path.Combine(_appData, _rootFolder, "User Data", "UserKeys.txt");
                if (!File.Exists(path))
                    throw new ArgumentException("Value returned null or empty.", nameof(path));
                return path;
            }
            catch (ArgumentException ex)
            {
                ErrorLogging.ErrorLog(ex);
                return null;
            }
        }
        private void enterPassBtn_Click(object sender, EventArgs e)
        {
            string path = GetUserInfoFilePath();
            if (path == null)
            {
                MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (enterPassTxt.Text == string.Empty)
            {
                MessageBox.Show("Value cannot be empty or null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (AuthenticateUser.CurrentLoggedInUser == null)
                    throw new ArgumentException("Value returned empty or null.", nameof(AuthenticateUser.CurrentLoggedInUser));
                string userName = AuthenticateUser.CurrentLoggedInUser;
                string[] lines = File.ReadAllLines(path);

                int index = Array.IndexOf(lines, userName);
                if (index == -1)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                userSalt = lines[index + 2];
                userEncryptedKey = lines[index + 4];

                string enteredPassword = enterPassTxt.Text;
                byte[] saltBytes = Encoding.UTF8.GetBytes(userSalt);

                string derivedKey = Crypto.deriveKey(enteredPassword, saltBytes, 256 / 8 / 2);
                string derivedCompareKey = Crypto.deriveKey(derivedKey, saltBytes, 256 / 8 / 2);

                if (derivedCompareKey == userEncryptedKey)
                {
                    MessageBox.Show("Success. Key will be printed to the output textbox. " +
                        "You may use this key to encrypt and decrypt files. " +
                        "Make sure you do NOT lose the password as you will lose access to any files encrypted using that key.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    keyTxtBox.Text = derivedKey; // Changed from "userKey" to "derivedKey"
                }
                else
                {
                    MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
#pragma warning restore
