using UnityEngine;

public class ManagerObjectsCartoon : MonoBehaviour
{
    [SerializeField] private ManagerPropertiesPanel _managerPropertiesPanel;

    [SerializeField] private ManagerIcon _prefabIcon;

    private void Start()
    {
        var instance = Connection.Instance;
        _managerPropertiesPanel.ChangeActiveWindowEventHandler.AddListener(AllocationOnPlayWindowsSelectedObjects);
    }

    public void AllocationOnPlayWindowsSelectedObjects(bool isVisibleWindow)
    {
        PoolObjects<SavePoint>.DisactiveObjects();
        var selectedObjects = PoolObjectsCartoon.GetSelectedObjects();

        foreach (var selectedObject in selectedObjects)
        {
            if (isVisibleWindow == false)
            {
                selectedObject.AllocationOnPlayWindows();
                if (selectedObject.IsActiveIcon)
                    selectedObject.CreateSavePoint();
            }

            selectedObject.SetActive(!isVisibleWindow);
        }
    }
}
