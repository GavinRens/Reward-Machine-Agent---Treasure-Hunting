using System.Collections.Generic;


public class MCTS : Planner_Interface
{
    public float realReturn;
    public static List<Node> Nodes;

    NMRDP_Agent agent;
    System.Random rand;
    List<Action> A_list;

    static readonly float gamma = Parameters.discountFactor;


    public MCTS(NMRDP_Agent _agent)
    {
        realReturn = 0;
        Nodes = new List<Node>();
        agent = _agent;
        rand = new System.Random();
        A_list = new List<Action>(agent.Actions);
    }
    
    
    public class Node
    {
        public State state;
        public Dictionary<Action, float> Q;  // Q(s,a) is reped by Q[a]
        public Dictionary<Action, int> N;  // N(s,a) is reped by N[a]
        public int Ns;  // Number of actions performed in s
        public HashSet<Action> triedActs;
        public Dictionary<Action, Node> children;  // children[a] is the node reached via action a
        public rmNode activeRMNode;  // a reference to the reward machine node that should be active if this MCTS node is reached
        
        public Node(State s, rmNode n, NMRDP_Agent agent)
        {
            state = s;
            activeRMNode = n;
            Q = new Dictionary<Action, float>();
            N = new Dictionary<Action, int>();
            foreach (Action a in agent.Actions)
            {
                Q.Add(a, 0);
                N.Add(a, 0);
            }
            Ns = 0;
            triedActs = new HashSet<Action>();  // record of actions tried in this node
            children = new Dictionary<Action, Node>();

            Nodes.Add(this);
        }
    }


    Action UCT(Node n)
    {
        Action bestAction = Action.No_Op;
        float maxValue = -float.MaxValue;
        foreach (Action a in agent.Actions)
        {
            float val = n.Q[a] + System.MathF.Sqrt(2 * System.MathF.Log(n.Ns) / n.N[a]);
            if (val > maxValue)
            {
                maxValue = val;
                bestAction = a;
            }
        }
        return bestAction;
    }


    float RollOut(State s, int d, rmNode rmn)
    {
        if (d == 0 || agent.HasFinished(s)) return 0;
        Action a = A_list[rand.Next(0, A_list.Count-1)];
        State ss = agent.GetNextState(a, s);
        float r = agent.ImmediateReward(a, ss, rmn);
        Observation z = agent.GetObservation(a, ss);
        rmNode newActiveRMNode = agent.GetNextActiveRMNode(z, rmn);
        return r + gamma * RollOut(ss, d - 1, newActiveRMNode);
    }
    
    
    float Simulate(Node n, int d)
    {
        if (d == 0) return 0;

        Action a;
        State s = n.state;
        Node nn;
        float futureValue;

        if (!agent.Actions.SetEquals(n.triedActs))  // some actions have not been tried at this node 
        {
            // Make temporary copy of all actions; a set
            HashSet<Action> tmpA = new HashSet<Action>(agent.Actions);
            // Keep only actions not yet tried
            tmpA.ExceptWith(n.triedActs);
            // Cast untried action set into a list (amenable to indexing)
            var NotTried = new List<Action>(tmpA);
            // Select untried action randomly
            a = NotTried[rand.Next(0, NotTried.Count-1)];
            // Add the selected action to the set of tried actions
            n.triedActs.Add(a);
            // Select next state
            State ss = agent.GetNextState(a, s);
            // Get the observation due to 'a' and 'ss'
            Observation z = agent.GetObservation(a, ss);
            // Get reference to next active rmNode
            rmNode newActiveRMNode = agent.GetNextActiveRMNode(z, n.activeRMNode);
            // Generate a new node
            nn = new Node(ss, newActiveRMNode, agent);
            // Add it to the children of the current node
            n.children.Add(a, nn);
            // Do the rollout stage starting from the state rep'ed by the new node
            futureValue = RollOut(ss, d - 1, newActiveRMNode);
        }
        else  // All actions have been tried from this node
        {
            // Select action to follow down (up?) the tree, using the UCT method
            a = UCT(n);
            // Find child node reached via selected action
            nn = n.children[a];
            // Continue with tree-traversal stage
            futureValue = Simulate(nn, d - 1);
        }

        // Increment nuof times action a was folowed in node n
        n.N[a] += 1;
        // Increment nuof actions followed in node n
        n.Ns += 1;
        // Estimate the value of performing action a in s (of node n) for this iteration
        float r = agent.ImmediateReward(a, nn.state, n.activeRMNode);
        //UnityEngine.Debug.Log("r: " + r);
        float q = r + gamma * futureValue;
        // Update the average estimate for performing action a in s node n
        n.Q[a] += (q - n.Q[a]) / n.N[a];

        return q;
    }
    
    
    public Action SelectAction(State state)
    {
        int I = Parameters.I;
        int D = Parameters.maximumNuofActions; // larger D might be detrimental, because w/ long enough episodes, the goal can be reached no matter the first action

        Node node = new Node(state, agent.RewardMachine.ActiveNode, agent);

        //nuof_nodes_gened = 0;
        int i = 0;

        while (i < I)
        {
            //UnityEngine.Debug.Log("----------------------- " + i + " -----------------------");
            Simulate(node, D);
            i++;
        }
        Action bestAction = Action.No_Op;
        float maxValue = -float.MaxValue;
        foreach (Action a in agent.Actions)
        {
            if (node.Q[a] > maxValue)
            {
                maxValue = node.Q[a];
                bestAction = a;
            }
        }
            
        return bestAction;
    }
}

