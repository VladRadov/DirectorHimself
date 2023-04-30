using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

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

    private ParametrsObjectCartoonController _currentParametersObjectCartoon;

    [SerializeField] private FlagObjectCartoon _flagObjectCartoon;

    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private ManagerIcon _prefabManagerIcon;

    [SerializeField] private ParametrsObjectCartoonController _parametrsObjectCartoonController;

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

    public float ScaleX { get { return _transform.localScale.x; } set { _transform.localScale = new Vector2(value, _transform.localScale.y); } }

    public float ScaleY { get { return _transform.localScale.y; } set { _transform.localScale = new Vector2(_transform.localScale.x, value); } }

    public string NameAnimation { get; set; }

    public float PositionFinishX { get; set; }

    public float PositionFinishY { get; set; }

    public bool IsChangedScale { get; set; }

    private void Awake()
    {
        _transform = transform;
        _isMove = false;
        _isSelected = false;

        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;

        Name = NameFlagObjectCartoon.ToString();
        PositionStartX = _transform.position.x;
        PositionStartY = _transform.position.y;
    }

    public void SetFlagHasAnimation(bool value) => _hasAnimation = value;

    public void SetStartPostion() => _transform.localPosition = new Vector3(PositionStartX, PositionStartY, 0);

    public void SetScale() => _transform.localScale = new Vector3(ScaleX, ScaleY, 0);

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

    private void OnMouseUp()
    {
        var changeStartPositionObjectCartoon = new ChangeStartPositionObjectCartoon(Id, _transform.localPosition.x, _transform.localPosition.y);
        changeStartPositionObjectCartoon.Execute();
    }

    private void DragAndDrop()
    {
        _transform.position = MousePosition - _deltaPosition;
        if (_hasAnimation)
            _currentParametersObjectCartoon.SetPosition((Vector2)_transform.position, _collider.size);
    }

    public void CreateSavePoint()
    {
        if (_hasAnimation)
        {
            PoolObjects<ParametrsObjectCartoonController>.DisactiveObjects();
            _currentParametersObjectCartoon = PoolObjects<ParametrsObjectCartoonController>.GetObject(_parametrsObjectCartoonController);

            _currentParametersObjectCartoon.SetPosition((Vector2)_transform.position, _collider.size);
            _currentParametersObjectCartoon.SetIcon(_icon);
            _currentParametersObjectCartoon.SaveSize.ChangingScaleEventHandler.RemoveAllListeners();
            _currentParametersObjectCartoon.SaveSize.ChangingScaleEventHandler.AddListener(ChangeSize);

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

    public void CreateIconOnTimemap()
    {
        if (_hasAnimation)
        {
            _icon = Instantiate(_prefabManagerIcon);
            _icon.SetIcon(_ImageIcon);
            _icon.SetParent(_timeline);
        }
    }

    private void ChangeSize(Vector3 newScale)
    {
        IsChangedScale = true;
        _transform.localScale += newScale;
    }

    private void OnMouseDown()
    {
        if (_isSelected && _isMove == false)
        {
            _deltaPosition = MousePosition - (Vector2)_transform.position;
            Player.Instance.CurrentCartoon.CurrentObjectCartoon = this;
            CreateSavePoint();
        }
        else if (_isSelected == false)
        {
            Selected();

            var addCartoonObject = new AddCartoonObject(Player.Instance.Id, Player.Instance.CurrentCartoon.Id, Name, this);
            addCartoonObject.Execute();
            Id = int.Parse(addCartoonObject.ParsingTableResult(0, 0).ToString());
            Player.Instance.CurrentCartoon.ObjectsCartoon.Add(this);
        }
    }

    public void Selected()
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