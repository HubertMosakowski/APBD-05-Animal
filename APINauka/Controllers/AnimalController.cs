using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Web.Http;
using System.Web.Http.Results;
using APINauka.Models.DTOs;
using APINauka.Repositories;

namespace APINauka.Controllers;

[ApiController]
[System.Web.Http.Route("api/animals")]
public class AnimalController: Controller
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IConfiguration _config;
    public AnimalController(IConfiguration configuration, IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
        _config = configuration;
        _animalRepository.setConfig(_config);
    }

    [Microsoft.AspNetCore.Mvc.HttpGet("get")]
    public async Task<ActionResult> GetAnimals()
    {
        return Ok(await _animalRepository.getAnimals());
    }

    [Microsoft.AspNetCore.Mvc.HttpPost("post")]
    public async Task<ActionResult> PostAnimal(AnimalDTO animalDto)
    {
        await _animalRepository.postAnimal(animalDto);
        return Created();
    }
}