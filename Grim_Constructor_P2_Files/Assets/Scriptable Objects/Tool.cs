using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New tool")]
[Serializable]
public class Tool : ScriptableObject
{
    public new string name;
    public int cost;
    public int amount;
    public int gridPlacementValue;
    public int[] tileIncrementsX;
    public int[] tileIncrementsY;
    public Dictionary<int, int> tileIncrementsCoordinates;

    private void Awake()
    {
        for (int i = 0; i < tileIncrementsX.Length; i++) {

            tileIncrementsCoordinates.Add(tileIncrementsX[i], tileIncrementsY[i]);
        }
    }
}
