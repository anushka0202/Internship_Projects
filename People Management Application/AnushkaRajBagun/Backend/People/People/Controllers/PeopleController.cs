using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using People.Models;
using PEOPLE.Data;
using PEOPLE.Dtos;
using System;
using System.Collections.Generic;
namespace PEOPLE.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepo _repository;
        private readonly IMapper _mapper;

        public PeopleController(IPeopleRepo repository, IMapper mapper)
        {
            
            _repository = repository;
            _mapper = mapper;
        }


        //GET api/people
        /// <summary>    
        /// Gets list of People   
        /// </summary>    
        /// <returns>List of People</returns>
        /// <response code="200">List of People</response>
        /// <response code="404">None</response>
        [HttpGet]
        public ActionResult<IEnumerable<PersonReadDto>> GetAllPeople()
        {
            var peopleList = _repository.GetAllPeople();

            return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(peopleList));
        }

        //GET api/people/{id}
        /// <summary>
        /// Gets a specific person by unique id
        /// </summary>
        /// <response code="200">Gets a specific person</response>
        /// <response code="404">None</response>
        [HttpGet("{id}", Name = "GetPersonById")]
        public ActionResult<PersonReadDto> GetPersonById(Guid id)
        {
            var singlePerson = _repository.GetPersonById(id);
            if (singlePerson != null) 
            { 
                return Ok(_mapper.Map<PersonReadDto>(singlePerson)); 
            }
            return NotFound();
        }

        //POST api/people
        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <response code="201">List of People</response>
        /// <response code="400">Example : “First Name is empty”</response>
        [HttpPost]
        public ActionResult<PersonReadDto> CreatePerson(PersonCreateDto personCreateDto)
        {
            var personModel = _mapper.Map<Person>(personCreateDto);
            _repository.CreatePerson(personModel);
            _repository.SaveChanges();

            var personReadDto = _mapper.Map<PersonReadDto>(personModel);

            return CreatedAtRoute(nameof(GetPersonById), new { Id = personReadDto.Id }, personReadDto);
        }

        //PUT api/people/{id}
        /// <summary>
        /// Updates a specific person by unique id
        /// </summary>
        /// <response code="204">None</response>
        /// <response code="404">None</response>
        /// <response code="400">Example : “First Name is empty”</response>
        [HttpPut("{id}")]
        public ActionResult UpdatePerson(Guid id, PersonUpdateDto personUpdateDto)
        {
            //Checking if we have a resourse to update from our repo which is a model
            var personModelFromRepo = _repository.GetPersonById(id);
            if (personModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(personUpdateDto, personModelFromRepo);

            _repository.UpdatePerson(personModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/people/{id}
        /// <summary>
        /// Updates a specific field of a person by unique id
        /// </summary>
        
        [HttpPatch("{id}")]
        public ActionResult PartialPersonUpdate(Guid id, JsonPatchDocument<PersonUpdateDto> patchDoc)
        {
            //Checking if we have a resourse to update from our repo which is a model
            var personModelFromRepo = _repository.GetPersonById(id);
            if (personModelFromRepo == null)
            {
                return NotFound();
            }

            //Applying the patch document and validating it
            var personToPatch = _mapper.Map<PersonUpdateDto>(personModelFromRepo);
            patchDoc.ApplyTo(personToPatch, ModelState);

            if (!TryValidateModel(personToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(personToPatch, personModelFromRepo);

            _repository.UpdatePerson(personModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/people/{id}
        /// <summary>
        /// Deletes a specific person by unique id
        /// </summary>
        /// <response code="204">None</response>
        /// <response code="404">None</response>
        
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(Guid id)
        {
            var personModelFromRepo = _repository.GetPersonById(id);
            if (personModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeletePerson(personModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
