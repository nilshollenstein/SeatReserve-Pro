using BusDBClasses.DrawBusClasses;
using Npgsql;

namespace BusDBClasses.DBOperations
{
    internal class DBOperations
    {
        //  Variables
        private List<Bus> busses = new List<Bus>();
        private string connectionString = "Host=10.80.4.136:5432;Username=postgres;Password=postgres;Database=SeatReserve-Pro";
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



        // Method to read the whole Database
        public List<Bus> ReadBusPartsDB()
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
        // Method to update the whole database
        public void UpdateBusPartsDB(Bus bus)
        {
            using (var dataSource = NpgsqlDataSource.Create(connectionString))
            {
                using (var connection = dataSource.OpenConnection())
                {
                    UpdateSeats(bus, connection);
                }
            }
        }
        // Method to generate the busses for the database

        // Method to read all the data from the database
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
            int reservedByUser = 0;

            for (int i = 1; i <= targetDestinations.Count; i++)
            {
                List<Seat> seats = new List<Seat>();
                // Read the busses
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
                // Check that seatCount is dividable by 4
                while (seatCount % 4 != 0)
                {
                    seatCount++;
                }


                // Read the whole data of the seats that belong to the correct bus
                for (; j <= seatCount + previousSeatCounts; j++)
                {
                    using (var selectAllSeatInformation = new NpgsqlCommand("SELECT seat.seatid, seat.width, seat.height, seat.reserved, seat.reservedbyuser FROM seat JOIN bus ON bus.busid = seat.busid WHERE bus.busid = :i AND seat.seatid = :j;", connection))
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
                                // If the database has a null value in this column, the variable gets the value -1
                                if (!reader.IsDBNull(4))
                                    reservedByUser = reader.GetInt32(4);
                                else
                                    reservedByUser = -1;
                            }
                        }
                    }

                    seats.Add(new Seat(seatid, width, height, reserved, busID, reservedByUser));
                }
                previousSeatCounts += seatCount;
                busses.Add(new Bus(busID, destination, seatCount, seats));
            }
        }
        // Method to update the seats
        public void UpdateSeats(Bus bus, NpgsqlConnection connection)
        {
            // Rewrite all the data of the current bus into the database
            foreach (var seat in bus.seats)
            {
                // Insert correct user id
                if (seat.reservedBy != -1)
                {
                    using var cmd = new NpgsqlCommand("UPDATE seat SET seatid = @p1 ,width = @p2, height = @p3, reserved = @p4, busid = @p5, reservedbyuser = @p6 FROM bus WHERE seat.seatid = @p1 AND bus.busid = @p7", connection)
                    {
                        Parameters =
                            {
                                new("p1", seat.id),
                                new("p2", seat.width),
                                new("p3", seat.height),
                                new("p4", seat.reserved),
                                new("p5", seat.busid),
                                new("p6", seat.reservedBy),
                                new("p7", bus.id),
                            }
                    };
                    cmd.ExecuteNonQuery();
                }
                // Insert Null value if userid is -1
                else
                {
                    using var cmd = new NpgsqlCommand("UPDATE seat SET seatid = @p1 ,width = @p2, height = @p3, reserved = @p4, busid = @p5, reservedbyuser = @p6 FROM bus WHERE seat.seatid = @p1 AND bus.busid = @p7 ", connection)
                    {
                        Parameters =
                            {
                                new("p1", seat.id),
                                new("p2", seat.width),
                                new("p3", seat.height),
                                new("p4", seat.reserved),
                                new("p5", seat.busid),
                                new("p6", DBNull.Value),
                                new("p7", bus.id),
                            }
                    };
                    cmd.ExecuteNonQuery();
                }
            }
        }
        // Method to get the count of all users
        public int GetUserCount(NpgsqlConnection connection)
        {
            int usercount = 0;
            using (var selectAllSeatInformation = new NpgsqlCommand("SELECT COUNT(userid) FROM users;", connection))
            {

                using (var reader = selectAllSeatInformation.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usercount = reader.GetInt32(0);
                    }
                }
            }
            return usercount;
        }
        // Insert single user
        public void InsertUserData(BusDBClasses.UserManagementClasses.User user)
        {
            using (var dataSource = NpgsqlDataSource.Create(connectionString))
            {
                using (var connection = dataSource.OpenConnection())
                {
                    int usercount = GetUserCount(connection);
                    int newID = usercount++;
                    using var cmd = new NpgsqlCommand("INSERT INTO users (userid, username, password, admin) VALUES (@p1, @p2, @p3, @p4) ", connection)
                    {
                        Parameters =
                    {
                        new("p1", newID),
                        new("p2", user.username),
                        new("p3", user.password),
                        new("p4", user.admin),
                    }
                    };
                    cmd.ExecuteNonQuery();
                }
            }

        }
        // Reads all users from the database
        public List<BusDBClasses.UserManagementClasses.User> ReadUserData()
        {
            int userid = 0;
            string username = "";
            string password = "";
            bool admin = false;
            List<BusDBClasses.UserManagementClasses.User> users = new List<BusDBClasses.UserManagementClasses.User>();
            using (var dataSource = NpgsqlDataSource.Create(connectionString))
            {
                using (var connection = dataSource.OpenConnection())
                {
                    int usercount = GetUserCount(connection);

                    for (int i = 0; i < usercount; i++)
                    {
                        using (var selectAllSeatInformation = new NpgsqlCommand("SELECT userid, username, password, admin FROM users WHERE userid = :i;", connection))
                        {
                            selectAllSeatInformation.Parameters.AddWithValue("i", i);

                            using (var reader = selectAllSeatInformation.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    userid = reader.GetInt32(0);
                                    username = reader.GetString(1);
                                    password = reader.GetString(2);
                                    admin = reader.GetBoolean(3);
                                }
                            }
                        }
                        users.Add(new BusDBClasses.UserManagementClasses.User(userid, username, password, admin));
                    }
                }
            }
            return users;
        }
    }
}
