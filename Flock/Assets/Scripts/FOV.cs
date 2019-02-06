using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ELS.Flock
{
    public class FOV : MonoBehaviour
    {
        [SerializeField] private List<GameObject> agents = new List<GameObject>();

        [SerializeField] private List<GameObject> neighbors = new List<GameObject>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Body" && !agents.Contains(collision.gameObject))
            {
                agents.Add(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(agents.Contains(collision.gameObject))
            {
                agents.Remove(collision.gameObject);
            }
        }



    }
}