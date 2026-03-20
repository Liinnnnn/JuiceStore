using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Order> orders = new List<Order>();
    public static OrderManager instance;
    [SerializeField] private OrderContainer orderContainer;
    void Awake()
    {
        instance = this;
    }
    public Order getRandomOrder()
    {
        return orders[Random.Range(0,orders.Count)];
    }
    public void SetUpOrder(Order order)
    {
        orderContainer.Configure(order);
    }
}
