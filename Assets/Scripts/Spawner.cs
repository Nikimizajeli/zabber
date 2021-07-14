using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] bool spawnLeft = false;
    [Range(0, 15)]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float minSpawnDelay = 2f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Vehicle[] vehiclePrefabs;

    bool keepSpawning = true;
    
    IEnumerator Start()         //coroutine
    {
        while (keepSpawning)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnVehicle();
        }
    }

    private void SpawnVehicle()
    {
        int vehicleIndex = Random.Range(0, vehiclePrefabs.Length);
        Vehicle vehicle = Instantiate(vehiclePrefabs[vehicleIndex], transform.position, Quaternion.identity);
        if (spawnLeft)
        {
            vehicle.StartMoving(Vector3.left * movementSpeed);
        }
        else
        {
            vehicle.StartMoving(Vector3.right * movementSpeed);
        }
    }
}
