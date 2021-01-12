using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    [SerializeField] Text sizeText;
    [SerializeField] Text percentText;

    void Awake() {
        //Caching.ClearCache();

        Debug.Log($"Application.dataPath {Application.dataPath}");
        Debug.Log($"Application.streamingAssetsPath {Application.streamingAssetsPath}");
        Debug.Log($"Application.persistentDataPath {Application.persistentDataPath}");
        Debug.Log($"Application.temporaryCachePath {Application.temporaryCachePath}");
        Debug.Log($"Caching.currentCacheForWriting.path {Caching.currentCacheForWriting.path}");
        Debug.Log($"Addressables.BuildPath {Addressables.BuildPath}");

        Debug.Log($"UnityEngine.AddressableAssets.Addressables.RuntimePath: {Addressables.RuntimePath}");
        Debug.Log($"UnityEngine.AddressableAssets.Addressables.BuildPath: {Addressables.BuildPath}");
        Debug.Log($"UnityEngine.AddressableAssets.Addressables.PlayerBuildDataPath: {Addressables.PlayerBuildDataPath}");
    }

    const string label = "remote";
    void Start() {
        Debug.Log("StartScene Start");
        Addressables.InitializeAsync().Completed += initHandle => {
            Addressables.CheckForCatalogUpdates().Completed += checkForUpdateHandle => {
                Debug.Log("CheckForCatalogUpdates complete");
                if (checkForUpdateHandle.Result.Count > 0) {
                    foreach (var str in checkForUpdateHandle.Result) {
                        Debug.Log($"Update: {str}");
                    }

                    Addressables.UpdateCatalogs().Completed += updateHandle => {
                        Debug.Log("UpdateCatalogs complete");
                        Addressables.GetDownloadSizeAsync(label).Completed += sizeOp => {
                            sizeText.text = sizeOp.Result.ToString();
                            Debug.Log($"GetDownloadSizeAsync_Completed: size {sizeOp.Result}");
                            var handle = Addressables.DownloadDependenciesAsync(label);
                            handle.Completed += downloadHandle => {
                                Debug.Log($"Download complete {downloadHandle.Result}");                              

                                percentText.text = "100%";
                                LoadFirstScene();
                            };
                            StartCoroutine(PercentTracking(handle));
                        };
                    };
                }
                else {
                    Debug.Log("there is no update");
                    LoadFirstScene();
                }
            };
        };
        
    }

    void LoadFirstScene() {
        //Addressables.LoadResourceLocationsAsync(new List<string> { "GirlScene", "PlantScene" }).Completed += op=> {
        //    SceneManager.LoadScene("GirlScene");
        //};
        //foreach (var locator in Addressables.ResourceLocators) {
        //    Debug.Log(locator.LocatorId);
        //    foreach (var key in locator.Keys) {
        //        Debug.Log(key);
        //    }
        //}  
        Addressables.LoadSceneAsync("GirlScene");                    
       
    }

    IEnumerator PercentTracking(AsyncOperationHandle handle) {
        while (!handle.IsDone) {
            //Debug.Log($"PercentTracking {handle.PercentComplete}");
            percentText.text = $"{handle.PercentComplete}";
            yield return null;
        }
        yield break;
    }
}
