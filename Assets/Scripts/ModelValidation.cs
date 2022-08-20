using UnityEngine;


/// <summary>
/// Use this code to validate that the implemented transition function is correct. Whenever "mass != 1f" (see below) is true, the transition function is ill-dfined.
/// </summary>
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
}
