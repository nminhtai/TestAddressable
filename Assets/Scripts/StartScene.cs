using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StartScene : MonoBehaviour {
    //public AssetReference reference;
    //public Canvas canvas;
    //void Awake() {
    //    reference.InstantiateAsync(canvas.transform, false);
    //}


    //void Start() {
    //    //Caching.ClearCache();
    //    //Addressables.ClearResourceLocators();

    //    //Debug.Log($"Application.dataPath {Application.dataPath}");
    //    //Debug.Log($"Application.streamingAssetsPath {Application.streamingAssetsPath}");
    //    //Debug.Log($"Application.persistentDataPath {Application.persistentDataPath}");
    //    //Debug.Log($"Application.temporaryCachePath {Application.temporaryCachePath}");
    //    //Debug.Log($"Caching.currentCacheForWriting.path {Caching.currentCacheForWriting.path}");
    //    //Debug.Log($"Addressables.BuildPath {Addressables.BuildPath}");


    //    //Debug.Log($"UnityEngine.AddressableAssets.Addressables.RuntimePath: {UnityEngine.AddressableAssets.Addressables.RuntimePath}");
    //    //Debug.Log($"UnityEngine.AddressableAssets.Addressables.BuildPath: {UnityEngine.AddressableAssets.Addressables.BuildPath}");
    //    //Debug.Log($"UnityEngine.AddressableAssets.Addressables.PlayerBuildDataPath: {UnityEngine.AddressableAssets.Addressables.PlayerBuildDataPath}");


    //    //Addressables.LoadSceneAsync("GirlScene");
    //    Addressables.UpdateCatalogs().Completed += StartScene_Completed; 



    //}

    void Start() {
        Addressables.CheckForCatalogUpdates().Completed += CheckForCatalogUpdates_Completed;
        Addressables.UpdateCatalogs().Completed += UpdateCatalogs_Completed;
    }

    private void UpdateCatalogs_Completed(AsyncOperationHandle<List<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>> obj) {
        Debug.Log("UpdateCatalogs_Completed");
        if (obj.Result != null) {
            foreach (var item in obj.Result) {
                Debug.Log(item);
            }
        }
    }

    private void CheckForCatalogUpdates_Completed(AsyncOperationHandle<List<string>> obj) {
        Debug.Log("CheckForCatalogUpdates_Completed");
        if (obj.Result != null) {
            foreach (var item in obj.Result) {
                Debug.Log(item);
            }
        }

    }

    //private void StartScene_Completed(AsyncOperationHandle<List<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>> obj) {
    //    Addressables.LoadSceneAsync("GirlScene");
    //}

    //public IEnumerator Start() {
    //    string key = "GirlScene";
    //    //Clear all cached AssetBundles
    //    Addressables.ClearDependencyCacheAsync(key);

    //    //Check the download size
    //    AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(key);
    //    yield return getDownloadSize;

    //    //If the download size is greater than 0, download all the dependencies.
    //    if (getDownloadSize.Result > 0) {
    //        AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(key);
    //        yield return downloadDependencies;
    //    }

    //    Addressables.LoadSceneAsync("GirlScene");
    //}
}
