using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeZone : SpawnZone
{
    [SerializeField]
    SpawnZone[] spawnZones;

    public override Vector2 SpawnPoint{
        get{
            int index = Random.Range(0,spawnZones.Length);
            return spawnZones[index].SpawnPoint;
        }
    }

    
}
