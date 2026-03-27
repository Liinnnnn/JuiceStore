using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    public static CurrencyManager instance;
    private float currency;
    void Start()
    {
        UpdateCurrencyTextUI();
    }
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        
    }
    public void AddCurrencyFromSuccess(float price)
    {
        float multiply = 1 + ((float) GameManager.instance.combo / 5);
        Debug.Log(multiply);
        if(GameManager.instance.combo <= 0) multiply = 1;
        currency += price * multiply;
        Debug.Log(currency);
        UpdateCurrencyTextUI();
        GameManager.instance.UpdateUI();
    }
    private void UpdateCurrencyTextUI()
    {
        currencyText.text = currency.ToString();
    }
}
