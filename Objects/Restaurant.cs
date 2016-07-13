using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace RestaurantList.Objects
{
  public class Restaurant
  {
    private int _id;
    private string _description;
    private int _cuisineId;

    public Restaurant(string Description, int CuisineId, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _cuisineId = CuisineId;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
        if (!(otherRestaurant is Restaurant))
        {
          return false;
        }
        else
        {
          Restaurant newRestaurant = (Restaurant) otherRestaurant;
          bool idEquality = this.GetId() == newRestaurant.GetId();
          bool descriptionEquality = this.GetDescription() == newRestaurant.GetDescription();
          bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
          return (idEquality && descriptionEquality && cuisineEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }
  public static List<Restaurant> GetAll()
  {
    List<Restaurant> AllRestaurants = new List<Restaurant>{};

    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int restaurantId = rdr.GetInt32(0);
      string restaurantDescription = rdr.GetString(1);
      int restaurantCuisineId = rdr.GetInt32(2);
      Restaurant newRestaurant = new Restaurant(restaurantDescription, restaurantCuisineId, restaurantId);
      AllRestaurants.Add(newRestaurant);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return AllRestaurants;
  }
  public void Save()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr;
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (description, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantDescription, @RestaurantCuisineId);", conn);

    SqlParameter descriptionParameter = new SqlParameter();
    descriptionParameter.ParameterName = "@RestaurantDescription";
    descriptionParameter.Value = this.GetDescription();

    SqlParameter cuisineIdParameter = new SqlParameter();
    cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
    cuisineIdParameter.Value = this.GetCuisineId();

    cmd.Parameters.Add(descriptionParameter);
    cmd.Parameters.Add(cuisineIdParameter);

    rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      this._id = rdr.GetInt32(0);
    }
    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
  }

  public static Restaurant Find(int id)
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
    SqlParameter restaurantIdParameter = new SqlParameter();
    restaurantIdParameter.ParameterName = "@RestaurantId";
    restaurantIdParameter.Value = id.ToString();
    cmd.Parameters.Add(restaurantIdParameter);
    rdr = cmd.ExecuteReader();

    int foundRestaurantId = 0;
    string foundRestaurantDescription = null;
    int foundRestaurantCuisineId = 0;

    while(rdr.Read())
    {
      foundRestaurantId = rdr.GetInt32(0);
      foundRestaurantDescription = rdr.GetString(1);
      foundRestaurantCuisineId = rdr.GetInt32(2);
    }
    Restaurant foundRestaurant = new Restaurant(foundRestaurantDescription, foundRestaurantCuisineId, foundRestaurantId);

    if (rdr != null)
    {
      rdr.Close();
    }
    if (conn != null)
    {
      conn.Close();
    }
    return foundRestaurant;
  }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }


  }
}
