using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelValidation : MonoBehaviour
{
    NMRDP_Agent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = new TreasureHunter_NMRDP_Agent();

        Debug.Log("-------- START TRANS FUNC VALIDATION --------");

        foreach(State s in Agent.States)
            foreach(Action a in Agent.Actions)
            {
                float mass = 0;
                foreach (State ss in Agent.States)
                {
                    mass += agent.TransitionFunction(s, a, ss);
                }
                if(mass != 1f)
                    Debug.Log(s.name + ", " + a + ", " + mass);
            }

        Debug.Log("-------- END TRANS FUNC VALIDATION --------");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
