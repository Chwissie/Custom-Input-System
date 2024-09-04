using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBinding : MonoBehaviour
{
    public Binding Action;
    private KeyCode _newKeyCode;
    
    public delegate void UpdateBinding();
    public event UpdateBinding OnBindingUpdated;

    public void UpdateBind(){
        StartCoroutine(WaitForKeyPress());
    }

    private IEnumerator WaitForKeyPress(){
        _newKeyCode = KeyCode.None;
        while(_newKeyCode == KeyCode.None){
            foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode))){
                if(Input.GetKeyDown(key)){
                    _newKeyCode = key;
                    break;
                }
            }
            yield return null;
        }
        
        InputManager.Instance.Bindings[Action] = _newKeyCode;
        OnBindingUpdated();
        
    }
}
