using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _types = new List<GameObject>();

    private void Awake()
    {

    }

    void Start()
    {
        ObjectPoolingManager.Instance.OnEnemyGetBackPool += SpawnObjectAtEnemyDeathPoint;
    }


    void Update()
    {

    }

    public void SpawnObjectAtEnemyDeathPoint(Transform sender)
    {
        int random = UnityEngine.Random.Range(0, _types.Count - 1);
        Instantiate(_types[random], sender.position, Quaternion.identity);
    }

}
