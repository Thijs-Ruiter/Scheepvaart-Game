using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Country homeCountry;
    public WorldMap map;
    public RouterScript routerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        homeCountry = Country.Netherlands;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RandomShipSpawning()
    {
        yield return new WaitForSeconds(Random.Range(20, 25));
        routerScript.SpawnShip(map.GetCountryLocalPosition((Country)Random.Range(0,1)));
    }
}
public enum CountryResources
{
    None, Potato, Gold, Iron, Wood, Nootmuskaat, Sugar
}

