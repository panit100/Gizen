using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnZone : MonoBehaviour
{
    public float waitUntilWaveStart = 3f;
    public List<Transform> Enemys = new List<Transform>();
    public int enemyAmount = 0;
    public int enemyLeft = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        InstantiateEnemys();
    }
    void InstantiateEnemys(){
        StartCoroutine(Wave());
    }

    //wait3Sec and random spawn
    IEnumerator Wave(){
        yield return new WaitForSeconds(waitUntilWaveStart);

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
        CreateEnemy();

        enemyLeft -= 1;
    }

    public abstract Vector2 SpawnPoint{
        get;
    }

    void CreateEnemy(){
        int _enemy = Random.Range(0,Enemys.Count);
        Transform instance = Instantiate(Enemys[_enemy]);
        instance.localPosition = SpawnPoint;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero,Vector3.one);
    }
}
