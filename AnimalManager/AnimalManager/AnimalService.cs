using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalManager.Models;
using System.Data.SQLite;
using System;
using AnimalManager.Services;

namespace AnimalManager
{
    namespace AnimalManager.Services
    {
        public class AnimalService
        {
            // Método para listar todos os animais
            public static List<Animal> ListarAnimais()
            {
                var animais = new List<Animal>();

                using (var connection = DatabaseService.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Animal";
                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            animais.Add(new Animal
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Idade = reader.GetInt32(2),
                                Especie = reader.GetString(3),
                                DataAdocao = reader.IsDBNull(4) ? (DateTime?)null : DateTime.Parse(reader.GetString(4))
                            });
                        }
                    }
                }

                return animais;
            }


            public static void AddAnimal(Animal animal)
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    connection.Open();
                    string query = @"
                    INSERT INTO Animal (Nome, Idade, Especie, DataAdocao) 
                    VALUES (@Nome, @Idade, @Especie, @DataAdocao)";

                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", animal.Nome);
                        command.Parameters.AddWithValue("@Idade", animal.Idade);
                        command.Parameters.AddWithValue("@Especie", animal.Especie);
                        command.Parameters.AddWithValue("@DataAdocao", animal.DataAdocao?.ToString("yyyy-MM-dd"));

                        command.ExecuteNonQuery();
                    }
                }
            }

            // Método para buscar um animal por ID
            public static Animal BuscarAnimalPorId(int id)
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Animal WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Animal
                                {
                                    Id = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    Idade = reader.GetInt32(2),
                                    Especie = reader.GetString(3),
                                    DataAdocao = reader.IsDBNull(4) ? (DateTime?)null : DateTime.Parse(reader.GetString(4))
                                };
                            }
                        }
                    }
                }
                return null;
            }

            // Método para atualizar um animal
            public static void AtualizarAnimal(Animal animal)
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    connection.Open();
                    string query = @"
                    UPDATE Animal 
                    SET Nome = @Nome, Idade = @Idade, Especie = @Especie, DataAdocao = @DataAdocao 
                    WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", animal.Nome);
                        command.Parameters.AddWithValue("@Idade", animal.Idade);
                        command.Parameters.AddWithValue("@Especie", animal.Especie);
                        command.Parameters.AddWithValue("@DataAdocao", animal.DataAdocao?.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Id", animal.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }

            // Método para deletar um animal por ID
            public static void DeletarAnimal(int id)
            {
                using (var connection = DatabaseService.GetConnection())
                {
                    connection.Open();

                    // Verifica se o ID existe
                    string checkQuery = "SELECT COUNT(*) FROM Animal WHERE Id = @Id";
                    using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Id", id);
                        long count = (long)checkCommand.ExecuteScalar();

                        if (count == 0)
                        {
                            Console.WriteLine("Erro: O animal com o ID fornecido não existe.");
                            return;
                        }
                    }

                    // Deleta o registro se ele existe
                    string deleteQuery = "DELETE FROM Animal WHERE Id = @Id";
                    using (var deleteCommand = new SQLiteCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@Id", id);
                        deleteCommand.ExecuteNonQuery();
                    }
                    Console.WriteLine("Animal deletado com sucesso!");
                }
            }
        }
    }
}
