﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GirlScene : MonoBehaviour {
    public void ButtonPressed() {
        SceneManager.LoadScene("PlantScene");
        //Addressables.LoadSceneAsync("PlantScene");
    }
}
