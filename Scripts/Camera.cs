using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private float y_offset;
    [SerializeField]
    private float x_offset;
    private Transform target;
    [SerializeField]
    private float speed = 4.0F;

    private void Awake() {
        if (!target)
            target = FindObjectOfType<Character>().transform;
    }


    void Update()
    {   
        Vector3 pos = target.position;
        pos.z = -12;
        pos.y += y_offset;
        pos.x += x_offset;

        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }
}
