# Reward Machine Agent
An API for controlling agents in Unity (3D real-time engine). The agent uses a 'reward machine' to guide its actions.

## Description
The API is based on my work with Reward Machines: Instead of rewarding an agent for a given action in a given state, a reward machine allows one to specify rewards for sequences of observations. Every observation is mapped from an action-state pair. For instance, if you want to make your agent kick the ball twice in a row, then give it a reward only after seeing that it has kicked the ball twice in a row. A regular reward function would only be able to give the same reward for the first and second kick.
