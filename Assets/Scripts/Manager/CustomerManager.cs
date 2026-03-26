using System;
using UnityEngine;
using UnityEngine.Pool;

public class CustomerManager : MonoBehaviour,IGameStateManager
{

    [SerializeField] private Customer Customer;
    [SerializeField] private Transform SpawnZone;
    [SerializeField] private Transform parent;
    public static CustomerManager instance;
    private ObjectPool<Customer> customerPool;
    void Start()
    {
    }
    void Awake()
    {
        instance = this;
        customerPool = new ObjectPool<Customer>(
            createFunc: () => {
                Customer c = Instantiate(Customer,parent);
                c.OnReachEnd = (customer) => {OnCustomerLeft(customer);};
                return c;
            },
            actionOnGet: (c) => c.gameObject.SetActive(true),
            actionOnRelease: (c) => c.gameObject.SetActive(false),
            actionOnDestroy: (c) => Destroy(c.gameObject),
            maxSize: 20
        );
    }
    private void OnCustomerLeft(Customer customer)
    {
        customerPool.Release(customer);
        customer.ResetState();
        Spawn();
    }
    public void Spawn()
    {
        Customer guest = customerPool.Get();
        guest.transform.position = SpawnZone.position; 
    }
    public void StopSpawning()
    {
        customerPool.Clear();
    }

    public void GameSateChangeCallback(GameState state)
    {
        switch (state)
        {
            case GameState.ORDER:
            case GameState.MENU:
                break; 
            case GameState.END:
                StopSpawning();
                break;

            case GameState.GAME:
                Spawn();
                break;
        }
    }
}
