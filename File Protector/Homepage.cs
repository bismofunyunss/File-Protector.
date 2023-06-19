using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Navigation;

#pragma warning disable
namespace File_Protector
{
    public partial class Homepage : Form
    {
        private static string UserSalt = string.Empty;
        private static string UserKey = string.Empty;
        private static string UserEncryptedKey = string.Empty;
        private readonly Random random = new Random();
        private bool fileOpened;
        private string loadedFile = string.Empty;
        private string fileName = string.Empty;

        public Homepage()
        {
            InitializeComponent();
        }

        private void rainbowLabel(Label label)
        {
            label.ForeColor = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }

        public string Encrypt(string data, byte[] key)
        {
            try
            {
                return DataConversionHelpers.ByteArrayToBase64String(Crypto.Encrypt(DataConversionHelpers.StringToByteArray(data), key)) ?? throw new CryptographicException(0);
            }
            catch (CryptographicException ex)
            {
                ShowErrorAndLog(ex, "File encryption error.");
            }
            return null;
        }

        public string Decrypt(string data, byte[] key)
        {
            try
            {
                byte[] result = Crypto.Decrypt(DataConversionHelpers.Base64StringToByteArray(data), key);
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File decryption error.");
            }
            return null;
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Filter = "Txt files(*.txt) | *.txt",
                FilterIndex = 1,
                ShowHiddenFiles = true,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true
            };

            openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User Data", "User Files", AuthenticateUser.CurrentLoggedInUser);

            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            fileName = openFileDialog.FileName;
            var fileInfo = new FileInfo(fileName);

            try
            {
                const int maxSize = 800_000_000;

                if (!Path.GetExtension(fileName).Equals(".txt", StringComparison.OrdinalIgnoreCase) || fileInfo.Length > maxSize)
                    throw new ArgumentException("Invalid file type or size.");

                loadedFile = Encoding.UTF8.GetString(File.ReadAllBytes(fileName));
                fileOpened = true;

                currentStatusLbl.Text = $"File Loaded! File size: {fileInfo.Length} bytes!";
                currentStatusLbl.ForeColor = Color.LimeGreen;
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File read error.");
                throw;
            }
            finally
            {
                var errorMessage = !fileOpened ? "Error while trying to open file." : "File Opened Successfully!";
                var errorColor = !fileOpened ? Color.Red : Color.LimeGreen;

                MessageBox.Show(errorMessage, "Open File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentStatusLbl.Text = !fileOpened ? "File read error." : $"File Loaded! File size: {fileInfo.Length} bytes!";
                currentStatusLbl.ForeColor = errorColor;
            }
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
            welcomeLbl.Text = $"Welcome, {AuthenticateUser.CurrentLoggedInUser} !";
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                rainbowLabel(welcomeLbl);
                Thread.Sleep(150);
            }
        }

