using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab;  // 引用Tile prefab
    public Transform gridParent;   // Grid的父对象
    public Tile[,] grid = new Tile[4, 4];  // 4x4的Tile网格
    public int score = 0;
    public TextMeshProUGUI scoreText;  // 显示分数的TextMeshPro
    public GameObject gameEndPopUp;  // 游戏结束界面
    public TextMeshProUGUI gameEndMEssage;

    void Start()
    {
        InitializeGrid();
        SpawnInitialTiles();
        UpdateScore(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2.up);   // 向上移动
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2.down); // 向下移动
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left); // 向左移动
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right); // 向右移动
        }
    }

    void InitializeGrid()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject tileObj = Instantiate(tilePrefab, gridParent);
                Tile tile = tileObj.GetComponent<Tile>();
                grid[i, j] = tile;
                tile.SetValue(0);  // 初始化时将所有Tiles设为0
            }
        }
    }

    void SpawnInitialTiles()
    {
        SpawnTileAtRandomPosition(Random.Range(0, 10) < 9 ? 2 : 4);
        SpawnTileAtRandomPosition(Random.Range(0, 10) < 9 ? 2 : 4);
    }

    List<Vector2Int> GetEmptyPositions()
    {
        List<Vector2Int> emptyPositions = new List<Vector2Int>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (grid[i, j].value == 0)  // 如果Tile的值为0，表示它是空闲的
                {
                    emptyPositions.Add(new Vector2Int(i, j));
                }
            }
        }

        return emptyPositions;
    }

    void SpawnTileAtRandomPosition(int value)
    {
        List<Vector2Int> emptyPositions = GetEmptyPositions();

        if (emptyPositions.Count > 0)
        {
            Vector2Int randomPosition = emptyPositions[Random.Range(0, emptyPositions.Count)];
            grid[randomPosition.x, randomPosition.y].SetValue(value);
        }
    }

    void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    void Move(Vector2 direction)
    {
        bool moved = false;

        // 检查移动方向，分别处理上下左右
        if (direction == Vector2.up)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 1; x < 4; x++) // 从第二列开始
                {
                    if (grid[x, y].value != 0)
                    {
                        int targetX = x;
                        while (targetX > 0 && grid[targetX - 1, y].value == 0)
                        {
                            targetX--;
                        }

                        if (targetX != x)
                        {
                            grid[targetX, y].SetValue(grid[x, y].value);
                            grid[x, y].SetValue(0);
                            moved = true;
                        }

                        if (targetX > 0 && grid[targetX - 1, y].value == grid[targetX, y].value)
                        {
                            MergeTiles(grid[targetX - 1, y], grid[targetX, y]);
                            moved = true;
                        }
                    }
                }
            }
        }
        else if (direction == Vector2.down)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 2; x >= 0; x--) // 从倒数第二列开始
                {
                    if (grid[x, y].value != 0)
                    {
                        int targetX = x;
                        while (targetX < 3 && grid[targetX + 1, y].value == 0)
                        {
                            targetX++;
                        }

                        if (targetX != x)
                        {
                            grid[targetX, y].SetValue(grid[x, y].value);
                            grid[x, y].SetValue(0);
                            moved = true;
                        }

                        if (targetX < 3 && grid[targetX + 1, y].value == grid[targetX, y].value)
                        {
                            MergeTiles(grid[targetX + 1, y], grid[targetX, y]);
                            moved = true;
                        }
                    }
                }
            }
        }
        else if (direction == Vector2.left)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 1; y < 4; y++) // 从第二行开始
                {
                    if (grid[x, y].value != 0)
                    {
                        int targetY = y;
                        while (targetY > 0 && grid[x, targetY - 1].value == 0)
                        {
                            targetY--;
                        }

                        if (targetY != y)
                        {
                            grid[x, targetY].SetValue(grid[x, y].value);
                            grid[x, y].SetValue(0);
                            moved = true;
                        }

                        if (targetY > 0 && grid[x, targetY - 1].value == grid[x, targetY].value)
                        {
                            MergeTiles(grid[x, targetY - 1], grid[x, targetY]);
                            moved = true;
                        }
                    }
                }
            }
        }
        else if (direction == Vector2.right)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 2; y >= 0; y--) // 从倒数第二行开始
                {
                    if (grid[x, y].value != 0)
                    {
                        int targetY = y;
                        while (targetY < 3 && grid[x, targetY + 1].value == 0)
                        {
                            targetY++;
                        }

                        if (targetY != y)
                        {
                            grid[x, targetY].SetValue(grid[x, y].value);
                            grid[x, y].SetValue(0);
                            moved = true;
                        }

                        if (targetY < 3 && grid[x, targetY + 1].value == grid[x, targetY].value)
                        {
                            MergeTiles(grid[x, targetY + 1], grid[x, targetY]);
                            moved = true;
                        }
                    }
                }
            }
        }

        if (moved)
        {
            SpawnTileAtRandomPosition(Random.Range(0, 10) < 9 ? 2 : 4);
            
            if (CheckGameOver())
            {
                gameEndMEssage.SetText("GAME OVER\nYOU LOSE!");
                gameEndPopUp.SetActive(true);
            }
            else if (Check2048())
            {
                gameEndMEssage.SetText("GAME OVER\nYOU WIN!");
                gameEndPopUp.SetActive(true);
            }
        }
    }

    void MergeTiles(Tile targetTile, Tile sourceTile)
    {
        targetTile.SetValue(targetTile.value * 2);  // 合并后的Tile值为两者之和
        sourceTile.SetValue(0);  // 将原Tile值设为0
        UpdateScore(targetTile.value);  // 更新分数
    }

    bool CheckGameOver()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (grid[x, y].value == 0)
                {
                    return false;  // 仍有空位，游戏未结束
                }

                if (x > 0 && grid[x, y].value == grid[x - 1, y].value)
                {
                    return false;  // 左侧可合并
                }

                if (y > 0 && grid[x, y].value == grid[x, y - 1].value)
                {
                    return false;  // 上方可合并
                }

                if (x < 3 && grid[x, y].value == grid[x + 1, y].value)
                {
                    return false;  // 右侧可合并
                }

                if (y < 3 && grid[x, y].value == grid[x, y + 1].value)
                {
                    return false;  // 下方可合并
                }
            }
        }

        return true;  // 没有可移动或可合并的Tile，游戏结束
    }

    bool Check2048()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (grid[x, y].value == 2048)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
