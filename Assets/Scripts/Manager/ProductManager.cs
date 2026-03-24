using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProductManager : MonoBehaviour
{
    [SerializeField] private List<Order> products = new List<Order>();
    [SerializeField] private ProductDragger dragObject;
    [SerializeField] private Canvas canvas;
    private ObjectPool<ProductDragger> productsPool;

    public static ProductManager instance;
    void Awake()
    {
        instance = this;
        productsPool = new ObjectPool<ProductDragger>(
            createFunc: () => {
                ProductDragger p = Instantiate(dragObject,transform.position,Quaternion.identity,canvas.transform);
                return p;
            },
            actionOnGet: (p) => p.gameObject.SetActive(true),
            actionOnRelease: (p) => p.gameObject.SetActive(false),
            actionOnDestroy: (p) => Destroy(p.gameObject),
            maxSize: 20);
    }
    public List<Order> currentOrder()
    {
        return products;
    }

    public void setUpObject(Order order,Transform pos)
    {
        ProductDragger productDragger = productsPool.Get();
        productDragger.Configure(order,pos);
    }
    public void ReturnToPool(ProductDragger product)
    {
        productsPool.Release(product);
    }
}
