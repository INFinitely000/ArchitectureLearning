using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.MainPlayer
{
    public class PlayerHook : MonoBehaviour
    {
        [field: SerializeField] public DistanceJoint2D Joint { get; private set; }

        public IHookPoint Point { get; private set; }
        public bool IsGrabbed => Point != null;

        public UnityEvent Grabbed;
        public UnityEvent Released;
        

        public void Grab(IHookPoint point)
        {
            Joint.enabled = true;
            Joint.connectedBody = point.Rigidbody;
            Joint.autoConfigureDistance = false;
            Joint.autoConfigureConnectedAnchor = false;

            Joint.anchor = Vector2.zero;
            Joint.connectedAnchor = Vector2.zero;

            Grabbed?.Invoke();
            
            Point = point;
        }

        private void Update()
        {
            if (IsGrabbed == false) return;

            Joint.distance = Vector3.Distance(transform.position, Point.Rigidbody.position);
        }

        public void Release()
        {
            Joint.enabled = false;
            Joint.connectedBody = null;

            Released?.Invoke();
            
            Point = null;
        }
    }
}