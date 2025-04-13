using UnityEngine;

namespace Gameplay
{
    public class Parallax : MonoBehaviour
    {
        [field: SerializeField] public virtual Transform Target { get; protected set; }
        [field: SerializeField] public SpriteRenderer Renderer { get; private set; }
    
        [field: SerializeField] public float Sensivitity { get; private set; }
        [field: SerializeField] public float RepeatDistance { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }


        public void SetTarget(Transform target) => Target = target;
    
    
        private void LateUpdate()
        {
            if (Target == null) return;

            var position = Vector3.zero;

            position.x = Target.position.x + (Target.position.x * Sensivitity) % RepeatDistance + Mathf.Sign(Target.position.x) * RepeatDistance / 2;
            position.y = Target.position.y;
            position.z = 0f;

            position += Offset;
        
            transform.position = position;
        }
    }
}
