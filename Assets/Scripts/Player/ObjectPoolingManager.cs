using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance { get; private set; }

    [Header("Const Variables")]
    private const int PROJECTILE_POOL_SIZE = 40;
    private const int ENEMY_POOL_SIZE = 40;

    [Header("Prefabs")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Prefab Lists")]
    private Queue<GameObject> _projectileList;
    private Queue<GameObject> _enemyList;
    [SerializeField] private float navMeshSampleDistance = 1.0f;

    public Action<Transform> OnEnemyGetBackPool;



    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        _enemyList = new Queue<GameObject>();
        _projectileList = new Queue<GameObject>();
        FillThePools(PROJECTILE_POOL_SIZE, _projectilePrefab, _projectileList);
        FillThePools(ENEMY_POOL_SIZE, _enemyPrefab, _enemyList);

    }

    public void GetOutProjectileFromPool()
    {
        if (_projectileList.Count > 0)
        {
            GameObject obj = _projectileList.Dequeue();
            obj.gameObject.SetActive(true);
        }
    }
    public GameObject GetEnemyFromPool(Vector3 spawnPosition)
    {
        if (_enemyList.Count > 0)
        {
            GameObject enemy = _enemyList.Dequeue();

            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
            {
                enemy.transform.position = hit.position;
                enemy.SetActive(true);
                return enemy;
            }
            else
            {
                ReturnEnemyToPool(enemy);
                return null;
            }
        }
        return null;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        _enemyList.Enqueue(enemy);
    }
    public void GetBackProjectileToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _projectileList.Enqueue(obj);
    }
    private void FillThePools(int size, GameObject prefab, Queue<GameObject> queue)
    {
        if (prefab != null)
        {
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
                queue.Enqueue(obj);
                prefab.gameObject.SetActive(false);
            }
        }
    }

}
