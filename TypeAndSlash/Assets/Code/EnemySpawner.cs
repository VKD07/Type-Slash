using System.Collections;
using UnityEngine;

namespace Code
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Vector2 _spawnRange;
        [SerializeField] private Enemy _enemyPrefab;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_spawnRange.x, _spawnRange.y));
                Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
            }
        }
    }
}