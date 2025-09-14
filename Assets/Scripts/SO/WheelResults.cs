using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelResults", menuName = "Scriptable Objects/WheelResults")]
public class WheelResults : ScriptableObject
{
    public string SuccsessMessage;
    public string FailMessage;
    public string BonusMessage;
    public string SecondChanceMessage;
}

