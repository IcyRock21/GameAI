using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] int unitHealth, maxHealth;
    [SerializeField] UnitMovement unitMovement;

    private void Start()
    {
        unitHealth = maxHealth;
         unitMovement = GetComponent<UnitMovement>();
    }

    public void MinusHealth (int damage)
    {
        unitHealth += damage;
    }

    public void Checkhealth ()
    {
        if (unitHealth < 0 ) 
        { 
            unitMovement.CheckEnemyCount();
            Destroy(gameObject);
        }
    }
}
