//using System;
//using System.Collections;
//using System.Collections.Generic;

//public class Test_NMRDP_Agent : NMRDP_Agent
//{
//    MCTS mctsPlanner;

//    public Test_NMRDP_Agent() : base()
//    {
//        InitializeAgentState();
//        mctsPlanner = new MCTS(this);
//    }


//    public override RewardMachine DefineRewardMachine()
//    {
//        //throw new NotImplementedException();

//        RewardMachine rm = new(Observations);

//        var startPos = new rmNode("startPos");
//        rm.AddNode(startPos);
//        rm.ActiveNode = startPos;
//        var goal1 = new rmNode("goal1");
//        rm.AddNode(goal1);
//        var goal2 = new rmNode("goal2");
//        rm.AddNode(goal2);

//        rm.AddEdge(startPos, goal1, Observation.In1, 1f);
//        rm.AddEdge(goal1, goal2, Observation.In2, 1f);
//        rm.AddEdge(goal2, goal2, Observation.Null, 1f);

//        return rm;
//    }
    
//    public override float TransitionFunction(State stateFrom, Action action, State stateTo)
//    {
//        //throw new NotImplementedException();
        
//        if (stateFrom.number == 0)
//        {
//            if (action == Action.Goal1 && stateTo.number == 1)
//                return 1f;
//            if (action == Action.Goal2 && stateTo.number == 0)
//                return 1f;
//            if (action == Action.No_Op && stateTo.number == 0)
//                return 1f;
//        }

//        if (stateFrom.number == 1)
//        {
//            if (action == Action.Goal1 && stateTo.number == 1)
//                return 1f;
//            if (action == Action.Goal2 && stateTo.number == 2)
//                return 1f;
//            if (action == Action.No_Op && stateTo.number == 1)
//                return 1f;
//        }

//        if (stateFrom.number == 2)
//        {
//            if (action == Action.Goal1 && stateTo.number == 2)
//                return 1f;
//            if (action == Action.Goal2 && stateTo.number == 2)
//                return 1f;
//            if (action == Action.No_Op && stateTo.number == 2)
//                return 1f;
//        }

//        return 0f;
//    }
    
//    // Return the observation perceived in (next state) s after performing a
//    public override Observation ObservationFunction(Action a, State s)
//    {
//        if (a == Action.Goal1 && s.number == 1)
//            return Observation.In1;
//        if (a == Action.Goal2 && s.number == 2)
//            return Observation.In2;
//        return Observation.Null;  // all other possibilities produce the null observation
//    }


//    // For testing
//    //public override Action SelectAction(State currentState, Agent agent = null)
//    //{
//    //    if (currentState.number == 0)
//    //        return Action.Goal1;

//    //    if (currentState.number == 1)
//    //        return Action.Goal2;

//    //    return Action.No_Op;
//    //}
//    public override Action SelectAction(State currentState, Agent agent = null)
//    {
//        return mctsPlanner.SelectAction(currentState);
//    }


//    // For Agent

//    public override List<State> GenerateStates()
//    {
//        states = new List<State>();
//        states.Add(new State(0));
//        states.Add(new State(1));
//        states.Add(new State(2));
//        return states;
//    }

//    public override void InitializeAgentState()
//    {
//        //throw new NotImplementedException();

//        // Example definitoin
//        CurrentState = States[0];
//    }

//    public override bool HasFinished(State state)
//    {
//        return false;
//    }
//}


