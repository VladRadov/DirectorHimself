using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(SpriteRenderer))]

public class ObjectCartoon : MonoBehaviour, IObjectCartoon
{
    private bool _isMove;

    private bool _isSelected;

    private bool _hasAnimation;

    private Transform _transform;

    private Sprite _ImageIcon;

    private Transform _timeline;

    private Vector2 _deltaPosition;

    private ManagerIcon _icon;

    private SavePoint _currentSavePoint;

    [SerializeField] private FlagObjectCartoon _flagObjectCartoon;

    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private ManagerIcon _prefabManagerIcon;

    [SerializeField] private SavePoint _prefabSavePoint;

    [SerializeField] private CapsuleCollider2D _collider;

    public FlagObjectCartoon NameFlagObjectCartoon { get => _flagObjectCartoon; }

    public Transform PlayWindow { get; set; }

    public bool IsSelected => _isSelected;

    public bool IsActiveIcon => _icon == null ? false : _icon.IsActiveIcon;

    public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public int Id { get; set; }

    public string Name { get; set; }

    public float PositionStartX { get; set; }

    public float PositionStartY { get; set; }

    public float ScaleX { get; set; }

    public float ScaleY { get; set; }

    public string NameAnimation { get; set; }

    public float PositionFinishX { get; set; }

    public float PositionFinishY { get; set; }

    private void Start()
    {
        _transform = transform;
        _isMove = false;
        _isSelected = false;

        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        Name = NameFlagObjectCartoon.ToString();
        PositionStartX = _transform.position.x;
        PositionStartY = _transform.position.y;
        ScaleX = _transform.localScale.x;
        ScaleY = _transform.localScale.y;
    }

    public void SetFlagHasAnimation(bool value) => _hasAnimation = value;

    private void FixedUpdate()
    {
        if (_isMove)
            GoingObjectToWindow();
    }

    private void OnMouseDrag()
    {
        if(_isMove == false)
            DragAndDrop();
    }

    private void DragAndDrop()
    {
        _transform.position = MousePosition - _deltaPosition;
        if (_hasAnimation)
            _currentSavePoint.SetPosition((Vector2)_transform.position, _collider.size);
    }

    public void CreateSavePoint()
    {
        if (_hasAnimation)
        {
            PoolObjects<SavePoint>.DisactiveObjects();
            _currentSavePoint = PoolObjects<SavePoint>.GetObject(_prefabSavePoint);

            _currentSavePoint.SetPosition((Vector2)_transform.position, _collider.size);
            _currentSavePoint.SetIcon(_icon);

            _icon.SetActivateIcon(true);
        }
    }

    private void GoingObjectToWindow()
    {
        _rigidbody.velocity = Vector2.right * Time.deltaTime * 500;
    }

    private void StopMoving()
    {
        _isMove = false;
        _rigidbody.velocity = Vector2.zero;
    }

    public void AllocationOnPlayWindows()
    {
        _transform.parent = PlayWindow;
        SetPosition();
        StopMoving();
    }

    private void SetPosition() => _transform.position = gameObject.activeSelf ? new Vector3(0, 0, 0) : _transform.position;

    public void SetActive(bool value) => gameObject.SetActive(value);

    public void SetIcon(Sprite icon) => _ImageIcon = icon;

    public void SetPanelTimeline(Transform timeline) => _timeline = timeline;

    private void CreateIconOnTimemap()
    {
        if (_hasAnimation)
        {
            _icon = Instantiate(_prefabManagerIcon);
            _icon.SetIcon(_ImageIcon);
            _icon.SetParent(_timeline);
        }
    }

    private void OnMouseDown()
    {
        if (_isSelected && _isMove == false)
        {
            _deltaPosition = MousePosition - (Vector2)_transform.position;
            CreateSavePoint();
        }
        else if (_isSelected == false)
            Selected();
    }

    private void Selected()
    {
        _isSelected = true;
        _isMove = true;
        CreateIconOnTimemap();
    }

    private void OnBecameInvisible()
    {
        if (_isSelected && gameObject.activeSelf)
            StopMoving();
    }
}