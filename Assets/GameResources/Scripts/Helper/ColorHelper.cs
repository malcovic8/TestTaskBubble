using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorHelper
{
    public static readonly Dictionary<int, Color> ColorEnum = new Dictionary<int, Color>()
    {
        { 0, Color.blue },
        { 1, Color.red },
        { 2, Color.green },
        { 3, Color.yellow }
    };
}
