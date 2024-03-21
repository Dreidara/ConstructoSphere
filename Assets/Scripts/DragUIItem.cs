using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUIItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private GameObject PrefabToInstantiate;
    [SerializeField] private RectTransform UIDragElement;
    [SerializeField] private RectTransform Canvas;

    // Variables for tracking original positions during the drag
    private Vector2 mOriginalLocalPointerPosition;
    private Vector3 mOriginalPanelLocalPosition;
    private Vector2 mOriginalPosition;


    private void Start()
    {
        mOriginalPosition = UIDragElement.localPosition;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        mOriginalPanelLocalPosition = UIDragElement.localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Canvas,
            data.position,
            data.pressEventCamera,
            out mOriginalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Canvas,
            data.position,
            data.pressEventCamera,
            out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - mOriginalLocalPointerPosition;
            UIDragElement.localPosition = mOriginalPanelLocalPosition + offsetToOriginal;
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        UIDragElement.localPosition = mOriginalPosition;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(ray, out hit, 1000.0f))
        {
            Vector3 worldPoint = hit.point;
            Debug.Log(worldPoint);
            CreateObject(worldPoint);
        }
    }


    public void CreateObject(Vector3 position)
    {
        // Check if the prefab is defined
        if (PrefabToInstantiate == null)
        {
            Debug.Log("No prefab to instantiate");
            return;
        }

        if (PositionWithinCell(position))
        {
            GameObject obj = Instantiate(PrefabToInstantiate, position, Quaternion.identity);
        }
    }

    private bool PositionWithinCell(Vector3 pos)
    {
        return true;
    }
}
