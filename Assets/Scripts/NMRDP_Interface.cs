using System.Collections.Generic;


public interface NMRDP_Interface
{
    static HashSet<Action> Actions { get; }  // Define enum Action in the code (and same namespace) instantiating this NMRDP
    static List<Observation> Observations { get; }  // Define enum Observation in the code (and same namespace) instantiating this NMRDP
    static List<State> States { get; }  // Define class State in the code (and same namespace) instantiating this NMRDP
    RewardMachine RewardMachine { get; }

    /// <summary>
    /// Defines the reward machine
    /// </summary>
    /// <returns></returns>
    RewardMachine DefineRewardMachine();

    /// <summary>
    /// The state transition function; 
    /// </summary>
    /// <param name="from">The originating state</param>
    /// <param name="a">An action</param>
    /// <param name="to">The successor state</param>
    /// <returns>The probability that an action performed in state "from" will end up in state "to"</returns>
    public float TransitionFunction(State from, Action a, State to);

    /// <summary>
    /// Defines which observation is made, given an action and a state reached
    /// </summary>
    /// <param name="a">An action</param>
    /// <param name="s">An environment state</param>
    /// <returns>An observation</returns>
    public Observation GetObservation(Action a, State s);

    /// <summary>
    /// Calculates an immediate reward, given the reward machine
    /// </summary>
    /// <param name="a">An action</param>
    /// <param name="s">An environment state</param>
    /// <returns>A reward</returns>
    public float ImmediateReward(Action a, State s);

    /// <summary>
    /// Determine which node should become active, given the currently active node and the current observation
    /// </summary>
    /// <param name="z">The current observation</param>
    /// <param name="currentActiveNode">The currently active node</param>
    /// <returns>The new active node of the reward machine</returns>
    public rmNode GetNextActiveRMNode(Observation z, rmNode currentActiveNode);
}







