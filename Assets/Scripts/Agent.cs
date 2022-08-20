using System.Collections.Generic;


public abstract class Agent
{
    State currentState;
    Action currentAction;
    static HashSet<Action> actions;
    static List<Observation> observations;
    static protected List<State> states; 
    
    public Agent()
    {
        currentState = null;  // must be initialized in the agent controller using this.InitializeAgentState()
        currentAction = Action.No_Op;

        actions = new HashSet<Action>();
        foreach (Action a in System.Enum.GetValues(typeof(Action)))
            actions.Add(a);

        observations = new List<Observation>();
        foreach (Observation o in System.Enum.GetValues(typeof(Observation)))
            observations.Add(o);

        states = GenerateStates();

        InitializeAgentState();
    }

    /// <summary>
    /// Keeps track of the agent's current state
    /// </summary>
    public State CurrentState { get { return currentState; } set { currentState = value; } }
    
    /// <summary>
    /// Remembers the next intended or recently executed action
    /// </summary>
    public Action CurrentAction { get { return currentAction; } set { currentAction = value; } }
    
    /// <summary>
    /// The set of actions the agent can perform
    /// </summary>
    public static HashSet<Action> Actions { get { return actions; } }

    /// <summary>
    /// The (possibly empty) list of observations the agent can make
    /// </summary>
    public static List<Observation> Observations { get { return observations; } }

    /// <summary>
    /// The list of states the agent can be in
    /// </summary>
    public static List<State> States { get { return states; } }

    /// <summary>
    /// Returns a successor state
    /// </summary>
    /// <param name="action">The action executed</param>
    /// <param name="state">The state in which the action was executed</param>
    /// <returns></returns>
    public abstract State GetNextState(Action action, State state);

    /// <summary>
    /// Generates the states that the agent can be in
    /// </summary>
    /// <returns>A list of agent states</returns>
    public abstract List<State> GenerateStates();

    /// <summary>
    /// Sets the state that the agent will start in
    /// </summary>
    public abstract void InitializeAgentState();

    /// <summary>
    /// Defines when the agent will stop being active, i.e., defines the end of an episode. Should always return false if the agent does not stop.
    /// </summary>
    /// <param name="state">An 'end' state; where the agent stops or the episode ends</param>
    /// <returns></returns>
    public abstract bool HasFinished(State state);
}
