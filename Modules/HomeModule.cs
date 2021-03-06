using System.Collections.Generic;
using System;
using Nancy;
using RestaurantList.Objects;

namespace RestaurantList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["index.cshtml", AllCuisines];
      };
      Get["/restaurants"] = _ => {
        List<Restaurant> AllRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", AllRestaurants];
      };
      Get["/cuisines"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", AllCuisines];
      };
      Get["/cuisines/new"] = _ => {
        return View["cuisines_form.cshtml"];
      };
      Post["/cuisines/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };
      Get["/restaurants/new"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["restaurant_form.cshtml", AllCuisines];
      };
      Post["/restaurants/new"] = _ => {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-description"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };
      Post["/restaurants/delete"] = _ => {
       Restaurant.DeleteAll();
       return View["cleared.cshtml"];
     };
     Post["/cuisines/delete"] = _ => {
      Cuisine.DeleteAll();
      return View["cleared.cshtml"];
    };
     Get["/cuisines/{id}"] = parameters => {
       Dictionary<string, object> model = new Dictionary<string, object>();
       var SelectedCuisine = Cuisine.Find(parameters.id);
       var CuisineRestaurants = SelectedCuisine.GetRestaurants();
       model.Add("cuisine", SelectedCuisine);
       model.Add("restaurants", CuisineRestaurants);
       return View["cuisine.cshtml", model];
     };
    }
  }
}
