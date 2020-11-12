using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public AudioClipGroup CoinDropAudio;
    public AudioClipGroup OwlAudio;
    public AudioClipGroup FireAudio;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            CoinDropAudio.Play();
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            OwlAudio.Play();
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            FireAudio.Play();
        }
    }
}
