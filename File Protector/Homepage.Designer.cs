namespace File_Protector
{
    partial class Homepage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            authKeyBox = new GroupBox();
            generateRnd32 = new Button();
            enterpassLabel = new Label();
            keyTxtBox = new TextBox();
            controlsBox = new GroupBox();
            saveFile = new Button();
            openFile = new Button();
            encryptBtn = new Button();
            decryptBtn = new Button();
            welcomeLbl = new Label();
            label1 = new Label();
            authKeyBox.SuspendLayout();
            controlsBox.SuspendLayout();
            SuspendLayout();
            // 
            // authKeyBox
            // 
            authKeyBox.Controls.Add(generateRnd32);
            authKeyBox.Controls.Add(enterpassLabel);
            authKeyBox.Controls.Add(keyTxtBox);
            authKeyBox.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            authKeyBox.ForeColor = Color.WhiteSmoke;
            authKeyBox.Location = new Point(12, 12);
            authKeyBox.Name = "authKeyBox";
            authKeyBox.Size = new Size(446, 160);
            authKeyBox.TabIndex = 5;
            authKeyBox.TabStop = false;
            authKeyBox.Text = "Key / Hash";
            // 
            // generateRnd32
            // 
            generateRnd32.BackColor = SystemColors.ControlDarkDark;
            generateRnd32.FlatAppearance.BorderSize = 3;
            generateRnd32.FlatStyle = FlatStyle.Flat;
            generateRnd32.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            generateRnd32.ForeColor = Color.WhiteSmoke;
            generateRnd32.Location = new Point(7, 94);
            generateRnd32.Name = "generateRnd32";
            generateRnd32.Size = new Size(432, 41);
            generateRnd32.TabIndex = 6;
            generateRnd32.Text = "&Generate Cryptographically Strong Key";
            generateRnd32.UseVisualStyleBackColor = false;
            // 
            // enterpassLabel
            // 
            enterpassLabel.AutoSize = true;
            enterpassLabel.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            enterpassLabel.Location = new Point(7, 28);
            enterpassLabel.Name = "enterpassLabel";
            enterpassLabel.Size = new Size(96, 25);
            enterpassLabel.TabIndex = 3;
            enterpassLabel.Text = "Enter Key";
            // 
            // keyTxtBox
            // 
            keyTxtBox.BackColor = SystemColors.ControlDarkDark;
            keyTxtBox.ForeColor = Color.WhiteSmoke;
            keyTxtBox.Location = new Point(7, 55);
            keyTxtBox.Multiline = true;
            keyTxtBox.Name = "keyTxtBox";
            keyTxtBox.Size = new Size(432, 33);
            keyTxtBox.TabIndex = 2;
            keyTxtBox.Text = "Key must be 32 characters long.";
            keyTxtBox.UseSystemPasswordChar = true;
            // 
            // controlsBox
            // 
            controlsBox.Controls.Add(saveFile);
            controlsBox.Controls.Add(openFile);
            controlsBox.Controls.Add(encryptBtn);
            controlsBox.Controls.Add(decryptBtn);
            controlsBox.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            controlsBox.ForeColor = Color.WhiteSmoke;
            controlsBox.Location = new Point(12, 178);
            controlsBox.Name = "controlsBox";
            controlsBox.Size = new Size(446, 224);
            controlsBox.TabIndex = 6;
            controlsBox.TabStop = false;
            controlsBox.Text = "Controls";
            // 
            // saveFile
            // 
            saveFile.BackColor = SystemColors.ControlDarkDark;
            saveFile.FlatAppearance.BorderSize = 3;
            saveFile.FlatStyle = FlatStyle.Flat;
            saveFile.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            saveFile.ForeColor = Color.WhiteSmoke;
            saveFile.Location = new Point(7, 166);
            saveFile.Name = "saveFile";
            saveFile.Size = new Size(432, 41);
            saveFile.TabIndex = 5;
            saveFile.Text = "&Save Output";
            saveFile.UseVisualStyleBackColor = false;
            // 
            // openFile
            // 
            openFile.BackColor = SystemColors.ControlDarkDark;
            openFile.FlatAppearance.BorderSize = 3;
            openFile.FlatStyle = FlatStyle.Flat;
            openFile.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            openFile.ForeColor = Color.WhiteSmoke;
            openFile.Location = new Point(7, 119);
            openFile.Name = "openFile";
            openFile.Size = new Size(432, 41);
            openFile.TabIndex = 4;
            openFile.Text = "&Import Text File";
            openFile.UseVisualStyleBackColor = false;
            openFile.Click += openFile_Click;
            // 
            // encryptBtn
            // 
            encryptBtn.BackColor = SystemColors.ControlDarkDark;
            encryptBtn.FlatAppearance.BorderSize = 3;
            encryptBtn.FlatStyle = FlatStyle.Flat;
            encryptBtn.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            encryptBtn.ForeColor = Color.WhiteSmoke;
            encryptBtn.Location = new Point(7, 25);
            encryptBtn.Name = "encryptBtn";
            encryptBtn.Size = new Size(432, 41);
            encryptBtn.TabIndex = 2;
            encryptBtn.Text = "&Encrypt";
            encryptBtn.UseVisualStyleBackColor = false;
            // 
            // decryptBtn
            // 
            decryptBtn.BackColor = SystemColors.ControlDarkDark;
            decryptBtn.FlatAppearance.BorderSize = 3;
            decryptBtn.FlatStyle = FlatStyle.Flat;
            decryptBtn.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            decryptBtn.ForeColor = Color.WhiteSmoke;
            decryptBtn.Location = new Point(7, 72);
            decryptBtn.Name = "decryptBtn";
            decryptBtn.Size = new Size(432, 41);
            decryptBtn.TabIndex = 3;
            decryptBtn.Text = "&Decrypt";
            decryptBtn.UseVisualStyleBackColor = false;
            // 
            // welcomeLbl
            // 
            welcomeLbl.AutoSize = true;
            welcomeLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            welcomeLbl.ForeColor = Color.WhiteSmoke;
            welcomeLbl.Location = new Point(12, 405);
            welcomeLbl.Name = "welcomeLbl";
            welcomeLbl.Size = new Size(139, 25);
            welcomeLbl.TabIndex = 7;
            welcomeLbl.Text = "Welcome, null";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(12, 430);
            label1.Name = "label1";
            label1.Size = new Size(124, 25);
            label1.TabIndex = 8;
            label1.Text = "Status :: Idle";
            // 
            // Homepage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(471, 466);
            Controls.Add(label1);
            Controls.Add(welcomeLbl);
            Controls.Add(controlsBox);
            Controls.Add(authKeyBox);
            Name = "Homepage";
            Text = "Home Page";
            Load += Homepage_Load;
            authKeyBox.ResumeLayout(false);
            authKeyBox.PerformLayout();
            controlsBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox authKeyBox;
        private Button generateRnd32;
        private Label enterpassLabel;
        private TextBox keyTxtBox;
        private GroupBox controlsBox;
        private Button saveFile;
        private Button openFile;
        private Button encryptBtn;
        private Button decryptBtn;
        private Label welcomeLbl;
        private Label label1;
    }
}