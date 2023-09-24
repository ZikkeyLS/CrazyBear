using System;
using UnityEngine;

[Serializable]
public class Location
{
    [NonSerialized] public LocationData Data;
    public LocationState State;

    public Location(LocationData data, LocationState state)
    {
        Data = data;
        State = state;
    }
}

[CreateAssetMenu(fileName = "Location", menuName = "Game/Location")]
public class LocationData : ScriptableObject
{
    public string Name;
    public int ScoreCost;

    public LocationData(string name, int scoreCost)
    {
        Name = name;
        ScoreCost = scoreCost;
    }
}

[Serializable]
public class LocationState
{
    public bool Unlocked = false;
}
