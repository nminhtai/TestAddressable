using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour {
    const float rotationSpeed = 200.0f;

    void Update() {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

}
