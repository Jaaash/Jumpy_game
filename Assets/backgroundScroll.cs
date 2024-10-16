using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScroll : MonoBehaviour
{
    int numLayers;
    Transform[] layers;
    public float[] layerFac;
    public float yOffset = 1.0f;
    public Camera attachedCamera;
    Vector3 initialPosition;
    void Start()
    {
        numLayers = layerFac.Length; // Reads the number of entries in the layerFac array
        layers = new Transform[numLayers];
        initialPosition = attachedCamera.transform.position;
        // find the layers, these are assumed to be the first numLayers children
        // layers objects are assumed to have zero local transform to the parent
        for (int i = 0; i < numLayers; i++)
        {
            layers[i] = transform.GetChild(i);
        }
    }
    void Update()
    {
        Vector3 diff = attachedCamera.transform.position - initialPosition;
        // scale by the scale factors for each layer, 
        // and set the local position of each child
        for (int i = 0; i < numLayers; i++)
        {
            Vector3 scaledDiff = diff * layerFac[i];
            layers[i].transform.localPosition = new Vector3(scaledDiff.x, yOffset, 0.0f);
        }
    }
}
