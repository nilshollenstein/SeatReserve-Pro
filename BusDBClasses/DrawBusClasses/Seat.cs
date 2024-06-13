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
     * 2024-06-12  Nils Hollenstein   Added second constructor and reserveByUser attribute

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
    public class Seat
    {
        public int id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Rectangle seatRectangle { get; set; }
        public bool reserved { get; set; }
        public bool selected { get; set; }
        public int busid { get; set; }
        public int reserveByUser { get; set; }
        public Seat()
        {
            selected = false;
            width = 30;
            height = 30;
        }
        public Seat(int id, int width, int height, bool reserved, int busid, int reserveByUser)
        {
            this.id = id;
            this.width = width;
            this.height = height;
            this.reserved = reserved;
            selected = false;
            this.busid = busid;
            this.reserveByUser = reserveByUser;
        }
        
    }
}
