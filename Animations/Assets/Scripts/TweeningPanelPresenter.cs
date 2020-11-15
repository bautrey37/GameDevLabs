using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningPanelPresenter : MonoBehaviour
{

    //TODO Add public fields to reference opening and closing animations
    public ScalingAnimation OpeningAnimation;
    public ScalingAnimation ClosingAnimation;

    public void Open()
    {
        if(gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
        //TODO Also enable the opening animation
        OpeningAnimation.enabled = true;
    }

    public void Close()
    {
        //TODO Return to the guide. Finish this method after the ScalingAnimation script
        if(!gameObject.activeSelf)
            return;

        ClosingAnimation.enabled = true;
    }

    public void CloseFinished()
    {
        gameObject.SetActive(false);
    }

}
