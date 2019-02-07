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
            public float speed = 1f;

            public void Update(Transform _origin, Transform _target)
            {
                Vector2 direction = _target.position - _origin.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                _origin.rotation = Quaternion.Slerp(_origin.rotation, rotation, speed * Time.deltaTime);
            }

            public void Update(Transform _origin, Vector2 _target)
            {
                Vector2 direction = _target - (Vector2)_origin.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                _origin.rotation = Quaternion.Slerp(_origin.rotation, rotation, speed * Time.deltaTime);
            }
        }

        [Serializable]
        class Target
        {
            public Transform transform;

            public Vector2 Destination(List<GameObject> neighbors)
            {
                if (neighbors.Count > 0)
                {
                    Vector2 result = neighbors[0].transform.position;

                    if (neighbors.Count > 1)
                    {
                        for (int i = 1; i < neighbors.Count; i++)
                        {
                            result += (Vector2)neighbors[i].transform.position;
                        }

                        result /= neighbors.Count;
                    }

                    return result;
                }

                float angle = transform.localRotation.z * Mathf.Rad2Deg;

                return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }
        }

        [Serializable]
        class Components
        {
            public FOV fov;
        }

        [SerializeField] private Orientation orientation;

        [SerializeField] private Target target;

        [SerializeField] private float speed;

        private Vector2 targetPos;

        [SerializeField] private Components components;

        private void Start()
        {
            float angle = transform.localRotation.z*Mathf.Rad2Deg;
            Debug.Log(angle);
            Debug.Log(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
            
            //components.fov = transform.GetChild(0).GetComponent<FOV>();
        }

        void Update()
        {

            StartCoroutine(DoYourStuff());
        }

        void MoveTowards(Vector2 _destination)
        {
            transform.Translate(_destination * speed * Time.deltaTime);

           
        }

        IEnumerator DoYourStuff()
        {
            orientation.Update(transform, target.Destination(components.fov.agents));

            float angle = transform.localRotation.z * Mathf.Rad2Deg;

            MoveTowards(target.Destination(components.fov.agents));

            Debug.DrawLine(transform.position, target.Destination(components.fov.agents), Color.green);

            //yield return new WaitForEndOfFrame();
            
            yield return new WaitForSeconds(UnityEngine.Random.value);
        }

        
    }
}