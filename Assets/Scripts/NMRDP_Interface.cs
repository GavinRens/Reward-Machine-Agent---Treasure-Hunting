//using System;
using System.Collections.Generic;
using TreasureHunting;

public interface NMRDP_Interface
{
    static HashSet<Action> Actions { get; }  // Define enum Action in the code (and same namespace) instantiating this NMRDP
    static List<Observation> Observations { get; }  // Define enum Observation in the code (and same namespace) instantiating this NMRDP
    static List<State> States { get; }  // Define class State in the code (and same namespace) instantiating this NMRDP
    RewardMachine RewardMachine { get; }

    /// <summary>
    /// Specify what states are in States
    /// </summary>
    /// <returns></returns>
    //List<State> GenerateStates();

    // Define the reward machine
    RewardMachine DefineRewardMachine();

    // Define the transition function; the probability that an action performed in stateFrom will end up in stateTo
    float TransitionFunction(State stateFrom, Action action, State stateTo);

    // Define the function that maps action-state pairs to observations
    public Observation ObservationFunction(Action a, State s);

    // Calculate immediate reward, given the reward machine and the current action taken in the agent's current state
    public float ImmediateReward(Action action, State state);
}







