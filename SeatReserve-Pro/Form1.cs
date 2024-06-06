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

namespace SeatReserve_Pro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        

        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int firstWidth = Width;
            int firstHeight = Height;
            DrawSeats(firstWidth, firstHeight);
        }
        private void DrawSeats(int firstWidth, int firstHeight)
        {
            Graphics graphics;
            graphics = this.CreateGraphics();
            SolidBrush grayBrush = new SolidBrush(Color.Gray);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            var bus = new Bus(50);
            
            int yCounter = 0;
            
            int xPos = 80;
            int yPos = 80;
            int maxWidth = 0;
            int maxHeight = 0;
            foreach (var seat in bus.seats)
            {
                seat.width = 30;
                seat.height = 30;
                graphics.FillRectangle(grayBrush, new Rectangle(xPos, yPos, seat.width, seat.height));

                yCounter++;
                if (yCounter == 4)
                {
                    yPos = 80;
                    xPos += seat.width + 10;
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
                    graphics.FillRectangle(blackBrush, xPos, yPos-10, seat.width, 10);

                }
                maxHeight = Math.Max(maxHeight,yPos + seat.height);
            }
            
            DrawBus(80, 80, graphics,maxWidth -80, maxHeight-80);
            grayBrush.Dispose();
            graphics.Dispose();
        }
        private void DrawBus(int startSeatXpos, int startSeatYpos,Graphics graphics, int totalWidth, int totalHeight)
        {
            
            Pen blackPen = new Pen(Color.Black);
            SolidBrush greenBrush = new SolidBrush(Color.FromArgb(255, 82, 83, 84));


            int xPos = startSeatXpos ;
            int yPos = startSeatYpos;
            Rectangle rect = new Rectangle(xPos, yPos, totalWidth + 60, totalHeight );
            Rectangle rectDriverSeat = new Rectangle(totalWidth + 70, yPos + 20, 30, 30);

            graphics.DrawLine(blackPen, totalWidth + 60, yPos, totalWidth + 60, yPos + 90);
            graphics.DrawLine(blackPen, totalWidth + 80, yPos + 90, totalWidth + 140, yPos + 90);
            graphics.FillRectangle(greenBrush,rectDriverSeat);
            graphics.DrawRectangle(blackPen,rectDriverSeat);

            graphics.DrawRectangle(blackPen,rect);
            

        }
    }
}
