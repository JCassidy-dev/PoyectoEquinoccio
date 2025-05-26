using UnityEngine;

public class HealItems : MonoBehaviour
{
    public bool IsActive() => gameObject.activeSelf;
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
