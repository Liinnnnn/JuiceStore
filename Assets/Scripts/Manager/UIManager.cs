using UnityEngine;

public class UIManager : MonoBehaviour,IGameStateManager
{
    [SerializeField] private GameObject MENU;
    [SerializeField] private GameObject ORDER;
    [SerializeField] private GameObject GAME;
    [SerializeField] private GameObject END;
    public void GameSateChangeCallback(GameState state)
    {
        switch (state)
        {
            case GameState.MENU: 
            setUIActive(MENU);
            break;

        case GameState.ORDER:
            setUIActive(ORDER);
            break;

        case GameState.GAME:
            setUIActive(GAME);
            break;

        case GameState.END:
            setUIActive(END);
            break;
        }
    }
    private void setUIActive(GameObject panel)
    {
        MENU.SetActive(false);
        ORDER.SetActive(false);
        GAME.SetActive(false);
        END.SetActive(false);

        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
}
