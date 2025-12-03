using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UiDataUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HighlightText,selectText;
    [SerializeField] GameObject countryMenu;
    [SerializeField] WorldMap map;
    // Start is called before the first frame update
    void Start()
    {
        map.onHighlightCountry += ChangeHighlightText;
        map.onSelectCountry += ChangeSelectText;
        map.onUnselectCountry += ChangeSelectText;
    }

    // Update is called once per frame
    void Update()
    {
        if (map.selectedCountry != Country.Empty)
        {
            ChangeCountryMenuState(true);
        } 
        else
        {
            ChangeCountryMenuState(false);
        }
    }
    void ChangeHighlightText(object sender,EventArgs e)
    {
        HighlightText.text = map.GetCountryName(map.highlightedCountry);
    }
    void ChangeSelectText(object sender,EventArgs e)
    {
        selectText.text = map.GetCountryName(map.selectedCountry);
    }
    void ChangeCountryMenuState(bool state)
    {
        countryMenu.SetActive(state);
    }
}
