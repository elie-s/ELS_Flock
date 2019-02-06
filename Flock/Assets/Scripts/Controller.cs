using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ELS.Flock
{
    public class Controller : MonoBehaviour
    {
        [Serializable]
        class Orientation
        {
            public float speed = 20f;

            public void Update(Transform _origin, Transform _target)
            {
                Vector2 direction = _target.position - _origin.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                _origin.rotation = Quaternion.Slerp(_origin.rotation, rotation, speed * Time.deltaTime);
            }
        }

        [Serializable]
        class Target
        {
            public Transform transform;
        }

        [SerializeField] private Orientation orientation;

        [SerializeField] private Transform target;

        private Vector2 targetPos;


        void Update()
        {
            orientation.Update(transform, target);
        }

        
    }
}