using System.Collections.Generic;
using System.Diagnostics;


/// <summary>
/// A node of a reward machine
/// </summary>
public class rmNode
{
    public string name;
    public List<rmEdge> edges; // all outgoing edges

    public rmNode(string _name)
    {
        name = _name;
        edges = new List<rmEdge>();
    }

    public rmNode(rmNode node)
    {
        this.name = node.name;
        this.edges = node.edges;
    }

    /// <summary>
    /// Adds an edge going out of this node
    /// </summary>
    /// <param name="e">The edge to be added</param>
    public void AddEdge(rmEdge e)
    {
        edges.Add(e);
    }

    /// <summary>
    /// Removes the edge from this node
    /// </summary>
    /// <param name="e">The edge to be removed</param>
    public void RemoveEdge(rmEdge e)
    {
        edges.Remove(e);
    }
}


/// <summary>
/// An edge of a reward machine (connecting nodes)
/// </summary>
public struct rmEdge
{
    public string name;
    public readonly rmNode start;
    public readonly rmNode end;
    public readonly Observation observation;
    public readonly float reward;

    public rmEdge(string _name, rmNode _start, rmNode _end, Observation _observation, float _reward)
    {
        name = _name;
        start = _start;
        end = _end;
        observation = _observation;
        reward = _reward;
    }

    public rmEdge(rmNode _start, rmNode _end, Observation _observation, float _reward)
    {
        name = "";
        start = _start;
        end = _end;
        observation = _observation;
        reward = _reward;
    }
}


public class RewardMachine
{
    List<Observation> observations;  // Define enum Observation in the code (and same namespace) using the reward machine,
    rmNode activeNode;
    List<rmNode> nodes;
    List<rmEdge> edges;


    // Constructor
    public RewardMachine(List<Observation> _observations)
    {
        //name = _name;
        observations = _observations;
        activeNode = new rmNode("Entry Node");
        nodes = new List<rmNode>();
        edges = new List<rmEdge>();
    }

    // Constructor
    public RewardMachine(List<Observation> _observations, rmNode _activeNode)
    {
        //name = _name;
        observations = _observations;
        activeNode = _activeNode;
        nodes = new List<rmNode> { _activeNode };
    }

    // Constructor
    public RewardMachine(List<Observation> _observations, rmNode _activeNode, List<rmNode> _nodes)
    {
        observations = _observations;
        activeNode = _activeNode;
        nodes = _nodes;
        nodes.Add(_activeNode);
    }

    /// <summary>
    /// Adds a node to the reward machine
    /// </summary>
    /// <param name="n">The node to be added</param>
    public void AddNode(rmNode n)
    {
        nodes.Add(n);
    }

    /// <summary>
    /// Removes the node from the reward machine
    /// </summary>
    /// <param name="n">The node to be removed</param>
    public void RemoveNode(rmNode n)
    {
        nodes.Remove(n);
    }

    /// <summary>
    /// Adds an edge to the reward machine
    /// </summary>
    /// <param name="_start">The node where the edge originates</param>
    /// <param name="_end">The node to which the edge points</param>
    /// <param name="_observation">The observation label for this edge</param>
    /// <param name="_reward">The reward label for this edge</param>
    public void AddEdge(rmNode _start, rmNode _end, Observation _observation, float _reward)
    {
        Debug.Assert(nodes.Contains(_start), "The node at the start of this edge is not part of this reward machine. Nodes must be added to a machine before used when adding edges involving them");
        Debug.Assert(nodes.Contains(_end), "The node at the end of this edge is not part of this reward machine. Nodes must be added to a machine before used when adding edges involving them");
        rmEdge e = new(_start, _end, _observation, _reward);
        _start.AddEdge(e);  // add the edge as an outgoing edge of the start node
    }

    /// <summary>
    /// Removes the edge from this reward machine
    /// </summary>
    /// <param name="e">The edge to be removed</param>
    public void RemoveEdge(rmEdge e)
    {
        edges.Remove(e);
        foreach (rmNode n in nodes)
        {
            if (n.Equals(e.start))
            {
                n.RemoveEdge(e);
                break;
            }
        }
    }

    /// <summary>
    /// Makes the correct node active, given the currently active node and the observation perceived
    /// </summary>
    /// <param name="observation">Current observation</param>
    public void AdvanceActiveNode(Observation observation)
    {
        foreach (rmEdge e in activeNode.edges)
        {
            if (e.observation == observation)
            {
                activeNode = e.end;
                break;
            }
        }
    }

    /// <summary>
    /// The currently active node
    /// </summary>
    public rmNode ActiveNode
    {
        get
        {
            return activeNode;
        }
        set
        {
            if (nodes.Contains(value)) activeNode = value;
            else Debug.Print("The node you are trying to set as active is not part of this reward machine.");
        }
    }

    /// <summary>
    /// Sets the currently active node
    /// </summary>
    /// <param name="n">The node to be made active</param>
    public void SetActiveNode(rmNode n)
    {
        if (nodes.Contains(n))
            activeNode = n;
        else
            Debug.Print("The node you are trying to set as active is not part of this reward machine.");
    }
}






