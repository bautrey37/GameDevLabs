using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Utility;

public class GameSaver : MonoBehaviour
{

    /*
     * Implement the save game so that player would continue from the same position.
     * We can use player prefs to save data between sessions.
     *
     * task:
     *  Record the player position
     *  Read the data when the script starts. We can use Awake and OnDisable for this.
     *  Also set the movement targets position.
     *
     * TODO Extra:
     *  Also save the state of the camera, so that the game would resume with same view.
     *  Use scriptable objects to save all the data as a single string. We will learn about
     *  scriptable objects in a later practice, so this challenge is only recommended for those with
     *  previous experience.
     */

    void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerPos"))
        {
            Vector3 pos = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPos"));
            transform.position = pos;
            GetComponent<NavMeshAgent>().Warp(pos);
            GameObject.FindObjectOfType<RaycastPosition>().transform.position = pos;
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetString("PlayerPos", JsonUtility.ToJson(transform.position));

    }
}
