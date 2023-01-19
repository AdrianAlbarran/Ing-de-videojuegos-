using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AbstractDrop : IPrototypeDrop
{
    private bool alive;
    private Vector3 _position;
    private string _dropType;
    private GameObject gameobject;
    private SpriteRenderer _spriteRenderer;
    private Sprite sprite;
    private GameObject dmgDrop;

    public Vector3 position
    {
        get { return _position; }
        set { _position = value; }
    }
    public AbstractDrop(string type)
    {
        Assert.IsTrue(type == "Damage" || type == "Health" || type == "AttackSpeed" || type == "MoveSpeed");
        switch (type)
        {
            case "Damage":
                sprite = Resources.Load<Sprite>("weapon_sword_red");
                break;

            case "Health":
                sprite = Resources.Load<Sprite>("flask_big_red");
                break;

            case "AttackSpeed":
                sprite = Resources.Load<Sprite>("weapon_sword_golden");
                break;

            case "MoveSpeed":
                sprite = Resources.Load<Sprite>("flask_green");
                break;
        }
        _dropType = type;
        alive = true;
    }
    public abstract IPrototypeDrop Clone(Vector3 position);

    public void Create(Vector3 position)
    {
        
        gameobject = new GameObject();
        gameobject.SetActive(false);
        _spriteRenderer = gameobject.AddComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = 2;
        gameobject.GetComponent<SpriteRenderer>().sprite = sprite;
        _position = position;
        gameobject.transform.position = position;
        //gameobject.transform.localScale = new Vector3(6,6,0);
        gameobject.AddComponent<BoxCollider2D>().isTrigger=true;
        gameobject.tag = "Drops";
        gameobject.layer = 8;
        gameobject.name = _dropType;
    }

    public bool IsAlive()
    {
        return alive;
    }

    public void SetAlive(bool isalive)
    {
        alive = isalive;
        if (!alive) die();
    }

    public void Render()
    {
        if (!gameobject.activeSelf)
        {
            gameobject.SetActive(true);
        }
    }

    public string GetType()
    {
        return this._dropType;
    }

    public Vector3 GetPosition()
    {
        return _position;
    }

    private void die()
    {
        gameobject.SetActive(false);
    }
}
