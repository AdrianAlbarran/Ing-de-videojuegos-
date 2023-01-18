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
        Assert.IsTrue(type == "Damage");
        switch (type)
        {
            case "Damage":

                //string path = EditorUtility.OpenFolderPanel("Select Directory", "", "flask_big_red.png");
                string path = "D:/URJC/Ing-de-videojuegos-/Iridescent/Assets/Tilesets/items/flask_big_red.png";
                sprite = Resources.Load<Sprite>("D:/URJC/Ing-de-videojuegos-/Iridescent/Assets/Tilesets/items/flask_big_red.png");
                Debug.Log(path);
                Debug.Log(sprite);
                break;
        }
        _dropType = type;
        alive = true;
    }
    public abstract IPrototypeDrop Clone(Vector3 position);

    public void Create(Vector3 position)
    {
        dmgDrop = Resources.Load<GameObject>("Assets / Prefab / DmgDropPrefab.prefab");
        gameobject = new GameObject();
        gameobject.SetActive(false);
        _spriteRenderer = gameobject.AddComponent<SpriteRenderer>();
        gameobject.GetComponent<SpriteRenderer>().sprite = sprite;
        _position = position;
    }

    public bool IsAlive()
    {
        return alive;
    }

    public void SetAlive(bool isalive)
    {
        alive = isalive;
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
}
