using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        
        // POST api/values
        [HttpPost]
        [Log]
        public string[] Post([FromBody]User value)
        {
            UserDataBase database = new UserDataBase();
            return database.AddUserToDataBase(value);
        }

    }
}
