using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CameraLead : MonoBehaviour
{
    Camera thisCamera;
    public Transform attachedPlayer;
    public float marginSize = 15f;
    

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = attachedPlayer.transform.position;
        Vector3 bottomLeft = thisCamera.ViewportToWorldPoint(new Vector3(0, 0,0));
        Vector3 topRight = thisCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        float h = Input.GetAxis("Horizontal");

        topRight.x -= marginSize;
        bottomLeft.x += marginSize;

        Vector3 marginTopLeft = new Vector3(bottomLeft.x, -10, 0);
        Vector3 marginBtmLeft = new Vector3(bottomLeft.x, 10, 0);

        Vector3 marginTopRight = new Vector3(topRight.x, -10, 0);
        Vector3 marginBtmRight = new Vector3(topRight.x, 10, 0);

        Debug.DrawLine(marginTopLeft, marginBtmLeft);
        Debug.DrawLine(marginTopRight, marginBtmRight);

        if (player.x > topRight.x) // If player passes right margin, adjust camera position to put the player near the left side of the screen
        {
            
        }
        else if (player.x < bottomLeft.x) //  if player passes left margin, adjust camera position to put the player near the right side of the screen
        {

        }
        else
        {

        }
    }
}
