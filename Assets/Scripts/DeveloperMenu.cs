using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeveloperMenu
{
    [MenuItem("Developer/Action1")]
    public static void Action1()
    {
        Debug.Log("option one was activated");
    }

    [MenuItem("Developer/Action2")]
    public static void Action2()
    {
        Debug.Log("option two was activated");
    }
}
