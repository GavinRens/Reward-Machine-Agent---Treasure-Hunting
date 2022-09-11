using UnityEngine;


public class Environment : MonoBehaviour
{
    AgentController agentController;
    TreasureHunter_NMRDP_Agent agent;

    
    void Start()
    {
        GameObject agentGO = GameObject.FindGameObjectWithTag("agent");
        agent = agentGO.GetComponent<AgentController>().nmrdpAgent;
    }


    /// <summary>
    /// The state the agent will end up in if it executes the action in the current state
    /// This is the `ground truth', not a model of what is expected <see cref="Agent.GetNextState(Action, State)"/>
    /// </summary>
    /// <param name="action">An action</param>
    /// <param name="currentState">An environment state</param>
    /// <returns>A successor state</returns>
    public static State GetRealNextState(State currentState, Action action)
    {
        if (currentState.name == "startPos")
        {
            if (action == Action.GotoTownSqr)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if(s.name == "townSquare")
                        return s;
        }

        if (currentState.name == "townSquare")
        {
            if (action == Action.GotoEquip)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "equipment")
                        return s;
            if (action == Action.GotoGuide)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "guide")
                        return s;
            if (action == Action.GotoJewlr)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "jeweler")
                        return s;
        }

        if (currentState.name == "equipment")
        {
            if (action == Action.GotoTreas)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "treasure")
                        return s;
            if (action == Action.GotoJewlr)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "jeweler")
                        return s;
        }

        if (currentState.name == "guide")
        {
            if (action == Action.GotoTreas)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "treasure")
                        return s;
            if (action == Action.GotoJewlr)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "jeweler")
                        return s;
        }

        if (currentState.name == "jeweler")
        {
            if (action == Action.GotoEquip)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "equipment")
                        return s;
            if (action == Action.GotoGuide)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "guide")
                        return s;
            if (action == Action.GotoTreas)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "treasure")
                        return s;
        }

        if (currentState.name == "treasure")
        {
            if (action == Action.GotoJewlr)
                foreach (State s in TreasureHunter_NMRDP_Agent.States)
                    if (s.name == "jeweler")
                        return s;
        }

        return currentState;
    }
}
