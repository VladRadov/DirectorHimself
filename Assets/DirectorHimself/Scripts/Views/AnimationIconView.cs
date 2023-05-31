using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationIconView : MonoBehaviour
{
    [SerializeField] private Image _iconAnimation;

    [SerializeField] private Text _nameAnimation;

    public Sprite Icon
    {
        set
        {
            _iconAnimation.sprite = value;
        }
    }

    public string Name
    {
        set
        {
            _nameAnimation.text = value;
        }
    }

    public void SetParent(Transform parent) => transform.SetParent(parent, false);
}