
public interface Planner_Interface
{
    /// <summary>
    /// The interface for a planning algorithm
    /// </summary>
    /// <param name="currentState">The state in which the agent is currently</param>
    /// <param name="agent">An optional reference to the agent</param>
    /// <returns>The action that should be executed in the agent's current state</returns>
    public Action SelectAction(State currentState, Agent agent = null);
}
