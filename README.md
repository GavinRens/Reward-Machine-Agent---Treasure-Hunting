# Reward Machine Agent for Treasure Hunting
A treasure-hunting agent controlled by a MCTS planner using a Reward Machine. 

## Description
A framework for controlling agents in Unity (3D real-time engine). The algorithm in the framework is based on my work with Reward Machines: Instead of rewarding an agent for a given action in a given state, a reward machine allows one to specify rewards for sequences of observations. Every observation is mapped from an action-state pair. For instance, if you want to make your agent kick the ball twice in a row, then give it a reward only after seeing that it has kicked the ball twice in a row. A regular reward function would only be able to give the same reward for the first and second kick.

I implemented a Monte Carlo Tree Search (MCTS) planner, which plans over the given reward machine. In this treasure-hunting environment, the observation mapping function is deterministic. This means that the MCTS planner can be based on a (fully observable) Markov decision process (MDP). The project (repository) for a *patrolling* reward machine agent can deal with probabilistic observations. The patrolling environment thus makes use of a MCTS planner based on a partially observable Markov decision process (POMDP).
