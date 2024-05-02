using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform farmer;

    private Vector3 tempPos;

    

    // Start is called before the first frame update
    void Start()
    {
        farmer = GameObject.FindWithTag("Farmer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        tempPos = transform.position;
        tempPos.x = farmer.position.x;
        tempPos.y = farmer.position.y;
        transform.position = tempPos;
    }
}
