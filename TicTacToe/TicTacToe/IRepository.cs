using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe
{
    interface IRepository
    {
        string[] AddUserToDataBase(User value);
        bool checkExistence(string key);
        void LogDatabase(Logger logger);
    }
}
