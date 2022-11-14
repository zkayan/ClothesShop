using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float time = 2f;

    void Update()
    {
        Destroy(gameObject, time);
    }
}
