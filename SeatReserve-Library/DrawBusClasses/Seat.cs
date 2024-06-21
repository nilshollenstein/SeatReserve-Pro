using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/******************************************************************************
     * File:        Seat.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file contains the Seat class, which stores the informations to display a seat
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-05  Nils Hollenstein   Initial creation.
     * 2024-06-12  Nils Hollenstein   Added second constructor and reserveBy attribute
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
    public class Seat
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle SeatRectangle { get; set; }
        public bool Reserved { get; set; }
        public bool Selected { get; set; }
        public int Busid { get; set; }
        public int ReservedBy { get; set; }
        public Seat()
        {
            Selected = false;
            Width = 30;
            Height = 30;
        }
        public Seat(int id, int width, int height, bool reserved, int busid, int reserveByUser)
        {
            Id = id;
            Width = width;
            Height = height;
            Reserved = reserved;
            Selected = false;
            Busid = busid;
            ReservedBy = reserveByUser;
        }
    }
}
