using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private ItemSlot[] itemSlots;
    
    private static Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    private Vector3 basePos;
    private Transform originalParent;
    private LayoutGroup parentLayoutGroup;

    public bool releasedOnItemSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemSlots = FindObjectsOfType<ItemSlot>();
        
        originalParent = transform.parent;
        parentLayoutGroup = originalParent.GetComponent<LayoutGroup>();
    }

    private void Start()
    {
        // Store the original position of this item
        if (!originalPositions.ContainsKey(gameObject))
        {
            originalPositions[gameObject] = transform.localPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (parentLayoutGroup != null)
        {
            parentLayoutGroup.enabled = false;
        }

        basePos = originalPositions[gameObject];
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        releasedOnItemSlot = false;
        
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.isPointerOver)
            {
                itemSlot.DeEquipSpell();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.lossyScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        CheckIfReleasedOnItemSlot();

        if (!releasedOnItemSlot)
        {
            transform.localPosition = basePos;
            transform.SetParent(originalParent);
        }

        if (parentLayoutGroup != null)
        {
            parentLayoutGroup.enabled = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    private void CheckIfReleasedOnItemSlot()
    {
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.isPointerOver && itemSlot.isEmpty)
            {
                releasedOnItemSlot = true;
                transform.position = itemSlot.transform.position;
      
                transform.SetParent(itemSlot.transform);
                
                itemSlot.EquipSpell(GetComponent<InventorySpellDisplay>().spell);
                    
                break;
            }
        }
    }
}
