using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable() {
        InputManager.OnInteracted += Message;
    }

    private void OnDisable() {
        InputManager.OnInteracted -= Message;
    }

    public void Message(){
        Debug.Log("Hello World");
    }
}
