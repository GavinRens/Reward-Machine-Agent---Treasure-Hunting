using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    AgentController agentController;
    //Test_NMRDP_Agent agent;
    TreasureHunter_NMRDP_Agent agent;

    
    void Start()
    {
        GameObject agentGO = GameObject.FindGameObjectWithTag("agent");
        agent = agentGO.GetComponent<AgentController>().nmrdpAgent;
    }


    //public static State GetNextState(State currentState, Action action)
    //{
    //    if (currentState.number == 0 && action == Action.Goal1)
    //        foreach (State s in Test_NMRDP_Agent.States)
    //            if (s.number == 1)
    //                return s;
    //    if (currentState.number == 1 && action == Action.Goal2)
    //        foreach (State s in Test_NMRDP_Agent.States)
    //            if (s.number == 2)
    //                return s;

    //    if (currentState.number == 2)
    //        return currentState;

    //    if(action == Action.No_Op)
    //        return currentState;

    //    return null;
    //}

    public static State GetNextState(State currentState, Action action)
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
