using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifeTime = 0.5f;
    public float minDist = 2f;
    public float maxDist = 3f;

    private Vector3 initialPos;
    private Vector3 targetPos;
    private float timer;

    public AnimationCurve alpha;
    public AnimationCurve speed;
    public AnimationCurve scale;

    private void Start()
    {

        float dist = Random.Range(minDist, maxDist);
        targetPos = new Vector2(initialPos.x, initialPos.y + dist);
        transform.localScale = Vector2.zero;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float fraction = timer / lifeTime;
        
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
        else if(timer > fraction)
        {
            Color txtColor = text.color;
            text.color = new Color(txtColor.r, txtColor.g, txtColor.b, alpha.Evaluate(fraction));
            //text.color = Color.Lerp(text.color, new Color(txtColor.r, txtColor.g, txtColor.b, alpha.Evaluate(timer)), (timer / fraction) );
        }
        Vector2 targetScale = new Vector2(2, 2);
        
        
        transform.localPosition = Vector2.Lerp(initialPos, targetPos, speed.Evaluate(fraction));
        transform.localScale = Vector2.Lerp(Vector2.zero, targetScale, scale.Evaluate(fraction));
    }

}
