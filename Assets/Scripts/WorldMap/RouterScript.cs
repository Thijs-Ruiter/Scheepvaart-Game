using UnityEngine;
using UnityEngine.UI;

public class RouterScript : MonoBehaviour
{
    WorldMap worldMap;
    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        worldMap = GetComponent<WorldMap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos1 = worldMap.transform.InverseTransformPoint(worldMap.GetCountryPosition(worldMap.selectedCountry));
        Vector3 pos2 = worldMap.transform.InverseTransformPoint(worldMap.GetCountryPosition(worldMap.highlightedCountry));
        pos1.z -= 1;
        pos2.z -= 1;
        Debug.DrawLine(pos1, pos2);
    }
}
