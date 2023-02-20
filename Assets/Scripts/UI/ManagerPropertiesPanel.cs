using UnityEngine;
using UnityEngine.Events;

public class ManagerPropertiesPanel : MonoBehaviour
{
    public UnityEvent<bool> ChangeActiveWindowEventHandler = new UnityEvent<bool>();

    public void SetActive(bool value)
    {
        ChangeActiveWindowEventHandler?.Invoke(value);
        this.gameObject.SetActive(value);
    }
}
