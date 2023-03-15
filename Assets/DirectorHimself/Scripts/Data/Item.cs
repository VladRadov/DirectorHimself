using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite _icon;

    [SerializeField] private string _nameSkin;

    [SerializeField] private ObjectCartoon _mainObject;

    [SerializeField] private GameObject _prefabIcon;

    [SerializeField] private bool _hasAnimation;

    public Sprite Icon => _icon;

    public string NameSkin => _nameSkin;

    public ObjectCartoon MainObject => _mainObject;

    public GameObject PrefabIcon => _prefabIcon;

    public bool HasAnimation => _hasAnimation;
}
