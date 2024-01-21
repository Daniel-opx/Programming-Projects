using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tic_tac_toe
{
    internal class Board
    {

        
        /// <summary>
        /// this functions create new cahr array and initialize the members to numbers ranging 1 - 9 (exclusive)
        /// </summary>
        /// <returns></returns>
        public static char[,] CreateBoard()
        {
            char boo = (char)49;
            char[,] newBoard = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for ( int j = 0; j < 3; j++)
                {
                    newBoard[i, j] = boo++;
                }
            }
            return newBoard;
        }
        /// <summary>
        /// this method is static and prints the board
        /// </summary>
        /// <param name="array"></param>
        public static void PrintBoard(char[,] array) 
        {
            Console.WriteLine(
                $@"   |   |  
 {array[0,0]} | {array[0,1]} | {array[0,2]} 
___|___|___
   |   |   
 {array[1,0]} | {array[1,1]} | {array[1,2]} 
___|___|___
   |   |   
 {array[2,0]} | {array[2,1]} | {array[2,2]} 
   |   |   "
                );
        }
        private static int UserInput(string name, char[,] array)
        {
            int value;
            while (true) { 
                Console.WriteLine(name + " please choose your number between 1-9");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out value))
                {
                    Console.WriteLine(name + " please peak an integer");
                }
                else
                {
                    if (value < 1 || value > 9)
                    {
                        Console.WriteLine("number out of range");
                    }
                    else if (!IsCellEpty(value, array))
                    {
                        Console.WriteLine("cell is ocuupied");
                    }
                    else
                    {
                        return value;
                        break;
                    }
                }
               
                
            }   
            
        }
        private static bool IsCellEpty(int parsedUserInput, char[,] array)
        {
            char foo = Convert.ToChar(parsedUserInput + '0');
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j <3; j++)
                {
                    if (foo == array[i,j])
                    {
                        
                        return true;
                    }

                }

            }
            return false;
        }

        private static bool IsWinner(char[,] array)
        {
            for(int i = 0;i < array.GetLength(0);i++)
            {
                if (array[i,0] == array[i,1] && array[i,1] == array[i,2])
                {
                    return true;
                }
                if (array[0, i] == array[1, i] && array[1, i] == array[2, i])
                {
                    return true;
                }
                
            }
            // Check main diagonal (top-left to bottom-right)
            if (array[0, 0] == array[1, 1] && array[1, 1] == array[2, 2])
            {
                return true;
            }

            // Check other diagonal (top-right to bottom-left)
            if (array[0, 2] == array[1, 1] && array[1, 1] == array[2, 0])
            {
                return true;
            }
            return false;
        }
        private static void setNumber(char[,] array,int num,char a)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == Convert.ToChar (num+ '0'))
                    {
                        array[i, j] = a;
                    }

                }

            }
            
        }

        public static void ResetBoard(char[,] board)
        {
            char nums = (char)49;
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = nums++;
                }
            }
        }
        public static void StartGame()
        {
            char[,] myBoard = CreateBoard();
            string player1 = "player 1";
            string player2 = "player 2";
            int player1Choice, player2Choice;
            while (true)
            {

                player1Choice = UserInput(player1, myBoard);
                if (IsCellEpty(player1Choice, myBoard))
                {
                    setNumber(myBoard, player1Choice, 'X');
                }
                PrintBoard(myBoard);
                if (IsWinner(myBoard))
                {
                    Console.WriteLine(player1 + " is winning");
                    break;
                }
                player2Choice = UserInput(player2, myBoard);
                if (IsCellEpty(player2Choice, myBoard))
                {
                    setNumber(myBoard, player2Choice, 'O');
                }
                PrintBoard(myBoard);
                if (IsWinner(myBoard))
                {
                    Console.WriteLine(player2 + " is winning");
                    break;
                }
 
            }
            string upperInput;
            do
            {
                Console.WriteLine("would you like to reset- answer Y Or N");
                string input = Console.ReadLine();
                 upperInput = input.ToUpper();
                
            }while (upperInput != "Y" && upperInput != "N");
            switch (upperInput)
            {
                case "Y":
                    ResetBoard(myBoard);
                    StartGame();
                    break;
                case "N":
                    return;

            }

        }

        



    }
}
