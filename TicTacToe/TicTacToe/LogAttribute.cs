using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class LogAttribute : ResultFilterAttribute, IActionFilter
    {
        Logger log = new Logger();
        UserDataBase Database = new UserDataBase();
        public static string status = "-";
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                log.Request = context.RouteData.Values["action"].ToString() + " " + context.RouteData.Values["controller"].ToString();
                log.Response = "Success";
                log.Exception = "NULL";
                log.Status = status;
                Database.LogDatabase(log);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}