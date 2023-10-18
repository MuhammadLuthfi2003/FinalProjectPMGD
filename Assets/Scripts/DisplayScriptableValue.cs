using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScriptableValue : MonoBehaviour
{
    [SerializeField] ScriptableInteger scriptable;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] string prefix = "";

    private void Update()
    {
        text.text = prefix + scriptable.value.ToString();
    }
   
}
