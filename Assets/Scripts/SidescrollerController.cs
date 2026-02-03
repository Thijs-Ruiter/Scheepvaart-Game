using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using TMPro;

public class SidescrollerController : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public Section[] sections;
    public TMP_Text timerText;
    public GameObject lostText;
    
    public enum Tiles
    {
        None = 0, Ground = 1, Obstacle = 100
    }

    public enum GameState
    {
        Start, Running, Lost, End
    }
    public GameState gameState = GameState.Start;

    public GameObject player;
    private int lastPlayerX;
    private Section currentSection;
    public int tileGenerationRange = 12;
    private float timeOffset;

    private int rowCount = 0;
    private int rowsSinceLastUniqueSection = 0;
    private bool timerEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeSections();
        GenerateStartingMap();
        StartCoroutine(StartCountDown(3.0f));
        lastPlayerX = Mathf.RoundToInt(player.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                if (timerEnded)
                {
                    timerEnded = false;
                    gameState = GameState.Running;
                    OnStartRunning();
                }
                break;
            case GameState.Running:
                Running();
                if (timerEnded)
                {
                    gameState = GameState.End;
                }
                break;
            case GameState.Lost:
                break;
            case GameState.End:
                break;
        }
    }

    void Running()
    {
        timerText.text = "Timer: " + Mathf.Round((Time.time - timeOffset)*100)/100;
        if (currentSection == null)
        {
            currentSection = sections[UnityEngine.Random.Range(0, sections.Length)];
            rowCount = 0;
        }
        int newPlayerX = Mathf.RoundToInt(player.transform.position.x);
        if (lastPlayerX != newPlayerX)
        {
            if (lastPlayerX < newPlayerX)
            {
                Tiles[] currentRow = new Tiles[currentSection.size.y];
                Array.Copy(currentSection.tiles, rowCount * currentSection.size.y, currentRow, 0, currentSection.size.y);
                SetTileRow(newPlayerX + tileGenerationRange, currentRow);
                RemoveTileRow(lastPlayerX - tileGenerationRange);
            }
            else
            {
                SetTileRow(newPlayerX - tileGenerationRange, currentSection.tiles);
                RemoveTileRow(lastPlayerX + tileGenerationRange);
            }
            lastPlayerX = newPlayerX;
            rowCount += 1;
            if (rowCount == currentSection.size.x)
            {
                if (rowsSinceLastUniqueSection < 3)
                {
                    currentSection = sections[0];
                    rowsSinceLastUniqueSection += 1;
                    rowCount = 0;
                }
                else
                {
                    currentSection = null;
                    rowsSinceLastUniqueSection = 0;
                }
            }
        }
    }

    void OnStartRunning()
    {
        StartCoroutine(GameTimer(60.0f));
    }

    public void OnLose()
    {
        lostText.SetActive(true);
    }

    void GenerateStartingMap()
    {
        Debug.Log("Starting Generation");
        for (int i = -tileGenerationRange; i < tileGenerationRange + 1; i++)
        {
            SetTileRow(i, sections[0].tiles);
        }
        Debug.Log("Finished Generation");
    }

    void SetTileRow(int _row, Tiles[] _tileOrder)
    {
        for (int i = 0; i < _tileOrder.Length; i++)
        {
            Vector3Int tilePos = new Vector3Int(_row, i - 5, 0);
            Tile _tile = GetTile(_tileOrder[i]);
            tilemap.SetTile(tilePos, _tile);
        }
    }

    void RemoveTileRow(int _row)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3Int tilePos = new Vector3Int(_row, i - 5, 0);
            tilemap.SetTile(tilePos, null);
        }
    }

    Tile GetTile(Tiles _tile)
    {
        switch (_tile)
        {
            case Tiles.None:
                return null;
            case Tiles.Ground:
                return tiles[0];
            case Tiles.Obstacle:
                return tiles[1];
        }
        return null;
    }

    void InitializeSections()
    {
        // Tiles are ordered from bottom to top, left to right
        sections = new Section[2];
        sections[0] = new Section(new Vector2Int(1, 3), new Tiles[] { Tiles.Ground, Tiles.Ground, Tiles.Ground });
        sections[1] = new Section(new Vector2Int(3, 4), new Tiles[] { Tiles.Ground, Tiles.Ground, Tiles.Ground, Tiles.None, Tiles.Ground, Tiles.Ground, Tiles.Ground, Tiles.Obstacle, Tiles.Ground, Tiles.Ground, Tiles.Ground, Tiles.None });
    }

    public class Section
    {
        public Vector2Int size;
        public Tiles[] tiles;

        public Section(Vector2Int size, Tiles[] tiles)
        {
            this.size = size;
            this.tiles = tiles;
        }
    }

    private IEnumerator StartCountDown(float countDownTime)
    {
        yield return new WaitForSeconds(countDownTime);
        timerEnded = true;
    }

    private IEnumerator GameTimer(float gameTime)
    {
        timeOffset = Time.time;
        yield return new WaitForSeconds(gameTime);
        timerEnded = true;
        UnityEngine.Debug.Log("Game won! You escaped!");
    }
}
