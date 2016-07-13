using System.Collections.Generic;
using System.IO;
using Microsoft.AspNet.Builder;
using Nancy;
using Nancy.Owin;
using Nancy.ViewEngines.Razor;

namespace RestaurantList
{
  public class Startup
  {
    public void Configure(IApplicationBuilder app)
    {
      app.UseOwin(x => x.UseNancy());
    }
  }
  public class CustomRootPathProvider : IRootPathProvider
  {
    public string GetRootPath()
    {
      return Directory.GetCurrentDirectory();
    }
  }
  public class RazorConfig : IRazorConfiguration
  {
    public IEnumerable<string> GetAssemblyNames()
    {
      return null;
    }

    public IEnumerable<string> GetDefaultNamespaces()
    {
      return null;
    }

    public bool AutoIncludeModelNamespace
    {
      get { return false; }
    }
  }
  public static class DBConfiguration
    {
        public static string ConnectionString = "Server=tcp:epicodus.database.windows.net,1433;Initial Catalog=restaurant;Persist Security Info=False;User ID=georgeolson92;Password=Fuckers!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
