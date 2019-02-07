using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ELS.Flock
{
    public class SimulationManager : MonoBehaviour
    {
        [SerializeField] int amountOfAgents;
        [SerializeField] int spawnAreaSize;
        [SerializeField] GameObject agentPrefab;

        private void Awake()
        {
            for (int i = 0; i < amountOfAgents; i++)
            {
                Instantiate(agentPrefab, Random.insideUnitCircle * spawnAreaSize, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}