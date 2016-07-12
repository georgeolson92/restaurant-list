using System.Collections.Generic;
using System;
using Nancy;
using ToDoList.Objects;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] =_=> View["index.cshtml"];
      Post["allTasks"] =_=> {
        Task newTask = new Task(Request.Form["newTask"], 1);
        newTask.Save();
        return View["allTasks.cshtml", Task.GetAll()];
      };
      Post["/clearedTasks"] =_=> {
        Task.DeleteAll();
        return View["index.cshtml"];
      };
    }
  }
}
