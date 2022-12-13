using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawnPositions = new List<Transform>();
    public List<GameObject> enemys = new List<GameObject>();

    public int enemyAmount = 0;
    public int enemyLeft = 0;

    void FixedUpdate()
    {
        InstantiateEnemys();
    }

    void InstantiateEnemys(){
        StartCoroutine("RandomSpawnEnemys");
    }

    IEnumerator RandomSpawnEnemys(){
        yield return new WaitForSeconds(5f);
        if(FindObjectOfType<Enemy>() != null || enemyLeft > 0){
            yield return null;
        }else{
            enemyAmount += Random.Range(1,3);
            enemyLeft = enemyAmount;

            for(int n = 1; n <= enemyAmount; n++){
                Invoke("RandomSpawnEnemy",Random.Range(0f,2f));
            }
        }
    }

    void RandomSpawnEnemy(){
        Transform spawnPos = spawnPositions[Random.Range(0,spawnPositions.Count)];
        GameObject spawnEnemy = enemys[Random.Range(0,enemys.Count)];

        Instantiate(spawnEnemy,spawnPos.position,spawnPos.rotation);

        enemyLeft -= 1;
    }


}
