using System;
using System.Collections;
using UnityEngine;
public enum CustomerState
{
    IDLE,
    WAITING,
    WALK
}
public class Customer : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform stand;
    [SerializeField] private Transform end;
    [SerializeField] private Collider2D colder;
    [Header("Customer Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float patiences;
    private Order currentOrder;
    private CustomerState CurrenState;
    private float currentTimer;
    private bool isWaiting = false; 
    public Action<Customer> OnReachEnd;
    void Start()
    {
        CurrenState = CustomerState.IDLE;
    }

    void Update()
    {
        SwitchState();
    }

    private void goToStand()
    {
        transform.position = Vector3.MoveTowards(transform.position,stand.position,speed * Time.deltaTime);
        if(Vector3.Distance(transform.position,stand.position) <= 0.1f)
        {
            CurrenState = CustomerState.WAITING;
            ZoneManager.instance.AppendTo(this);
        }
    }
    
    private IEnumerator Waiting()
    {
        isWaiting = true;
        currentOrder = OrderManager.instance.getRandomOrder();
        OrderManager.instance.SetUpOrder(currentOrder);
        yield return new WaitForSeconds(patiences);
        CurrenState = CustomerState.WALK;
        ZoneManager.instance.ResetZone();
        isWaiting = false;
    }

    private void leftStand()
    {
        transform.position = Vector3.MoveTowards(transform.position,end.position,speed * Time.deltaTime);
        StopAllCoroutines();
        if(Vector3.Distance(transform.position,end.position) <0.1f)
        {
            OnReachEnd?.Invoke(this);
        }
    }
    private void SwitchState()
    {
        switch (CurrenState)
        {
            case CustomerState.IDLE:
                goToStand();
                break;
            case CustomerState.WAITING:
                currentTimer+= Time.deltaTime;
                OrderManager.instance.UpdateUI(CalculatePatiences());
                if (!isWaiting) 
                {
                    StartCoroutine(Waiting());
                }
                break;
            case CustomerState.WALK:
                OrderManager.instance.ClearOrder();
                leftStand();
                break;
        }
    }
    public void ResetState()
    {
        CurrenState = CustomerState.IDLE;
        currentTimer = 0f;
        isWaiting = false;
    }
    public void SetCustomerState(CustomerState state)
    {
        CurrenState = state;
    }
    public Order GetOrder()
    {
        return currentOrder;
    }
    public Vector3 getCenter()
    {
        return colder.bounds.center;
    }
    private float CalculatePatiences()
    {
        float val = Mathf.Clamp01(currentTimer/patiences);
        return val;
    }
}
