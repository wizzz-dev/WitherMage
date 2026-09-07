using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform hoverRect;
    private Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1.5f);
    public bool hover = false;

    void OnEnable()
    {
        hoverRect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData pointer)
    {
        hover = true;
    }
    public void OnPointerExit(PointerEventData pointer)
    {
        hover = false;
    }
    void Update()
    {
        if(hover)
        {
            hoverRect.localScale = Vector3.MoveTowards(hoverRect.localScale, hoverScale, 2f * Time.deltaTime);
        }
        else
        {
            hoverRect.localScale = Vector3.MoveTowards(hoverRect.localScale, Vector3.one, 2f * Time.deltaTime);
        }
    }
}
