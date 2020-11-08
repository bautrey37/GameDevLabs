using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public AudioClipGroup CoinDropAudio;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            CoinDropAudio.Play();
        }
    }
}
