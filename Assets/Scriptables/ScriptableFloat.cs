using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Float", menuName = "Scriptable Variable/Float")]
public class ScriptableFloat : ScriptableObject
{
    public float value;
    public float defaultValue;
    public bool resetOnEnable;

    private void OnEnable()
    {
        if (resetOnEnable)
        {
            value = defaultValue;
        }
    }
}
