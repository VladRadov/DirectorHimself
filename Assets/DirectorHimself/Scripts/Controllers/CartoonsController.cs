using System.Collections;
using UnityEngine.Events;

public class CartoonsController
{
    private ManagerCartoonsPanel _managerCartoonsPanel;

    public CartoonsController(ManagerCartoonsPanel managerCartoonsPanel)
    {
        _managerCartoonsPanel = managerCartoonsPanel;
    }

    public Cartoon CreateCartoon(int id, string name)
    {
        var cartoon = new Cartoon()
        {
            Id = id,
            Name = name
        };

        return cartoon;
    }

    public void ShowAddedCartoon(string nameCartoon) => _managerCartoonsPanel.CreateViewCartoonName(nameCartoon);

    public void AddListenerForAddCartoonEventhandler(UnityAction<string> action) => _managerCartoonsPanel.AddCartoonEventhandler.AddListener(action);

    public void GetSavedCartoons(IEnumerable cartoons)
    {
        foreach (ICartoon cartoon in cartoons)
            _managerCartoonsPanel.CreateViewCartoonName(cartoon.Name); 
    }

    public void ShowCartoonsPanel() => _managerCartoonsPanel.ShowCartoons(true);

    public void SelectedErrorInputFieldAddCartoon() => _managerCartoonsPanel.SelectedErrorInputFieldAddCartoon();
}
