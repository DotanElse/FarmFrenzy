using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    public void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipe(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        foreach(Transform child in iconContainer)
        {
            if(child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(KitchenObjectSO kitchenObject in recipeSO.items)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObject.sprite;
        }
    }
}
