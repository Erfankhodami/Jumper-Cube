
using UnityEngine;

public class CheckPontController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < PlayerPrefs.GetFloat("X"))
        {
            Destroy(gameObject);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
