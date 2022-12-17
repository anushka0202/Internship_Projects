using Microsoft.AspNetCore.Mvc;
using People.Models;
using PEOPLE.Controllers;
using PEOPLE.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace People.Test
{
    public class PeopleControllerTest
    {
        readonly PeopleController _controller;
        readonly MockPeopleRepo _repository;

        public PeopleControllerTest()
        {
            _repository = new MockPeopleRepo();
            _controller = new PeopleController(_repository);

        }

        [Fact]
        public void GetAllPeopleTest()
        {
            //Arrange
            //Act
            var result = _controller.GetAllPeople();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;

            Assert.IsType<List<Person>>(list.Value);



            var listBooks = list.Value as List<Person>;

            Assert.Equal(2, listBooks.Count);
        }
        
    }
}
