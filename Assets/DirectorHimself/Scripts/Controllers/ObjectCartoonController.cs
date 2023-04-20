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
        for (int i = 0; i < objectsCartoon.CounItems(); i++)
        {
            var objectCartoon = new ObjectCartoon()
            {
                Id = Converter.ToInt(objectsCartoon.ParsingTableResult(i, 0)),
                Name = Convert.ToString(objectsCartoon.ParsingTableResult(i, 1)),
                PositionStartX = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 2))),
                PositionStartY = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 3))),
                ScaleX = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 4))),
                ScaleY = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 5))),
                PositionFinishX = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 6))),
                PositionFinishY = Converter.ToFloat(Convert.ToString(objectsCartoon.ParsingTableResult(i, 7))),
                NameAnimation = Convert.ToString(objectsCartoon.ParsingTableResult(i, 9))
            };
            cartoon.ObjectsCartoon.Add(objectCartoon);

            var item = PoolObjects<ManagerItem>.Find(findedItem => findedItem.DataItem.NameSkin == objectCartoon.Name);
            var savedObjectCartoon = item.LoadObjectCartoon();
            savedObjectCartoon.PositionStartX = objectCartoon.PositionStartX;
            savedObjectCartoon.PositionStartY = objectCartoon.PositionStartY;
            savedObjectCartoon.Id = objectCartoon.Id;

            savedObjectCartoon.Selected();
            savedObjectCartoon.AllocationOnPlayWindows();
            savedObjectCartoon.SetStartPostion();
        }
    }
}
