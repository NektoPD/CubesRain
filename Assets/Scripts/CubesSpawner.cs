using System.Collections;
using UnityEngine;

public class CubesSpawner : CubePool
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _minXPosition;
    [SerializeField] private float _maxXPosition;
    [SerializeField] private float _secondsBetweenSpawn;

    private void Awake()
    {
        Initalize(_prefab);
    }

    private void Start()
    {
        StartCoroutine(GenerateCubes());
    }

    private IEnumerator GenerateCubes()
    {
        WaitForSeconds interval = new WaitForSeconds(_secondsBetweenSpawn);

        while (enabled)
        {
            Spawn();

            yield return interval;
        }
    }

    private void Spawn()
    {
        Cube cube = null;

        if(TryGetObject(out cube, _prefab))
        {
            float spawnPositionX = Random.Range(_minXPosition, _maxXPosition);

            cube.transform.position = new Vector3(spawnPositionX, transform.position.y, transform.position.z);
            cube.gameObject.SetActive(true);
        }
    }
}
