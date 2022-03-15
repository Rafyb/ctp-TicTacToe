using System;
using System.Collections.Generic;
using System.Linq;

namespace TP_IA_TicTacToe
{
    public class Game
    {
        
        private char[,] _grid;
        private bool _IATurned;

        private Algorithm _IA;

        public void Start()
        {
            _IA = new MinmaxAlgo(this);
            
            _grid = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _grid[i, j] = ' ';
                }
            }

            while (CheckWin(_grid) == 0 && !CheckFull(_grid))
            {
                if (_IATurned)
                {
                    MakeIAMove();
                }
                else
                {
                    //Console.Clear();
                    ShowGrid();
                    PlayerChoice();
                }

                _IATurned = !_IATurned;
            }
            
            Console.Clear();
            ShowGrid();
            if(CheckWin(_grid) == 1) Console.WriteLine("Player Won");
            else if(CheckWin(_grid) == -1) Console.WriteLine("IA Won");
            else Console.WriteLine("It's a draw");
            
            
        }

        void MakeIAMove()
        {
            _grid = _IA.MakeBestMove(_grid);
        }
        

        public static char[,] CloneGrid(char[,] grid)
        {
            char[,] b = new char[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    b[i, j] = grid[i, j];
                }
            }
            return b;
        }
        
        public bool CheckFull(char[,] grid)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(grid[i,j] == ' ') return false;
                }
            }
            return true;
        }

        public int CheckWin(char[,] grid)
        {
                for (int i = 0; i < 3; i++)
                {
                    if (grid[i,0] == grid[i,1] && grid[i,1] == grid[i,2])
                    {
                        if(grid[i,0] == 'X') return -1;
                        if(grid[i,0] == 'O') return 1;
                    }
                    
                    if (grid[0,i] == grid[1,i] && grid[1,i] == grid[2,i])
                    {
                        if(grid[0,i] == 'X') return -1;
                        if(grid[0,i] == 'O') return 1;
                    }
                }

                if (grid[0,0] == grid[1,1] && grid[1,1] == grid[2,2])
                {
                    if(grid[0,0] == 'X') return -1;
                    if(grid[0,0] == 'O') return 1;
                }
                else if (grid[0,2] == grid[1,1] && grid[1,1] == grid[2,0])
                {
                    if(grid[1,1] == 'X') return -1;
                    if(grid[1,1] == 'O') return 1;
                }

                return 0;
        }
        
        

        public void ShowGrid()
        {
            string table = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    table += " "+_grid[i, j]+" ";
                    if (j < 2) table += "|";
                }
                if (i < 2) table += "\n-----------\n";
            }
            Console.WriteLine(table);
        }

        public void PlayerChoice()
        {
            do
            {
                Console.WriteLine("Où jouer ? Case [1-9] ");
            } 
            while (!EntryValid(Console.ReadLine()));
        }

        public bool EntryValid(string entry)
        {
            if (string.IsNullOrEmpty(entry))
                return false;

            int input = Int32.Parse(entry)-1;
            
            

            if (input < 0 || input > 8)
                return false;

            //Console.WriteLine("_grid["+input%3+", "+input/3+"] = "+_grid[input%3, input/3]);
            
            if (_grid[input/3, input%3] != ' ') 
                return false;

            _grid[input/3, input%3]  = 'O';
            return true;
        }
        
    }
}