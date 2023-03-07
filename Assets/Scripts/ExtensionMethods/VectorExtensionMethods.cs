using UnityEngine;

public static class VectorExtensionMethods
{
    public static bool IsZero(this Vector2 v)
    {
       return Mathf.Approximately(v.sqrMagnitude, 0);
    }

    public static bool IsZero(this Vector3 v)
    {
        return Mathf.Approximately(v.sqrMagnitude, 0);
    }
}
