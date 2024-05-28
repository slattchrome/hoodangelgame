using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{

    [SerializeField]
    private GameObject[] objects;

    [SerializeField]
    private GameObject[] containers, bariers, boxes, collectableItems, enemies, traps, checkpoints;

    [SerializeField]
    private GameObject ground;

    private List<Vector3> occupiedPositions = new List<Vector3>();

    private float groundWidth ;
    private float groundLength ;
    private int columns = 3;
    private int rows = 3;

    void Start()
    {
        MeshRenderer meshRenderer = ground.GetComponent<MeshRenderer>();
        Bounds bounds = meshRenderer.bounds;
        groundWidth = bounds.size.x - 10;
        groundLength = bounds.size.z - 10;

        InstantiateObject(boxes, 7, "boxes");
        InstantiateObject(enemies, 2, "enemies");
        InstantiateObject(collectableItems, 5, "collectables");
        InstantiateObject(containers, 3, "containers");
        InstantiateObject(checkpoints, 2, "checkpoints");
        InstantiateObject(traps, 2, "traps");
        InstantiateObject(bariers, 5, "bariers");
       
        Debug.Log(nameof(containers));
    }

    private void InstantiateObject(GameObject[] objects, int maxSpawns, string objectType)
    {
        List<Vector3> availablePositions = new List<Vector3>();
        int elementOfArray = Random.Range(0, objects.Length);
        float yObjectPos = objects[elementOfArray].transform.position.y;

        switch (objectType)
        {
            case "containers":
                for (int row = 0 ; row < rows; row += rows - 1)
                {
                    for (int col = 0; col < columns; col += columns - 1)
                    {
                        float cellWidth = groundWidth / columns;
                        float cellLength = groundLength / rows;

                        float startX = -groundWidth / 2;
                        float startZ = -groundLength / 2;

                        float cornerX = startX + col * cellWidth;
                        float cornerZ = startZ + row * cellLength;

                        float xObjectPos = cornerX + cellWidth / 2;
                        float zObjectPos = cornerZ + cellLength / 2;
                        

                        availablePositions.Add(new Vector3(xObjectPos, yObjectPos, zObjectPos));
                    }
                }
                break;

            case "checkpoints":
                GameObject[] containers = GameObject.FindGameObjectsWithTag("Container");
                foreach (GameObject container in containers)
                {
                    float containerPositionX = container.transform.position.x;
                    float containerPositionZ = container.transform.position.z;
                    
                    availablePositions.Add(new Vector3(containerPositionX, yObjectPos + 0.5f, containerPositionZ));
                }
                break;

            default:
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        float cellWidth = groundWidth / columns;
                        float cellLength = groundLength / rows;

                        float startX = -groundWidth / 2;
                        float startZ = -groundLength / 2;

                        float cornerX = startX + col * cellWidth;
                        float cornerZ = startZ + row * cellLength;

                        float xObjectPos = Random.Range(cornerX, cornerX + cellWidth);
                        float zObjectPos = Random.Range(cornerZ, cornerZ + cellLength);
                        

                        availablePositions.Add(new Vector3(xObjectPos, yObjectPos, zObjectPos));
                    }
                }
                break;
        }

        for (int i = 0; i < maxSpawns && availablePositions.Count > 0; i++)
        {
            
            int randomIndex = Random.Range(0, availablePositions.Count);
            Vector3 spawnPosition = availablePositions[randomIndex];
            
            Quaternion randomRotate = Quaternion.Euler(0f, Random.Range(0f, 180f), 0f);
            Instantiate(objects[elementOfArray], spawnPosition, randomRotate);

            availablePositions.RemoveAt(randomIndex);
        }
    }
}