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
}
