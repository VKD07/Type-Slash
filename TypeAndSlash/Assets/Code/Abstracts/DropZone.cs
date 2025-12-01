using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Abstracts
{
    public abstract class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler
    {
        public abstract void OnDrop(PointerEventData eventData);
        public abstract void OnPointerEnter(PointerEventData eventData);
    }
}