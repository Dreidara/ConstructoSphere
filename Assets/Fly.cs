using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fly : MonoBehaviour
{
    //Fly
    [SerializeField] private float speed, levitationSpeed;

    private CharacterController characterController;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        FlyMe();
    }
    private void FlyMe()
    {
        moveDirection = Vector3.up * levitationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Tab))
        {
            characterController.Move(moveDirection);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            characterController.Move(-moveDirection);
        }
    }

}
