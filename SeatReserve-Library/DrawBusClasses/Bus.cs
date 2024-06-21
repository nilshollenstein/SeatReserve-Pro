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
     * 2024-06-12  Nils Hollenstein   Added second constructor
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the BusDBClasses project.
     * 
     ******************************************************************************/

namespace SeatReserveLibrary.DrawBusClasses
{
    public class Bus
    {
        public int Id { get; set; }
        public List<Seat> Seats { get; set; }
        public string? Destination { get; set; }
        public int SeatCount { get; set; }

        // Constructor to create the seats with the bus
        public Bus(int id, int seatCount, string destination)
        {
            Id = id;
            Destination = destination;
            SeatCount = seatCount;
            Seats = new List<Seat>();
            while (seatCount % 4 != 0)
            {
                seatCount++;
            }
            for (int i = 0; i < seatCount; i++)
            {
                var seat = new Seat();
                if (seat != null)
                {
                    Seats.Add(seat);
                }
            }
        }

        // Constructor to give the seats to the bus
        public Bus(int id, string destination, int seatCount, List<Seat> seats)
        {
            Id = id;
            Destination = destination;
            SeatCount = seatCount;
            Seats = seats;
        }
        public Bus() { }
    }
}