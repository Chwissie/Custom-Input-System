using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Rb;
    public Vector2 MoveDirection;

    private void Update() {
        //Get Inputs
        GetInputs();
    }

    private void FixedUpdate() {
        Move();
    }

    private void GetInputs(){
        int x;
        int y;

        if(Input.GetKey(InputManager.Instance.Bindings[Binding.up])){
            y = 1;
        }
        else if(Input.GetKey(InputManager.Instance.Bindings[Binding.down])){
            y = -1;
        }
        else{
            y = 0;
        }

        if(Input.GetKey(InputManager.Instance.Bindings[Binding.right])){
            x = 1;
        }
        else if(Input.GetKey(InputManager.Instance.Bindings[Binding.left])){
            x = -1;
        }
        else{
            x = 0;
        }
        MoveDirection = new Vector2(x,y);
    }
    private void Move() {
        Rb.velocity = MoveDirection.normalized;
    }
}
