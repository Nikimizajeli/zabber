using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("Spawns vehicles moving to the left instead of right when checked")]       // tooltip do serializowanego pola ponizej
    [SerializeField] bool spawnLeft = false;
    [Tooltip("Movement speed of spawned vehicles"), Range(0, 15)]                       // slider do serializowanego pola ponizej
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float minSpawnDelay = 2f;
    [SerializeField] float maxSpawnDelay = 5f;
    [SerializeField] Vehicle[] vehiclePrefabs;

    bool keepSpawning = true;           // domyslny modyfikator dostepu dla zmiennej - private
    bool emptyRoad = true;
    
    IEnumerator Start()         // coroutine
    {
        while (keepSpawning)
        {
            if (emptyRoad)
            {
                FirstSpawn();
            }

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnVehicle();
        }
    }

    private void FirstSpawn()
    {
        for(int i=0; i < 5; i++)
        {
            var offset = Random.Range(5, 6);
            var vehicle = Random.Range(0, vehiclePrefabs.Length);
            if (spawnLeft)
            {
                Spawn(vehicle, Vector3.left * i * offset);
            }
            else
            {
                Spawn(vehicle, Vector3.right * i * offset);
            }
            emptyRoad = false;
        }
    }

    private void SpawnVehicle()
    {
        int vehicleIndex = Random.Range(0, vehiclePrefabs.Length);
        if (vehiclePrefabs[vehicleIndex].GetComponent<Turtle>())
        {
            for (int i = 0; i < 3; i++)
            {
                var spawnOffset = new Vector3(i, 0, 0);        // var - typ zmiennej okreslany przez kompilator
                Spawn(vehicleIndex, spawnOffset);        // zolwie plywaja stadami
            }
        }
        else
        {
            Spawn(vehicleIndex);
        }
    }

    private void Spawn(int vehicleIndex)
    {
        Vehicle vehicle = Instantiate(vehiclePrefabs[vehicleIndex],         // klonowanie obiektu
                                      transform.position,
                                      Quaternion.identity);
        vehicle.transform.parent = transform;                       // przypisanie spawnera jako rodzica klonowanych obiektow dla porzadku w hierarchy
        StartVehicle(vehicle);
    }

    private void Spawn(int vehicleIndex, Vector3 spawnOffset)               // polimorfizm, przeciazenie metody, inna iloœæ parametrow dla zolwiow
    {
        Vehicle vehicle = Instantiate(vehiclePrefabs[vehicleIndex],         
                                      transform.position + spawnOffset,
                                      Quaternion.identity);
        vehicle.transform.parent = transform;
        StartVehicle(vehicle);
    }

    private void StartVehicle(Vehicle vehicle)                      // wyodrebnione, zeby nie powtarzac dwa razy tego samego i zwiekszyc czytelnosc
    {
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
