
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Roommates.Models;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
                conn.Open();
                using (SqlCommand cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = @"SELECT FirstName, LastName, RentPortoin, RoomID 
                                        FROM Roommate
                                        WHERE Id = @id
                                        LEFT JOIN ROOM r ON r.Id = RoomId";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;

                    if(reader.Read())
                    {
                        roommate = new Roommate
                        {
                            Id = id,
                            Firstname = roommate.Firstname,
                            Lastname = roommate.Lastname,
                            RentPortion = roommate.RentPortion,
                            Room = roommate.Room
                        };
                    }

                reader.Close();

                return roommate;

                }
        }


    }
}
