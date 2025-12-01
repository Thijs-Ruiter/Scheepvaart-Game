using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RouterScript : MonoBehaviour
{
    WorldMap worldMap;
    Camera cam;
    public Vector2[] coastlineNodes = new Vector2[5]
    {
        new Vector2(375, 304),
        new Vector2(367, 301),
        new Vector2(373.5f, 293),
        new Vector2(371, 287),
        new Vector2(357.5f, 287.5f)
    };
    public GameObject shipPrefab;
    public GameObject debugCircle;

    public bool nodeVisualisation = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        cam = Camera.main;
        worldMap = GameObject.Find("WorldMap").GetComponent<WorldMap>();
        if (nodeVisualisation)
        {
            EnableNodeVisualisation();
        }
    }

    // Update is called once per frame
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.A)
        {
            GameObject ship = Instantiate(shipPrefab, transform.position, Quaternion.identity, transform);
            ship.transform.localPosition = new Vector3(372, 305, 0);
        }
    }

    void EnableNodeVisualisation()
    {
        foreach(Vector2 pos in coastlineNodes)
        {
            Instantiate(debugCircle, pos, Quaternion.identity);
        }
    }
}
