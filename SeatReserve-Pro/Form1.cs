/******************************************************************************
     * File:        Form1.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.3
     * Description: This file contains the partial Form1 class, which contains the handlers for the different UI-elements.
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation
     * 2024-06-06  Nils Hollenstein   Bus gets drawn, seats can be reserved
     * 2024-06-07  Nils Hollenstein   Busses can be selected
     * 2024-06-07  Nils Hollenstein   Database inserts integrated
     * 2024-06-12  Nils Hollenstein   Database read and update integratet
     * 2024-06-13  Nils Hollenstein   User is able to sign-up and log in
     * 2024-06-13  Nils Hollenstein   User gets error message on sign-up
     * 2024-06-14  Nils Hollenstein   Error message on login
     * 2024-06-14  Nils Hollenstein   User is able to logout
     * 2024-06-14  Nils Hollenstein   Seats get reserved on a user
     * 2024-06-19  Nils Hollenstein   User can cancel his own reservation
     * 2024-06-20  Nils Hollenstein   Admin interface blocks reservations
     * 2024-06-20  Nils Hollenstein   Admin can cancel reservations of all users
     * 2024-06-20  Nils Hollenstein   Admin can cancel reservations of all users
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

using SeatReserveLibrary.DBOperations;
using SeatReserveLibrary.DrawBusClasses;
using SeatReserveLibrary.HashSecurityClasses;
using SeatReserveLibrary.UserClasses;
namespace SeatReserve_Pro
{
    public partial class Form1 : Form
    {
        // Variables
        User loggedInUser = new();
        User firstUser = new();
        List<Bus> busses = new List<Bus>();
        List<User> users = new List<User>();
        private Bus userBusSelected = new();
        private bool busSelected = false;
        private bool loggedIn = false;
        private bool openedLoginForm = false;
        private bool openedSignUpForm = false;
        private string selectedUsernameNewAdmin;


        // Constructor
        public Form1()
        {
            GetBusPartsDB();
            InitializeComponent();
            FillBusSelection();
            DisplayCorrectUIComponents();
            FillNewAdminSelection();
        }

        // Methods
        // Event-Handler

        // Paint event of the Form
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (busSelected)
                DrawBus(userBusSelected);
        }
        // Click handler for a click in the form on a seat
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (busSelected)
            {
                foreach (var seat in userBusSelected.Seats)
                {
                    if (seat.SeatRectangle.Contains(e.Location))
                    {
                        if (!seat.Selected && !seat.Reserved && !loggedInUser.Admin)
                        {
                            seat.Selected = true;
                            Invalidate();
                        }
                        else if (seat.Selected && !seat.Reserved && !loggedInUser.Admin)
                        {
                            seat.Selected = false;
                            Invalidate();
                        }
                        else if (!seat.Selected && seat.Reserved && (seat.ReservedBy == loggedInUser.Userid || loggedInUser.Admin))
                        {
                            seat.Selected = true;
                            Invalidate();
                        }
                        else if (seat.Selected && seat.Reserved && (seat.ReservedBy == loggedInUser.Userid || loggedInUser.Admin))
                        {
                            seat.Selected = false;
                            Invalidate();
                        }
                        else if (seat.Reserved && seat.ReservedBy != loggedInUser.Userid && !loggedInUser.Admin)
                            MessageBox.Show("Seat already reserved by another user");
                    }
                }
            }
        }
        // Opens the login-formular
        private void OpenLoginButton_Click(object sender, EventArgs e)
        {
            openedLoginForm = true;
            DisplayCorrectUIComponents();
        }
        // React to the Button Click
        private void ReserveButton_Click(object sender, EventArgs e)
        {
            foreach (var seat in userBusSelected.Seats)
            {
                if (seat.Selected && !seat.Reserved)
                {
                    seat.Reserved = true;
                    seat.Selected = false;
                    seat.ReservedBy = loggedInUser.Userid;
                }
            }
            UpdateBusPartsDB();
            Invalidate();
        }
        // Cancle Reservations with button
        private void CancelReservationButton_Click(object sender, EventArgs e)
        {
            foreach (var seat in userBusSelected.Seats)
            {
                if (seat.Selected && seat.Reserved && (seat.ReservedBy == loggedInUser.Userid || loggedInUser.Admin))
                {
                    seat.Reserved = false;
                    seat.Selected = false;
                    seat.ReservedBy = -1;
                }
            }
            UpdateBusPartsDB();
            Invalidate();
        }
        // Handler for the combobox, lets you select the busses
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/6901070/getting-selected-value-of-a-combobox
            ComboBox comboBox = (ComboBox)sender;
            string? selectedValue = comboBox.SelectedItem as string;

            if (selectedValue != null)
            {
                foreach (var bus in busses)
                {
                    if (bus.Destination == selectedValue)
                    {
                        bus.Destination = selectedValue;
                        userBusSelected = bus;
                        busTitle.Text = bus.Destination;
                        SetBusSelectionPartsVisibility(false);
                        SetSeatReservePartsVisibility(true);
                    }
                }
                DrawBus(userBusSelected);
                busSelected = true;
            }
            DisplayCorrectUIComponents();
        }
        // Buttonhandler for the menu button
        private void BackToSelectionButton_Click(object sender, EventArgs e)
        {
            busSelected = false;
            SetBusSelectionPartsVisibility(true);
            SetSeatReservePartsVisibility(false);
            DisplayCorrectUIComponents();
            Invalidate();
        }
        // Opens the sign up form
        private void OpenSignUpButton_Click(object sender, EventArgs e)
        {
            openedSignUpForm = true;
            DisplayCorrectUIComponents();
        }
        // Buttonhandler for the registration button
        private void SignUpButton_Click(object sender, EventArgs e)
        {
            // Variables
            var hashString = new HashString();
            var dbClient = new DBOperations();
            var users = dbClient.ReadUsers();
            bool usernameUsed = false;

            // Checks if someone has the same username
            var username = usernameSignUpInput.Text;
            foreach (var oldUser in users)
            {
                if (oldUser.Username == username)
                {
                    MessageBox.Show("Dieser Benutzernamen wird bereits verwendet");
                    usernameUsed = true;
                    break;
                }
            }
            var password = passwordSignUpInput.Text;
            // Hash the two informations
            var passwordHashed = HashString.HashBCrypt(password);

            var user = new SeatReserveLibrary.UserClasses.User(username, passwordHashed, false);
            
            // Checks if a field is empty
            if (username == null || password == null || username == "" || password == "")
                MessageBox.Show("Bitte füllens sie alle Felder aus");
            else if (!usernameUsed)
            {
                // Registrates the user
                dbClient.InsertNewUser(user);
                openedSignUpForm = false;
                loggedIn = false;
                usernameSignUpInput.Text = "";
                passwordSignUpInput.Text = "";
                DisplayCorrectUIComponents();
            }

        }
        // Buttonhandler for the login button
        private void LoginButton_Click(object sender, EventArgs e)
        {
            // Variables
            var hashString = new HashString();
            var dbClient = new DBOperations();
            string username = usernameLoginInput.Text;
            string password = passwordLoginInput.Text;
            var users = dbClient.ReadUsers();
            bool wrongLoginData = false;

            // Check if a field is empty
            if (username == null || password == null || username == "" || password == "")
                MessageBox.Show("Bitte füllens sie alle Felder aus");
            else
            {
                // Check if the inputs fit to an existing user
                foreach (var user in users)
                {
                    // This if loggs the user in
                    if (HashString.VerifyBCrypt(user.Password, password) && user.Username == username)
                    {
                        loggedIn = true;
                        openedLoginForm = false;
                        loggedInUser = user;
                        wrongLoginData = false;
                        DisplayCorrectUIComponents();
                        usernameLoginInput.Text = "";
                        passwordLoginInput.Text = "";
                        break;
                    }
                    else
                        wrongLoginData = true;
                }
                if (wrongLoginData)
                {
                    // Error message in case of no fitting inputs
                    MessageBox.Show("Benutzername und Passwort stimmen nicht überein");
                    wrongLoginData = false;
                }
            }

        }
        // Buttonhandler for the logout button
        private void LogoutButton_Click(object sender, EventArgs e)
        {
            // logs the user out
            loggedIn = false;
            openedSignUpForm = false;
            openedLoginForm = false;
            //loggedInUser = new SeatReserveLibrary.UserClasses.User();
            DisplayCorrectUIComponents();
        }
        // Button to get out of the Login/Sign-up form
        private void BackToStartButton_Click(object sender, EventArgs e)
        {
            if (openedSignUpForm)
                openedSignUpForm = false;
            else if (openedLoginForm)
                openedLoginForm = false;
            DisplayCorrectUIComponents();
        }
        // ComboBox to select user for new admin
        private void NewAdminSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var dbClient = new DBOperations();
            string? selectedValue = comboBox.SelectedItem as string;
            users = dbClient.ReadUsers();
            // Save the first user for the buttonhandler
            firstUser = users.Find(user => user.Userid == 0);
            // Save the username for the buttonhandler
            selectedUsernameNewAdmin = selectedValue;

            if (firstUser != null)
                if (selectedValue != null)
                {
                    // Display the needed components
                    passwordFirstAdminInput.PlaceholderText = $"Passwort von {firstUser.Username} eingeben";
                    SetCreateNewAdminUserPartsVisibility(true);

                    // Get Length of string and adjust textbox
                    // https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphics.measurestring?view=net-8.0
                    Graphics graphics;
                    graphics = this.CreateGraphics();
                    SizeF size = new SizeF();
                    size = graphics.MeasureString(passwordFirstAdminInput.PlaceholderText, passwordFirstAdminInput.Font);
                    passwordFirstAdminInput.Width = (int)size.Width;
                }
        }
        // Buttonhandler to create new admin out of selected user
        private void CreateNewAdminSubmitButton_Click(object sender, EventArgs e)
        {
            string password = passwordFirstAdminInput.Text;
            var dbClient = new DBOperations();
            HashString hashString = new HashString();
            if (password != "")
                if (HashString.VerifyBCrypt(firstUser.Password, password))
                {
                    User user = users.Find(user => user.Username == selectedUsernameNewAdmin);
                    dbClient.UpdateExistingUser(true, user.Userid);

                    passwordFirstAdminInput.Text = "";
                    newAdminSelection.Text = "";
                    SetCreateNewAdminUserPartsVisibility(false);
                }
                else
                    MessageBox.Show("Falsches Passwort");
            else
                MessageBox.Show("Bitte geben sie das Passwort ein");
            FillNewAdminSelection();
        }

        // Other methods

        // Fills the Bus selection with busses
        private void FillBusSelection()
        {
            busTitle.Location = new Point(Width / 2 - busTitle.Width / 2, 30);
            // Only adds the locations to the selection if it does not contain the location
            List<string> existingDestinations = new List<string>();
            foreach (var item in busSelection.Items)
            {
                if (item != null)
                    existingDestinations.Add(item.ToString());
            }

            foreach (var bus in busses)
            {
                if (!string.IsNullOrEmpty(bus.Destination) && !existingDestinations.Contains(bus.Destination))
                    busSelection.Items.Add(bus.Destination);
            }
        }
        // Fill the user selection to create new admins
        private void FillNewAdminSelection()
        {
            var dbService = new DBOperations();
            List<User> users = dbService.ReadUsers();
            // Only adds the locations to the selection if it does not contain the location
            newAdminSelection.Items.Clear();
            foreach (var user in users)
            {
                if (!string.IsNullOrEmpty(user.Username) && !user.Admin)
                    newAdminSelection.Items.Add(user.Username);
            }
        }
        // Decides which parts should be displayed on the form
        private void DisplayCorrectUIComponents()
        {
            // Decides which UI should be displayed
            // check what should be displayed
            if (busSelected && loggedIn && !openedLoginForm && !openedSignUpForm)
            {
                SetLoginFormVisibility(false);
                SetLoginSignUpPartsVisibility(false);
                SetSignUpFormVisibility(false);
                SetBusSelectionPartsVisibility(false);
                SetAdminPartsVisibility(false);
                SetSeatReservePartsVisibility(true);

            }
            else if (!busSelected && loggedIn && !openedSignUpForm && !openedLoginForm)
            {
                SetLoginFormVisibility(false);
                SetLoginSignUpPartsVisibility(false);
                SetSignUpFormVisibility(false);
                SetSeatReservePartsVisibility(false);
                SetAdminPartsVisibility(false);
                SetBusSelectionPartsVisibility(true);
                if (loggedInUser.Admin)
                    SetAdminPartsVisibility(true);
                else
                    SetAdminPartsVisibility(false);
            }
            else if (openedLoginForm && !loggedIn)
            {
                SetLoginSignUpPartsVisibility(false);
                SetSignUpFormVisibility(false);
                SetSeatReservePartsVisibility(false);
                SetBusSelectionPartsVisibility(false);
                SetAdminPartsVisibility(false);
                SetLoginFormVisibility(true);
            }
            else if (openedSignUpForm && !loggedIn)
            {
                SetLoginFormVisibility(false);
                SetLoginSignUpPartsVisibility(false);
                SetSeatReservePartsVisibility(false);
                SetBusSelectionPartsVisibility(false);
                SetAdminPartsVisibility(false);
                SetSignUpFormVisibility(true);
            }
            else
            {
                SetLoginFormVisibility(false);
                SetSignUpFormVisibility(false);
                SetSeatReservePartsVisibility(false);
                SetBusSelectionPartsVisibility(false);
                SetAdminPartsVisibility(false);
                SetLoginSignUpPartsVisibility(true);
            }
        }

        // Methods to draw the bus

        // Function to draw the whole bus
        private void DrawBus(Bus bus)
        {
            Graphics graphics;
            graphics = this.CreateGraphics();
            int yCounter = 0;
            CreateDrawingUtilities(graphics, yCounter, bus);
        }
        // Method to Create all things needed to draw a bus
        private void CreateDrawingUtilities(Graphics graphics, int yCounter, Bus bus)
        {
            // Needed informations to draw the rectangle
            int xPos = 80;
            int yPos = 80;
            // Needed informations
            int maxWidth = 0;
            int maxHeight = 0;
            // Brushes and pens to draw the bus
            SolidBrush darkGreyBrush = new SolidBrush(Color.FromArgb(255, 64, 63, 63));

            Pen blackPen = new Pen(Color.Black);
            DrawSeats(graphics, yCounter, xPos, yPos, maxWidth, maxHeight, darkGreyBrush, blackPen, bus);
        }
        // Method to choose the color for the bus seats
        private void DrawSeatCorrectColor(Seat seat, Graphics graphics, Bus bus)
        {
            SolidBrush grayBrush = new SolidBrush(Color.Gray);

            // Brush when seat selected and its not reserved
            SolidBrush selectedNotReservedBrush = new SolidBrush(Color.FromArgb(255, 87, 119, 150));
            // Brush when seat selected and its reserved
            SolidBrush selectedReservedBrush = new SolidBrush(Color.FromArgb(255, 247, 216, 12));
            // Brush when seat is reserved by this user
            SolidBrush reservedByLogedInUserBrush = new SolidBrush(Color.FromArgb(255, 191, 31, 15));
            // Brush when seat is reserved by another user
            SolidBrush reservedByOtherUserBrush = new SolidBrush(Color.FromArgb(255, 94, 12, 5));

            if (seat.Selected && !seat.Reserved)
                graphics.FillRectangle(selectedNotReservedBrush, seat.SeatRectangle);
            else if (seat.Selected && seat.Reserved)
                graphics.FillRectangle(selectedReservedBrush, seat.SeatRectangle);
            else if (!seat.Selected && seat.Reserved && (seat.ReservedBy == loggedInUser.Userid || loggedInUser.Admin))
                graphics.FillRectangle(reservedByLogedInUserBrush, seat.SeatRectangle);
            else if (!seat.Selected && seat.Reserved && seat.ReservedBy != loggedInUser.Userid)
                graphics.FillRectangle(reservedByOtherUserBrush, seat.SeatRectangle);
            else
                graphics.FillRectangle(grayBrush, seat.SeatRectangle);
        }
        // Method which draws all the seats and also saves them in the seat objects
        private void DrawSeats(Graphics graphics, int yCounter, int xPos, int yPos, int maxWidth, int maxHeight, SolidBrush darkGreyBrush, Pen blackPen, Bus bus)
        {
            // Foreach to iterate throug the seats list
            foreach (var seat in bus.Seats)
            {
                // If the seatRectangle property of the seat is empty, asign a new Rectangle to it
                if (seat.SeatRectangle == new Rectangle(0, 0, 0, 0))
                {
                    Rectangle seatRectangle = new Rectangle(xPos, yPos, seat.Width, seat.Height);
                    seat.SeatRectangle = seatRectangle;
                }
                // Choose the color for the seat
                DrawSeatCorrectColor(seat, graphics, bus);
                // Draw the border for the seat
                graphics.DrawRectangle(blackPen, seat.SeatRectangle);
                yCounter++;
                if (yCounter == 4)
                {
                    yPos = 80;
                    xPos += seat.Width + 10;
                    // Set the max width of the seat rows
                    maxWidth = xPos + seat.Width + 10;
                    yCounter = 0;
                }
                else if (yCounter == 2)
                {
                    yPos += seat.Width + 20;
                }
                else
                {
                    yPos += seat.Height + 10;
                    graphics.DrawRectangle(blackPen, new Rectangle(xPos, yPos - 10, seat.Width, 10));
                    graphics.FillRectangle(darkGreyBrush, xPos, yPos - 10, seat.Width, 10);
                }
                // Check which of the heights is higher and chose the higher one as maxHeight 
                maxHeight = Math.Max(maxHeight, yPos + seat.Height);
            }
            // Call the DrawOutline Method with fiting parameters
            DrawOutline(80, 80, graphics, maxWidth - 80, maxHeight - 80, blackPen);
        }
        // Method to draw the bus outlines/detailes
        private void DrawOutline(int startSeatXpos, int startSeatYpos, Graphics graphics, int totalWidth, int totalHeight, Pen blackPen)
        {
            // Brush for the driver seat
            SolidBrush driverSeatBrush = new SolidBrush(Color.FromArgb(255, 82, 83, 84));

            // Positions for the outer lines of the bus 
            int xPos = startSeatXpos;
            int yPos = startSeatYpos;
            // Create rectangles for the seats
            Rectangle rectOuterLines = new Rectangle(xPos, yPos, totalWidth + 60, totalHeight);
            Rectangle rectDriverSeat = new Rectangle(totalWidth + 70, yPos + 20, 30, 30);

            // Draw the Lines for the Bus
            graphics.DrawLine(blackPen, totalWidth + 60, yPos, totalWidth + 60, yPos + 90);
            graphics.DrawLine(blackPen, totalWidth + 80, yPos + 90, totalWidth + 140, yPos + 90);
            graphics.FillRectangle(driverSeatBrush, rectDriverSeat);
            graphics.DrawRectangle(blackPen, rectDriverSeat);

            graphics.DrawRectangle(blackPen, rectOuterLines);
        }

        // Set the visibility of the UI elements

        // Set the visibility of the SeatReservationParts
        private void SetSeatReservePartsVisibility(bool setVisibility)
        {
            backToSelectionButton.Visible = setVisibility;
            busTitle.Visible = setVisibility;
            cancelReservationButton.Visible = setVisibility;
            if (!loggedInUser.Admin)
                ReserveButton.Visible = setVisibility;
            else
                ReserveButton.Visible = setVisibility;
        }
        // Set the visibility of the bus selection
        private void SetBusSelectionPartsVisibility(bool setVisibility)
        {
            subTitleBusSelection.Visible = setVisibility;
            busSelection.Visible = setVisibility;
            logoutButton.Visible = setVisibility;
            loggedInStatus.Visible = setVisibility;
            loggedInStatus.Text = $"Angemeldet als {loggedInUser.Username}";
        }
        // Set the visibility of the login/signup selection
        private void SetLoginSignUpPartsVisibility(bool setVisibility)
        {
            if (setVisibility)
            {

                loginSignUpLabel.Text = "Anmelden/Registrieren";
            }
            loginSignUpLabel.Visible = setVisibility;
            openLoginButton.Visible = setVisibility;
            openSignUpButton.Visible = setVisibility;
            loginSignUpLabel.Location = new Point(Width / 2 - loginSignUpLabel.Width / 2, loginSignUpLabel.Location.Y);
            openLoginButton.Location = new Point(Width / 2 - openLoginButton.Width / 2, openLoginButton.Location.Y);
            openSignUpButton.Location = new Point(Width / 2 - openSignUpButton.Width / 2, openSignUpButton.Location.Y);


        }
        // Set the visibility of the login form
        private void SetLoginFormVisibility(bool setVisibility)
        {
            usernameLoginLabel.Visible = setVisibility;
            usernameLoginInput.Visible = setVisibility;
            passwordLoginInput.Visible = setVisibility;
            passwordLoginLabel.Visible = setVisibility;
            loginButton.Visible = setVisibility;
            backToStartButton.Visible = setVisibility;

            if (setVisibility)
            {
                loginSignUpLabel.Text = "Anmelden";
                loginSignUpLabel.Visible = setVisibility;

                loginSignUpLabel.Location = new Point(Width / 2 - loginSignUpLabel.Width / 2, loginSignUpLabel.Location.Y);
                usernameLoginLabel.Location = new Point(Width / 2 - usernameLoginLabel.Width / 2, usernameLoginLabel.Location.Y);
                usernameLoginInput.Location = new Point(Width / 2 - usernameLoginInput.Width / 2, usernameLoginInput.Location.Y);
                passwordLoginInput.Location = new Point(Width / 2 - passwordLoginInput.Width / 2, passwordLoginInput.Location.Y);
                passwordLoginLabel.Location = new Point(Width / 2 - passwordLoginLabel.Width / 2, passwordLoginLabel.Location.Y);
                loginButton.Location = new Point(Width / 2 - loginButton.Width / 2, loginButton.Location.Y);
            }

        }
        // Set the visibility of the signup selection
        private void SetSignUpFormVisibility(bool setVisibility)
        {
            usernameSignUpLabel.Visible = setVisibility;
            usernameSignUpInput.Visible = setVisibility;
            passwordSignUpInput.Visible = setVisibility;
            passwordSignUpLabel.Visible = setVisibility;
            backToStartButton.Visible = setVisibility;

            signUpButton.Visible = setVisibility;

            if (setVisibility)
            {
                loginSignUpLabel.Text = "Registrieren";
                loginSignUpLabel.Visible = setVisibility;

                loginSignUpLabel.Location = new Point(Width / 2 - loginSignUpLabel.Width / 2, loginSignUpLabel.Location.Y);
                usernameSignUpInput.Location = new Point(Width / 2 - usernameSignUpInput.Width / 2, usernameSignUpInput.Location.Y);
                usernameSignUpLabel.Location = new Point(Width / 2 - usernameSignUpLabel.Width / 2, usernameSignUpLabel.Location.Y);
                passwordSignUpInput.Location = new Point(Width / 2 - passwordSignUpInput.Width / 2, passwordSignUpInput.Location.Y);
                passwordSignUpLabel.Location = new Point(Width / 2 - passwordSignUpLabel.Width / 2, passwordSignUpLabel.Location.Y);
                signUpButton.Location = new Point(Width / 2 - signUpButton.Width / 2, signUpButton.Location.Y);
            }

        }
        //
        private void SetAdminPartsVisibility(bool setVisibility)
        {
            labelNewAdmin.Visible = setVisibility;
            newAdminSelection.Visible = setVisibility;
            if (!setVisibility)
            {
                SetCreateNewAdminUserPartsVisibility(false);
            }
        }
        //
        private void SetCreateNewAdminUserPartsVisibility(bool setVisibility)
        {
            passwordFirstAdminInput.Visible = setVisibility;
            createNewAdminSubmitButton.Visible = setVisibility;
        }

        // Operate with the database 

        // Methodes to update the database
        private void UpdateBusPartsDB()
        {
            var dbService = new DBOperations();
            dbService.UpdateBusPartsDB(userBusSelected);
            GetBusPartsDB();

        }
        // Methodes to get the data from databases
        private void GetBusPartsDB()
        {
            var dbService = new DBOperations();
            busses = dbService.ReadBusPartsDB();
        }

        private void loggedInStatus_Click(object sender, EventArgs e)
        {

        }
    }
}