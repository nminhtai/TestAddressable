using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AASpriteImage : MonoBehaviour {
    [SerializeField] Sprite sprite;

    void Awake() {
        GetComponent<Image>().sprite = sprite;
    }
}
