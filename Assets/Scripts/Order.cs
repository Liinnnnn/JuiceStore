using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "Scriptable Objects/Order")]
public class Order : ScriptableObject
{
    public string OrderName;
    public Sprite image;
    public float EstimatePrice;
}
