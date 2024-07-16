﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Projekt.Models;

namespace Projekt.Data
{
    public class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Projekt.Properties.Settings.PersonDBConnectionString"].ConnectionString;
        }

        public IEnumerable<Person> GetPersons()
        {
            var persons = new List<Person>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Persons", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            persons.Add(new Person
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                Age = reader.GetInt32(3),
                                Gender = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return persons;
        }

        public void AddPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Persons (Name, Surname, Age, Gender) VALUES (@Name, @Surname, @Age, @Gender)", connection))
                {
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@Surname", person.Surname);
                    command.Parameters.AddWithValue("@Age", person.Age);
                    command.Parameters.AddWithValue("@Gender", person.Gender);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePerson(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Persons WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
