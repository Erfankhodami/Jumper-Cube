using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObstacleController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float blinkMultiplier;
    [SerializeField] private float alpha=0;
    private bool fadedout;
    [SerializeField]private bool fadeTimeIsOver=true;
    [SerializeField] private Color color;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeFadeSituation();
        sprite.color = Color.Lerp(new Color(1, 1, 1, 0), color,alpha);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ChangeFadeSituation();
            fadeTimeIsOver = false;
            StartCoroutine(BlinkTimeBound(1));
        }
    }

    IEnumerator BlinkTimeBound(float time)
    {
        yield return new WaitForSeconds(time);
        fadeTimeIsOver = true;
    }

    void ChangeFadeSituation()
    {
        if (!fadeTimeIsOver)
        {
            if (alpha >= 0.8f)
            {
                fadedout = false;
            }

            if (alpha <= 0)
            {
                fadedout = true;
            }

            if (fadedout)
            {
                alpha += Time.deltaTime*blinkMultiplier;
            }
            else
            {
                alpha -= Time.deltaTime*blinkMultiplier;
            }

            print(alpha);

        }
        else
        {
            alpha = 0;
        }
    }
}
