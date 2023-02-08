using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMap : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 5f)]
    private float speed = 3;

    [SerializeField]
    private float endPosValue;

    private Vector3 startPos;
    private float newPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Mathf.Repeat(Time.time * speed, endPosValue);
        transform.position = startPos + (Vector3.down * newPos); 
    }
}
