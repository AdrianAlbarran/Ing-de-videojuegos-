using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GObject: MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    public IMoveComponent moveComponent;
    [HideInInspector]
    public Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Move(GameObject gameObject, float deltaTime)
    {
       
        moveComponent?.Move(gameObject, deltaTime);
    }
}
