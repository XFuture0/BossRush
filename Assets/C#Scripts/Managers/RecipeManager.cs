using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : SingleTons<RecipeManager>
{
    public GameObject RecipeCanvs;
    public void OpenRecipeCanvs()
    {
        RecipeCanvs.SetActive(true);
        KeyBoardManager.Instance.StopAnyKey = true;
    }
    public void CheckRecipeMenu()
    {
        RecipeCanvs.GetComponent<RecipeCanvs>().CheckRecipeMenu();
    }
}
