using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OrderContainer : MonoBehaviour
{
    [SerializeField] private GameObject orderObj;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Price;
    [SerializeField] private Image sprite;
    public void Configure(Order order)
    {
        Name.text = "Name : " + order.OrderName;
        Price.text = "Estimate Payout: " +  order.EstimatePrice.ToString();
        sprite.sprite = order.image;        
        orderObj.SetActive(true);
    }
}
