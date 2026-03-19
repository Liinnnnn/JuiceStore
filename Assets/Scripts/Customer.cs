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
    [SerializeField] private Animator animator;
    [SerializeField] private Transform stand;
    [SerializeField] private Transform end;
    [SerializeField] private float speed;
    public Action<Customer> OnTaskComplete;
    private State CurrenState;
    private bool isWaiting = false; 
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
        if(Vector3.Distance(transform.position,stand.position) <0.1f)
        {
            CurrenState = State.WAITING;
        }
    }
    
    private IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(5f);
        CurrenState = State.WALK;
        isWaiting = false;
    }

    private void leftStand()
    {
        transform.position = Vector3.MoveTowards(transform.position,end.position,speed * Time.deltaTime);
        if(Vector3.Distance(transform.position,end.position) <0.1f)
        {
            OnTaskComplete?.Invoke(this);
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
                if (!isWaiting) 
                {
                    StartCoroutine(Waiting());
                }
                break;
            case State.WALK:
                leftStand();
                break;
        }
    }

}
