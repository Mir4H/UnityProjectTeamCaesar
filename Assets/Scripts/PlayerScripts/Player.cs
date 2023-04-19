using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDataPersistence
{
    public InventorySystem inventory;

    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _lookAtTarget;
    [SerializeField] private float lookSpeed = 1f;
    [SerializeField] private ShowGuidance showGuidance;
    [SerializeField] InputActionReference interactionInput;
    private GameObject collectableItem;

    private bool useLookAt;
    private Vector3 _targetPosition;

    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    [Min(1)]
    private float hitRange = 2f;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;
    private Rigidbody heldObjRB;

    private RaycastHit hit;

    [SerializeField] private float pickupForce = 150.0f;

    [SerializeField] private GameObject rustyKey;
    [SerializeField] private GameObject goldKey;
    [SerializeField] private GameObject scroll;
    private GameObject selectedObject;

    public static SerializableDictionary<string, ItemPickUpSaveData> activeItems;

    //Interaction
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.25f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;

    private string playedScene;

    private void Awake()
    {
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Saving Inventory Items");
        Debug.Log(inventory.Container.Items.Count.ToString());
        data.inventoryItems = inventory.Container.Items;
        data.activeItems = activeItems;
        foreach (var item in activeItems)
        {
            Debug.Log(item.ToString());
        }

        // Adding played scenes to savefile
        if (playedScene != null)
        {
            data.scenesCompleted.Add(playedScene, true);
            data.lastFinishedScene = playedScene;
        }
    }
    
    public void LoadData(GameData data)
    {
        inventory.Container.Items.Clear();
        Debug.Log("Loading Inventory Items");
        inventory.Container.Items = data.inventoryItems;
        InventorySystem.OnInventoryChanged?.Invoke(false);
        if (data.activeItems.Count > 0)
        {
            placeItems(data);
        }
    }

    private void OnEnable()
    {
        EventManager.GetInventoryItem += EventManagerOnGetInventoryItem;
    }

    private void EventManagerOnGetInventoryItem(string name)
    {
        GameObject inventoryItem;

        if (name == "Rusty Key")
        {
            selectedObject = rustyKey;
        }
        if (name == "Gold Key")
        {
            selectedObject = goldKey;
        }
        if (name == "Scroll")
        {
            EventManager.OnShowStory();
        }
        if (selectedObject != null)
        {
            Time.timeScale = 1;
            if (inHandItem != null)
            {
                DropObject();
            }
            inventoryItem = Instantiate(selectedObject, pickUpParent.position, Quaternion.identity);
            
            PickupObject(inventoryItem);
            inventoryItem.TryGetComponent<ItemCollectable>(out ItemCollectable item);
            inventory.RemoveItem(item);
            inventoryItem.GetComponent<ItemObject>().OnHandleTakeItemFromInv();
        }
        return;
    }

    private void OnDisable()
    {
        EventManager.GetInventoryItem -= EventManagerOnGetInventoryItem;
    }

    private void placeItems(GameData data)
    {
        UniqueID[] itemsWithIds = FindObjectsOfType<UniqueID>();

        foreach (KeyValuePair<string, ItemPickUpSaveData> entry in data.activeItems)
        {
            if (!(itemsWithIds.Any(x => x.ID == entry.Key)))
            {
                Debug.Log("creating" + entry.Key);
                Instantiate(inventory.database.GetItem[entry.Value.id].prefab, entry.Value.position, entry.Value.rotation);
            }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PointOfInterest")
        {
            Debug.Log("Seeing point of interest");
            showGuidance.SetUpGuidance("Press E to Collect Item");
            _targetPosition = other.transform.position;
            useLookAt = true;
            collectableItem = other.gameObject;
        }

        //TESTING MOVING GOAL TRIGGER
        if (other.gameObject.tag == "goal")
        {
            playedScene = SceneManager.GetActiveScene().name;

            SceneManager.LoadSceneAsync("MovingBtwnScene");

            transform.position = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
            transform.rotation = new Quaternion((float)0.00000, (float)0.65060, (float)0.00000, (float)0.75942);

            DataPersistenceManager.instance.SaveGame();

            /*
            OnTimerStop();
            OnFinishSuccess();
            */

        }
    }

    public void OnTriggerExit(Collider other)
    {
        useLookAt = false;
        collectableItem = null;
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;
            heldObjRB.transform.parent = pickUpParent;
            heldObjRB.drag = 5;
            inHandItem = pickObj;
            if (pickObj.tag == "PointOfInterest") inHandItem.tag = "Pickable";
            if (pickObj.layer == 8) pickObj.layer = 10;
        }
    }

    void MoveObject()
    {
        heldObjRB.velocity += (pickUpParent.transform.position + heldObjRB.position) * Time.deltaTime;
        if (Vector3.Distance(inHandItem.transform.position, pickUpParent.position) > 0.1f)
        {
            Vector3 moveDirection = (pickUpParent.position - inHandItem.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void DropObject()
    {
        if (inHandItem.tag == "Pickable") inHandItem.tag = "PointOfInterest";
        if (inHandItem.layer == 10) inHandItem.layer = 8;
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        heldObjRB.transform.parent = null;
        inHandItem = null;
    }

    private void Collect(InputAction.CallbackContext obj)
    {
        if (useLookAt && collectableItem != null)
        {
            var item = collectableItem.GetComponent<ItemCollectable>();
            //var uniqueId = collectableItem.GetComponent<UniqueID>().ID;
            if (item)
            {
                inventory.AddItem(new Item(item.item));
                collectableItem.GetComponent<ItemObject>().OnHandlePickupItem();
            }
            return;
        }
        if (hit.collider != null && inHandItem == null)
        {
            Debug.Log(hit.collider.name);
            PickupObject(hit.collider.gameObject);
        }
        else if (hit.collider == null && inHandItem != null)
        {
            if (inHandItem != null)
            {
                DropObject();
            }
            else
            {
                return;
            }
        }

        //interactable?.Interact(this);

        if (inHandItem != null)
        {
            MoveObject();
        }
        return;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
   /* 
    private void DoInteraction()
    {
        interactable.Interact(this);
    }*/

    private void Update()
    {
        //collect item
        if (!useLookAt || !collectableItem)
        {
            showGuidance.CloseGuidance();
            _targetPosition = _parent.position + _parent.forward * 2f + new Vector3(0f, 2f, 0f);
        }
        _lookAtTarget.transform.position = Vector3.Lerp(_lookAtTarget.transform.position, _targetPosition, Time.deltaTime * lookSpeed);
        interactionInput.action.performed += Collect;

        //open inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventManager.OnOpenInventory();
        }

        //close app
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }*/

        //interaction
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);
        
        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                if (!interactionPromptUI.IsDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter)) interactable.Interact(this);
            }
        }
        if (numFound <= 0)
        {
            if (interactable != null) interactable = null;
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
        }


        //Lift item
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            showGuidance.CloseGuidance();
        }
        /*
        if (inHandItem != null)
        {
            return;
        }*/

        if (Physics.Raycast(
            playerCameraTransform.position,
            playerCameraTransform.forward,
            out hit,
            hitRange,
            pickableLayerMask) && inHandItem == null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            showGuidance.SetUpGuidance("Press E to Pick Up");
        }

    }
    /*
    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
    }*/
}
