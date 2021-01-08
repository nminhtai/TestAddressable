using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class StartScene : MonoBehaviour {

    void Start() {
        Caching.ClearCache();

        //Debug.Log($"Application.dataPath {Application.dataPath}");
        //Debug.Log($"Application.streamingAssetsPath {Application.streamingAssetsPath}");
        //Debug.Log($"Application.persistentDataPath {Application.persistentDataPath}");
        //Debug.Log($"Application.temporaryCachePath {Application.temporaryCachePath}");
        //Debug.Log($"Caching.currentCacheForWriting.path {Caching.currentCacheForWriting.path}");
        //Debug.Log($"Addressables.BuildPath {Addressables.BuildPath}");


        Debug.Log($"UnityEngine.AddressableAssets.Addressables.RuntimePath: {UnityEngine.AddressableAssets.Addressables.RuntimePath}");
        Debug.Log($"UnityEngine.AddressableAssets.Addressables.BuildPath: {UnityEngine.AddressableAssets.Addressables.BuildPath}");
        Debug.Log($"UnityEngine.AddressableAssets.Addressables.PlayerBuildDataPath: {UnityEngine.AddressableAssets.Addressables.PlayerBuildDataPath}");
       

        Addressables.LoadSceneAsync("GirlScene");
    }

}
