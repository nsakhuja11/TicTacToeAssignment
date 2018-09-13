using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class AutorizeAttribute : ResultFilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        { 
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string apiKey = context.HttpContext.Request.Headers["apikey"].ToString();
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new UnauthorizedAccessException("Api Key not passed");
            }
            else
            {
                UserDataBase database = new UserDataBase();
                bool status = database.checkExistence(apiKey);
                if (status == false)
                {
                    throw new UnauthorizedAccessException("Invalid Api Key passed");
                }
            }
            //throw new NotImplementedException();
        }
    }
}
