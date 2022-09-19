using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    public GameObject townSqr;
    public GameObject equip;
    public GameObject guide;
    public GameObject jewlr;
    public GameObject treasr;
    public GameObject actionStatus;
    public TreasureHunter_NMRDP_Agent nmrdpAgent;

    TextMeshPro actionStatusText;
    enum Phase { Planning, Execution, Updating }
    Phase phase;
    NavMeshAgent navMeshAgent;
    bool alreadyPlanning;
    bool alreadyExecuting;
    bool waitingToGetPath;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.stoppingDistance = 1.9f;

        nmrdpAgent = new TreasureHunter_NMRDP_Agent();

        phase = Phase.Planning;

        alreadyPlanning = false;
        alreadyExecuting = false;
        waitingToGetPath = false;

        actionStatusText = actionStatus.GetComponent<TextMeshPro>();

        Time.timeScale = 2f;
    }


    void LateUpdate()
    {
        if (phase == Phase.Planning)
        {
            //Debug.Log("----------------------------------");
            //Debug.Log("Entered Planning Phase");
            //Debug.Log("CurrentState: " + nmrdpAgent.CurrentState.name);
            //Debug.Log("waitingToGetPath: " + waitingToGetPath);
            //Debug.Log("alreadyPlanning: " + alreadyPlanning);

            if (!waitingToGetPath && !alreadyPlanning)
            {
                alreadyPlanning = true;
                nmrdpAgent.CurrentAction = nmrdpAgent.SelectAction(nmrdpAgent.CurrentState);
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
                    //Debug.Log("Entered Execution Phase");
                }
            }
            else
            {
                phase = Phase.Execution;
                //Debug.Log("----------------------------------");
                //Debug.Log("Entered Execution Phase");
            }
        }

        if (phase == Phase.Execution)
        {
            if (nmrdpAgent.isNavigationAction(nmrdpAgent.CurrentAction))
            {
                //Debug.Log("----------------------------------");
                //Debug.Log("Entered Execution Phase");
                //Debug.Log("remainingDistance: " + navMeshAgent.remainingDistance);
                //Debug.Log("hasPath: " + navMeshAgent.hasPath);

                if (navMeshAgent.remainingDistance < Parameters.AT_TARGET_DISTANCE)
                {
                    navMeshAgent.ResetPath();
                    phase = Phase.Updating;
                }
            }
            else if (!alreadyExecuting)
            {
                switch (nmrdpAgent.CurrentAction)
                {
                    case Action.Buy:
                        if (nmrdpAgent.CurrentState.name == "equipment")
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
            //Debug.Log("Entered Updating Phase");
            State nextState = Environment.GetRealNextState(nmrdpAgent.CurrentState, nmrdpAgent.CurrentAction);
            Observation obs = nmrdpAgent.GetObservation(nmrdpAgent.CurrentAction, nextState);
            nmrdpAgent.RewardMachine.AdvanceActiveNode(obs);
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

