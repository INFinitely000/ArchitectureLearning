using System;
using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour
    {
        [field: SerializeField] public float Sensitivity { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
        
        public Player Player { get; private set; }
        public Camera Camera { get; private set; }
        
        public void SetPlayer(Player player) => Player = player;

        private void Awake() => Camera = GetComponent<Camera>();

        private void LateUpdate()
        {
            if (Player == null) return;
            
            var from = transform.position;
            var to = Player.transform.position + Offset;
            var time = Sensitivity * Time.deltaTime;
            
            transform.position = Vector3.Lerp(from, to, time);
        }
    }
}