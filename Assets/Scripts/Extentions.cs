using UnityEngine;

public static class Extentions
{
    public static float AbsMax(float first, float second)
    {
        return Mathf.Abs(first) > Mathf.Abs(second) ? first : second;
    }
    
    public static float AbsMin(float first, float second)
    {
        return Mathf.Abs(first) < Mathf.Abs(second) ? first : second;
    }
}