using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private int _points;
    [SerializeField] private string _timeSaveData;

    public int Points { get { return _points; } set { _points = value; } }
    public string TimeSaveData { get { return _timeSaveData; } set { _timeSaveData = value; } }
}
