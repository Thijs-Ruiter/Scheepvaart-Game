using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CountryMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    public WorldMap map;

    void Update()
    {
        switch (mouse_over)
        {
            case true:
                map.canInteracte = false;
                break;
            case false:
                map.canInteracte = true;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }

    void OnDisable()
    {
        map.canInteracte = true;
        mouse_over = false;
    }
}
