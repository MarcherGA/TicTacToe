using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TicTacToe
{
    public class GameGrid : MonoBehaviour
    {

        public HintActivator HintActivator { get => _hintActivator; set => _hintActivator = value; }
        public TicTacToeGrid Grid { get => _grid; }
        public Tile[,] TilesGrid { get => _tilesGrid; }

        public Timer TurnTimer;
        public Tile TilePrefab;
        [SerializeField] private int TurnLengthInSeconds;

        private HintActivator _hintActivator;
        private TicTacToeGrid _grid;
        private Tile[,] _tilesGrid;

        private void Awake()
        {
            GameEventsManager.Instance.OnWaitForPlayerPress += WaitForPress;
            GameEventsManager.Instance.OnPlayerTurn += UpdateTile;
            GameEventsManager.Instance.OnRestartGame += OnRestartGame;
            GameEventsManager.Instance.OnDisplayHint += OnDisplayHint;
            GameEventsManager.Instance.OnUpdateTile += UpdateTile;
            GameEventsManager.Instance.OnTurnTimerEnd += delegate { WaitForPress(false); };

            _grid = new TicTacToeGrid();
            Grid.InitalizeGame();

            HintActivator = new HintActivator(Grid.GridState, TilesGrid);
        }

        private void Start()
        {
            InitalizeGrid();
            GameEventsManager.Instance.RestartGame();
        }

        public void UpdateTile (TicTacToeGrid.Sign sign, TilePosition pos)
        {
            TilesGrid[pos.Row, pos.Column].UpdateTile(sign);
        }

        public void InitalizeGrid()
        {
            CreateTiles();

        }

        public void OnRestartGame()
        {
            ResetGrid();
        }



        public void WaitForPress(bool isOn) 
        {
            var availablePositions = Grid.AvailablePositions;
            foreach (var pos in availablePositions)
            {
                TilesGrid[pos.Row, pos.Column].SetActiveButton(isOn);
            }

        }

        private void ResetGrid()
        {
            foreach(Tile tile in TilesGrid)
            {
                tile.ResetTile();
            }
        }

        public void CreateTiles()
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
                    TilesGrid[row, column] = tile;
                }
            }
        }

        public void OnDisplayHint(int length)
        {
            HintActivator.ActivateHint(length);
        }
    }
}