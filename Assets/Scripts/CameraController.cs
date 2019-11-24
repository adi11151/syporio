using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject _syporio;

    private float xMin = -2f;
    private float xMax = 100f;
    private float yMin = -20f;
    private float yMax = 20.5f;

    public void Setup(GameObject syporio)
    {
        _syporio = syporio;
    }

	// Use this for initialization
    void LateUpdate()
    {
        if (_syporio == null)
        {
            return;
        }

        float x = Mathf.Clamp(_syporio.transform.position.x + 2, xMin, xMax);
        float y = Mathf.Clamp(_syporio.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
