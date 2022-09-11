using System.Collections.Generic;


public abstract class NMRDP_Agent : Agent, NMRDP_Interface, Planner_Interface
{
    RewardMachine rewardMachine;
    System.Random rand;

    public NMRDP_Agent() : base()
    {
        rewardMachine = DefineRewardMachine();
        rand = new System.Random();
    }

    public rmNode GetNextActiveRMNode(Observation observation, rmNode currentActiveNode)
    {
        foreach (rmEdge e in currentActiveNode.edges)
            if (e.observation == observation)
            {
                //UnityEngine.Debug.Log("Next active node: " + e.end.name);
                return e.end;
            }

        //UnityEngine.Debug.Log("Active node not advanced !!");
        return currentActiveNode;  // If observation does not point to another node, then by default, the active node does not chnage
    }


    // For Agent (other methods to be implemented in final agent instance)

    public override State GetNextState(Action action, State state)
    {
        float r = (float)rand.NextDouble();
        float mass = 0;
        foreach (State ss in States)
        {
            mass += TransitionFunction(state, action, ss);
            if (r <= mass)
                return ss;
        }
        return null;
    }


    // For NMRDP_Interface

    public RewardMachine RewardMachine { get { return rewardMachine; } }

    public abstract RewardMachine DefineRewardMachine();

    public abstract float TransitionFunction(State stateFrom, Action action, State stateTo);

    public abstract Observation GetObservation(Action a, State s);

    public float ImmediateReward(Action action, State state)
    {// Note that state is the state reached via action
        Observation obsrv = GetObservation(action, state);
        foreach (rmEdge e in RewardMachine.ActiveNode.edges)
            if (e.observation == obsrv)
                return e.reward;

        //UnityEngine.Debug.Log(string.Format("No edge with an observation matching LabelingFunction(" + action + ", " + state.number + ")"));
        return 0;
    }

    // Overloaded for use in MCTS algorithm
    public float ImmediateReward(Action action, State state, rmNode activeNode)
    {// Note that state is the state reached via action
        Observation obsrv = GetObservation(action, state);
        foreach (rmEdge e in activeNode.edges)  
            if (e.observation == obsrv)
                return e.reward;

        //UnityEngine.Debug.Log(string.Format("No edge with an observation matching LabelingFunction(" + action + ", " + state.number + ")"));
        return 0;
    }
    
    
    // For Planner_Interface

    public abstract Action SelectAction(State currentState, Agent agent = null);   
}



