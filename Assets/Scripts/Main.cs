using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] Transform SpawnPointSmallCreep;
    [SerializeField] Transform SpawnPointArmourCreep;
    
    private void Update ()
    {
        if (Input.GetMouseButtonDown (0))
            CreepFactory.Instantiate<SmallCreep> (SpawnPointSmallCreep.position, null);

        if (Input.GetMouseButtonDown (1))
            CreepFactory.Instantiate<ArmourCreep> (SpawnPointArmourCreep.position, null);  
    }
}
