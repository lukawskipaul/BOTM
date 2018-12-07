using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Public actions mean that they are accessed by another class object
// Private actions are used only within the class itself

public class BattleStateMachine : MonoBehaviour
{
    // The three stages of the State Machine
    public enum ActionState
    {
        WAIT,           // Waiting for input form the character objects

        TAKEACTION,     // Retrieving data from current character
                        // (Target Data, Action Type, etc.)
                        // and performing action

        PERFORMACTION   // Currently a Placholder State
    }

    public ActionState BattleState;

    // HandleTurn is a class object holding the information of
    // the current actor and their target
    //
    // This list manages which character is
    // next in line to perform an action
    //
    public List<HandleTurn> PerformersList = new List<HandleTurn>();

    public List<HandleTurn> ExecutePerformersList = new List<HandleTurn>();

    // A list of all the player characters currently on the field
    public List<GameObject> HeroesInBattle = new List<GameObject>();

    // A list of all the enemy characters currently on the field
    public List<GameObject> EnemiesInBattle = new List<GameObject>();

    // A combined list of all characters on the field
    public List<GameObject> CharactersInBattle = new List<GameObject>();

    // The combined count of all characters on the field
    [SerializeField]
    private int charactersCount = 0;

    // Actionable player characters (I.E. not dead) on the field
    public List<GameObject> HeroesToManage = new List<GameObject>();

    // State machine for 
    // Character Action GUI
    public enum HeroGUI
    {
        // GUI activates and conforms to
        // next actionable player character
        // on the list
        ACTIVATE,

        // 
        WAITING,

        // not used yet
        INPUT1,

        // not used yet
        INPUT2,

        // 
        DONE
    }
    public HeroGUI PlayerInput;

    // Turn data for player character to be added to turn list
    private HandleTurn herosChoice;

    // GUI Objects in Unity
    // - - -
    // Specifies which enemy to target
    public GameObject EnemyButton;

    // The panel that contains the enemy buttons
    public Transform Spacer;

    // The panel containing possible character actions
    public GameObject AttackPanel;

    // Contains Enemy info
    public GameObject EnemySelectPanel;

    // List containing the enemy select buttons
    private List<GameObject> ActiveEnemies;

