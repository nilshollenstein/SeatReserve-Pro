using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************
     * File:        Bus.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0.1
     * Description: This file contains the Bus class, which stores the informations needed to display a bus
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 2024-06-07  Nils Hollenstein   Added id and removed the seat-id giver.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the BusDBClasses project.
     * 
     ******************************************************************************/

namespace BusDBClasses.DrawBusClasses
{
    public class Bus
    {
        public int id { get; set; }
        public List<Seat> seats { get; set; }
        public string destination { get; set; }
        public int seatCount { get; set; }

        // Constructor to create the seats with the bus
        public Bus(int id, int seatCount, string destination)
        {
            this.id = id;
            this.destination = destination;
            this.seatCount = seatCount;
            seats = new List<Seat>();
            for (int i = 0; i < seatCount; i++)
            {
                Seat seat = new Seat();
                if (seat != null)
                {
                    seats.Add(seat);
                }
            }
        }

        // Constructor to give the seats
        public Bus(int id, string destination, int seatCount, List<Seat> seats)
        {
            this.id = id;
            this.destination = destination;
            this.seatCount = seatCount;
            this.seats = seats;
        }
    }
}