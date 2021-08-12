using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Object_3d : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    private string text_name;

    // Encapsulation 
    public string Name { get; protected set; }

    // Abstraction 
    protected virtual void LogName()
    {
        text_name = $"this is {Name}";
        nameText.text = text_name;
        Debug.Log(Name);
    }

    private void OnMouseDown() => LogName();
}
