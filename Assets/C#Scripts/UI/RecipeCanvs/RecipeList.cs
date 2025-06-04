using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeList : MonoBehaviour
{
    public GameObject RecipeBag;
    public Transform Content;
    private void OnEnable()
    {
        RefreshRecipeBag();
    }
    private void RefreshRecipeBag()
    {
        for(int i = 0; i < Content.childCount; i++)
        {
            Destroy(Content.GetChild(i).gameObject);
        }//Çå¿Õ
        foreach (var fruit in FruitManager.Instance.Fruitdata.Fruits)
        {
           var NewRecipeBag = Instantiate(RecipeBag, Content);
            NewRecipeBag.GetComponent<RecipeBag>().RefreshData(fruit);
        }
    }
}
