using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveComponent : IMoveComponent
{
    private Player player;
    private float moveSpeed;

    public EnemyMoveComponent(GObject gObject, int speed)
    {
        player = gObject.player;
        moveSpeed = speed;
    }

    public void Move(GameObject gameObject, float deltaTime)
    {
        var step = moveSpeed * deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
    }
}
