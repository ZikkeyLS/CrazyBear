using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DataStorage : MonoBehaviour
{
    [SerializeField] private GameData _data = new GameData();

    public void AddLocation(Location location) => _data.Locations.Add(location);
    public void ClearLocations() => _data.Locations.Clear();
}

[Serializable]
public class GameData
{
    [ReadOnly] public int GlobalScore;
    [ReadOnly] public float CurrentScore;
    [ReadOnly] public List<Location> Locations = new List<Location>();
}

#if UNITY_EDITOR
[CustomEditor(typeof(DataStorage))]
public class DataStorageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Update Scriptable Data"))
            AutoAttach();
    }

    private void AutoAttach()
    {
        DataStorage storage = (DataStorage)target;

        ClearGlobals(storage);

        List<LocationData> locations = ScriptableObjectUtilities.FindAllScriptableObjectsOfType<LocationData>();
        foreach (LocationData location in locations)
            storage.AddLocation(new Location(location, new LocationState()));
    }

    private void ClearGlobals(DataStorage storage)
    {
        storage.ClearLocations();
    }
}

public static class ScriptableObjectUtilities
{

    public static List<T> FindAllScriptableObjectsOfType<T>(string folder = "Assets")
        where T : ScriptableObject
    {
        return AssetDatabase.FindAssets("t:" + typeof(T).FullName, new[] { folder })
            .Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }
}
#endif
