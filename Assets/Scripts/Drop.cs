using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : Collectible {
    public override void Collect()
    {
        GameManager.AddPoint();
    }
}
