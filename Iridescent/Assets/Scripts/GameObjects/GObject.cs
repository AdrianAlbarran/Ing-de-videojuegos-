using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GObject: MonoBehaviour
{
    public Rigidbody2D rb;
    public IMoveComponent moveComponent;

    public void Move(GameObject gameObject, float deltaTime)
    {
       
        moveComponent?.Move(gameObject, deltaTime);
    }
}
