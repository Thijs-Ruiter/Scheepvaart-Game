using UnityEditor.Media;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start, Animation, Decision, Minigame
    }
    public GameState currentGameState = GameState.Start;
    public int gameStage = -1;
    private bool decisionMade = false;
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
            case GameState.Start:
                if (gameStage == 0)
                {
                    OnAnimation();
                }
                break;
            case GameState.Animation:
                if (animationPlayer.vp.time == animationPlayer.vp.length)
                {
                    OnDecision();
                }
                break;
            case GameState.Decision:
                if (decisionMade)
                {
                    decisionMade = false;
                    if (gameStage == 1)
                    {
                        OnMinigame();
                    } 
                    else
                    {
                        OnAnimation();
                    }
                }
                break;
            case GameState.Minigame:

                break;
        }
    }

    public void IncreaseGameStage(int value)
    {
        gameStage += value;
    }

    // GameStages:
    // Before Starting = -1
    // StartingAnimation = 0
    // EscapeMinigame = 1
    // Boarding = 2
    // 

    public void OnButton(int buttonNumber)
    {
        decisionMade = true;
        switch (buttonNumber)
        {
            case 0:
                IncreaseGameStage(1);
                break;
            case 1:
                IncreaseGameStage(2);
                break;
            case 2:
                IncreaseGameStage(3);
                break;
            default:
                break;
        }
    }

    void OnAnimation()
    {
        currentGameState = GameState.Animation;
        animationPlayer.PlayAnimation(gameStage);
    }

    void OnDecision()
    {
        currentGameState = GameState.Decision;
        decisionUI.SetActive(true);
    }

    void OnMinigame()
    {
        currentGameState = GameState.Minigame;
    }
}
