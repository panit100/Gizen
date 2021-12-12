using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneManager : SpawnZone
{
    public override Vector2 SpawnPoint{
        get{
            Vector2 p;
            p.x = Random.Range(-0.5f,0.5f);
            p.y = Random.Range(-0.5f,0.5f);
            return transform.TransformPoint(p);
        }
    }

    
}
