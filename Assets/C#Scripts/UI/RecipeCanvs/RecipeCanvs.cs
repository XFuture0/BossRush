using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeCanvs : MonoBehaviour
{
    public Button ReturnButton;
    public Button ClearButton;
    public RecipeSlotData[] recipeSlotDatas;
    public ResultSlot ResultSlot;
    public FruitMenuList FruitMenuList;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(OnReturnButton);
        ClearButton.onClick.AddListener(OnClear);
    }
    private void OnReturnButton()
    {
        KeyBoardManager.Instance.StopAnyKey = false;
        gameObject.SetActive(false);
    }
    private void OnClear()
    {
        foreach (var recipeSlotdata in recipeSlotDatas)
        {
            foreach(var fruit in FruitManager.Instance.Fruitdata.Fruits)
            {
                if(fruit == recipeSlotdata.ThisFruit)
                {
                    fruit.Count++;
                    recipeSlotdata.ThisFruit = null;
                    recipeSlotdata.image.sprite = null;
                }
            }
            ResultSlot.ResultData.sprite = null;
        }
    }
    public void CheckRecipeMenu()
    {
        string NewMenu = "";
        for(int i = 0;i< recipeSlotDatas.Length; i++)
        {
            if (recipeSlotDatas[i].image.sprite != null)
            {
                NewMenu += recipeSlotDatas[i].ThisFruit.Index;
            }
            else if(recipeSlotDatas[i].image.sprite == null)
            {
                NewMenu += "0";
            }
        }
        Debug.Log(NewMenu);
        foreach (var menu in FruitMenuList.MenuList)
        {
            if(NewMenu == menu.JuiceIndex)
            {
                ResultSlot.SetResult(menu.Result.juicesprite);
            }
        }
    }
}
