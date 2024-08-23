using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Tile[,] grid = new Tile[4, 4];
    public int score = 0;
    public Text scoreText;
    public Text bestScoreText;

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        // Initialize grid and spawn initial tiles
        SpawnTile();
        SpawnTile();
        UpdateScore();
    }

    void SpawnTile()
    {
        // Logic to spawn a new tile at a random empty position
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
