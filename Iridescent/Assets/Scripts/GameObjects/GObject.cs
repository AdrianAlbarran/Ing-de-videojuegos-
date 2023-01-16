using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GObject: MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    public IMoveComponent moveComponent;
    public IDieComponent dieComponent;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public float attackCD;

    public void Move(GameObject gameObject, float deltaTime)
    {
        moveComponent?.Move(gameObject, deltaTime);
    }

    public void Die(GameObject gameObject)
    {
        dieComponent?.Die(gameObject);
    }

}
