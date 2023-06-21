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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Homepage));
            authKeyBox = new GroupBox();
            confirmPassLbl = new Label();
            confirmPasswordTxt = new TextBox();
            createPassBtn = new Button();
            label2 = new Label();
            createPassTxt = new TextBox();
            enterPassBtn = new Button();
            enterPassLbl = new Label();
            enterPassTxt = new TextBox();
            enterpassLabel = new Label();
            keyTxtBox = new TextBox();
            controlsBox = new GroupBox();
            saveFile = new Button();
            openFile = new Button();
            encryptBtn = new Button();
            decryptBtn = new Button();
            welcomeLbl = new Label();
            statusLbl = new Label();
            worker = new System.ComponentModel.BackgroundWorker();
            currentStatusLbl = new Label();
            authKeyBox.SuspendLayout();
            controlsBox.SuspendLayout();
            SuspendLayout();
            // 
            // authKeyBox
            // 
            authKeyBox.Controls.Add(confirmPassLbl);
            authKeyBox.Controls.Add(confirmPasswordTxt);
            authKeyBox.Controls.Add(createPassBtn);
            authKeyBox.Controls.Add(label2);
            authKeyBox.Controls.Add(createPassTxt);
            authKeyBox.Controls.Add(enterPassBtn);
            authKeyBox.Controls.Add(enterPassLbl);
            authKeyBox.Controls.Add(enterPassTxt);
            authKeyBox.Controls.Add(enterpassLabel);
            authKeyBox.Controls.Add(keyTxtBox);
            authKeyBox.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            authKeyBox.ForeColor = Color.WhiteSmoke;
            authKeyBox.Location = new Point(12, 12);
            authKeyBox.Name = "authKeyBox";
            authKeyBox.Size = new Size(588, 395);
            authKeyBox.TabIndex = 5;
            authKeyBox.TabStop = false;
            authKeyBox.Text = "Key / Hash";
            // 
            // confirmPassLbl
            // 
            confirmPassLbl.AutoSize = true;
            confirmPassLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            confirmPassLbl.Location = new Point(7, 263);
            confirmPassLbl.Name = "confirmPassLbl";
            confirmPassLbl.Size = new Size(175, 25);
            confirmPassLbl.TabIndex = 14;
            confirmPassLbl.Text = "Confirm Password";
            // 
            // confirmPasswordTxt
            // 
            confirmPasswordTxt.BackColor = SystemColors.ControlDarkDark;
            confirmPasswordTxt.ForeColor = Color.WhiteSmoke;
            confirmPasswordTxt.Location = new Point(7, 291);
            confirmPasswordTxt.Name = "confirmPasswordTxt";
            confirmPasswordTxt.Size = new Size(575, 32);
            confirmPasswordTxt.TabIndex = 13;
            confirmPasswordTxt.UseSystemPasswordChar = true;
            // 
            // createPassBtn
            // 
            createPassBtn.BackColor = SystemColors.ControlDarkDark;
            createPassBtn.FlatAppearance.BorderSize = 3;
            createPassBtn.FlatStyle = FlatStyle.Flat;
            createPassBtn.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            createPassBtn.ForeColor = Color.WhiteSmoke;
            createPassBtn.Location = new Point(7, 329);
            createPassBtn.Name = "createPassBtn";
            createPassBtn.Size = new Size(575, 41);
            createPassBtn.TabIndex = 12;
            createPassBtn.Text = "&Create Key";
            createPassBtn.UseVisualStyleBackColor = false;
            createPassBtn.Click += createPassBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(7, 200);
            label2.Name = "label2";
            label2.Size = new Size(295, 25);
            label2.TabIndex = 11;
            label2.Text = "Enter Password To Create A Key";
            // 
            // createPassTxt
            // 
            createPassTxt.BackColor = SystemColors.ControlDarkDark;
            createPassTxt.ForeColor = Color.WhiteSmoke;
            createPassTxt.Location = new Point(7, 228);
            createPassTxt.Name = "createPassTxt";
            createPassTxt.Size = new Size(575, 32);
            createPassTxt.TabIndex = 10;
            createPassTxt.UseSystemPasswordChar = true;
            // 
            // enterPassBtn
            // 
            enterPassBtn.BackColor = SystemColors.ControlDarkDark;
            enterPassBtn.FlatAppearance.BorderSize = 3;
            enterPassBtn.FlatStyle = FlatStyle.Flat;
            enterPassBtn.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            enterPassBtn.ForeColor = Color.WhiteSmoke;
            enterPassBtn.Location = new Point(7, 156);
            enterPassBtn.Name = "enterPassBtn";
            enterPassBtn.Size = new Size(575, 41);
            enterPassBtn.TabIndex = 9;
            enterPassBtn.Text = "&Enter";
            enterPassBtn.UseVisualStyleBackColor = false;
            enterPassBtn.Click += enterPassBtn_Click;
            // 
            // enterPassLbl
            // 
            enterPassLbl.AutoSize = true;
            enterPassLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            enterPassLbl.Location = new Point(7, 90);
            enterPassLbl.Name = "enterPassLbl";
            enterPassLbl.Size = new Size(150, 25);
            enterPassLbl.TabIndex = 8;
            enterPassLbl.Text = "Enter Password";
            // 
            // enterPassTxt
            // 
            enterPassTxt.BackColor = SystemColors.ControlDarkDark;
            enterPassTxt.ForeColor = Color.WhiteSmoke;
            enterPassTxt.Location = new Point(7, 118);
            enterPassTxt.Name = "enterPassTxt";
            enterPassTxt.Size = new Size(575, 32);
            enterPassTxt.TabIndex = 7;
            enterPassTxt.UseSystemPasswordChar = true;
            // 
            // enterpassLabel
            // 
            enterpassLabel.AutoSize = true;
            enterpassLabel.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            enterpassLabel.Location = new Point(7, 28);
            enterpassLabel.Name = "enterpassLabel";
            enterpassLabel.Size = new Size(113, 25);
            enterpassLabel.TabIndex = 3;
            enterpassLabel.Text = "Key Output";
            // 
            // keyTxtBox
            // 
            keyTxtBox.BackColor = SystemColors.ControlDarkDark;
            keyTxtBox.ForeColor = Color.WhiteSmoke;
            keyTxtBox.Location = new Point(7, 55);
            keyTxtBox.Name = "keyTxtBox";
            keyTxtBox.ReadOnly = true;
            keyTxtBox.Size = new Size(575, 32);
            keyTxtBox.TabIndex = 2;
            // 
            // controlsBox
            // 
            controlsBox.Controls.Add(saveFile);
            controlsBox.Controls.Add(openFile);
            controlsBox.Controls.Add(encryptBtn);
            controlsBox.Controls.Add(decryptBtn);
            controlsBox.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            controlsBox.ForeColor = Color.WhiteSmoke;
            controlsBox.Location = new Point(12, 431);
            controlsBox.Name = "controlsBox";
            controlsBox.Size = new Size(588, 224);
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
            saveFile.Size = new Size(575, 41);
            saveFile.TabIndex = 5;
            saveFile.Text = "&Save Output";
            saveFile.UseVisualStyleBackColor = false;
            saveFile.Click += saveFile_Click;
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
            openFile.Size = new Size(575, 41);
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
            encryptBtn.Size = new Size(575, 41);
            encryptBtn.TabIndex = 2;
            encryptBtn.Text = "&Encrypt";
            encryptBtn.UseVisualStyleBackColor = false;
            encryptBtn.Click += encryptBtn_Click;
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
            decryptBtn.Size = new Size(575, 41);
            decryptBtn.TabIndex = 3;
            decryptBtn.Text = "&Decrypt";
            decryptBtn.UseVisualStyleBackColor = false;
            decryptBtn.Click += decryptBtn_Click;
            // 
            // welcomeLbl
            // 
            welcomeLbl.AutoSize = true;
            welcomeLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            welcomeLbl.ForeColor = Color.WhiteSmoke;
            welcomeLbl.Location = new Point(12, 658);
            welcomeLbl.Name = "welcomeLbl";
            welcomeLbl.Size = new Size(139, 25);
            welcomeLbl.TabIndex = 7;
            welcomeLbl.Text = "Welcome, null";
            // 
            // statusLbl
            // 
            statusLbl.AutoSize = true;
            statusLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            statusLbl.ForeColor = Color.WhiteSmoke;
            statusLbl.Location = new Point(12, 683);
            statusLbl.Name = "statusLbl";
            statusLbl.Size = new Size(85, 25);
            statusLbl.TabIndex = 8;
            statusLbl.Text = "Status ::";
            // 
            // worker
            // 
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            // 
            // currentStatusLbl
            // 
            currentStatusLbl.AutoSize = true;
            currentStatusLbl.Font = new Font("Segoe UI Black", 9F, FontStyle.Bold, GraphicsUnit.Point);
            currentStatusLbl.ForeColor = Color.WhiteSmoke;
            currentStatusLbl.Location = new Point(105, 683);
            currentStatusLbl.Name = "currentStatusLbl";
            currentStatusLbl.Size = new Size(64, 25);
            currentStatusLbl.TabIndex = 9;
            currentStatusLbl.Text = "Idle...";
            currentStatusLbl.UseWaitCursor = true;
            // 
            // Homepage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(612, 719);
            Controls.Add(currentStatusLbl);
            Controls.Add(statusLbl);
            Controls.Add(welcomeLbl);
            Controls.Add(controlsBox);
            Controls.Add(authKeyBox);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Label enterpassLabel;
        private TextBox keyTxtBox;
        private GroupBox controlsBox;
        private Button saveFile;
        private Button openFile;
        private Button encryptBtn;
        private Button decryptBtn;
        private Label welcomeLbl;
        private Label statusLbl;
        private System.ComponentModel.BackgroundWorker worker;
        private Label currentStatusLbl;
        private Button enterPassBtn;
        private Label enterPassLbl;
        private TextBox enterPassTxt;
        private Button createPassBtn;
        private Label label2;
        private TextBox createPassTxt;
        private Label confirmPassLbl;
        private TextBox confirmPasswordTxt;
    }
}