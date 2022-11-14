using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public void Play(GameObject audioPrefab)
    {
        Instantiate(audioPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
