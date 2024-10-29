using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGamObject;
    public IInteractable curInteractable;

    //public TextMeshProUGUI promtText;
    private Camera cam;

    private PlayerController controller;

    void Start()
    {
        cam = Camera.main;
        controller = CharacterManager.Instance.Player.controller;
        CharacterManager.Instance.Player.useItem += EventItem;
    }

    void Update()
    {
        if (Time.deltaTime - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = cam.ScreenPointToRay(new Vector3 (Screen.width / 2, Screen.height / 2));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if(hit.collider.gameObject != curInteractGamObject)
                {
                    curInteractGamObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                }
            }
            else
            {
                curInteractable = null;
                curInteractGamObject = null;
            }
        }
    }

    public void OnIteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)//&& curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGamObject = null;
            curInteractable = null;
            Debug.Log("´­·¶´Ù");
            //promtText.gameObject.SetActive(false);
        }
    }

    void EventItem()
    {
        StartCoroutine(UseItem());
    }

    public IEnumerator UseItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;
        controller.moveSpeed += data.value;
        yield return new WaitForSeconds(data.duration);
        controller.moveSpeed -= data.value;
    }
}
