using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class TimeManager : MonoBehaviour
{
    [Header("General UI settings")]
    [SerializeField] private TextMeshProUGUI time;
    public static TimeManager instance;
    [Header("Viewing in Debug Mode")]
    [SerializeField] private float currentTime;
    [SerializeField] private float endTime;
    private float remainingTime;
    private float sec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time.text = currentTime.ToString() + ":00";
        remainingTime = endTime - currentTime;
    }
    void Awake()
    {
        instance =  this;
    }
    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        currentTimeUpdater();
    }
    private void currentTimeUpdater()
    {
        if(sec >= 60)
        {
            sec = 0;
            currentTime +=1;
            remainingTime -= 1;
            time.text = currentTime.ToString() + ":00";
            checkStopageGame();
        }
    }
    private void checkStopageGame()
    {
        if(currentTime >= endTime)
        {
            time.color = Color.red;
            GameManager.instance.EndGame();
            CustomerManager.instance.StopSpawning();
        }
    }
}
