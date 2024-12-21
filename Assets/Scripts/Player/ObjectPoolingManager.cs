using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager Instance { get; private set; }

    [Header("Const Variables")]
    private const int PROJECTILE_POOL_SIZE = 30;
    private const int ENEMY_POOL_SIZE = 15;

    [Header("Prefabs")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [Header("Prefab Lists")]
    [SerializeField] private Queue<GameObject> _projectileList;
    [SerializeField] private Queue<GameObject> _enemyList;



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

        _projectileList = new Queue<GameObject>();
        _enemyList = new Queue<GameObject>();

        FillThePools(PROJECTILE_POOL_SIZE, _projectilePrefab, _projectileList);
        FillThePools(ENEMY_POOL_SIZE, _enemyPrefab, _enemyList);
    }

    public void GetOutProjectileFromPool()
    {
        if (_projectileList.Count > 0)
        {
            GameObject obj = _projectileList.Dequeue();
            obj.gameObject.SetActive(true);
            Debug.Log(_projectileList.Count);
        }
    }
    public void GetOutEnemyFromPool()
    {
        if (_enemyList.Count > 0)
        {
            GameObject obj = _enemyList.Dequeue();
            obj.gameObject.SetActive(true);
        }
    }

    public void GetBackProjectileToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _projectileList.Enqueue(obj);
    }

    public void GetBackEnemyToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _enemyList.Enqueue(obj);
    }

    private void FillThePools(int size, GameObject prefab, Queue<GameObject> queue)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            queue.Enqueue(obj);
            prefab.gameObject.SetActive(false);
        }
    }
}
