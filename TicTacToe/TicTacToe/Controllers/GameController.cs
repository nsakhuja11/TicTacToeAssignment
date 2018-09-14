using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        static string player1Key;
        static string player2Key;
        static int[] gameBoard = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static int turn = 1;
        static int moves = 0;
        static bool player1 = false;
        static bool player2 = false;
        static int flag = 0;
        // POST api/values
        [HttpPost]
        [Autorize]
        [Log]
        [Exception]
        public string GameMoves([FromBody]int position,[FromHeader]string apikey)
        {
            if (player1Key == null)
            {
                player1Key = apikey;
                if (apikey == player1Key && turn == 1)
                {
                    if (gameBoard[position - 1] == 0)
                    {
                        gameBoard[position - 1] = 1;
                        turn = 2;
                        moves++;
                        player1 = check(1);
                    }
                    else
                    {
                        throw new Exception("This Position is Already Marked");
                    }
                }
                else
                {
                    throw new Exception("Its Not Your Turn...!!!");
                }
            }
            else if (player2Key == null && apikey != player1Key)
            {
                player2Key = apikey;
                if (apikey == player2Key && turn == 2)
                {
                    if (gameBoard[position - 1] == 0)
                    {
                        gameBoard[position - 1] = 2;
                        turn = 1;
                        moves++;
                        player2 = check(2);
                    }
                    else
                    {
                        throw new Exception("This Position is Already Marked");
                    }
                }
                else
                {
                    throw new Exception("Its Not Your Turn...!!!");
                }
            }
            else if((player1Key == apikey || player2Key == apikey) && flag == 0)
            {
                if(apikey == player1Key && turn == 1)
                {
                    if (gameBoard[position - 1] == 0)
                    {
                        gameBoard[position - 1] = 1;
                        turn = 2;
                        moves++;
                        player1 = check(1);
                    }
                    else
                    {
                        throw new Exception("This Position is Already Marked");
                    }
                }
                else if(apikey == player2Key && turn == 2)
                {
                    if (gameBoard[position - 1] == 0)
                    {
                        gameBoard[position - 1] = 2;
                        turn = 1;
                        moves++;
                        player2 = check(2);
                    }
                    else
                    {
                        throw new Exception("This Position is Already Marked");
                    }
                }
                else
                {
                    throw new Exception("Its Not Your Turn...!!!");
                }
                if (player1)
                {
                    flag = 1;
                    LogAttribute.status = "Player 1 Won The Game";
                    return "Player 1 Won The Game";
                }
                else if (player2)
                {
                    flag = 2;
                    LogAttribute.status = "Player 2 Won The Game";
                    return "Player 2 Won The Game";
                }
                else if (moves == 9)
                {
                    LogAttribute.status = "Game Is Tied";
                    return "Game Is Tied";
                }
                else
                {
                    LogAttribute.status = "Game is in Process";
                    return "Game is in Process";
                }
            }
            else if (flag == 1)
            {
                throw new Exception("Game is over and Player 1 has Won");
            }
            else if (flag == 2)
            {
                throw new Exception("Game is over and Player 2 has Won");
            }
            else
            {
                throw new Exception("No More Players Allowed");
            }
            LogAttribute.status = "Game is in Process";
            return "Game is in Process";
        }

        bool check(int i)
        {
            if((gameBoard[0] == i && gameBoard[1] == i && gameBoard[2] == i) || (gameBoard[3] == i && gameBoard[4] == i && gameBoard[5] == i) || (gameBoard[6] == i && gameBoard[7] == i && gameBoard[8] == i))
            {
                return true;
            }
            else if((gameBoard[0] == i && gameBoard[3] == i && gameBoard[6] == i) || (gameBoard[1] == i && gameBoard[4] == i && gameBoard[7] == i)|| (gameBoard[2] == i && gameBoard[5] == i && gameBoard[8] == i))
            {
                return true;
            }
            else if((gameBoard[0] == i && gameBoard[4] == i && gameBoard[8] == i) || (gameBoard[2] == i && gameBoard[4] == i && gameBoard[6] == i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
