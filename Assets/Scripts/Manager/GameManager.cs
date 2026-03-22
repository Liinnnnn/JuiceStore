using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Time.timeScale = 0;
        Debug.Log("End");
    }
}
