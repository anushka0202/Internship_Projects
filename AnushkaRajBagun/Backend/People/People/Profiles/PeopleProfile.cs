using AutoMapper;
using People.Models;
using PEOPLE.Dtos;

namespace PEOPLE.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            //Source -> Target
            CreateMap<Person, PersonReadDto>();
            CreateMap<PersonCreateDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, PersonUpdateDto>();
        }

    }
}
