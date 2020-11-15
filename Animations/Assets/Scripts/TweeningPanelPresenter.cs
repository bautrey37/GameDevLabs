using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningPanelPresenter : MonoBehaviour
{

    //TODO Add public fields to reference opening and closing animations

    public void Open()
    {
        if(gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
        //TODO Also enable the opening animation
    }

    public void Close()
    {
        //TODO Return to the guide. Finish this method after the ScalingAnimation script
        if(!gameObject.activeSelf)
            return;

        gameObject.SetActive(false);
    }

}
