using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T buffer = b;
        b = a;
        a = buffer;
    }
}
