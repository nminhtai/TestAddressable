using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {
    [SerializeField] Text sizeText;
    [SerializeField] Text percentText;

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
                            sizeText.text = sizeOp.Result.ToString();
                            Debug.Log($"GetDownloadSizeAsync_Completed: size {sizeOp.Result}");
                            var handle = Addressables.DownloadDependenciesAsync(label);
                            handle.Completed += downloadHandle => {
                                Debug.Log("download complete");
                                percentText.text = "100%";
                                Addressables.LoadSceneAsync("GirlScene");
                            };
                            StartCoroutine(PercentTracking(handle));
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

    IEnumerator PercentTracking(AsyncOperationHandle handle) {
        while (!handle.IsDone) {
            //Debug.Log($"PercentTracking {handle.PercentComplete}");
            percentText.text = $"{handle.PercentComplete}";
            yield return null;
        }
        yield break;
    }    
}
