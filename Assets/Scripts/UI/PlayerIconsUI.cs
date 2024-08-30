using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    // Start is called before the first frame update

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    void Start()
    {
        plateKitchenObject.OnIngredientAdd += UpdateVisual;
    }
    private void UpdateVisual(object sender, PlateKitchenObject.OnIngredientAddArgs e)
    {
        foreach (Transform child in transform)
        {
            if(child == iconTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetItemsOnPlate())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
