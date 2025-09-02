using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class cursor
{
    public Texture2D cursorSprite;
    public Vector2 cursorHotspot;
}
public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    public cursor defaultCursor;

    public cursor onHoverButton;

    public cursor combatCursorLeft;
    public cursor combatCursorRight;
    public cursor combatCursorBoth;

    public bool leftArmActive = false;
    public bool rightArmActive = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
 
    private void Update()
    {


        
        bool isOnButton = IsPointerOverUIElement();
        
        if (isOnButton)
        {
            ButtonEnter();
        }
        else if(!isOnButton && (!leftArmActive && !rightArmActive))
        {
            ButtonExit();
        }
    }

    public void CheckCombatCursor()
    {
        bool isOnButton = IsPointerOverUIElement();


        if (!isOnButton)
        {
            if (leftArmActive && rightArmActive)
            {
                Cursor.SetCursor(combatCursorBoth.cursorSprite, combatCursorBoth.cursorHotspot, CursorMode.Auto);

            }
            else if (rightArmActive)
            {
                Cursor.SetCursor(combatCursorRight.cursorSprite, combatCursorRight.cursorHotspot, CursorMode.Auto);

            }
            else if (leftArmActive)
            {
                Cursor.SetCursor(combatCursorLeft.cursorSprite, combatCursorLeft.cursorHotspot, CursorMode.Auto);

            }
        }
        

    }

    public void ButtonEnter()
    {
        Cursor.SetCursor(onHoverButton.cursorSprite, onHoverButton.cursorHotspot, CursorMode.Auto);
    }
    
    public void ButtonExit()
    {
        Cursor.SetCursor(defaultCursor.cursorSprite, defaultCursor.cursorHotspot, CursorMode.Auto);
    }
    
    
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.tag == "Button")
                return true;
        }
        return false;
    }
 
 
    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
    
}
