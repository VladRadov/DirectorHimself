using UnityEngine;
using UnityEngine.UI;

public class ManagerIcon : MonoBehaviour
{
    [SerializeField] private Image _skin;

    public bool IsActiveIcon => _skin.color == Color.green ? true : false;

    public void SetIcon(Sprite sprite) => _skin.sprite = sprite;

    public void SetParent(Transform parent) => transform.SetParent(parent, false);

    public void SetActivateIcon(bool value) => _skin.color = value ? Color.green : Color.white;
}
