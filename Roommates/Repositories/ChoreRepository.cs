using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using Roommates.Models;

namespace Roommates.Repositories
{
    class ChoreRepository : BaseRepository
    {
        public ChoreRepository(string connectionString) : base(connectionString) { }

        public List<RoommateChore> GetAll()
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, RoommateId, ChoreId FROM RoommateChore";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<RoommateChore> chores = new List<RoommateChore>();

                    while(reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);
                        
                        int roommateIdColumnPosition = reader.GetOrdinal("RoommateId");
                        int roommateIdValue = reader.GetInt32(roommateIdColumnPosition);

                        int choreIdColumnPosition = reader.GetOrdinal("ChoreId");
                        int choreIdValue = reader.GetInt32(choreIdColumnPosition);

                        RoommateChore chore = new RoommateChore
                        {
                            Id = idValue,
                            RoommateId = roommateIdValue,
                            ChoreId = choreIdValue
                        };

                        chores.Add(chore);
                    }

                    reader.Close();

                    return chores;
                }
            }
        }

        public RoommateChore GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT RoommateId, ChoreId FROM RoommateChore WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    RoommateChore chore = null;

                    if (reader.Read())
                    {
                        chore = new RoommateChore
                        {
                            Id = id,
                            RoommateId = reader.GetInt32(reader.GetOrdinal("RoommateId")),
                            ChoreId = reader.GetInt32(reader.GetOrdinal("ChoreId"))
                        };
                    }

                    reader.Close();

                    return chore;
                }
            }
        }

        public void Insert(RoommateChore newChore)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO RoommateChore (RoommateId, ChoreId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@RoommateId, @ChoreId)";
                    cmd.Parameters.AddWithValue("@RoommateId", newChore.RoommateId);
                    cmd.Parameters.AddWithValue("@ChoreId", newChore.ChoreId);
                    int id = (int)cmd.ExecuteScalar();

                    newChore.Id = id;
                }
            }
        }
    }
}