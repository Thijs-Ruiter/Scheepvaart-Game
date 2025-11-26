using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RouterScript : MonoBehaviour
{
    WorldMap worldMap;
    Camera cam;
    Vector2[] coastlineNodes;
    public GameObject shipPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void GenerateNodes()
    {
        AddNode(375, 304);
        AddNode(367, 301);
        AddNode(373.5f, 293);
        AddNode(371, 287);
        AddNode(357.5f, 287.5f);
    }

    void AddNode(float x, float y)
    {
        coastlineNodes.Append(new Vector2(x, y));
    }

    void Start()
    {
        cam = Camera.main;
        worldMap = GetComponent<WorldMap>();
        GenerateNodes();
    }

    // Update is called once per frame
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.A)
        {
            GameObject ship = Instantiate(shipPrefab, new Vector3(372, 305, 0), Quaternion.identity);
            ship.GetComponent<Movement>().nodes = coastlineNodes;
        }
    }
}
