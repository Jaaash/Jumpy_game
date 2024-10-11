using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform attachedPlayer;
    Camera thisCamera;
    public float boxSizeX = 15.0f; //Size of box around player. Higher numbers = more responsive camera
    public float boxSizeY = 10.0f;
    public float blendAmount = 0.05f; // lower values blend slower


    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 playerPos = attachedPlayer.transform.position;
        Vector3 cameraPos = transform.position;
        float camX, camY;
        camX = cameraPos.x;
        camY = cameraPos.y;
        float screenX0, screenX1, screenY0, screenY1;
        float box_x0, box_x1, box_y0, box_y1;
        box_x0 = playerPos.x - boxSizeX;
        box_x1 = playerPos.x + boxSizeX;
        box_y0 = playerPos.y - boxSizeY;
        box_y1 = playerPos.y + boxSizeY;
        Vector3 bottomLeft = thisCamera.ViewportToWorldPoint(new Vector3(0, 0,
       0));
        Vector3 topRight = thisCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        screenX0 = bottomLeft.x;
        screenX1 = topRight.x;
        if (box_x0 < screenX0)
            camX = playerPos.x + 0.5f * (screenX1 - screenX0) - boxSizeX;
        else if (box_x1 > screenX1)
            camX = playerPos.x - 0.5f * (screenX1 - screenX0) + boxSizeX;
        screenY0 = bottomLeft.y;
        screenY1 = topRight.y;
        if (box_y0 < screenY0)
            camY = playerPos.y + 0.5f * (screenY1 - screenY0) - boxSizeY;
        else if (box_y1 > screenY1)
            camY = playerPos.y - 0.5f * (screenY1 - screenY0) + boxSizeY;
        transform.position = new Vector3(camX, camY, cameraPos.z);
    }
}


/* TO DO:
 * Figure out how to make an invisible floating dot infront of the player
 * have the distance between the dot and the player be decided by lookAheadAmount * h
 * centre the camera box around that dot instead of the player.
 */