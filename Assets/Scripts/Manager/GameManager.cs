using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public int combo = -1;
    [SerializeField] private TextMeshProUGUI ComboText;
    void Start()
    {
        SetGameSate(GameState.MENU);
        ComboText.text = "";
    }
    void Awake()
    {
        instance = this;
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
    public void UpdateUI()
    {
        if (combo <= 0)
        {
            return;   
        }
        ComboText.text = "x" + (1 + (float) combo/5).ToString();
    }
}
public interface IGameStateManager
{
    public void GameSateChangeCallback(GameState state);
}
