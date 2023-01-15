using UnityEngine;

public class PlayerMoveComponent : IMoveComponent
{
    protected Rigidbody2D rb;
    public float MovementSpeed = 5f;

    private float x;
    private float y;

    public PlayerMoveComponent(GObject gObject)
    {
        rb = gObject.rb;
    }
    public void Move(GameObject gameObject, float deltaTime)
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        Vector2 moveDelta = new Vector2(x, y);
        moveDelta = Vector2.ClampMagnitude(moveDelta, 1);

        rb.MovePosition(rb.position + moveDelta * MovementSpeed * deltaTime);
    }
}