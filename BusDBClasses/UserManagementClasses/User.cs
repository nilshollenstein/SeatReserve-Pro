using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************
     * File:        User.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-13
	 * Version:     1.0
     * Description: This file contains the User class, which stores informations about users of the database
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-12  Nils Hollenstein   Initial creation.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

namespace BusDBClasses.UserManagementClasses
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string rolekey { get; set; }
        public User(int userid, string username, string password, string rolekey)
        {
            this.userid = userid;
            this.username = username;
            this.password = password;
            this.rolekey = rolekey;
        }
        public User( string username, string password, string rolekey)
        {
            this.username = username;
            this.password = password;
            this.rolekey = rolekey;
        }
    }
}
