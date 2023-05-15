using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCartoonController : MonoBehaviour
{
    public void CreateObjectsCartoon(string nameCartoon)
    {
        var objectsCartoon = new GetObjectsCartoon(Player.Instance.Id, nameCartoon);
        objectsCartoon.Execute();

        var cartoon = Player.Instance.Cartoons.Find(cartoon => cartoon.Name == nameCartoon);
        Player.Instance.CurrentCartoon = cartoon;
        for (int i = 0; i < objectsCartoon.CounItems(); i++)
        {
            var id = Converter.ToInt(objectsCartoon.ParsingTableResult(i, 0));
            var name = Convert.ToString(objectsCartoon.ParsingTableResult(i, 1));
            var positionStartX = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 2)));
            var positionStartY = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 3)));
            var scaleX = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 4)));
            var scaleY = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 5)));
            var positionFinishX = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 6)));
            var positionFinishY = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 7)));
            var nameAnimation = Convert.ToString(objectsCartoon.ParsingTableResult(i, 9));
            var idLayer = Converter.ToInt(objectsCartoon.ParsingTableResult(i, 10));

            var item = PoolObjects<ManagerItem>.Find(findedItem => findedItem.DataItem.NameSkin == name);
            var savedObjectCartoon = item.LoadObjectCartoon();
            savedObjectCartoon.PositionStartX = positionStartX;
            savedObjectCartoon.PositionStartY = positionStartY;
            savedObjectCartoon.ScaleX = scaleX;
            savedObjectCartoon.ScaleY = scaleY;
            savedObjectCartoon.Id = id;
            savedObjectCartoon.NameAnimation = nameAnimation;
            savedObjectCartoon.SortingLayerID = idLayer;

            savedObjectCartoon.Selected();
            savedObjectCartoon.AllocationOnPlayWindows();
            savedObjectCartoon.SetStartPostion();
            savedObjectCartoon.SetScale();

            cartoon.ObjectsCartoon.Add(savedObjectCartoon);
        }
    }
}
