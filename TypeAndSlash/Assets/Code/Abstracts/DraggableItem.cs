using System;
using Code.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Abstracts
{
    public abstract class DraggableItem<T> : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDraggableItem
    {
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected CanvasGroup _canvasGroup;
        private RectTransform _rect;
        private Vector2 _startPos;

        protected virtual void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        protected void OnDisable()
        {
            ResetToDefault();
        }

        public abstract T Data();
        
        public object GetData()
        {
            return Data();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 0.17f;
            _startPos = _rect.anchoredPosition;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rect.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            _canvasGroup.blocksRaycasts = true;
            transform.localScale = Vector3.one;
            _rect.anchoredPosition = _startPos;
        }
        
    }
}