using UnityEngine;
using UnityEngine.UI;

public class ProductContainer : MonoBehaviour
{
    [SerializeField] private ProductContents contents;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetContents();
    }
    private void GetContents()
    {
        foreach (Order order in ProductManager.instance.currentOrder())
        {
            ProductContents productContents = Instantiate(contents,gameObject.transform);
            productContents.Configure(order);
        }
    }
}
