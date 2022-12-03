using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDrugController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float circleSizeMultiplier; 
    private float timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime*speed;
        transform.localPosition = new Vector3(Mathf.Cos(timeCounter)* circleSizeMultiplier, Mathf.Sin(timeCounter)* circleSizeMultiplier, 0);
    }
}
