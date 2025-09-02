using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamagePopup : MonoBehaviour
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
        float dir = Random.rotation.eulerAngles.z;
        initialPos = transform.position + Vector3.up * 0.5f;
        float dist = Random.Range(minDist, maxDist);
        targetPos = new Vector2(initialPos.x + dist , initialPos.y + 2);
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
        
        var dmgScale = Mathf.Clamp(float.Parse(text.text) / 10, 1, 3);
        Vector2 targetScale = new Vector2(dmgScale, dmgScale);
        
        
        transform.localPosition = Vector2.Lerp(initialPos, targetPos, speed.Evaluate(fraction));
        transform.localScale = Vector2.Lerp(Vector2.zero, targetScale, scale.Evaluate(fraction));
    }

    public void SetDamage(float _dmg)
    {
        text.text = _dmg.ToString();
    }
}
