using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Grid Shape")]
[Serializable]
public class Shape : ScriptableObject
{
    [SerializeField] private List<Vector2Int> tileIncrements;
    [SerializeField] private List<int> tileIncrementValues;
    public List<Vector2Int> TileIncrements { get { return tileIncrements; } }
    public List<int> TileIncrementValues { get { return tileIncrementValues; } }


}
