using DataAccess.Interfaces;
using Models.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccess.PostgreSQL
{
    public class CustomerDBContext : ICustomerDBContext
    {
        private string _connectionString;

        public CustomerDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Contact info)
        {
            string query = @"insert into public.customers(Name,Surname,TelephoneNumber,DateOfBirth)values(@Name,@Surname,@TelephoneNumber,@DateOfBirth)";

            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);


                cmd.Parameters.AddWithValue("Name", info.Name);
                cmd.Parameters.AddWithValue("Surname", info.Surname);
                cmd.Parameters.AddWithValue("TelephoneNumber", info.TelephoneNumber);
                cmd.Parameters.AddWithValue("EmailAddress", info.EmailAddress);
                cmd.Parameters.AddWithValue("DateOfBirth", info.DateOfBirth);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

        }

        public void Delete(int Id)
        {
            string query = $"delete from customers where id = {Id}";
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
            string query = @"update public.customers set Name = @Name,Surname = @Surname,TelephoneNumber = @TelephoneNumber, EmailAddress = @EmailAddress,DateOfBirth = @DateOfBirth where Id = @Id";
            using (var _connection = new NpgsqlConnection(_connectionString))
            {
                NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);

                cmd.Parameters.AddWithValue("Name", info.Name);
                cmd.Parameters.AddWithValue("Surname", info.Surname);
                cmd.Parameters.AddWithValue("TelephoneNumber", info.TelephoneNumber);
                cmd.Parameters.AddWithValue("EmailAddress", info.EmailAddress);
                cmd.Parameters.AddWithValue("DateOfBirth", info.DateOfBirth);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Contact> List(int skip = 0, int take = 10, string orderColumn = "", string ascDesc = "")
        {
            string query = $"select * from public.customers";
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
    }
}
