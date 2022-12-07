using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] AssetReferenceGameObject PlayerAssetReference;
    [SerializeField] AssetReferenceSprite ItemSkinAssetPreference;

    [Header("Loader")]
    [SerializeField] GameObject Loader;

    private void Start()
    {
        Loader.SetActive(true);
        Addressables.InitializeAsync().Completed += OnCompleted;
    }

    private void OnCompleted(AsyncOperationHandle<IResourceLocator> obj)
    {
        PlayerAssetReference.LoadAssetAsync<GameObject>().Completed += playerAsset =>
        {
            PlayerAssetReference.InstantiateAsync().Completed += playerGameObject =>
            {
                Debug.Log(playerGameObject.Result.name);
            };
        };

        ItemSkinAssetPreference.LoadAssetAsync<Sprite>().Completed += data =>
        {
            GameObject[] Items = GameObject.FindGameObjectsWithTag("Item");

            foreach (GameObject item in Items)
            {
                item.GetComponent<SpriteRenderer>().sprite = data.Result;
            }
        };
    }

    private void Update()
    {
        if (PlayerAssetReference.Asset != null && ItemSkinAssetPreference != null && Loader.activeInHierarchy)
        {
            Loader.SetActive(false);
        }
    }
}
