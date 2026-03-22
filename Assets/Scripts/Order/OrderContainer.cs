using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OrderContainer : MonoBehaviour
{
    [SerializeField] private GameObject orderContainer;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Price;
    [SerializeField] private Image image;
    [SerializeField] private Slider timer;
    public void Configure(Order order)
    {
        Name.text = "Name : " + order.OrderName;
        Price.text = "Estimate Payout: " +  order.EstimatePrice.ToString();
        image.sprite = order.image;        
        orderContainer.SetActive(true);
        timer.value = 1;

    }
    public void UpdateTimer(float val)
    {
        timer.value = val;
    }
}
