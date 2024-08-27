using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectToGameObject> kitchenObjectToGameObjectList;
    [Serializable]
    public struct KitchenObjectToGameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        plateKitchenObject.OnIngredientAdd += VisualizeIngredient;
        foreach(KitchenObjectToGameObject kitchenObjectToGameObject in kitchenObjectToGameObjectList)
        {
            kitchenObjectToGameObject.gameObject.SetActive(false);
        }
    }

    private void VisualizeIngredient(object sender, PlateKitchenObject.OnIngredientAddArgs e)
    {
        foreach(KitchenObjectToGameObject kitchenObjectToGameObject in kitchenObjectToGameObjectList)
        {
            if(kitchenObjectToGameObject.kitchenObjectSO == e.ingredient)
            {
                kitchenObjectToGameObject.gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
