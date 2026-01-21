using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start, Decision, Minigame
    }
    public GameState currentGameState = GameState.Start;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameState) 
        {
            case GameState.Start:
                // if animation finished: currentGameState == GameState.Decision
                break;
            case GameState.Decision:
                // 
                break;
            case GameState.Minigame:
                break;
        }
    }
}
