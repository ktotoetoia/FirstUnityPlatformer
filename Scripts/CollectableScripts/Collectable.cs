using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICollectable
{
    void Collect(GameObject gameObj);
}

public class Collectable : MonoBehaviour, ICollectable
{
    protected string isCollected = "IsCollected";
    protected bool collected = false;
    public virtual void Collect(GameObject gameObj)
    {
        Destroy(gameObject);
    }
}
