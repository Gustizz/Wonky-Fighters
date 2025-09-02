using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{

    public Image panelBackground;
    public GameObject Crossout;

    [Header("Player Head")]
    public Image faceHolderPlayer;
    public Image hairHolderPlayer;
    
    [Header("Enemy Head")]
    public Image faceHolder;
    public Image hairHolder;

    public void InitialiseFace(Sprite hairStyle, Color hairColour, Sprite face)
    {
        faceHolder.sprite = face;

        hairHolder.sprite = hairStyle;
        hairHolder.color = hairColour;
    }
    
    public void InitialisePlayerFace(Sprite hairStyle, Color hairColour, Sprite face)
    {
        faceHolderPlayer.sprite = face;

        hairHolderPlayer.sprite = hairStyle;
        hairHolderPlayer.color = hairColour;
    }
    public void CrossOut()
    {
        Crossout.SetActive(true);
        
    }
}
