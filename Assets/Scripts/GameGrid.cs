using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe
{
    public class GameGrid : MonoBehaviour
    {

        public Timer TurnTimer;
        [SerializeField] private Tile TilePrefab;
        [SerializeField] private int TurnLengthInSeconds; 

        
        private TicTacToeGrid _grid;
        private Tile[,] _tilesGrid;

        private void Awake()
        {
            InitalizeGrid();
        }

        private void Start()
        {
            GameEventsManager.Instance.RestartGame();
        }

        public void InitalizeGrid()
        {
            CreateTiles();
            GameEventsManager.Instance.OnWaitForPlayerPress += WaitForPress;
            GameEventsManager.Instance.OnPlayerTurn += (TicTacToeGrid.Sign sign, TilePosition pos) => {_tilesGrid[pos.Row, pos.Column].UpdateTile(sign); };
            GameEventsManager.Instance.OnRestartGame += OnRestartGame;
            GameEventsManager.Instance.OnDisplayHint += OnDisplayHint;
            GameEventsManager.Instance.OnTurnTimerEnd += () => WaitForPress(false);


            _grid = new TicTacToeGrid();
            _grid.InitalizeGame();
        }

        private void OnRestartGame()
        {
            ResetGrid();
        }



        private void WaitForPress(bool isOn) 
        {
            var availablePositions = _grid.AvailablePositions;
            foreach (var pos in availablePositions)
            {
                _tilesGrid[pos.Row, pos.Column].SetActiveButton(isOn);
            }

        }

        private void ResetGrid()
        {
            foreach(Tile tile in _tilesGrid)
            {
                tile.ResetTile();
            }
        }

        private void CreateTiles()
        {
            int gridSize = TicTacToeGrid.GridSize;
            _tilesGrid = new Tile[gridSize, gridSize];


            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    Tile tile = Instantiate(TilePrefab, transform);
                    TilePosition pos = new TilePosition(row, column);
                    tile.OnTileClicked += delegate { GameEventsManager.Instance.TilePress(pos); };
                    _tilesGrid[row, column] = tile;
                }
            }
        }

        private void OnDisplayHint(int length)
        {
            TilePosition hint = MiniMaxAlgorithm.GetBestMove(_grid.GridState);
            _tilesGrid[hint.Row, hint.Column].Flicker(length);
        }
    }
}