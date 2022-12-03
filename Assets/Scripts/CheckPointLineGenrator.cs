
using UnityEngine;

public class CheckPointLineGenrator : MonoBehaviour
{ 
    private LineRenderer line;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        target = GameObject.Find("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,transform.position);
        line.SetPosition(1, new Vector3(transform.position.x, target.position.y, -0.1f));
    }
}
