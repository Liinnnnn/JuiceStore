using System;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager instance;
    [SerializeField] private Customer customer;
    public event Action onReachZone;  
    void Awake()
    {
        instance = this;
    }
    public void AppendTo(Customer c)
    {
        customer = c;
        onReachZone?.Invoke();
    }
    public void ResetZone()
    {
        customer = null;
    }
    public Customer GetCustomer()
    {
        return customer;
    }
}
