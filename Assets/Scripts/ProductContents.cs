using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

public class ProductContents : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    private Transform currentPos;
    private Order current;
    private string pname;
    void Start()
    {
        ZoneManager.instance.onReachZone += setButton;
    }
    void OnDestroy()
    {
        ZoneManager.instance.onReachZone -= setButton;
    }
    public void Configure(Order order)
    {
        image.sprite = order.image;
        pname = order.OrderName;
        current = order;
        currentPos = transform;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(()=>SpawnTaggable());
        button.interactable = false;
    }
    private void SpawnTaggable()
    {
        if(current == null)
        {
            return;
        }
        Debug.Log("Spawing");
        ProductManager.instance.setUpObject(current,currentPos);
    }
    private void setButton()
    {
        if(ZoneManager.instance.GetCustomer() == null)
        {
            button.interactable = false;
            return;
        }
        button.interactable = true;
    }
}
