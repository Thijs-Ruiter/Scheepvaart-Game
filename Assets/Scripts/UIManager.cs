using UnityEngine;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text[] buttons;
    public ButtonSet[] buttonSets;

    // GameStages:
    // 0 - 99 = Animations
    // 0 = Starting Animation
    // 1 = Boarding Animation
    // 2 = Alternate Boarding Animation
    // 11 = Sleep Animation
    // 12 = Talk Animation
    // 13 = Music Animation
    // 100 - 199 = Decisions
    // 200 - 299 = Minigames

    void Start()
    {
        buttonSets[0] = new ButtonSet(2, new string[2] { "Cooperate", "Escape" }, new int[] { 1, 200 } );
        buttonSets[1] = new ButtonSet(3, new string[3] { "Sleep", "Talk", "Music" }, new int[] { 11, 12, 13 } );
    }

    public class ButtonSet
    {
        int buttonAmount;
        TMP_Text[] buttons;
        string[] buttonTexts;
        int[] gameStages;

        public ButtonSet(int buttonAmount, string[] buttonTexts, int[] gameStages)
        {
            this.buttonAmount = buttonAmount;
            this.buttonTexts = buttonTexts;
            this.gameStages = gameStages;
        }
    }
}
