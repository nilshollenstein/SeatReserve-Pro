/******************************************************************************
     * File:        Form1.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file contains the partial Form1 class, which contains the handlers for the different UI-elements.
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 2024-06-06  Nils Hollenstein   Draw the Bus
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

using SeatReserve_Pro.BusClasses;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace SeatReserve_Pro
{
    public partial class Form1 : Form
    {

        private Bus bus = new Bus(50);
        public Form1()
        {
            InitializeComponent();

        }

        // Drawing of the Form and get the Form Width and Heigth,
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBus();
        }
        // Function to draw the whole bus
        private void DrawBus()
        {
            Graphics graphics;
            graphics = this.CreateGraphics();
            int yCounter = 0;
            DrawSeats(graphics, yCounter);
        }
        // Function which draws all the seats and also saves them in the seat objects
        private void DrawSeats(Graphics graphics, int yCounter)
        {
            // Needed informations to draw the rectangle
            int xPos = 80;
            int yPos = 80;

            // Needed informations
            int maxWidth = 0;
            int maxHeight = 0;

            // Brushes and pens to draw the bus
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            SolidBrush darkGreyBrush = new SolidBrush(Color.FromArgb(255, 64, 63, 63));
            SolidBrush selectedBrush = new SolidBrush(Color.FromArgb(255, 87, 119, 150));
            SolidBrush reservedBrush = new SolidBrush(Color.FromArgb(255, 247, 101, 116));
            Pen blackPen = new Pen(Color.Black);

            // Foreach to iterate throug the seats list
            foreach (var seat in bus.seats)
            {
                // If the seatRectangle property of the seat is empty, asign a new Rectangle to it
                if (seat.seatRectangle == new Rectangle(0, 0, 0, 0))
                {
                    Rectangle seatRectangle = new Rectangle(xPos, yPos, seat.width, seat.height);
                    seat.seatRectangle = seatRectangle;
                }

                // If the seat is selected, draw it with blue(selectedBrush)
                if (seat.selected)
                    graphics.FillRectangle(selectedBrush, seat.seatRectangle);
                else if (seat.reserved)
                    graphics.FillRectangle(reservedBrush, seat.seatRectangle);
                else
                    graphics.FillRectangle(grayBrush, seat.seatRectangle);

                // Draw the border for the seat
                graphics.DrawRectangle(blackPen, seat.seatRectangle);
                yCounter++;

                // If to check if there should be a new column with seats and also the gaps between
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
        // Function to draw the bus outlines/detailes
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
        // Click handler for a click in the form on a seat
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var seat in bus.seats)
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

        // React to the Button Click
        private void reserveButton_Click(object sender, EventArgs e)
        {
            foreach (var seat in bus.seats)
            {
                if (seat.selected)
                {
                    seat.reserved = true;
                    seat.selected = false;
                }
            }
            Invalidate();
        }


    }
}
