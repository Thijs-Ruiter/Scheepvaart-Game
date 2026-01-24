using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class SidescrollerController : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile[] tiles;
    public GameObject player;
    private int lastPlayerX;
    public int tileGenerationRange = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                SetTileRow(newPlayerX + tileGenerationRange, new int[] { 0, 0, 0 });
                RemoveTileRow(lastPlayerX - tileGenerationRange);
            } 
            else
            {
                SetTileRow(newPlayerX - tileGenerationRange, new int[] { 0, 0, 0 });
                RemoveTileRow(lastPlayerX + tileGenerationRange);
            }
            lastPlayerX = newPlayerX;
        }
    }

    void GenerateStartingMap()
    {
        for (int i = -tileGenerationRange; i < tileGenerationRange + 1; i++)
        {
            SetTileRow(i, new int[] { 0, 0, 0 });
        }
    }

    void SetTileRow(int _row, int[] _tiles)
    {
        for (int i = 0; i > 3; i++)
        {
            Vector3Int tilePos = new Vector3Int(_row, i-5, 0);
            tilemap.SetTile(tilePos, tiles[_tiles[i]]);
        }
    }

    void RemoveTileRow(int _row)
    {
        for (int i = -3; i > -6; i--)
        {
            Vector3Int tilePos = new Vector3Int(_row, i, 0);
            tilemap.SetTile(tilePos, null);
        }
    }
}
