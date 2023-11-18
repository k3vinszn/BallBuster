using System.Collections;
using UnityEngine;

public class spawncontroller : MonoBehaviour
{
    [SerializeField] GameObject powerUpPrefab;

    void Start()
    {
        // Delay the instantiation of the power-up object by 10 seconds
        StartCoroutine(SpawnPowerUpDelay());
    }

    IEnumerator SpawnPowerUpDelay()
    {
        yield return new WaitForSeconds(10f);
        SpawnPowerUp();
    }

    void SpawnPowerUp()
    {
        // Instantiate the power-up object or activate it if it's already in the scene
        Instantiate(powerUpPrefab, Vector3.zero, Quaternion.identity);
    }
}
