using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LeveLoader : MonoBehaviour
{
    public Animator transition;
    public float animationTime = 1f;
    
    public IEnumerator LoadLevel()
    {
        transition.SetBool("Start", true);

        yield return new WaitForSeconds(animationTime);
    }

    public void PlaySwoosh()
    {
        AudioManager.Instance.PlayEffect("TransitionSwooshIn");

    }
    
    public void PlaySwooshOut()
    {
        AudioManager.Instance.PlayEffect("TransitionSwooshOut");

    }

    public void PlayGateClosed()
    {
        AudioManager.Instance.PlayEffect("TransitionGate");
    }
}
