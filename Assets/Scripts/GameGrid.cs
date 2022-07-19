using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class GameGrid : MonoBehaviour
    {
        private Tile[,] _grid;
        private static readonly int gridSize = 3;

        [SerializeField] private Tile TilePrefab;

        private void Awake()
        {
            CreateTiles();
        }

        private void CreateTiles()
        {
            _grid = new Tile[gridSize, gridSize];


            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    _grid[row, column] = Instantiate(TilePrefab, transform);
                }
            }
        }
    }
}