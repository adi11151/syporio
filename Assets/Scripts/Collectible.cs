using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    [HideInInspector]
    public GameManager GameManager;
    public abstract void Collect();
}
