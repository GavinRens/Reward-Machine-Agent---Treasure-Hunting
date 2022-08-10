using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentController : MonoBehaviour
{
    public GameObject townSqr;
    public GameObject equip;
    public GameObject guide;
    public GameObject jewlr;
    public GameObject treasr;
    public GameObject actionStatus;
    TextMeshPro actionStatusText;
    public TreasureHunter_NMRDP_Agent nmrdpAgent;
    //public Test_NMRDP_Agent nmrdpAgent;

    enum Phase { Planning, Execution, Updating }
    Phase phase;
    NavMeshAgent navMeshAgent;
    bool alreadyPlanning;
    bool alreadyExecuting;
    bool waitingToGetPath;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        //navMeshAgent.SetDestination(target1.transform.position);
        navMeshAgent.stoppingDistance = 1.9f;

        nmrdpAgent = new TreasureHunter_NMRDP_Agent();

        phase = Phase.Planning;

        alreadyPlanning = false;
        alreadyExecuting = false;
        waitingToGetPath = false;

        actionStatusText = actionStatus.GetComponent<TextMeshPro>();

        // For testing
        //rmNode activeNode = nmrdpAgent.RewardMachine.ActiveNode;
        //Debug.Log("Active node: " + activeNode.name);
        //foreach (Action act in Agent.Actions)
        //    foreach (State stt in Agent.States)
        //        Debug.Log("ImmediateReward for " + act + ", " + stt.name + ": " + nmrdpAgent.ImmediateReward(act, stt, activeNode));

    }


    void LateUpdate()
    {
        if(phase == Phase.Planning)
        {
            //Debug.Log("----------------------------------");
            //Debug.Log("Entered Planning Phse");
            //Debug.Log("CurrentState: " + nmrdpAgent.CurrentState.name);
            //Debug.Log("waitingToGetPath: " + waitingToGetPath);
            //Debug.Log("alreadyPlanning: " + alreadyPlanning);

            
            if (!waitingToGetPath && !alreadyPlanning)
            {
                alreadyPlanning = true;
                nmrdpAgent.CurrentAction = nmrdpAgent.SelectAction(nmrdpAgent.CurrentState);
                if(nmrdpAgent.CurrentAction != null)
                    actionStatusText.text = nmrdpAgent.CurrentAction.ToString();
                Debug.Log("CurrentAction: " + nmrdpAgent.CurrentAction);

                switch (nmrdpAgent.CurrentAction)
                {
                    case Action.GotoTownSqr:
                        navMeshAgent.SetDestination(townSqr.transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoEquip:
                        navMeshAgent.SetDestination(equip.transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoGuide:
                        navMeshAgent.SetDestination(guide.transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoTreas:
                        navMeshAgent.SetDestination(treasr.transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoJewlr:
                        navMeshAgent.SetDestination(jewlr.transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                }
                alreadyPlanning = false;
            }

            if (nmrdpAgent.isNavigationAction(nmrdpAgent.CurrentAction))
            {
                if (navMeshAgent.hasPath)
                {
                    waitingToGetPath = false;
                    phase = Phase.Execution;
                    //Debug.Log("----------------------------------");
                    //Debug.Log("Entered Execution Phse");
                }
            }
            else
            {
                phase = Phase.Execution;
                //Debug.Log("----------------------------------");
                //Debug.Log("Entered Execution Phse");
            }
        }

        if (phase == Phase.Execution)
        {
            if (nmrdpAgent.isNavigationAction(nmrdpAgent.CurrentAction))
            {
                //Debug.Log("----------------------------------");
                //Debug.Log("Entered Execution Phse");
                //Debug.Log("remainingDistance: " + navMeshAgent.remainingDistance);
                //Debug.Log("hasPath: " + navMeshAgent.hasPath);

                if (navMeshAgent.remainingDistance < Parameters.atTargetDistance)
                {
                    navMeshAgent.ResetPath();
                    phase = Phase.Updating;
                }
            }
            else if(!alreadyExecuting)
            {
                switch (nmrdpAgent.CurrentAction)
                {
                    case Action.Buy:
                        if(nmrdpAgent.CurrentState.name == "equipment")
                            Debug.Log("Buying equipment");
                        if (nmrdpAgent.CurrentState.name == "guide")
                            Debug.Log("Buying guide");
                        break;
                    case Action.Sell:
                        Debug.Log("Selling treasure");
                        break;
                    case Action.Collect:
                        if (nmrdpAgent.CurrentState.name == "townSquare")
                            Debug.Log("Collecting map");
                        if (nmrdpAgent.CurrentState.name == "treasure")
                            Debug.Log("Collecting treasure");
                        break;
                    case Action.No_Op:
                        Debug.Log("Doing nothing");
                        break;
                }
                alreadyExecuting = true;
                Invoke("ChangePhaseToUpdateAfterSeconds", 2f);                
            }
        }

        if (phase == Phase.Updating)
        {
            //Debug.Log("----------------------------------");
            //Debug.Log("Entered Updating Phse");
            State nextState = Environment.GetNextState(nmrdpAgent.CurrentState, nmrdpAgent.CurrentAction);

            Observation obs = nmrdpAgent.ObservationFunction(nmrdpAgent.CurrentAction, nextState);

            //Debug.Log("CurrentState: " + nmrdpAgent.CurrentState.name + ", CurrentAction: " + nmrdpAgent.CurrentAction + ", nextState: " + nextState.name + ", Observation: " + obs);

            nmrdpAgent.RewardMachine.AdvanceActiveNode(obs);

            //Debug.Log("New active node: " + nmrdpAgent.RewardMachine.ActiveNode.name);

            nmrdpAgent.CurrentState = nextState;
            phase = Phase.Planning;
        }
    }

    void ChangePhaseToUpdateAfterSeconds()
    {
        phase = Phase.Updating;
        alreadyExecuting = false;
    }
}





