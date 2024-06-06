using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************
     * File:        Bus.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file contains the Bus class, which stores the informations needed to display a bus
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

namespace SeatReserve_Pro.BusClasses
{
    internal class Bus
    {
        public List<Seat> seats { get; set; }
        public int seatCount { get; set; }
        private int SeatId = 0;
        public Bus(int seatCount)
        {
            this.seatCount = seatCount;
            if (seatCount % 4 != 0)
            {
                while (seatCount % 4 != 0)
                {
                    seatCount++;
                }
                
seats = new List<Seat>();
            }seats = new List<Seat>();
            for (int i = 0; i < seatCount; i++)
            {
                Seat seat = new Seat();
                if (seat != null)
                {
                    seats.Add(seat);
                }
            }
        }
    }
}
