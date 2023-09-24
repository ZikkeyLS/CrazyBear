using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelSelection _levelSelection;
    [Inject] private DataStorage _data;

    private void Start()
    {
        _levelSelection.Initialize(_data);
    }
}
