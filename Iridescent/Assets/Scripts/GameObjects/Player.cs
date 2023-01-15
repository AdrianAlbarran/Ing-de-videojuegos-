using UnityEngine;

public class Player : GObject
{
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveComponent = new PlayerMoveComponent(this);
        player = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(this.gameObject, Time.fixedDeltaTime);
    }
}