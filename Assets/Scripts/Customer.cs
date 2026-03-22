using System;
using System.Collections;
using UnityEngine;
enum State
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
    [Header("Customer Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float patiences;
    private State CurrenState;
    private Order currentOrder;
    private float currentTimer;
    private bool isWaiting = false; 
    public Action<Customer> OnReachEnd;
    void Start()
    {
        CurrenState = State.IDLE;
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
            CurrenState = State.WAITING;
        }
    }
    
    private IEnumerator Waiting()
    {
        isWaiting = true;
        currentOrder = OrderManager.instance.getRandomOrder();
        OrderManager.instance.SetUpOrder(currentOrder);
        yield return new WaitForSeconds(patiences);
        CurrenState = State.WALK;
        isWaiting = false;
    }

    private void leftStand()
    {
        transform.position = Vector3.MoveTowards(transform.position,end.position,speed * Time.deltaTime);
        if(Vector3.Distance(transform.position,end.position) <0.1f)
        {
            OnReachEnd?.Invoke(this);
        }
    }
    private void SwitchState()
    {
        switch (CurrenState)
        {
            case State.IDLE:
                goToStand();
                break;
            case State.WAITING:
                currentTimer+= Time.deltaTime;
                OrderManager.instance.UpdateUI(CalculatePatiences());
                if (!isWaiting) 
                {
                    StartCoroutine(Waiting());
                }
                break;
            case State.WALK:
                OrderManager.instance.ClearOrder();
                leftStand();
                break;
        }
    }
    public void ResetState()
    {
        CurrenState = State.IDLE;
        currentTimer = 0f;
        isWaiting = false;
    }
    private float CalculatePatiences()
    {
        float val = Mathf.Clamp01(currentTimer/patiences);
        return val;
    }
}
