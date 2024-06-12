/******************************************************************************
     * File:        Form1.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.1
     * Description: This file contains the partial Form1 class, which contains the handlers for the different UI-elements.
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 2024-06-06  Nils Hollenstein   Draw the Bus
     * 2024-06-06  Nils Hollenstein   Enable Seat Reservation
     * 2024-06-07  Nils Hollenstein   Busselection is working
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

using BusDBClasses;
using SeatReserve_Pro_DBService;
using System.CodeDom.Compiler;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Xml.Serialization;

namespace SeatReserve_Pro
{
    public partial class Form1 : Form
    {
        // Variables
        List<Bus> busses = new List<Bus>();
        private Bus userBusSelected;
        private bool busSelected = false;


        // Constructor
        public Form1()
        {
            GetDataFromDB();
            InitializeComponent();
            DisplayCorrectUI();
        }

        // Methods
        // Event-Handler

        // Paint event of the Form
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (busSelected)
            {
                DrawBus(userBusSelected);
            }
        }

        // Click handler for a click in the form on a seat
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (busSelected)
            {
                foreach (var seat in userBusSelected.seats)
                {
                    if (seat.seatRectangle.Contains(e.Location))
                    {
                        if (seat.seatRectangle.Contains(e.Location) && !seat.selected && !seat.reserved)
                            // Set selected to true
                            seat.selected = true;
                        else if (seat.seatRectangle.Contains(e.Location) && seat.selected && !seat.reserved)
                            seat.selected = false;
                        else if (seat.seatRectangle.Contains(e.Location) && seat.reserved)
                            MessageBox.Show("Seat already reserved");
                        Invalidate();
                    }
                }
            }

        }

        // React to the Button Click
        private void ReserveButton_Click(object sender, EventArgs e)
        {

            foreach (var seat in userBusSelected.seats)
            {
                if (seat.selected)
                {
                    seat.reserved = true;
                    seat.selected = false;
                }
            }
            updateDB();
            Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/6901070/getting-selected-value-of-a-combobox
            ComboBox comboBox = (ComboBox)sender;
            string? selectedValue = comboBox.SelectedItem as string;

            if (selectedValue != null)
            {
                foreach (var bus in busses)
                {
                    if (bus.destination == selectedValue)
                    {
                        bus.destination = selectedValue;
                        userBusSelected = bus;
                        busTitle.Text = bus.destination;
                        SetBusSelectionPartsVisibility(false);
                        SetSeatReservePartsVisibility(true);

                    }
                }
                DrawBus(userBusSelected);
                busSelected = true;
            }
        }

        private void backToSelectionButton_Click(object sender, EventArgs e)
        {
            busSelected = false;
            SetBusSelectionPartsVisibility(true);
            SetSeatReservePartsVisibility(false);
            Invalidate();
        }

        // Decides which UI should be displayed
        private void DisplayCorrectUI()
        {
            busTitle.Location = new Point(Width / 2 - busTitle.Width, 30);

            foreach (var bus in busses)
            {
                if (bus.destination != "")
                    busSelection.Items.Add(bus.destination);
                if (busSelected)
                {
                    SetSeatReservePartsVisibility(true);
                    SetBusSelectionPartsVisibility(false);
                }
                else
                {
                    SetSeatReservePartsVisibility(false);
                    SetBusSelectionPartsVisibility(true);
                }
            }
        }

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

            SolidBrush selectedBrush = new SolidBrush(Color.FromArgb(255, 87, 119, 150));
            SolidBrush reservedBrush = new SolidBrush(Color.FromArgb(255, 247, 101, 116));

            if (seat.selected)
                graphics.FillRectangle(selectedBrush, seat.seatRectangle);
            else if (seat.reserved)
                graphics.FillRectangle(reservedBrush, seat.seatRectangle);
            else
                graphics.FillRectangle(grayBrush, seat.seatRectangle);
        }
        // Method which draws all the seats and also saves them in the seat objects
        private void DrawSeats(Graphics graphics, int yCounter, int xPos, int yPos, int maxWidth, int maxHeight, SolidBrush darkGreyBrush, Pen blackPen, Bus bus)
        {
            // Foreach to iterate throug the seats list
            foreach (var seat in bus.seats)
            {
                // If the seatRectangle property of the seat is empty, asign a new Rectangle to it
                if (seat.seatRectangle == new Rectangle(0, 0, 0, 0))
                {
                    Rectangle seatRectangle = new Rectangle(xPos, yPos, seat.width, seat.height);
                    seat.seatRectangle = seatRectangle;
                }
                // Choose the color for the seat
                DrawSeatCorrectColor(seat, graphics, bus);
                // Draw the border for the seat
                graphics.DrawRectangle(blackPen, seat.seatRectangle);
                yCounter++;
                if (yCounter == 4)
                {
                    yPos = 80;
                    xPos += seat.width + 10;
                    // Set the max width of the seat rows
                    maxWidth = xPos + seat.width + 10;
                    yCounter = 0;
                }
                else if (yCounter == 2)
                {
                    yPos += seat.width + 20;
                }
                else
                {
                    yPos += seat.height + 10;
                    graphics.DrawRectangle(blackPen, new Rectangle(xPos, yPos - 10, seat.width, 10));
                    graphics.FillRectangle(darkGreyBrush, xPos, yPos - 10, seat.width, 10);
                }
                // Check which of the heights is higher and chose the higher one as maxHeight 
                maxHeight = Math.Max(maxHeight, yPos + seat.height);
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


        private void SetSeatReservePartsVisibility(bool setVisibility)
        {
            ReserveButton.Visible = setVisibility;
            backToSelectionButton.Visible = setVisibility;
            busTitle.Visible = setVisibility;

        }
        private void SetBusSelectionPartsVisibility(bool setVisibility)
        {
            appTitle.Visible = setVisibility;
            subTitleBusSelection.Visible = setVisibility;
            busSelection.Visible = setVisibility;

        }
        // Methodes to update the database
        private void updateDB()
        {
            var dbService = new SeatReserve_ProDBService();
            dbService.updateDB(busses);
            GetDataFromDB();

        }
        // Methodes to get the data from databases
        private void GetDataFromDB()
        {
            var dbService = new SeatReserve_ProDBService();
            busses = dbService.readDB();
        }

    }
}
