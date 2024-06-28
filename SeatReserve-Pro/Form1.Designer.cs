/******************************************************************************
     * File:        Form1.Designer.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file contains the partial Form1 class, which sets the design for the UI parts
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

namespace SeatReserve_Pro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ReserveButton = new Button();
            busSelection = new ComboBox();
            appTitle = new Label();
            subTitleBusSelection = new Label();
            backToSelectionButton = new Button();
            busTitle = new Label();
            loginSignUpLabel = new Label();
            openLoginButton = new Button();
            openSignUpButton = new Button();
            usernameLoginInput = new TextBox();
            passwordLoginInput = new TextBox();
            usernameLoginLabel = new Label();
            passwordLoginLabel = new Label();
            passwordSignUpLabel = new Label();
            usernameSignUpLabel = new Label();
            passwordSignUpInput = new TextBox();
            usernameSignUpInput = new TextBox();
            loginButton = new Button();
            signUpButton = new Button();
            logoutButton = new Button();
            cancelReservationButton = new Button();
            backToStartButton = new Button();
            newAdminSelection = new ComboBox();
            labelNewAdmin = new Label();
            passwordFirstAdminInput = new TextBox();
            createNewAdminSubmitButton = new Button();
            loggedInStatus = new Label();
            repeatPasswordLabel = new Label();
            repeatPasswordInput = new TextBox();
            SuspendLayout();
            // 
            // ReserveButton
            // 
            ReserveButton.Location = new Point(961, 415);
            ReserveButton.Name = "ReserveButton";
            ReserveButton.Size = new Size(75, 23);
            ReserveButton.TabIndex = 0;
            ReserveButton.Text = "Reservieren";
            ReserveButton.UseVisualStyleBackColor = true;
            ReserveButton.Click += ReserveButton_Click;
            // 
            // busSelection
            // 
            busSelection.FormattingEnabled = true;
            busSelection.Location = new Point(12, 104);
            busSelection.Name = "busSelection";
            busSelection.Size = new Size(121, 23);
            busSelection.TabIndex = 1;
            busSelection.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // appTitle
            // 
            appTitle.AutoSize = true;
            appTitle.Font = new Font("Segoe UI", 20F);
            appTitle.Location = new Point(12, 9);
            appTitle.Name = "appTitle";
            appTitle.Size = new Size(209, 37);
            appTitle.TabIndex = 2;
            appTitle.Text = "SeatReserve-Pro";
            // 
            // subTitleBusSelection
            // 
            subTitleBusSelection.AutoSize = true;
            subTitleBusSelection.Font = new Font("Segoe UI", 12F);
            subTitleBusSelection.Location = new Point(12, 77);
            subTitleBusSelection.Name = "subTitleBusSelection";
            subTitleBusSelection.Size = new Size(172, 21);
            subTitleBusSelection.TabIndex = 3;
            subTitleBusSelection.Text = "Wählen sie den Bus aus";
            // 
            // backToSelectionButton
            // 
            backToSelectionButton.Location = new Point(742, 415);
            backToSelectionButton.Name = "backToSelectionButton";
            backToSelectionButton.Size = new Size(75, 23);
            backToSelectionButton.TabIndex = 4;
            backToSelectionButton.Text = "Zurück";
            backToSelectionButton.UseVisualStyleBackColor = true;
            backToSelectionButton.Click += BackToSelectionButton_Click;
            // 
            // busTitle
            // 
            busTitle.AutoSize = true;
            busTitle.Font = new Font("Segoe UI", 13F);
            busTitle.Location = new Point(380, 50);
            busTitle.Name = "busTitle";
            busTitle.Size = new Size(59, 25);
            busTitle.TabIndex = 5;
            busTitle.Text = "label1";
            // 
            // loginSignUpLabel
            // 
            loginSignUpLabel.AutoSize = true;
            loginSignUpLabel.Font = new Font("Segoe UI", 12F);
            loginSignUpLabel.Location = new Point(449, 104);
            loginSignUpLabel.Name = "loginSignUpLabel";
            loginSignUpLabel.Size = new Size(171, 21);
            loginSignUpLabel.TabIndex = 6;
            loginSignUpLabel.Text = "Anmelden/Registrieren";
            // 
            // openLoginButton
            // 
            openLoginButton.Location = new Point(465, 128);
            openLoginButton.Name = "openLoginButton";
            openLoginButton.Size = new Size(127, 23);
            openLoginButton.TabIndex = 7;
            openLoginButton.Text = "Login";
            openLoginButton.UseVisualStyleBackColor = true;
            openLoginButton.Click += OpenLoginButton_Click;
            // 
            // openSignUpButton
            // 
            openSignUpButton.Location = new Point(465, 157);
            openSignUpButton.Name = "openSignUpButton";
            openSignUpButton.Size = new Size(127, 23);
            openSignUpButton.TabIndex = 8;
            openSignUpButton.Text = "Registrieren";
            openSignUpButton.UseVisualStyleBackColor = true;
            openSignUpButton.Click += OpenSignUpButton_Click;
            // 
            // usernameLoginInput
            // 
            usernameLoginInput.Location = new Point(330, 186);
            usernameLoginInput.Name = "usernameLoginInput";
            usernameLoginInput.PlaceholderText = "Benutzername";
            usernameLoginInput.Size = new Size(127, 23);
            usernameLoginInput.TabIndex = 9;
            // 
            // passwordLoginInput
            // 
            passwordLoginInput.Location = new Point(330, 237);
            passwordLoginInput.Name = "passwordLoginInput";
            passwordLoginInput.PasswordChar = '*';
            passwordLoginInput.PlaceholderText = "Passwort";
            passwordLoginInput.Size = new Size(127, 23);
            passwordLoginInput.TabIndex = 10;
            // 
            // usernameLoginLabel
            // 
            usernameLoginLabel.AutoSize = true;
            usernameLoginLabel.Location = new Point(330, 168);
            usernameLoginLabel.Name = "usernameLoginLabel";
            usernameLoginLabel.Size = new Size(83, 15);
            usernameLoginLabel.TabIndex = 11;
            usernameLoginLabel.Text = "Benutzername";
            // 
            // passwordLoginLabel
            // 
            passwordLoginLabel.AutoSize = true;
            passwordLoginLabel.Location = new Point(330, 219);
            passwordLoginLabel.Name = "passwordLoginLabel";
            passwordLoginLabel.Size = new Size(54, 15);
            passwordLoginLabel.TabIndex = 12;
            passwordLoginLabel.Text = "Passwort";
            // 
            // passwordSignUpLabel
            // 
            passwordSignUpLabel.AutoSize = true;
            passwordSignUpLabel.Location = new Point(605, 219);
            passwordSignUpLabel.Name = "passwordSignUpLabel";
            passwordSignUpLabel.Size = new Size(54, 15);
            passwordSignUpLabel.TabIndex = 16;
            passwordSignUpLabel.Text = "Passwort";
            // 
            // usernameSignUpLabel
            // 
            usernameSignUpLabel.AutoSize = true;
            usernameSignUpLabel.Location = new Point(605, 168);
            usernameSignUpLabel.Name = "usernameSignUpLabel";
            usernameSignUpLabel.Size = new Size(83, 15);
            usernameSignUpLabel.TabIndex = 15;
            usernameSignUpLabel.Text = "Benutzername";
            // 
            // passwordSignUpInput
            // 
            passwordSignUpInput.Location = new Point(605, 237);
            passwordSignUpInput.Name = "passwordSignUpInput";
            passwordSignUpInput.PasswordChar = '*';
            passwordSignUpInput.PlaceholderText = "Passwort";
            passwordSignUpInput.Size = new Size(127, 23);
            passwordSignUpInput.TabIndex = 14;
            // 
            // usernameSignUpInput
            // 
            usernameSignUpInput.Location = new Point(605, 186);
            usernameSignUpInput.Name = "usernameSignUpInput";
            usernameSignUpInput.PlaceholderText = "Benutzername";
            usernameSignUpInput.Size = new Size(127, 23);
            usernameSignUpInput.TabIndex = 13;
            // 
            // loginButton
            // 
            loginButton.Location = new Point(330, 266);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(127, 23);
            loginButton.TabIndex = 19;
            loginButton.Text = "Anmelden";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += LoginButton_Click;
            // 
            // signUpButton
            // 
            signUpButton.Location = new Point(605, 325);
            signUpButton.Name = "signUpButton";
            signUpButton.Size = new Size(127, 23);
            signUpButton.TabIndex = 20;
            signUpButton.Text = "Registrieren";
            signUpButton.UseVisualStyleBackColor = true;
            signUpButton.Click += SignUpButton_Click;
            // 
            // logoutButton
            // 
            logoutButton.Font = new Font("Segoe UI", 10F);
            logoutButton.Location = new Point(956, 12);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(80, 30);
            logoutButton.TabIndex = 21;
            logoutButton.Text = "Abmelden";
            logoutButton.UseVisualStyleBackColor = true;
            logoutButton.Click += LogoutButton_Click;
            // 
            // cancelReservationButton
            // 
            cancelReservationButton.Location = new Point(823, 415);
            cancelReservationButton.Name = "cancelReservationButton";
            cancelReservationButton.Size = new Size(132, 23);
            cancelReservationButton.TabIndex = 22;
            cancelReservationButton.Text = "Reservation aufheben";
            cancelReservationButton.UseVisualStyleBackColor = true;
            cancelReservationButton.Click += CancelReservationButton_Click;
            // 
            // backToStartButton
            // 
            backToStartButton.Location = new Point(12, 415);
            backToStartButton.Name = "backToStartButton";
            backToStartButton.Size = new Size(134, 23);
            backToStartButton.TabIndex = 23;
            backToStartButton.Text = "Zurück zur Startseite";
            backToStartButton.UseVisualStyleBackColor = true;
            backToStartButton.Click += BackToStartButton_Click;
            // 
            // newAdminSelection
            // 
            newAdminSelection.FormattingEnabled = true;
            newAdminSelection.Location = new Point(736, 104);
            newAdminSelection.Name = "newAdminSelection";
            newAdminSelection.Size = new Size(121, 23);
            newAdminSelection.TabIndex = 24;
            newAdminSelection.Visible = false;
            newAdminSelection.SelectedIndexChanged += NewAdminSelection_SelectedIndexChanged;
            // 
            // labelNewAdmin
            // 
            labelNewAdmin.AutoSize = true;
            labelNewAdmin.Font = new Font("Segoe UI", 11F);
            labelNewAdmin.Location = new Point(736, 78);
            labelNewAdmin.Name = "labelNewAdmin";
            labelNewAdmin.Size = new Size(173, 20);
            labelNewAdmin.TabIndex = 25;
            labelNewAdmin.Text = "Neuen Admin auswählen";
            labelNewAdmin.Visible = false;
            // 
            // passwordFirstAdminInput
            // 
            passwordFirstAdminInput.Location = new Point(736, 133);
            passwordFirstAdminInput.Name = "passwordFirstAdminInput";
            passwordFirstAdminInput.PasswordChar = '*';
            passwordFirstAdminInput.PlaceholderText = "Password of user ....";
            passwordFirstAdminInput.Size = new Size(157, 23);
            passwordFirstAdminInput.TabIndex = 26;
            // 
            // createNewAdminSubmitButton
            // 
            createNewAdminSubmitButton.Location = new Point(920, 133);
            createNewAdminSubmitButton.Name = "createNewAdminSubmitButton";
            createNewAdminSubmitButton.Size = new Size(75, 23);
            createNewAdminSubmitButton.TabIndex = 27;
            createNewAdminSubmitButton.Text = "Bestätigen";
            createNewAdminSubmitButton.UseVisualStyleBackColor = true;
            createNewAdminSubmitButton.Click += CreateNewAdminSubmitButton_Click;
            // 
            // loggedInStatus
            // 
            loggedInStatus.AutoSize = true;
            loggedInStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            loggedInStatus.Location = new Point(12, 395);
            loggedInStatus.Name = "loggedInStatus";
            loggedInStatus.Size = new Size(121, 17);
            loggedInStatus.TabIndex = 28;
            loggedInStatus.Text = "LoggedInStatusText";
            // 
            // repeatPasswordLabel
            // 
            repeatPasswordLabel.AutoSize = true;
            repeatPasswordLabel.Location = new Point(605, 274);
            repeatPasswordLabel.Name = "repeatPasswordLabel";
            repeatPasswordLabel.Size = new Size(122, 15);
            repeatPasswordLabel.TabIndex = 30;
            repeatPasswordLabel.Text = "Passwort wiederholen";
            // 
            // repeatPasswordInput
            // 
            repeatPasswordInput.Location = new Point(605, 292);
            repeatPasswordInput.Name = "repeatPasswordInput";
            repeatPasswordInput.PasswordChar = '*';
            repeatPasswordInput.PlaceholderText = "Passwort wiederholen";
            repeatPasswordInput.Size = new Size(127, 23);
            repeatPasswordInput.TabIndex = 31;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 450);
            Controls.Add(repeatPasswordInput);
            Controls.Add(repeatPasswordLabel);
            Controls.Add(loggedInStatus);
            Controls.Add(createNewAdminSubmitButton);
            Controls.Add(passwordFirstAdminInput);
            Controls.Add(labelNewAdmin);
            Controls.Add(newAdminSelection);
            Controls.Add(backToStartButton);
            Controls.Add(cancelReservationButton);
            Controls.Add(logoutButton);
            Controls.Add(signUpButton);
            Controls.Add(loginButton);
            Controls.Add(passwordSignUpLabel);
            Controls.Add(usernameSignUpLabel);
            Controls.Add(passwordSignUpInput);
            Controls.Add(usernameSignUpInput);
            Controls.Add(passwordLoginLabel);
            Controls.Add(usernameLoginLabel);
            Controls.Add(passwordLoginInput);
            Controls.Add(usernameLoginInput);
            Controls.Add(openSignUpButton);
            Controls.Add(openLoginButton);
            Controls.Add(loginSignUpLabel);
            Controls.Add(busTitle);
            Controls.Add(backToSelectionButton);
            Controls.Add(subTitleBusSelection);
            Controls.Add(appTitle);
            Controls.Add(busSelection);
            Controls.Add(ReserveButton);
            Name = "Form1";
            Text = "SeatReserve-Pro";
            Paint += Form1_Paint;
            MouseClick += Form1_MouseClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ReserveButton;
        private ComboBox busSelection;
        private Label appTitle;
        private Label subTitleBusSelection;
        private Button backToSelectionButton;
        private Label busTitle;
        private Label loginSignUpLabel;
        private Button openLoginButton;
        private Button openSignUpButton;
        private TextBox usernameLoginInput;
        private TextBox passwordLoginInput;
        private Label usernameLoginLabel;
        private Label passwordLoginLabel;
        private Label passwordSignUpLabel;
        private Label usernameSignUpLabel;
        private TextBox passwordSignUpInput;
        private TextBox usernameSignUpInput;
        private Button loginButton;
        private Button signUpButton;
        private Button logoutButton;
        private Button cancelReservationButton;
        private Button backToStartButton;
        private ComboBox newAdminSelection;
        private Label labelNewAdmin;
        private TextBox passwordFirstAdminInput;
        private Button createNewAdminSubmitButton;
        private Label loggedInStatus;
        private Label repeatPasswordLabel;
        private TextBox repeatPasswordInput;
    }
}