using DataAccess.Interfaces;
using Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess.PostgreSQL
{
    public class ContactsDBContext : ICustomerDBContext
    {
        private string _connectionString;

        public ContactsDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Contact info)
        {
            string query = @"insert into public.contacts(Name,Surname,TelephoneNumber,DateOfBirth,EmailAddress)values(@Name,@Surname,@TelephoneNumber,@DateOfBirth,@EmailAddress)";

            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                _connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);


                cmd.Parameters.AddWithValue("Name", info.Name);
                cmd.Parameters.AddWithValue("Surname", info.Surname);
                cmd.Parameters.AddWithValue("TelephoneNumber", info.TelephoneNumber);
                cmd.Parameters.AddWithValue("EmailAddress", info.EmailAddress);
                cmd.Parameters.AddWithValue("DateOfBirth", info.DateOfBirth);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }

        }

        public void Delete(int Id)
        {
            string query = $"delete from contacts where id = {Id}";
            using (var _connection = new NpgsqlConnection(_connectionString))
            {                
                NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void Edit(Contact info)
        {
            string query = $"update public.contacts set Name = @Name,Surname = @Surname,TelephoneNumber = @TelephoneNumber, EmailAddress = @EmailAddress,DateOfBirth = @DateOfBirth where Id = {info.Id}";
            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                _connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);

                cmd.Parameters.AddWithValue("Name", info.Name);
                cmd.Parameters.AddWithValue("Surname", info.Surname);
                cmd.Parameters.AddWithValue("TelephoneNumber", info.TelephoneNumber);
                cmd.Parameters.AddWithValue("EmailAddress", info.EmailAddress);
                cmd.Parameters.AddWithValue("DateOfBirth", info.DateOfBirth);
               
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public List<Contact> List(int skip = 0, int take = 10, string orderColumn = "", string ascDesc = "")
        {
            string query = $"select * from public.contacts";
            if (!string.IsNullOrEmpty(orderColumn))
            {
                query += $" order by {orderColumn}";
            }
            if (!string.IsNullOrEmpty(ascDesc))
            {
                query += $" {ascDesc}";
            }
            query += $" limit {take} offset {skip}";
            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                _connection.Open();

                var Contacts = new List<Contact>();

                using (NpgsqlCommand command = new NpgsqlCommand(query, _connection))
                {
                    //int val;
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contacts.Add(new Contact
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            TelephoneNumber = int.Parse(reader["TelephoneNumber"].ToString()),
                            EmailAddress = reader["EmailAddress"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString())
                        });
                    }
                    _connection.Close();
                }

                return Contacts;
            }
        }

        public int Count()
        {
            string query = $"select count(*) from contacts";

            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                _connection.Open();

                var count = 0;

                using (NpgsqlCommand command = new NpgsqlCommand(query, _connection))
                {
                    //int val;
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count = int.Parse(reader["Count"].ToString());
                    }
                    _connection.Close();
                }

                return count;
            }
        }

        public Contact Get(int Id)
        {
            string query = $"select * from contacts where id = {Id}";
           
            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                _connection.Open();

                var Contact = new Contact();

                using (NpgsqlCommand command = new NpgsqlCommand(query, _connection))
                {
                    //int val;
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contact = new Contact
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            TelephoneNumber = int.Parse(reader["TelephoneNumber"].ToString()),
                            EmailAddress = reader["EmailAddress"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString())
                        };
                    }
                    _connection.Close();
                }

               return Contact;
            }
        }
    }
}
