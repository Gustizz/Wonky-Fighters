using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class baseItem : MonoBehaviour
{
    public int itemIndex;
    public bool isEmpty;
    public wardrobeManager wardrobeManager;
    public ScriptableObject scriptableObject;
    
    [Header("Aura")]
    public GameObject auraObject;
    public RawImage bgAura;
    public Color auraColour;

    [Header("Item Display")]
    public TextMeshProUGUI title;
    public Image itemPreview;
    
    // Start is called before the first frame update
    void Start()
    {
        //isEmpty= true;
    }


    public virtual void Clicked()
    {
        wardrobeManager.ItemSelected(this);
        //print("Click");
        
        //Highlighted();
    }
    public virtual void Highlighted()
    {
        //print("Pressed");
        auraObject.gameObject.SetActive(true);
        
    }
}
