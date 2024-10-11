using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class inFrontOfPlayer : MonoBehaviour
{
    public float lookAheadAmount = 10.0f;
    

    public Transform attachedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");      // Reads player input, full Left = -1.0, full Right = +1.0
        float offsetMult = h * h;               // makes offsetMult accelerate exponentially, but also makes negative numbers positive as a side effect.
        if (h < 0) { offsetMult *= -1f; }           // If h is neg, i.e. player is moving left, makes offsetMult negative too.
        
        Vector3 player = attachedPlayer.transform.position;


        float offset = player.x + (lookAheadAmount * offsetMult);
        transform.position = new Vector3(offset, player.y, player.z);
    }
}
