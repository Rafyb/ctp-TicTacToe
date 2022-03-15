using System;
using System.Collections.Generic;

namespace TP_IA_TicTacToe
{
    public class MinmaxAlgo : Algorithm
    {
        private Game _g;
        
        public MinmaxAlgo(Game game)
        {
            _g = game;
        }

        public char[,] MakeBestMove(char[,] grid)
        {
            List<char[,]> possibleMoves = GetBoardChildren(grid, 'X');

            int bestValue = Minimax(possibleMoves[0], 3, false);
            char[,] bestBoard = possibleMoves[0];
            
            foreach (char[,] move in possibleMoves)
            {
                int value =  Minimax(move, 15, false);
                Console.WriteLine(value);
                if (value > bestValue)
                {
                    bestBoard = move;
                    bestValue = value;
                }
            }

            return bestBoard;
        }
        
        
        int Minimax(char[,] board, int depth, bool maximizingPlayer) {
            int score = ScoreBoard(board,depth);
            if(depth == 0 || _g.CheckFull(board) || score != 0)
                return score;
            
            if(maximizingPlayer) {
                int value = -10;
                foreach(char[,] child in GetBoardChildren(board, 'X'))
                {
                    value = Math.Max(value, Minimax(child, depth - 1, false));
                };
                return value;
            } else {
                int value = 10;
                foreach(char[,] child in GetBoardChildren(board, 'O'))
                {
                    value = Math.Min(value, Minimax(child, depth - 1, true));
                };
                return value;
            }
        }

        int ScoreBoard(char[,] board, int depth)
        {
            if(_g.CheckWin(board) < 0) 
                return 10 - depth;
            else if(_g.CheckWin(board) > 0)
                return -10 - depth;

            return 0;
        }
        
        List<char[,]> GetBoardChildren(char[,] grid, char player)
        {
            List<char[,]> childrens = new List<char[,]>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (grid[i, j] == ' ')
                    {
                        grid[i, j] = player;
                        childrens.Add(Game.CloneGrid(grid));
                        grid[i, j] = ' ';
                    }
                }
            }
            return childrens;
        }
    }
}