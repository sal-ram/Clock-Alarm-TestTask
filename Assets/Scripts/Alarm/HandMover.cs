using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Assets.Scripts
{
    public class HandMover : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform _transformParent;
        [SerializeField] private Transform _point;
        [SerializeField] private Transform _center;
        [SerializeField] private float _oneTimeAngle;

        public event Action<float> TimeUpdated;

        private Vector3 firstVector;
        private Vector3 secondVector;
        private Vector3 offset;

        private bool IsDragged = false;
        private bool IsActive = false;

        private void Update()
        {
            if (IsActive)
            {
                firstVector = _point.position - _center.position;

                if (IsDragged)
                {
                    var angle = SignedAngleBetween(firstVector, secondVector, Vector3.forward);
                    _transformParent.eulerAngles += new Vector3(0, 0, angle);
                    TimeUpdated.Invoke(angle);
                }
            }
        }

        private float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
        {
            float angle = Vector3.Angle(a, b);
            float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));

            float signed_angle = angle * sign;

            return signed_angle;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsActive)
            {
                IsDragged = true;
                secondVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;         // for PC
                //secondVector = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + offset;      // for Android
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsActive)
            {
                //offset = firstVector - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);           // for Android
                offset = firstVector - Camera.main.ScreenToWorldPoint(Input.mousePosition);                  // for PC
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsActive)
                IsDragged = false;
        }

        public void TurnHandMover(float time)
        {
            var angle = time * _oneTimeAngle;
            _transformParent.eulerAngles += new Vector3(0, 0, angle);
        }

        public void SetNewTime(float time)
        {
            _transformParent.eulerAngles = new Vector3(0, 0, -1 * _oneTimeAngle * time);
        }

        public void ActivateHand(bool IsActive)
        {
            this.IsActive = IsActive;
        }
    }
}
