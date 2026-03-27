using UnityEngine;
using UnityEngine.UI;

public class ProductDragger : MonoBehaviour
{
    [SerializeField] private Image image;
    private RectTransform rect;
    private Canvas canvas;
    private float money;
    private Customer customer;
    private Order current;
    void Start()
    {
        customer = FindAnyObjectByType<Customer>(FindObjectsInactive.Exclude);
        rect = GetComponent<RectTransform>();
        canvas = FindAnyObjectByType<Canvas>();
    }
    public void Configure(Order order,Transform pos)
    {
        image.sprite = order.image;
        money = order.EstimatePrice;
        current = order;
        transform.position = pos.position;
    }
    void Update()
    {
        if(customer == null) 
        {
            Debug.Log("Nothing here");
            return; 
        }
        if(current != customer.GetOrder())
        {
            customer.SetCustomerState(CustomerState.WALK);
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,
            new Vector2((float) Screen.width/2,(float) Screen.height/2),
            5f * Time.deltaTime);
            ProductManager.instance.ReturnToPool(this);
            Debug.Log("Wrong order");
            GameManager.instance.combo = 0;
        }else
        {
            customer.SetCustomerState(CustomerState.WALK);
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,
            new Vector2((float) Screen.width/2,(float) Screen.height/2),
            5f * Time.deltaTime);
            CurrencyManager.instance.AddCurrencyFromSuccess(money);
            GameManager.instance.combo += 1;
            ProductManager.instance.ReturnToPool(this);
            Debug.Log("Correct order");
        }
    }
  
}
