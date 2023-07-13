using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] int TimeToDestroy;
    [Space (10)]
    [SerializeField] int Health;
    [SerializeField] int Damage;
    [SerializeField] int Armor;

    public void OnEnable ()
    {
        StartCoroutine (Destroy ());
    }

    IEnumerator Destroy ()
    {
        yield return new WaitForSeconds (TimeToDestroy);
        CreepFactory.Destroy (this.gameObject);
    }
}
