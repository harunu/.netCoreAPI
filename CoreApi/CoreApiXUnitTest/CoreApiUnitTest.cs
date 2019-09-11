using System;
using Xunit;
using Microsoft.AspNetCore;
using CoreApi.Controllers;
using Microsoft.Extensions.Options;
using CoreApi;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreApiXUnitTest
{
    
    public class CoreApiUnitTest 
    {
        IOptions<AppSettings> someOptions = Options.Create(new AppSettings());
        string path = @"C:\\Projects\\CoreApi\\CoreApi\\hotelsrates.json";
         public const string expected = "{}";


        /*Correct Input*/
        [Fact]
        public void JSonObjectReturnsExpected()
        {
            // Arrange
            int id = 7294;
            string arrivalDate = "2016-03-15";
            someOptions.Value.JsonFile = path;
            ValuesController controller = new ValuesController(someOptions);
            //Act
            var result =  controller.Get(id, arrivalDate);
            //Assert
            Assert.NotEmpty(result.Value.ToString());
            Assert.Contains("targetDay", result.Value.ToString());
        }

        /*Both Id and arrival Date Wrong Output*/
        [Fact]
        public void JSonObjectReturnsEmpty()
        {
            // Arrange
            int id = 7293;
            string arrivalDate = "2016-03-16";
            someOptions.Value.JsonFile = path;
            ValuesController controller = new ValuesController(someOptions);
            //Act
            var result = controller.Get(id, arrivalDate);

            //Assert
            Assert.Equal(expected, result.Value.ToString());
        }

        /* Id  Wrong Output*/
        [Fact]
        public void IDWrong()
        {
            // Arrange
            int id = 7293;
            string arrivalDate = "2016-03-15";
            someOptions.Value.JsonFile = path;
            ValuesController controller = new ValuesController(someOptions);


            //Act
            var result = controller.Get(id, arrivalDate);
          //  var players = JsonConvert.DeserializeObject<ResponseClass>>(result);
            //Assert
            Assert.Equal(expected, result.Value.ToString());
        }


        /*arrival Date Wrong Output*/
        [Fact]
        public void arrivalDateWrong()
        {
            // Arrange
            int id = 7294;
            string arrivalDate = "2020-03-16";
            someOptions.Value.JsonFile = path;
            ValuesController controller = new ValuesController(someOptions);
            //Act
            var result = controller.Get(id, arrivalDate);

            //Assert
            Assert.DoesNotContain("targetDay", result.Value.ToString());
        }
    }

 














}
