using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawblade : MonoBehaviour
{
    public float rotateSpeed = 360.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = rotateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, angle);
    }
}
