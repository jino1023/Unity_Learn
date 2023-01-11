using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheritance
public class Sphere : Object_3d
{
    private void Start()
    {
        Name = "Sphere";
    }
    // Polymorphism 
    protected override void LogName()
    {
        base.LogName();
    }
}
