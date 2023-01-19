using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrototypeDrop
{
    public IPrototypeDrop Clone(Vector3 position);

    public bool IsAlive();

    public void Render();

    public string GetType();

    public void SetAlive(bool isalive);

    public Vector3 GetPosition();

}
