using System;
using UnityEngine;

public class TestIMGUI : MonoBehaviour
{
    private void OnGUI()
    {
        var rect = new Rect(10,10,100,100);

        GUI.Label(rect, "Sample text");
    }
}
