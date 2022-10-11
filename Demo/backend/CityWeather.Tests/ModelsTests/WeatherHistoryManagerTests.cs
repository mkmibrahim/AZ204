using CityWeather.Data;
using CityWeather.Models;
using CityWeather.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CityWeather.Tests.ModelsTests
{
    public class WeatherHistoryManagerTests :IDisposable
    {
        private readonly DbContextOptions<WeatherDbContext> _options;
        private WeatherDbContext _context;

        #region Helpers
        static void AssertWeatherRecordsEqual(WeatherInfoObject WeatherInfoObject, WeatherInfoObject retrievedRecord)
        {
            Assert.Equal(WeatherInfoObject.Time, retrievedRecord.Time);
            Assert.Equal(WeatherInfoObject.cityName, retrievedRecord.cityName);
            Assert.Equal(WeatherInfoObject.Temperature, retrievedRecord.Temperature);
            Assert.Equal(WeatherInfoObject.Humidity, retrievedRecord.Humidity);
        }
        int newRecordId = 0;
        WeatherInfoObject WeatherInfoObject = new WeatherInfoObject
        {
            Time = DateTime.Now,
            cityName = "Amsterdam",
            Temperature = 20,
            Humidity = 10
        };
        #endregion
        public WeatherHistoryManagerTests()
        {
            _options = optionsHelper.CreateOptionsDb();
        }

        public void Dispose()
        {
            //_connection.Close();
        }


        [Fact]
        public void CreateWeatherHistoryManager()
        {
            // Arrange
            

            // Act 
            IWeatherHistoryManager manager = new WeatherHistoryManager(_context);

            // Assert
            Assert.NotNull(manager);
        }

        #region AddWeatherInfo
        [Fact]
        public void AddWeatherInfo_ExistingCity()
        {
            // Arrange
            
            using (_context = new WeatherDbContext(_options))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
              
                // Act
                newRecordId = manager.AddWeatherInfo(WeatherInfoObject);
            }

            // Assert
            Assert.True(newRecordId > 0);
            using (_context = new WeatherDbContext(_options))
            {
                var managerToCheck = new WeatherHistoryManager(_context);
                var retrievedRecord = managerToCheck.GetWeatherInfo(newRecordId);
                AssertWeatherRecordsEqual(WeatherInfoObject, retrievedRecord);
            }
        }

        [Fact]
        public void AddWeatherInfo_NewCityTest()
        {
            // Arrange
            WeatherInfoObject.cityName = "NewCity";
            
            using (_context = new WeatherDbContext(_options))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);

                // Act
                newRecordId = manager.AddWeatherInfo(WeatherInfoObject);
            }

            // Assert
            Assert.True(newRecordId > 0);
            using (_context = new WeatherDbContext(_options))
            {
                var managerToCheck = new WeatherHistoryManager(_context);
                var retrievedRecord = managerToCheck.GetWeatherInfo(newRecordId);
                AssertWeatherRecordsEqual(WeatherInfoObject, retrievedRecord);
            }
        }


        [Fact]
        public void AddWeatherInfo_UpdateExistingRecord()
        {
            // Arrange
            using (_context = new WeatherDbContext(_options))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
                WeatherInfoObject previousWeatherInfoObject = new WeatherInfoObject
                {
                    Time = WeatherInfoObject.Time,
                    cityName = WeatherInfoObject.cityName,
                    Temperature = 25,
                    Humidity = 15
                };
                manager.AddWeatherInfo(previousWeatherInfoObject);

                // Act
                newRecordId = manager.AddWeatherInfo(WeatherInfoObject);
            }

            // Assert
            Assert.True(newRecordId > 0);
            using (_context = new WeatherDbContext(_options))
            {
                var managerToCheck = new WeatherHistoryManager(_context);
                var retrievedRecord = managerToCheck.GetWeatherInfo(newRecordId);
                AssertWeatherRecordsEqual(WeatherInfoObject, retrievedRecord);
            }
        }
        #endregion

        #region GetWeatherInfo Using city name
        [Fact]
        public void GetWeatherInfoUsingCityName()
        {
            // Arrange
            using (_context = new WeatherDbContext(_options))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
                newRecordId = manager.AddWeatherInfo(WeatherInfoObject);
                for (int i = 0; i < 9; i++)
                {
                    WeatherInfoObject.Time = WeatherInfoObject.Time.AddMinutes(5);
                    newRecordId = manager.AddWeatherInfo(WeatherInfoObject);
                }
            }

            // Act
            using (_context = new WeatherDbContext(_options))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
                var retrievedRecords = manager.GetWeatherInfo(WeatherInfoObject.cityName);

                // Assert
                Assert.Equal(10, retrievedRecords.Count);
                AssertWeatherRecordsEqual(WeatherInfoObject, retrievedRecords.FirstOrDefault());
            }
        }
        #endregion


    }
}
