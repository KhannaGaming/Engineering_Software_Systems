using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float backgroundSize;
    public float paralaxSpeed;
    public float backgroundOffset;
    public bool movesWithCamera;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;


    

    private void Start()
    {
       
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * paralaxSpeed);
        lastCameraX = cameraTransform.position.x;

        if(cameraTransform.position.x < (layers[leftIndex].transform.position.x+viewZone))
        {
            ScrollLeft();
        }
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }
        if (movesWithCamera)
        {

            transform.position = new Vector3(transform.position.x, cameraTransform.position.y +backgroundOffset, 0);
        }
    
        else
        {
            transform.position = new Vector3(transform.position.x, backgroundOffset, 0);
        }
    }
    private void ScrollLeft()
    {

        layers[rightIndex].position = new Vector3((layers[leftIndex].position.x - backgroundSize), layers[leftIndex].position.y, 0);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex<0)
        {
            rightIndex = layers.Length - 1;
        }
    }
    private void ScrollRight()
    {

        layers[leftIndex].position = new Vector3((layers[rightIndex].position.x + backgroundSize), layers[rightIndex].position.y, 0);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

}
