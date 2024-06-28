/******************************************************************************
     * File:        User.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-13
	 * Version:     1.0.1
     * Description: This file contains the User class, which stores informations about users of the database
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-12  Nils Hollenstein   Initial creation.
     * 2024-06-13  Nils Hollenstein   Modified the class
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

namespace SeatReserveLibrary.UserClasses
{
    public class User
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        // Constructor to fill data into user
        public User(int userid, string username, string password, bool admin)
        {
            Userid = userid;
            Username = username;
            Password = password;
            Admin = admin;
        }
        // Constructor to create new user (has no id at the moment)
        public User(string username, string password, bool admin)
        {
            Username = username;
            Password = password;
            Admin = admin;
        }
        // Constructor to create invalid user
        public User()
        {
            Userid = -1;
        }
    }
}
