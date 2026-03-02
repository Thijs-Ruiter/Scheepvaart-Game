using UnityEditor.Media;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{

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
    public enum GameState
    {
        Pre0, A00, C00, A01, A02, C10
        // Naming Scheme, first number is choice stage, second number determines which stage within the choice stage
    }
    public GameState currentGameState = GameState.Pre0;
    private bool decisionMade = false;
    private int buttonPressed;
    public AnimationPlayer animationPlayer;
    public GameObject decisionUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animationPlayer = GetComponent<AnimationPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState) 
        {
            case GameState.Pre0: // Menu Screen
                if (buttonPressed == 1)
                {
                    OnChangeState(GameState.A00);
                }
                break;
            case GameState.A00: // Leadup Animation
                if (animationPlayer.vp.time == animationPlayer.vp.length)
                {
                    OnChangeState(GameState.C00);
                }
                break;
            case GameState.C00: // First decision
                if (decisionMade && buttonPressed != 0)
                {
                    decisionMade = false;
                    if (buttonPressed == 1)
                    {
                        OnChangeState(GameState.A01);
                    } 
                    else if (buttonPressed == 2)
                    {
                        OnChangeState(GameState.A02);
                    } 
                    else
                    {
                        Debug.Log("Invalid button pressed");
                    }
                }
                break;
            case GameState.A01: // Transfer onto the ship Animation
                break;
            case GameState.A02: // Minigame start Animation
                break;
            case GameState.C10:
                break;
        }
    }

    public void OnChangeState(GameState newState)
    {
        currentGameState = newState;
        switch (currentGameState)
        {
            case GameState.Pre0:
                break;
            case GameState.A00:
                animationPlayer.PlayAnimation(0);
                break;
            case GameState.C00:
                decisionUI.SetActive(true);
                break;
            case GameState.A01:
                animationPlayer.PlayAnimation(1);
                break;
            case GameState.A02:
                animationPlayer.PlayAnimation(2);
                break;
            case GameState.C10:
                break;
        }
    }

    public void OnButton(int buttonNumber)
    {
        decisionMade = true;
        buttonPressed = buttonNumber;
    }
}
