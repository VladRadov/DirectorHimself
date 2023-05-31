using UnityEngine;

public class ManagerObjectsCartoon : MonoBehaviour, IViewAllAnimations, IViewSelectedObjects
{
    [SerializeField] private ManagerPropertiesPanel _managerPropertiesPanel;

    [SerializeField] private ManagerIcon _prefabIcon;

    private void Start()
    {
        _managerPropertiesPanel.ChangeActiveWindowEventHandler.AddListener(AllocationOnPlayWindowsSelectedObjects);
        EventBus.Subscribe(this);
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

    public void HandleViewAllAnimations() => SetActiveSelectedObjects(false);

    public void ViewSelectedObjects() => SetActiveSelectedObjects(true);

    private void SetActiveSelectedObjects(bool value)
    {
        var selectedObjects = PoolObjectsCartoon.GetSelectedObjects();
        foreach (var selectedObject in selectedObjects)
        {
            if (selectedObject.IsSettingObject == false)
                selectedObject.SetActive(value);
            else
            {
                selectedObject.IsToolAnimation = !value;
                TransformObjectForShowAnimation(selectedObject, value);
            }
        }
    }

    private void TransformObjectForShowAnimation(ObjectCartoon objectCartoon, bool isActiveSelectedObjects)
    {
        if (!isActiveSelectedObjects)
        {
            objectCartoon.ScaleBeforeEntryAnimation = objectCartoon.transform.localScale;
            var reductionScaleObject = new Vector3(30.92f, 30.92f, 30.92f);
            var newPosition = new Vector3(-437f, -415f, -3682.8f);
            objectCartoon.SetLocalTransform(reductionScaleObject, newPosition);
        }
        else
        {
            var oldPosition = new Vector3(objectCartoon.PositionStartX, objectCartoon.PositionStartY, 0);
            objectCartoon.SetTransform(objectCartoon.ScaleBeforeEntryAnimation, oldPosition);
        }
    }
}
