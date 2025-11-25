using System;
using SensorSim.Models;

namespace SensorSim.Test

{
    public class SensorTest
    {

        [Fact]
        public void CreateNewSensor()
        {
            //Arrange
            var newSensor = new Sensor(5, "Kitchen", "Auckland", 15, 21);

            //Assert
            Assert.Equal(5, newSensor.Id);
            Assert.Equal("Kitchen", newSensor.Name);
            Assert.Equal("Auckland", newSensor.Location);
            Assert.Equal(15, newSensor.MinTemp);
            Assert.Equal(21, newSensor.MaxTemp);

        }

        [Fact]
        public void RetrieveObjectFromFile()
        {
            //Arrange
            var sensor = Sensor.InitializeSensor();

            //Assert
            Assert.Equal(1, sensor.Id);
            Assert.Equal("Sensor 1", sensor.Name);
            Assert.Equal("Wellington", sensor.Location);
            Assert.Equal(22, sensor.MinTemp);
            Assert.Equal(24, sensor.MaxTemp);

        }

        [Fact]
        public void ValidateObjectFailure()
        {
            //Arrange
            var sensor = new Sensor(0, " ", " ", 15, 12);

            //Act
            bool valid = Sensor.IsValid(sensor);

            //Assert
            Assert.False(valid);
        }

        [Fact]
        public void ValidateObjectSuccess()
        {
            //Arrange
            var sensor = new Sensor(5, "Name", "Location", 15, 22);

            //Act
            bool valid = Sensor.IsValid(sensor);

            //Assert
            Assert.True(valid);
        }

        [Fact]
        public void StringSanitized()
        {
            //Arrange
            string htmlString = "<script scr='sjdfl'>hello<body></body>";

            //Act
            string sanitized = Sensor.SanitizeString(htmlString);

            //Assert
            Assert.Equal("hello", sanitized);

        }
    }

    public class DataTests
    {

    }
}