using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{
    [SerializeField] private float movement;
    [SerializeField] private float time;
    private float targetXPos;
    private bool leftMostPosReached = false;

    // Start is called before the first frame update
    void Start()
    {
        leftMostPosReached = true;
        targetXPos = transform.position.x + movement;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= targetXPos - 0.01f && leftMostPosReached)
        {
            leftMostPosReached = false;
            transform.position = new Vector3(targetXPos, transform.position.y, transform.position.z);
            movement *= -1;
            targetXPos = transform.position.x + movement;
        }
        else if (transform.position.x <= targetXPos + 0.01f && !leftMostPosReached)
        {
            leftMostPosReached = true;
            transform.position = new Vector3(targetXPos, transform.position.y, transform.position.z);
            movement *= -1;
            targetXPos = transform.position.x + movement;
        }
        else
        {
            //float direction = targetXPos - transform.position.x;
            float movementPerSec = movement / time;

            transform.Translate( movementPerSec * Time.deltaTime, 0f, 0f);
        }
    }
}
