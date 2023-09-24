using System;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    [SerializeField] private LocationData[] _locations = new LocationData[0];
    [SerializeField] private GameData _save = new GameData();

    private void Awake()
    {
        _save.LocationStates = new LocationState[_locations.Length];
        for (int i = 0; i < _locations.Length; i++)
            _save.LocationStates[i] = new LocationState() { LocationID = _locations[i].GetInstanceID(), Unlocked = i == 0 };
    }

    public LocationData GetCurrentLocation() => _locations[_save.CurrentLocationIndex];

    public LocationData GetLocationByOrder(bool left)
    {
        if (left)
            _save.CurrentLocationIndex = _save.CurrentLocationIndex == 0 ? _locations.Length - 1 : _save.CurrentLocationIndex - 1;
        else
            _save.CurrentLocationIndex = _save.CurrentLocationIndex == _locations.Length - 1 ? 0 : _save.CurrentLocationIndex + 1;

        return _locations[_save.CurrentLocationIndex];
    }

    public void ClearLocations() => _locations = null;
}

[Serializable]
public class GameData
{
    [ReadOnly] public int GlobalScore;
    [ReadOnly] public float CurrentScore;
    [ReadOnly] public int CurrentLocationIndex = 0;
    [ReadOnly] public LocationState[] LocationStates = new LocationState[0];
}
