using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Spawns vehicles moving to the left instead of right when checked")]       // tooltip do serializowanego pola ponizej
    [SerializeField] bool spawnLeft = false;
    [Tooltip("Movement speed of spawned vehicles"), Range(0, 15)]                                          // slider do serializowanego pola ponizej
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
        Vehicle vehicle = Instantiate(vehiclePrefabs[vehicleIndex],         // klonowanie obiektu
                                        transform.position, 
                                        Quaternion.identity);           
        vehicle.transform.parent = transform;                       // przypisanie spawnera jako rodzica klonowanych obiektow dla porzadku w hierarchy
        if (spawnLeft)
        {
            vehicle.StartMoving(Vector3.left * movementSpeed);
            vehicle.FlipHorizontally();
        }
        else
        {
            vehicle.StartMoving(Vector3.right * movementSpeed);
        }
    }


}