	// Use this for initialization
	private void Start ()
    {
        BattleState = ActionState.WAIT;
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));

        EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        EnemiesInBattle.Sort(SortEnemiesByName);

        PlayerInput = HeroGUI.ACTIVATE;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);

        SetEnemyButtons();

        charactersCount = HeroesInBattle.Count + EnemiesInBattle.Count;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (PerformersList.Count == charactersCount)
        {
            ExecutePerformersList = new List<HandleTurn>(PerformersList);
            SortTurnOrder(ExecutePerformersList);
            PerformersList.Clear();
        }//*/

		switch (BattleState)
        {
            case (ActionState.WAIT):
                {
                    winLoseCheck();

                    // if at least one character has 
                    // pushed action data to turn list
                    if (ExecutePerformersList.Count > 0)
                    {
                        BattleState = ActionState.TAKEACTION;
                        Debug.Log("TAKEACTION Started");
                    }
                    break;
                }
            case (ActionState.TAKEACTION):
                {
                    GameObject performer = GameObject.Find(ExecutePerformersList[0].AttackersName);

                    if (ExecutePerformersList[0].Type == "Enemy")
                    {
                        EnemyStateMachine esm = performer.GetComponent<EnemyStateMachine>();

                        // Check if target is still alive
                        if (HeroesInBattle.Count > 0) { esm.CheckTargetDead(ExecutePerformersList[0]); }

                        // Set Enemy's current target
                        esm.HeroToAttack = ExecutePerformersList[0].AttackersTarget;

                        // Attack Target
                        esm.CurrentState = EnemyStateMachine.TurnState.ACTION;
                    }
                    if (ExecutePerformersList[0].Type == "Hero")
                    {
                        HeroStateMachine hsm = performer.GetComponent<HeroStateMachine>();

                        hsm.Action = ExecutePerformersList[0].Action;

                        //if (EnemiesInBattle.Count > 0 && hsm.EnemyToAttack != null) { hsm.CheckTargetDead(ExecutePerformersList[0]); }

                        if (hsm.Action != ActionType.GUARD) { hsm.EnemyToAttack = ExecutePerformersList[0].AttackersTarget; }   // review this code when more actions become developed

                        hsm.CurrentState = HeroStateMachine.TurnState.ACTION;
                    }

                    BattleState = ActionState.PERFORMACTION;

                    break;
                }
            case (ActionState.PERFORMACTION):
                {
                    // placeholder state while
                    // characters perform action
                    // Resets to WAIT
                    // when action is completed
                    break;
                }
        }   // switch (BattleState)

        switch (PlayerInput)
        {
            case (HeroGUI.ACTIVATE):
                {
                    if (HeroesToManage.Count > 0 && ExecutePerformersList.Count == 0)
                    {
                        HeroesToManage.Sort(SortHeroesByTurnPriority);
                        HeroesToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                        herosChoice = new HandleTurn();
                        AttackPanel.SetActive(true);
                        PlayerInput = HeroGUI.WAITING;
                    }
                    break;
                }
            case (HeroGUI.WAITING):
                {
                    // idle state

                    break;
                }
            case (HeroGUI.INPUT1):
                {
                    // not used yet
                    break;
                }
            case (HeroGUI.INPUT2):
                {
                    // not used yet
                    break;
                }
            case (HeroGUI.DONE):
                {
                    HeroInputDone();
                    break;
                }
        }   // switch (HeroInput)

    }   // void Update()


    // Called by character objects so that
    // they can be added to turn list
    public void CollectActions(HandleTurn input)
    {
        PerformersList.Add(input);
    }

    // Retrieves information about every enemy character
    // on the field and adds them to enemy selction GUI
    public void SetEnemyButtons()
    {
        foreach(GameObject enemy in EnemiesInBattle)
        {
            GameObject newButton = Instantiate(EnemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine currentEnemy = enemy.GetComponent<EnemyStateMachine>();
            Debug.Log("Enemy Found");

            Debug.Log(currentEnemy.Enemy.Name);

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text>();
            Debug.Log("Enemy Target Button Text Found");

            button.EnemyPrefab = enemy;
            Debug.Log("Enemy Target Button Prefab Set");

            buttonText.text = currentEnemy.Enemy.Name;
            Debug.Log("Enemy Target Button Text Set");
            
            newButton.transform.SetParent(Spacer, false);

            currentEnemy.EnemyButton = button;
        }
    }

    // Attack Button
    // Called by player button to specify
    // specify which action they want a
    // character to take
    public void PlayerActionInput() // Attack
    {
        herosChoice.AttackersName = HeroesToManage[0].name;
        herosChoice.AttackersGameObject = HeroesToManage[0];
        herosChoice.Type = "Hero";
        herosChoice.Action = ActionType.MELEE_ATTACK;
        herosChoice.TurnPriority = HeroesToManage[0].GetComponent<HeroStateMachine>().Hero.TurnPriority;

        AttackPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
        Debug.Log("Player Action Selected");
    }

    // Target selection
    // Called by player button to specify
    // what they want their action to affect
    public void PlayerTargetInput(GameObject ChosenEnemy)
    {
        herosChoice.AttackersTarget = ChosenEnemy;
        PlayerInput = HeroGUI.DONE;
        Debug.Log("Action Target Selected");
    }

    public void Guard()
    {
        herosChoice.AttackersName = HeroesToManage[0].name;
        herosChoice.AttackersGameObject = null;
        herosChoice.Type = "Hero";
        herosChoice.Action = ActionType.GUARD;
        herosChoice.TurnPriority = HeroesToManage[0].GetComponent<HeroStateMachine>().Hero.TurnPriority;

        AttackPanel.SetActive(false);
        //herosChoice.AttackersTarget = herosChoice.AttackersGameObject;
        PlayerInput = HeroGUI.DONE;
        Debug.Log("Player Guard Selected");

    }

    // Adds player characters to turn list,
    // deactivates character selection,
    // moves on to next PC, 
    // and resets action GUI
    private void HeroInputDone()
    {
        PerformersList.Add(herosChoice);
        EnemySelectPanel.SetActive(false);
        HeroesToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HeroesToManage.RemoveAt(0);
        PlayerInput = HeroGUI.ACTIVATE;
        Debug.Log("HeroInputDone");
    }

    
    // Possible function to establish turn order
    // Creates combined list of character objects
    // and sorts them by their priority
    private List<HandleTurn> SortTurnOrder(List<HandleTurn> Actors)
    {
        Actors.Sort(SortCharactersByTurnPriority);

        return Actors;
    }

    // Sorts character objects by their priority (int)
    // PC: 0
    // Party characters: 2-5
    // Boss characters : +10
    // Standard enemies: +50
    private static int SortCharactersByTurnPriority(HandleTurn h1, HandleTurn h2)
    {
        return h1.TurnPriority.CompareTo(h2.TurnPriority);

        //return 0;
    }//*/

    // Sort enemy characters by name (for GUI)
    private static int SortEnemiesByName(GameObject e1, GameObject e2)
    {
        return e1.GetComponent<EnemyStateMachine>().Enemy.Name.CompareTo(e2.GetComponent<EnemyStateMachine>().Enemy.Name);
    }

    // Sort player characters by priority (for GUI)
    private static int SortHeroesByTurnPriority(GameObject h1, GameObject h2)
    {
        return h1.GetComponent<HeroStateMachine>().Hero.TurnPriority.CompareTo(h2.GetComponent<HeroStateMachine>().Hero.TurnPriority);
    }

    public void IncrementCharactersCount(int increment = 1)
    {
        if (increment <= 0)
        {
            Debug.Log("Tried to increment with a non-positive value.");
            return;
        }

        charactersCount += increment;
        return;
    }

    public void DecrementCharactersCount(int decrement = -1)
    {
        if (decrement >= 0)
        {
            Debug.Log("Tried to decrement with a non-negative value.");
            return;
        }

        charactersCount += decrement;
        return;
    }

    private void winLoseCheck()
    {
        if (EnemiesInBattle.Count <= 0)
        {
            SceneManager.LoadScene(2);
        }
        else if (HeroesInBattle.Count <= 0)
        {
            SceneManager.LoadScene(1);
        }

        return;
    }
}
