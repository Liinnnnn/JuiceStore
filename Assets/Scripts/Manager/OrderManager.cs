using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Order> orders = new List<Order>();
    public static OrderManager instance;
    [SerializeField] private OrderContainer orderContainer;
    [SerializeField] private OrderShortcut orderShortcut;
    void Awake()
    {
        instance = this;
        orderContainer.gameObject.SetActive(false);
        orderShortcut.gameObject.SetActive(false);
    }
    public Order getRandomOrder()
    {
        return orders[Random.Range(0,orders.Count)];
    }
    public void SetUpOrder(Order order)
    {
        orderContainer.gameObject.SetActive(true);
        orderContainer.Configure(order);
        orderShortcut.Configure(order);
    }
    public void ClearOrder()
    {
        orderContainer.gameObject.SetActive(false);
        orderShortcut.gameObject.SetActive(false);
    }
    public void UpdateUI(float val)
    {
        orderContainer.UpdateTimer(val);
        orderShortcut.UpdateTimer(val);
    }
}
