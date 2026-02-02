using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class SidescrollerController : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public Section[] sections;
    public enum Tiles
    {
        None = 0, Ground = 1, Obstacle = 100
    }
    public GameObject player;
    private int lastPlayerX;
    public int tileGenerationRange = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeSections();
        GenerateStartingMap();
        lastPlayerX = Mathf.RoundToInt(player.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        int newPlayerX = Mathf.RoundToInt(player.transform.position.x);
        if (lastPlayerX != newPlayerX)
        {
            if (lastPlayerX < newPlayerX)
            {
                SetTileRow(newPlayerX + tileGenerationRange, sections[0].tiles);
                RemoveTileRow(lastPlayerX - tileGenerationRange);
            }
            else
            {
                SetTileRow(newPlayerX - tileGenerationRange, sections[0].tiles);
                RemoveTileRow(lastPlayerX + tileGenerationRange);
            }
            lastPlayerX = newPlayerX;
        }
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
        sections = new Section[1];
        sections[0] = new Section(new Vector2Int(1, 3), new Tiles[] { Tiles.Ground, Tiles.Ground, Tiles.Ground });
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
}
