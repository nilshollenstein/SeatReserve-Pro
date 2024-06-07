using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data;
using Npgsql;
using BusDBClasses;
using System.Threading.Tasks;
using System.Drawing;
/******************************************************************************
     * File:        SeatReserve_ProDBService.cs
     * Author:      Nils Hollenstein
     * Created:     2024-06-05
	 * Version:     1.0
     * Description: This file contains the SeatReserve_ProDBService class, which contains methods to initialize the database and get the content from it
     * 
     * History:
     * Date        Author             Changes
     * ----------  ----------------   ----------------------------------------------------
     * 2024-06-07  Nils Hollenstein   Initial creation.
     * 
     * License:
     * This software is provided 'as-is', without any express or implied
     * warranty. In no event will the authors be held liable for any damages
     * arising from the use of this software.
     * 
     * This file is part of the SeatReserve-Pro project.
     * 
     ******************************************************************************/

namespace SeatReserve_Pro_DBService
{
    internal class SeatReserve_ProDBService
    {
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
        public void initDB()
        {
            GenerateBusSelection();
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
        private void GenerateBusSelection()
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
        private void DeleteDBContent(NpgsqlConnection connection)
        {

            using var cmd = new NpgsqlCommand("DELETE FROM seat", connection);
            cmd.ExecuteNonQuery();
            using var cmd2 = new NpgsqlCommand("DELETE FROM bus", connection);
            cmd2.ExecuteNonQuery();


        }
        private void InsertBusData(NpgsqlConnection connection)
        {
            if (busses.Count > 0)
            {
                foreach (var bus in busses)
                {
                    using var cmd = new NpgsqlCommand("INSERT INTO bus(busID, destination, seatcount) VALUES (@p1, @p2, @p3)", connection)
                    {
                        Parameters =
                            {
                                new("p1", bus.id),
                                new("p2", bus.destination),
                                new("p3", bus.seatCount),
                            }
                    };
                    cmd.ExecuteNonQuery();

                }
            }
        }
        private void InsertSeatData(NpgsqlConnection connection)
        {
            foreach (var bus in busses)
            {
                foreach (var seat in bus.seats)
                {
                    using var cmd = new NpgsqlCommand("INSERT INTO seat(width, height, reserved, busID) VALUES (@p1, @p2, @p3, @p4)", connection)
                    {
                        Parameters =
                            {
                                new("p1", seat.width),
                                new("p2", seat.height),
                                new("p3", seat.reserved),
                                new("p4", bus.id),
                            }
                    };
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}