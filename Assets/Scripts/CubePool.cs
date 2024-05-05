using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private readonly Queue<Cube> _cubesQueue = new Queue<Cube>();
    
    protected void Initalize(Cube cubePrefab)
    {
        for(int i = 0; i < _capacity; i++)
        {
            Cube spawnedCube = Instantiate(cubePrefab, _container.transform);
            spawnedCube.gameObject.SetActive(false);
            spawnedCube.ReadyForDiactivation += PutObject;

            _cubesQueue.Enqueue(spawnedCube);
        }
    }

    protected bool TryGetObject(out Cube cube, Cube cubePrefab) 
    { 
        if(_cubesQueue.Count > 0)
        {
            cube = _cubesQueue.Dequeue();

            return cube != null && cube.gameObject.activeSelf == false;
        }
        else
        {
            cube = Instantiate(cubePrefab, _container.transform);
            cube.gameObject.SetActive(false);
            cube.ReadyForDiactivation += PutObject;

            return cube != null;
        }
    }

    protected void PutObject(Cube cube)
    {
        _cubesQueue.Enqueue(cube);
        cube.gameObject.SetActive(false);
    }
}
