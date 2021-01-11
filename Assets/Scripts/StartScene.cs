using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
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

    //void test() {
    //    Addressables.InitializeAsync().Completed += objects =>
    //    {
    //        Addressables.CheckForCatalogUpdates().Completed += checkforupdates =>
    //        {
    //            if (checkforupdates.Result.Count > 0)
    //                Addressables.UpdateCatalogs().Completed += updates => Load(label);
    //            else
    //                Load(LabelsToDownload);
    //        };
    //    };
    //}

    const string label = "remote";
    void Start() {
        Debug.Log("StartScene Start");
        Addressables.InitializeAsync().Completed += initHandle => {
            Addressables.CheckForCatalogUpdates().Completed += checkForUpdateHandle => {
                Debug.Log("CheckForCatalogUpdates complete");
                if (checkForUpdateHandle.Result.Count > 0) {
                    foreach (var str in checkForUpdateHandle.Result) {
                        Debug.Log($"update: {str}");
                    }

                    Addressables.UpdateCatalogs().Completed += updateHandle => {
                        Debug.Log("UpdateCatalogs complete");
                        Addressables.GetDownloadSizeAsync(label).Completed += sizeOp => {
                            Debug.Log($"GetDownloadSizeAsync_Completed: size {sizeOp.Result}");
                            var handle = Addressables.DownloadDependenciesAsync(label);
                            handle.Completed += downloadHandle => {
                                Debug.Log("download complete");
                                Addressables.LoadSceneAsync("GirlScene");
                            };
                        };
                    };
                }
                else {
                    Debug.Log("there is no update");
                    Addressables.LoadSceneAsync("GirlScene");
                }
            };
        };
    }

    IEnumerator PercentTracking() {
        //Debug.Log("PercentTracking");
        //Caching.ClearCache();
        yield return new WaitForSeconds(5);

        Addressables.GetDownloadSizeAsync(label).Completed += op => {
            Debug.Log($"GetDownloadSizeAsync_Completed: size {op.Result}");
            var handle = Addressables.DownloadDependenciesAsync(label);
            while (!handle.IsDone) {
                Debug.Log($"PercentTracking {handle.PercentComplete}");
            }
        };
    }

    //IEnumerator UpdateCatalogs() {
    //    List<string> catalogsToUpdate = new List<string>();
    //    AsyncOperationHandle<List<string>> checkForUpdateHandle = Addressables.CheckForCatalogUpdates();
    //    checkForUpdateHandle.Completed += op => {
    //        Debug.Log("UpdateCatalogs completed");
    //        foreach (var item in catalogsToUpdate) {
    //            Debug.Log(item);
    //        }
    //        catalogsToUpdate.AddRange(op.Result);
    //    };
    //    yield return checkForUpdateHandle;
    //    if (catalogsToUpdate.Count > 0) {
    //        AsyncOperationHandle<List<IResourceLocator>> updateHandle = Addressables.UpdateCatalogs(catalogsToUpdate);
    //        yield return updateHandle;
    //    }
    //}



    //private void UpdateCatalogs_Completed(AsyncOperationHandle<List<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>> obj) {
    //    Debug.Log("UpdateCatalogs_Completed");
    //    if (obj.Result != null) {

    //        foreach (var item in obj.Result) {
    //            Debug.Log(item.LocatorId);
    //            foreach (var name in item.Keys) {
    //                Debug.Log(name);

    //            }
    //            Addressables.DownloadDependenciesAsync(item).Completed += DownloadDependenciesAsync_Completed;
    //        }
    //    }



    //}

    //private void DownloadDependenciesAsync_Completed(AsyncOperationHandle obj) {
    //    Debug.Log("DownloadDependenciesAsync_Completed");
    //    Debug.Log($"{obj},{obj.Result}");
    //}

    //private void CheckForCatalogUpdates_Completed(AsyncOperationHandle<List<string>> obj) {
    //    Debug.Log("CheckForCatalogUpdates_Completed");
    //    if (obj.Result != null) {
    //        foreach (var item in obj.Result) {
    //            Debug.Log(item);
    //        }
    //    }

    //}

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