        private void generateRnd32_Click(object sender, EventArgs e)
        {
            keyTxtBox.Text = Crypto.GenerateRndKey();
        }
        private void exportKeyBtn_Click(object sender, EventArgs e)
        {
            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Txt files(*.txt) | *.txt",
                FilterIndex = 1,
                ShowHiddenFiles = true,
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    File.WriteAllText(fileName, keyTxtBox.Text);
                    MessageBox.Show("File Saved Successfully!", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            MessageBox.Show("Error saving file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    throw new Exception("Encryption value returned empty or null.");

                currentStatusLbl.Text = "File Encrypted! New byte size: " + loadedFile.Length + " bytes!";
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File encryption error.");
                return;
            }
        }

        private void decryptBtn_Click(object sender, EventArgs e)
        {
            string inputData = loadedFile;
            byte[] Key = Encoding.UTF8.GetBytes(keyTxtBox.Text);

            try
            {
                if (Key == null || Key.Length != 256 / 8)
                    throw new ArgumentException("Invalid key size.", nameof(Key));

                string? decryptedText = Decrypt(inputData, Key);

                if (string.IsNullOrEmpty(decryptedText))
                    throw new Exception("Decryption value was empty or null.");

                currentStatusLbl.Text = "File Decrypted! New byte size: " + loadedFile.Length + " bytes!";
                loadedFile = decryptedText;
            }
            catch (Exception ex)
            {
                ShowErrorAndLog(ex, "File decryption error.");
                return;
            }
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileOpened)
                    throw new ArgumentException("No file is opened.", nameof(loadedFile));

                using var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Txt files(*.txt) | *.txt",
                    FilterIndex = 1,
                    ShowHiddenFiles = true,
                    RestoreDirectory = true,
                    InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User Data", "User Files", AuthenticateUser.CurrentLoggedInUser)
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        File.WriteAllText(fileName, loadedFile);
                    }
                    else
                    {
                        throw new ArgumentException("File name is null or empty.", nameof(fileName));
                    }

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
                return;
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
            return password.Length >= 8 && password.Length <= 32
       && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit)
       && !password.Contains(' ') && password == password2
       && (password.Any(char.IsSymbol) || password.Any(char.IsPunctuation));
        }
        private void createPassBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(createPassTxt.Text))
                    throw new ArgumentException("Value returned null or empty.", nameof(createPassTxt));

                if (!CheckPasswordValidity(createPassTxt.Text, confirmPasswordTxt.Text))
                    throw new ArgumentException("Invalid password.");

                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User Data", "User Data", "UserKeys.txt");
                string header = @"=========////(DO NOT modify this file as doing so may cause a loss of data.)\\\\=========";

                if (!File.Exists(path))
                    File.WriteAllText(path, header + "\n");
                UserSalt = DataConversionHelpers.ByteArrayToHexString(Crypto.RndByteSized(Crypto.SaltSize));
                UserKey = Crypto.DeriveKey(createPassTxt.Text, Encoding.UTF8.GetBytes(UserSalt));
                UserEncryptedKey = Crypto.DeriveKey(UserKey, Encoding.UTF8.GetBytes(UserSalt));

                string userName = AuthenticateUser.CurrentLoggedInUser;
                string[] lines = File.ReadAllLines(path);

                if (Array.IndexOf(lines, userName) != -1)
                {
                    MessageBox.Show("User already has a unique key assigned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.AppendAllText(path, "Username: \n" + AuthenticateUser.CurrentLoggedInUser + "\n" + "Salt: \n" + UserSalt + "\n" + "Key: \n" + UserEncryptedKey + "\n");

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
                var appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var rootFolder = Path.Combine(appDataFolderPath, "User Data");
                var path = Path.Combine(appDataFolderPath, rootFolder, "User Data", "UserKeys.txt");

                if (!File.Exists(path))
                    throw new ArgumentException("Invalid file path.", nameof(path));

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

            if (string.IsNullOrEmpty(enterPassTxt.Text))
            {
                MessageBox.Show("Value cannot be empty or null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(AuthenticateUser.CurrentLoggedInUser))
                    throw new ArgumentException("Value returned empty or null.", nameof(AuthenticateUser.CurrentLoggedInUser));

                string userName = AuthenticateUser.CurrentLoggedInUser;
                string[] lines = File.ReadAllLines(path);

                int index = Array.IndexOf(lines, userName);
                if (index == -1)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                UserSalt = lines[index + 2];
                UserEncryptedKey = lines[index + 4];

                string enteredPassword = enterPassTxt.Text;
                byte[] saltBytes = Encoding.UTF8.GetBytes(UserSalt);

                string derivedKey = Crypto.DeriveKey(enteredPassword, saltBytes);
                string derivedCompareKey = Crypto.DeriveKey(derivedKey, saltBytes);

                if (derivedCompareKey == UserEncryptedKey)
                {
                    MessageBox.Show("Success. Key will be printed to the output textbox. " +
                        "You may use this key to encrypt and decrypt files. " +
                        "Make sure you do NOT lose the password as you will lose access to any files encrypted using that key.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    keyTxtBox.Text = derivedKey;
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
