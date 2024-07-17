using System;
using System.Collections.Generic;
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
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["Projekt.Properties.Settings.PersonDBConnectionString"]?.ConnectionString;

                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new InvalidOperationException("Connection string 'PersonDB' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing PersonRepository: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<Person> GetPersons()
        {
            var persons = new List<Person>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Person", connection)) // Updated table name
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving persons: {ex.Message}");
                throw;
            }

            return persons;
        }

        public void AddPerson(Person person)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("INSERT INTO dbo.Person (Name, Surname, Age, Gender) VALUES (@Name, @Surname, @Age, @Gender)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", person.Name);
                        command.Parameters.AddWithValue("@Surname", person.Surname);
                        command.Parameters.AddWithValue("@Age", person.Age);
                        command.Parameters.AddWithValue("@Gender", person.Gender);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding person: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DELETE FROM dbo.Person WHERE Id = @Id", connection)) // Updated table name
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting person: {ex.Message}");
                throw;
            }
        }
    }
}
