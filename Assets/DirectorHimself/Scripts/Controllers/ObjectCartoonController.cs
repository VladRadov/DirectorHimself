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
            var idLayer = Converter.ToInt(objectsCartoon.ParsingTableResult(i, 9));

            var item = PoolObjects<ManagerItem>.Find(findedItem => findedItem.DataItem.NameSkin == name);

            var objectCartoon = new BuilderCartoonObject(item.LoadObjectCartoon())
                .SetPositionStart(positionStartX, positionStartY)
                .SetScale(scaleX, scaleY)
                .SetId(id)
                .SetSortingLayerID(idLayer)
                .Build();

            objectCartoon.Selected();
            objectCartoon.AllocationOnPlayWindows();
            objectCartoon.SetStartPostion();
            objectCartoon.SetScale();

            var getAnimation = new GetAnimation(objectCartoon.Id);
            getAnimation.Execute();
            for (int j = 0; j < getAnimation.CounItems(); j++)
            {
                var idAnimation = getAnimation.ParsingTableResult(j, 0);
                var nameAnimation = getAnimation.ParsingTableResult(j, 1);
                var nameGroupAnimtions = getAnimation.ParsingTableResult(j, 2);
                var duration = getAnimation.ParsingTableResult(j, 3);
                var quantity = getAnimation.ParsingTableResult(j, 4);

                var animation = new Animation()
                {
                    ID = Converter.ToInt(idAnimation),
                    Name = nameAnimation.ToString(),
                    GroupAnimation = Converter.ToEnumAnimationGroup(nameGroupAnimtions.ToString()),
                    Duration = Converter.ToFloat(duration),
                    Quantity = Converter.ToInt(quantity)
                };
                objectCartoon.UpdateAnimtaion(animation);
            }

            cartoon.ObjectsCartoon.Add(objectCartoon);
        }
    }
}
