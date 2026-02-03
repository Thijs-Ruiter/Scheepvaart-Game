using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text[] buttons;
    public ButtonSet[] buttonSets;
    void Start()
    {
        buttonSets[0] = new ButtonSet(2, new string[2] { "Cooperate", "Escape" } );
        buttonSets[1] = new ButtonSet(3, new string[3] { "Sleep", "Talk", "Music" });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class ButtonSet
    {
        int buttonAmount;
        TMP_Text[] buttons;
        string[] buttonTexts;

        public ButtonSet(int buttonAmount, string[] buttonTexts)
        {
            this.buttonAmount = buttonAmount;
            this.buttonTexts = buttonTexts;
        }
    }
}
