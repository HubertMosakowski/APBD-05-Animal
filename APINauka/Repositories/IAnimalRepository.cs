using APINauka.Models;
using APINauka.Models.DTOs;

namespace APINauka.Repositories;

public interface IAnimalRepository
{
    public Task<List<Animal>> getAnimals();

    public Task postAnimal(AnimalDTO animalDto);

    public void setConfig(IConfiguration configuration);
}