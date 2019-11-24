using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDrop : Collectible {
    [SerializeField] private Transform _secretCloudPosition;

    public override void Collect()
    {

        GameManager.AddPoint();
        GameManager.CreateCloud(_secretCloudPosition.transform.position.x, _secretCloudPosition.transform.position.y);
        Debug.Log("Took secret drop");
    }
}
