using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum GameState
{
    MENU,
    ORDER,
    GAME,
    END
}
public class GameManager : MonoBehaviour{
    public static GameManager instance;
    private GameState currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetGameSate(GameState.MENU);
    }
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndGame()
    {
        SetGameSate(GameState.END);
        Debug.Log("End");
    }
    public void StartGame()
    {
        SetGameSate(GameState.GAME);
    }
    public void SetGameSate(GameState state)
    {
        IEnumerable<IGameStateManager> enumerator = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateManager>();
        foreach (IGameStateManager item in enumerator)
        {
            item.GameSateChangeCallback(state);
        }
    }
}
public interface IGameStateManager
{
    public void GameSateChangeCallback(GameState state);
}
