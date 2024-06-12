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
using System.Drawing.Text;
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
     * 2024-06-07  Nils Hollenstein   Initial creation, able to insert Data to DB
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
    public class SeatReserve_ProDBService
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
        public List<Bus> readDB()
        {
            using (var dataSource = NpgsqlDataSource.Create(connectionString))
            {
                using (var connection = dataSource.OpenConnection())
                {
                    ReadBusData(connection);
                }
            }
            return busses;
        }
        public void updateDB(List<Bus> busses)
        {
            using (var dataSource = NpgsqlDataSource.Create(connectionString))
            {
                using (var connection = dataSource.OpenConnection())
                {
                    updateSeats(busses, connection);
                }
            }
        }
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
                    using var cmd = new NpgsqlCommand("INSERT INTO bus (busid, destination, seatcount) VALUES (@p1, @p2, @p3)", connection)
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
            int seatID = 1;
            foreach (var bus in busses)
            {
                foreach (var seat in bus.seats)
                {

                    using var cmd = new NpgsqlCommand("INSERT INTO seat(seatid, width, height, reserved, busID) VALUES (@p0, @p1, @p2, @p3, @p4)", connection)
                    {
                        Parameters =
                            {
                                new("p0", seatID),
                                new("p1", seat.width),
                                new("p2", seat.height),
                                new("p3", seat.reserved),
                                new("p4", bus.id),
                            }
                    };
                    cmd.ExecuteNonQuery();
                    seatID++;
                }
            }
        }

        private void ReadBusData(NpgsqlConnection connection)
        {
            busses = new List<Bus>();
            int busID = 0;
            string destination = "";
            int seatCount = 0;
            int seatid = 0;
            int width = 0;
            int height = 0;
            bool reserved = false;
            int previousSeatCounts = 0;
            int j = 1 + previousSeatCounts;

            for (int i = 1; i <= targetDestinations.Count; i++)
            {
                List<Seat> seats = new List<Seat>();
                // Busse lesen
                using (var selectAllBusInformation = new NpgsqlCommand("SELECT bus.busid, bus.destination, bus.seatcount FROM bus WHERE bus.busid = :i;", connection))
                {
                    selectAllBusInformation.Parameters.AddWithValue("i", i);
                    using (var reader = selectAllBusInformation.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            busID = reader.GetInt32(0);
                            destination = reader.GetString(1);
                            seatCount = reader.GetInt32(2);
                        }
                    }
                }
                while (seatCount % 4 != 0)
                {
                    seatCount++;
                }
                previousSeatCounts += seatCount;
                for (; j <= seatCount + previousSeatCounts; j++)
                {
                    using (var selectAllSeatInformation = new NpgsqlCommand("SELECT seat.seatid, seat.width, seat.height, seat.reserved FROM seat JOIN bus ON bus.busid = seat.busid WHERE bus.busid = :i AND seat.seatid = :j;", connection))
                    {
                        selectAllSeatInformation.Parameters.AddWithValue("i", i);
                        selectAllSeatInformation.Parameters.AddWithValue("j", j);
                        using (var reader = selectAllSeatInformation.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                seatid = reader.GetInt32(0);
                                width = reader.GetInt32(1);
                                height = reader.GetInt32(2);
                                reserved = reader.GetBoolean(3);
                            }
                        }
                    }
                    seats.Add(new Seat(seatid, width, height, reserved, busID));
                }
                busses.Add(new Bus(busID, destination, seatCount, seats));
            }
        }
        public void updateSeats(List<Bus> busses, NpgsqlConnection connection)
        {

            foreach (var bus in busses)
            {
                foreach (var seat in bus.seats)
                {
                    using var cmd = new NpgsqlCommand("UPDATE seat SET seatid = @p1 ,width = @p2, height = @p3, reserved = @p4, busid = @p5 WHERE seatid = @p1 AND busid = @p5", connection)
                    {
                        Parameters =
                            {
                                new("p1", seat.id),
                                new("p2", seat.width),
                                new("p3", seat.height),
                                new("p4", seat.reserved),
                                new("p5", seat.busid),
                            }
                    };
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}