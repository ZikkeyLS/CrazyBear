using DG.Tweening;
using UnityEngine;

public enum Swipe : byte
{
    none = 0,
    left = 1,
    right = 2
}

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private float _swipeDuration = 2f;
    [SerializeField] private RectTransform _container;
    [SerializeField] private TMPro.TMP_Text _levelName;
    [SerializeField] private LevelUI _levelTemplate;

    private LevelUI _current;
    private LocationData _currentLocation;
    private DataStorage _data;

    private bool _transition = false;

    public void Initialize(DataStorage data)
    {
        _data = data;
        LoadLevel(Swipe.none, data.GetCurrentLocation());
    }

    public void ArrowClick(bool left)
    {
        if (_transition)
            return;

        LoadLevel(left ? Swipe.left : Swipe.right, _data.GetLocationByOrder(left));
    }

    public void LoadLevel(Swipe swipe, LocationData location)
    {
        _currentLocation = location;

        _levelName.text = _currentLocation.Name;
        SwipeLevels(swipe);
    }

    private void SwipeLevels(Swipe swipe)
    {
        if (swipe != Swipe.none)
        {
            MoveOldLevel(swipe);
            _transition = true;
        }

        MoveNewLevel(swipe);
    }

    private void MoveOldLevel(Swipe swipe)
    {
        if (_current != null)
            _current.transform.DOLocalMoveX(swipe == Swipe.left ? _container.sizeDelta.x : -_container.sizeDelta.x, _swipeDuration);
    }

    private void MoveNewLevel(Swipe swipe)
    {
        LevelUI updated = Instantiate(_levelTemplate, _container);
        updated.SetBackground(_currentLocation.Background);

        if (swipe == Swipe.none)
            TransitionComplete(updated);
        else
        {
            updated.transform.DOLocalMove(new Vector3(swipe == Swipe.left ? -_container.sizeDelta.x : _container.sizeDelta.x, 0), 0);
            updated.transform.DOLocalMoveX(0, _swipeDuration).OnComplete(() => TransitionComplete(updated));
        }

    }

    private void TransitionComplete(LevelUI updated)
    {
        if (_current != null)
            Destroy(_current.gameObject);

        _current = updated;
        _transition = false;
    }

}
