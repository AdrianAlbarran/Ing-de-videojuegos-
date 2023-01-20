using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieComponent : IDieComponent
{
    public EnemyDieComponent()
    {
       
    }
    public void Die(GameObject gameObject)
    {
       
        gameObject.SetActive(false);
        
    }
}
