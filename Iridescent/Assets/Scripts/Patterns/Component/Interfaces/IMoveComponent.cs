using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveComponent
{
    public void Move(GameObject gameObject, float deltaTime);
}
