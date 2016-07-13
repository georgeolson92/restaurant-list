using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantList.Objects
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurantlist;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Mow the lawn", 1);
      Restaurant secondRestaurant = new Restaurant("Mow the lawn", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Mow the lawn", 1);

      //Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Mow the lawn", 1);

      //Act
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Mow the lawn", 1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Mow the lawn", 1);
      Restaurant secondRestaurant = new Restaurant("Mow the lawn", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Mow the lawn", 1);
      testRestaurant.Save();

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Mow the lawn", 1);
      testRestaurant.Save();

      //Act
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Mow the lawn", 1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }
  }
}
