using CityWeather.Data;
using CityWeather.Models;
using CityWeather.Tests.Helpers;
using Microsoft.Data.Sqlite;
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
        private SqliteConnection _connection;
        private readonly DbContextOptions<WeatherDbContext> _options;
        private readonly IOptions<DatabaseConfigurationClass> _optionsdb;
        private WeatherDbContext _context;

        #region Helpers
        static void AssertWeatherRecordsEqual(WeatherInfoInstance weatherInfoInstance, WeatherInfoInstance retrievedRecord)
        {
            Assert.Equal(weatherInfoInstance.Time, retrievedRecord.Time);
            Assert.Equal(weatherInfoInstance.cityName, retrievedRecord.cityName);
            Assert.Equal(weatherInfoInstance.Temperature, retrievedRecord.Temperature);
            Assert.Equal(weatherInfoInstance.Humidity, retrievedRecord.Humidity);
        }
        int newRecordId = 0;
        WeatherInfoInstance weatherInfoInstance = new WeatherInfoInstance
        {
            Time = DateTime.Now,
            cityName = "Amsterdam",
            Temperature = 20,
            Humidity = 10
        };
        #endregion
        public WeatherHistoryManagerTests()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<WeatherDbContext>()
                        .UseSqlite(_connection)
                        .Options;

             _optionsdb = optionsHelper.CreateOptionsDb();

            using (_context = new WeatherDbContext(_options, _optionsdb))
                _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
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
            
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
              
                // Act
                newRecordId = manager.AddWeatherInfo(weatherInfoInstance);
            }

            // Assert
            Assert.True(newRecordId > 0);
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                var managerToCheck = new WeatherHistoryManager(_context);
                var retrievedRecord = managerToCheck.GetWeatherInfo(newRecordId);
                AssertWeatherRecordsEqual(weatherInfoInstance, retrievedRecord);
            }
        }

        [Fact]
        public void AddWeatherInfo_NewCityTest()
        {
            // Arrange
            weatherInfoInstance.cityName = "NewCity";
            
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);

                // Act
                newRecordId = manager.AddWeatherInfo(weatherInfoInstance);
            }

            // Assert
            Assert.True(newRecordId > 0);
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                var managerToCheck = new WeatherHistoryManager(_context);
                var retrievedRecord = managerToCheck.GetWeatherInfo(newRecordId);
                AssertWeatherRecordsEqual(weatherInfoInstance, retrievedRecord);
            }
        }


        [Fact]
        public void AddWeatherInfo_UpdateExistingRecord()
        {
            // Arrange
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
                WeatherInfoInstance previousWeatherInfoInstance = new WeatherInfoInstance
                {
                    Time = weatherInfoInstance.Time,
                    cityName = weatherInfoInstance.cityName,
                    Temperature = 25,
                    Humidity = 15
                };
                manager.AddWeatherInfo(previousWeatherInfoInstance);

                // Act
                newRecordId = manager.AddWeatherInfo(weatherInfoInstance);
            }

            // Assert
            Assert.True(newRecordId > 0);
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                var managerToCheck = new WeatherHistoryManager(_context);
                var retrievedRecord = managerToCheck.GetWeatherInfo(newRecordId);
                AssertWeatherRecordsEqual(weatherInfoInstance, retrievedRecord);
            }
        }
        #endregion

        #region GetWeatherInfo Using city name
        [Fact]
        public void GetWeatherInfoUsingCityName()
        {
            // Arrange
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
                newRecordId = manager.AddWeatherInfo(weatherInfoInstance);
                for (int i = 0; i < 9; i++)
                {
                    weatherInfoInstance.Time = weatherInfoInstance.Time.AddMinutes(5);
                    newRecordId = manager.AddWeatherInfo(weatherInfoInstance);
                }
            }

            // Act
            using (_context = new WeatherDbContext(_options, _optionsdb))
            {
                IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
                var retrievedRecords = manager.GetWeatherInfo(weatherInfoInstance.cityName);

                // Assert
                Assert.Equal(10, retrievedRecords.Count);
                AssertWeatherRecordsEqual(weatherInfoInstance, retrievedRecords.FirstOrDefault());
            }
        }
        #endregion


    }
}
