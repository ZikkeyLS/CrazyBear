using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Game/Location")]
public class LocationData : ScriptableObject
{
    public string Name;
    public int ScoreCost;
    public Sprite Background;
}

[Serializable]
public class LocationState
{
    public int LocationID = 0;
    public bool Unlocked = false;
}
