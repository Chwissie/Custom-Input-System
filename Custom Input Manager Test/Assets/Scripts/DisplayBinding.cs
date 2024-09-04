using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayBinding : MonoBehaviour
{
    public Binding Action;
    public TMP_Text ButtonText;

    private void OnEnable() {
        gameObject.GetComponent<ChangeBinding>().OnBindingUpdated += UpdateBindingDisplay;
        InputManager.DataLoaded += UpdateBindingDisplay;
    }

    private void OnDisable() {
        gameObject.GetComponent<ChangeBinding>().OnBindingUpdated -= UpdateBindingDisplay;
        InputManager.DataLoaded -= UpdateBindingDisplay;
    }

    private void Start() {
        UpdateBindingDisplay();
    }

    public void UpdateBindingDisplay(){
        ButtonText.text = InputManager.Instance.Bindings[Action].ToString();
    }
}
