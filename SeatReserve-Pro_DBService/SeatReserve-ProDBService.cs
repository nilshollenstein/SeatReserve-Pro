using Npgsql;
using SeatReserveLibrary.DrawBusClasses;

/******************************************************************************
     * File:        SeatReserve_ProDBService.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.2
     * Description: This file contains the SeatReserve_ProDBService class, which contains methods to initialize the database
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-07  Nils Hollenstein   Initial creation, able to insert Data to DB
     * 2024-06-12  Nils Hollenstein   Able to read from DB and to write to DB
     * 2024-06-12  Nils Hollenstein   Basic operations with User-Table possible
     * 2024-06-13  Nils Hollenstein   Seat-Table insert modified
     * 2024-06-19  Nils Hollenstein   Seat-Table modified
     * 2024-06-19  Nils Hollenstein   Moved everything thats not needed for the init method to another file
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro_DBService project.
     * 
     ******************************************************************************/

namespace SeatReserve_Pro_DBService
{
    public class SeatReserve_ProDBServiceR
    {
        //  Variables
        private List<Bus> busses = new List<Bus>();
        private string connectionString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=SeatReserve-Pro";
        List<string> targetDestinations = new List<string>
        {
            "Berlin, Deutschland",
            "Prag, Tschechien",
            "Wien, Österreich",
            "Budapest, Ungarn",
            "Krakau, Polen",
            "Amsterdam, Niederlande",
            "Brüssel, Belgien",
            "Paris, Frankreich",
            "Zürich, Schweiz",
            "München, Deutschland",
            "Moskau, Russland"
        };

        // Methods

        // Method to initialize the Database
        public void InitDB()
        {
            GenerateBusList();
            using (var dataSource = NpgsqlDataSource.Create(connectionString))
            {
                using (var connection = dataSource.OpenConnection())
                {
                    DeleteDBContent(connection);
                    InsertBusData(connection);
                    InsertSeatData(connection);
                }
            }
        }
        // Method to delete the whole data in the database
        private void DeleteDBContent(NpgsqlConnection connection)
        {
            using var cmd = new NpgsqlCommand("DELETE FROM seat", connection);
            cmd.ExecuteNonQuery();
            using var cmd2 = new NpgsqlCommand("DELETE FROM bus", connection);
            cmd2.ExecuteNonQuery();
        }
        // Method to insert the bus data into the db
        private void InsertBusData(NpgsqlConnection connection)
        {
            if (busses.Count > 0)
            {
                foreach (var bus in busses)
                {
                    using var cmd = new NpgsqlCommand("INSERT INTO bus (busid, destination, seatcount) VALUES (@p1, @p2, @p3)", connection)
                    {
                        Parameters =
                            {
                                new("p1", bus.Id),
                                new("p2", bus.Destination),
                                new("p3", bus.SeatCount),
                            }
                    };
                    cmd.ExecuteNonQuery();

                }
            }
        }
        // Method to insert the seats to the database
        private void InsertSeatData(NpgsqlConnection connection)
        {
            int seatID = 1;
            foreach (var bus in busses)
            {
                foreach (var seat in bus.Seats)
                {

                    using var cmd = new NpgsqlCommand("INSERT INTO seat(seatid, width, height, reserved, busID) VALUES (@p0, @p1, @p2, @p3, @p4)", connection)
                    {
                        Parameters =
                            {
                                new("p0", seatID),
                                new("p1", seat.Width),
                                new("p2", seat.Height),
                                new("p3", seat.Reserved),
                                new("p4", bus.Id),
                            }
                    };
                    cmd.ExecuteNonQuery();
                    seatID++;
                }
            }
        }
        // Method to generate the busses for the database
        private void GenerateBusList()
        {
            int busID = 1;
            foreach (var target in targetDestinations)
            {
                var random = new Random();
                int randomNumber = random.Next(20, 40);
                busses.Add(new Bus(busID, randomNumber, target));
                busID++;
            }
        }

    }
}