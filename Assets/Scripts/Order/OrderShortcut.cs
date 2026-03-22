using UnityEngine;
using UnityEngine.UI;

public class OrderShortcut : MonoBehaviour
{
    [SerializeField] private Slider timer;
    [SerializeField] private Image sprites;
    public void Configure(Order order)
    {
        sprites.sprite = order.image;
    }
    public void UpdateTimer(float val)
    {
        timer.value = val;
    }
}
