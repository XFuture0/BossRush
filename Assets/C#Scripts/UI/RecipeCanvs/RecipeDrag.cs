using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform LastParent;
    public LayerMask RecipeSlot;
    public LayerMask RecipeItem;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count > 0)
        {
            LastParent = eventData.pointerDrag.transform.parent;
            eventData.pointerDrag.transform.SetParent(transform.parent.parent.parent.parent.parent);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count > 0)
        {
            transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count > 0)
        {
            List<RaycastResult> result = new List<RaycastResult>();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerEventData, result);
            foreach (RaycastResult raycastResult in result)
            {
                if (raycastResult.gameObject.name != null)
                {
                    switch (raycastResult.gameObject.name)
                    {
                        case "RecipeSlot":
                            eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count -= 1;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<RecipeSlotData>().ThisFruit = eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                            eventData.pointerDrag.transform.SetParent(LastParent);
                            RecipeManager.Instance.CheckRecipeMenu();
                            return;
                        case "RecipeSlot1":
                            eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count -= 1;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<RecipeSlotData>().ThisFruit = eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                            eventData.pointerDrag.transform.SetParent(LastParent);
                            RecipeManager.Instance.CheckRecipeMenu();
                            return;
                        case "RecipeSlot2":
                            eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count -= 1;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<RecipeSlotData>().ThisFruit = eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                            eventData.pointerDrag.transform.SetParent(LastParent);
                            RecipeManager.Instance.CheckRecipeMenu();
                            return;
                        case "RecipeSlot3":
                            eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit.Count -= 1;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<RecipeSlotData>().ThisFruit = eventData.pointerDrag.GetComponent<RecipeBag>().ThisFruit;
                            raycastResult.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
                            eventData.pointerDrag.transform.SetParent(LastParent);
                            RecipeManager.Instance.CheckRecipeMenu();
                            return;
                    }
                }
            }
        }
        eventData.pointerDrag.transform.SetParent(LastParent);
    }
}
