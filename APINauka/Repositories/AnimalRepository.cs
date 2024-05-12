using APINauka.Controllers;
using APINauka.Models;
using APINauka.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace APINauka.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private List<Animal> lista = new();

    private IConfiguration _config;
    
    public AnimalRepository()
    {
        
    }

    public void setConfig(IConfiguration configuration)
    {
        _config = configuration;
    }
    public async Task<List<Animal>> getAnimals()
    {
        List<Animal> animalList = new(); 
        
        using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
        {
            string insertQuery = @"SELECT * FROM Animal";
        
            SqlCommand command = new SqlCommand(insertQuery , connection);
            
            await connection.OpenAsync();
            
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                
                while (reader.Read())
                {
                    Animal animal = new Animal();
                    animal.IdAnimal = (int)reader["IdAnimal"];
                    animal.Name = reader["Name"].ToString();
                    animal.Category = reader["Category"].ToString();
                    animal.Description = reader["Description"].ToString();
                    animal.Area = reader["Area"].ToString();
                    
                    animalList.Add(animal);
                }
            }
        }

        return animalList;
    }

    public async Task postAnimal(AnimalDTO animalDto)
    {
        //List<string> genres = new List<string>();
        using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
        {
            string insertQuery = @"INSERT INTO Animal (Name, Description, Category, Area) VALUES
                                                      (@Name, @Description, @Category, @Area);";
        
            SqlCommand command = new SqlCommand(insertQuery , connection);
            command.Parameters.AddWithValue("@Name", animalDto.Name);
            command.Parameters.AddWithValue("@Description", animalDto.Description);
            command.Parameters.AddWithValue("@Category", animalDto.Category);
            command.Parameters.AddWithValue("@Area", animalDto.Area);
        
            await connection.OpenAsync();

            animalDto.Name = Convert.ToString(command.ExecuteScalarAsync());
            animalDto.Description = Convert.ToString(command.ExecuteScalarAsync());
            animalDto.Category = Convert.ToString(command.ExecuteScalarAsync());
            animalDto.Area = Convert.ToString(command.ExecuteScalarAsync());
            
            // using (SqlDataReader reader = await command.ExecuteReaderAsync())
            // {
            //     while (reader.Read())
            //     {
            //         string genreName = reader["name"].ToString();
            //         genres.Add(genreName);
            //     }
            // }
        }

        // var animal = new Animal();
        // animal.Name = animalDto.Name;
        // animal.IdAnimal = lista.Count;
        // animal.Area = animalDto.Area;
        // animal.Category = animalDto.Category;
        // animal.Description = animalDto.Description;
        //
        // lista.Add(animal);
        
        
        // Connection string do pjatkowej bazy
        //Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=2019SBD/s28829;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False
    }
}