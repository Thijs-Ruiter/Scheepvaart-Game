using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

public class RouterScript : MonoBehaviour
{
    WorldMap worldMap;
    Camera cam;
    public Vector2[] coastlineNodes = new Vector2[43]
    {
        new Vector2(375, 304),
        new Vector2(367, 301),
        new Vector2(373.5f, 293),
        new Vector2(371, 287),
        new Vector2(357.5f, 287.5f),
        new Vector2(351.5f, 266),
        new Vector2(355, 260),
        new Vector2(345, 246),
        new Vector2(330, 233),
        new Vector2(331, 205),
        new Vector2(336, 190),
        new Vector2(358, 175),
        new Vector2(383, 180),
        new Vector2(395, 175),
        new Vector2(393, 166),
        new Vector2(402, 152),
        new Vector2(405, 141),
        new Vector2(400, 126),
        new Vector2(405, 109),
        new Vector2(412, 86),
        new Vector2(423, 72),
        new Vector2(445, 77),
        new Vector2(460, 91),
        new Vector2(466, 110),
        new Vector2(470, 120),
        new Vector2(481, 128.5f),
        new Vector2(480, 145),
        new Vector2(478, 154),
        new Vector2(495, 175),
        new Vector2(506, 195),
        new Vector2(517.5f, 214.5f),
        new Vector2(525, 231),
        new Vector2(537, 228),
        new Vector2(549.5f, 208),
        new Vector2(565.5f, 185.5f),
        new Vector2(577, 205),
        new Vector2(590, 225),
        new Vector2(601.5f, 212),
        new Vector2(609.5f, 199),
        new Vector2(609, 190),
        new Vector2(608, 175),
        new Vector2(628, 149),
        new Vector2(655, 138)
    };

    public List<Country> coastCountries = new List<Country>();

    public GameObject shipPrefab;
    public GameObject debugCircle;

    public Country lastSelectedCountry;

    public bool nodeVisualisation = true;

    public enum GameState
    {
        None,
        TargetCountrySelection
    }
    public GameState gameState;
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

    void Update()
    {
        switch(gameState)
        {
            case GameState.None:
                break;
            case GameState.TargetCountrySelection:
                if (worldMap.selectedCountry != Country.Empty)
                {
                    SpawnShip(worldMap.GetCountryLocalPosition(worldMap.selectedCountry));
                    gameState = GameState.None;
                }
                break;
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
            GameObject circle = Instantiate(debugCircle, pos, Quaternion.identity, transform);
            circle.transform.localPosition = pos;
        }
    }

    public void SpawnShip(Vector2 pos, Vector2 target) 
    {
        GameObject ship = Instantiate(shipPrefab, transform.position, Quaternion.identity, transform);
        ship.transform.localPosition = pos;
        ship.GetComponent<Movement>().endPoint = target;
    }

    public void SpawnShip(Vector2 target)
    {
        GameObject ship = Instantiate(shipPrefab, transform.position, Quaternion.identity, transform);
        ship.transform.localPosition = worldMap.GetCountryLocalPosition(lastSelectedCountry);
        ship.GetComponent<Movement>().endPoint = target;
    }

    public void SelectTarget()
    {
        lastSelectedCountry = worldMap.selectedCountry;
        worldMap.Clear();
        gameState = GameState.TargetCountrySelection;
    }

    private void CoastCountries()
    {
        coastCountries.Add(Country.Netherlands);
        coastCountries.Add(Country.United_Kingdom);
        coastCountries.Add(Country.France);
        coastCountries.Add(Country.Spain);
        coastCountries.Add(Country.Portugal);
        coastCountries.Add(Country.Morocco);
        coastCountries.Add(Country.Western_Sahara);
    }
}
