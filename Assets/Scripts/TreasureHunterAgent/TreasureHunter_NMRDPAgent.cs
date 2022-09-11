using System.Collections.Generic;


    public class TreasureHunter_NMRDP_Agent : NMRDP_Agent
    {
        MCTS mctsPlanner;

        public TreasureHunter_NMRDP_Agent() : base()
        {
            InitializeAgentState();
            mctsPlanner = new MCTS(this);
        }


        public override RewardMachine DefineRewardMachine()
        {
            RewardMachine rm = new(Agent.Observations);

            var startPos = new rmNode("startPos");
            rm.AddNode(startPos);
            var townSquare = new rmNode("townSquare");
            rm.AddNode(townSquare);
            var haveMap = new rmNode("haveMap");
            rm.AddNode(haveMap);
            var atEquip = new rmNode("atEquip");
            rm.AddNode(atEquip);
            var haveEquip = new rmNode("haveEquip");
            rm.AddNode(haveEquip);
            var atGuide = new rmNode("atGuide");
            rm.AddNode(atGuide);
            var haveGuide = new rmNode("haveGuide");
            rm.AddNode(haveGuide);
            var atTreas = new rmNode("atTreas");
            rm.AddNode(atTreas);
            var haveTreas = new rmNode("haveTreas");
            rm.AddNode(haveTreas);
            var atJewlr = new rmNode("atJewlr");
            rm.AddNode(atJewlr);
            var treasSold = new rmNode("treasSold");
            rm.AddNode(treasSold);

            rm.ActiveNode = startPos;

            rm.AddEdge(startPos, townSquare, Observation.AtTownSqr, 1f);
            rm.AddEdge(townSquare, haveMap, Observation.HaveMap, 1f);
            rm.AddEdge(haveMap, atEquip, Observation.AtEquipment, 1f);
            rm.AddEdge(atEquip, haveEquip, Observation.HaveEquipment, 1f);
            rm.AddEdge(haveMap, atGuide, Observation.AtGuide, 1f);
            rm.AddEdge(atGuide, haveGuide, Observation.HaveGuide, 1f);
            rm.AddEdge(haveEquip, atTreas, Observation.AtTreasure, 1f);
            rm.AddEdge(haveGuide, atTreas, Observation.AtTreasure, 1f);
            rm.AddEdge(atTreas, haveTreas, Observation.HaveTreasure, 1f);
            rm.AddEdge(haveTreas, atJewlr, Observation.AtJeweler, 1f);
            rm.AddEdge(atJewlr, treasSold, Observation.TreasureSold, 10f);
            rm.AddEdge(treasSold, atEquip, Observation.AtEquipment, 1f);
            rm.AddEdge(treasSold, atGuide, Observation.AtGuide, 1f);

            return rm;
        }


        public override List<State> GenerateStates()
        {
            states = new List<State>();
            states.Add(new State("startPos"));
            states.Add(new State("townSquare"));
            states.Add(new State("equipment"));
            states.Add(new State("guide"));
            states.Add(new State("jeweler"));
            states.Add(new State("treasure"));
            return states;
        }


        public override void InitializeAgentState()
        {
            CurrentState = States[0];
        }


        public override bool HasFinished(State state)
        {
            return false;
        }


        public override float TransitionFunction(State stateFrom, Action action, State stateTo)
        {
            if (stateFrom.name == "startPos")
            {
                if (action == Action.GotoTownSqr)
                {
                    if (stateTo.name == "townSquare")
                        return 1f;
                }
                else if (stateTo.name == "startPos")
                    return 1f;  // for all other actions, the agent remains in same state
            }

            if (stateFrom.name == "townSquare")
            {
                if (action == Action.GotoEquip)
                {
                    if (stateTo.name == "equipment")
                        return 1f;
                }
                else if (action == Action.GotoGuide)
                {
                    if (stateTo.name == "guide")
                        return 1f;
                }
                else if (action == Action.GotoJewlr)
                {
                    if (stateTo.name == "jeweler")
                        return 1f;
                }
                else if (stateTo.name == "townSquare")
                    return 1f;  // for all other actions, the agent remains in same state
            }

            if (stateFrom.name == "equipment")
            {
                if (action == Action.GotoTreas)
                {
                    if (stateTo.name == "treasure")
                        return 1f;
                }
                else if (action == Action.GotoJewlr)
                {
                    if (stateTo.name == "jeweler")
                        return 1f;
                }
                else if (stateTo.name == "equipment")
                    return 1f;  // for all other actions, the agent remains in same state
            }

            if (stateFrom.name == "guide")
            {
                if (action == Action.GotoTreas)
                {
                    if (stateTo.name == "treasure")
                        return 1f;
                }
                else if (action == Action.GotoJewlr)
                {
                    if (stateTo.name == "jeweler")
                        return 1f;
                }
                else if (stateTo.name == "guide")
                    return 1f;  // for all other actions, the agent remains in same state
            }

            if (stateFrom.name == "jeweler")
            {
                if (action == Action.GotoTreas)
                {
                    if (stateTo.name == "treasure")
                        return 1f;
                }
                else if (action == Action.GotoEquip)
                {
                    if (stateTo.name == "equipment")
                        return 1f;
                }
                else if (action == Action.GotoGuide)
                {
                    if (stateTo.name == "guide")
                        return 1f;
                }
                else if (stateTo.name == "jeweler")
                    return 1f;  // for all other actions, the agent remains in same state
            }

            if (stateFrom.name == "treasure")
            {
                if (action == Action.GotoJewlr)
                {
                    if (stateTo.name == "jeweler")
                        return 1f;
                }
                else if (stateTo.name == "treasure")
                    return 1f;  // for all other actions, the agent remains in same state
            }

            return 0f;
        }


        // Return the observation perceived in (next state) s after performing a
        public override Observation ObservationFunction(Action a, State s)
        {
            if (a == Action.GotoTownSqr)
                if (s.name == "townSquare")
                    return Observation.AtTownSqr;

            if (a == Action.GotoEquip)
                if (s.name == "equipment")
                    return Observation.AtEquipment;

            if (a == Action.GotoGuide)
                if (s.name == "guide")
                    return Observation.AtGuide;

            if (a == Action.GotoTreas)
                if (s.name == "treasure")
                    return Observation.AtTreasure;

            if (a == Action.GotoJewlr)
                if (s.name == "jeweler")
                    return Observation.AtJeweler;

            if (a == Action.Buy)
            {
                if (s.name == "equipment")
                    return Observation.HaveEquipment;
                if (s.name == "guide")
                    return Observation.HaveGuide;
            }

            if (a == Action.Sell)
                if (s.name == "jeweler")
                    return Observation.TreasureSold;

            if (a == Action.Collect)
            {
                if (s.name == "townSquare")
                    return Observation.HaveMap;
                if (s.name == "treasure")
                    return Observation.HaveTreasure;
            }

            if (a == Action.No_Op)
                return Observation.Null;

            return Observation.Null;  // all other possibilities produce the null observation
        }


        public override Action SelectAction(State currentState)
        {
            return mctsPlanner.SelectAction(currentState);
        }


        public bool isNavigationAction(Action a)
        {
            switch (a)
            {
                case Action.GotoGuide: return true;
                case Action.GotoTreas: return true;
                case Action.GotoEquip: return true;
                case Action.GotoTownSqr: return true;
                case Action.GotoJewlr: return true;
                default: return false;
            }
        }
    }
