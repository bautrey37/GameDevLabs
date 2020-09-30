using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RaycastPosition : MonoBehaviour
{

    private Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        // A quick workaround not to go chasing ui button clicks
        // It is generally better to implemented things like this as part of an utility class
	    if (IsUiButtonClicked())
            return;

        /*
         * Our player character is chasing around this object. So to make it move
         * where we want it to move, we have to set this object position in game.
         *
         * To do that, we have to:
         * Check if mouse button is held
         * Get the mouse position on screen
         * Create a ray from screen to the world.
         * Cast the ray and find what position in game the mouse is pointing at
         * Move this object to that position
         *
         * Extra:
         *  Also check that we are not clicking on the player
         */

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit; 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); 
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = hit.point;
            }
        }
         
	}
    
    private static bool IsUiButtonClicked()
    {
        return EventSystem.current.currentSelectedGameObject != null 
               && EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    }
}
