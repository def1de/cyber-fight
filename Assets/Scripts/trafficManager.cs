using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficManager : MonoBehaviour
{
    public int direction = 1;
    public GameObject[] trafficCars;
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnCar", 0f, 2f);
    }

    private void SpawnCar()
    {
        int carIndex = Random.Range(0, trafficCars.Length);

        GameObject car = Instantiate(trafficCars[carIndex], transform.position, Quaternion.identity);

        car.GetComponent<trafficCar>().direction = direction;
        if (direction == -1)
        {
            car.transform.Rotate(0f, 180f, 0f);
        }
    }
}
