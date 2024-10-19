using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    private float speed = 10;
    private float despawnDistance = 150f;
    public int direction = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed * direction);
        if (Mathf.Abs(transform.position.z) > despawnDistance)
        {
            Destroy(gameObject);
        }
    }
}
