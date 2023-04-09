using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerAddCartoon : MonoBehaviour
{
    [SerializeField] private InputField _inputNameCartoon;

    public string GetNameCartoon() => _inputNameCartoon.text;

    public IEnumerator SelectedErrorInputFieldAddCartoon()
    {
        var colorBlock = _inputNameCartoon.colors;
        var oldColorNormal = _inputNameCartoon.colors.normalColor;
        colorBlock.normalColor = new Color(1, 0.654717f, 0.654717f);
        _inputNameCartoon.colors = colorBlock;

        yield return new WaitForSeconds(1);

        colorBlock.normalColor = oldColorNormal;
        _inputNameCartoon.colors = colorBlock;
    }
}
