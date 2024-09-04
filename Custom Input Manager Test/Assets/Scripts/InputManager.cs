using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Binding{up,down,left,right,interact}

public class InputManager : MonoBehaviour, ISaveable
{
    public static InputManager Instance;
    public Dictionary<Binding,KeyCode> Bindings = new Dictionary<Binding, KeyCode>();

    public delegate void InteractAction();
    public static event InteractAction OnInteracted;

    public delegate void LoadData();
    public static event LoadData DataLoaded;

    private void Awake() {
        if(Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }

        Bindings.Add(Binding.up,KeyCode.W);
        Bindings.Add(Binding.down,KeyCode.S);
        Bindings.Add(Binding.left,KeyCode.A);
        Bindings.Add(Binding.right,KeyCode.D);
        Bindings.Add(Binding.interact,KeyCode.Mouse0);
        Load();

    }


    private void Update() {
        if(Input.GetKeyDown(Bindings[Binding.interact])){
            //Shoot
            OnInteracted();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Save();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Load();
        }
    }

    private class KeyBindsObject{
        public string Up;
        public string Down;
        public string Left;
        public string Right;
        public string Interact;
        public string Save;

    }

    public void Save(){
        KeyBindsObject keyBindsObject = new KeyBindsObject{
            Up = Bindings[Binding.up].ToString(),
            Down = Bindings[Binding.down].ToString(),
            Left = Bindings[Binding.left].ToString(),
            Right = Bindings[Binding.right].ToString(),
            Interact = Bindings[Binding.interact].ToString(),
            
        };
        string json = JsonUtility.ToJson(keyBindsObject);

        File.WriteAllText(Application.dataPath + "/PlayerKeyBinds.txt", json);
        Debug.Log("Keybinds successfully saved!");
    }

    public void Load()
    {
        try{
            string loadedString = File.ReadAllText(Application.dataPath + "/PlayerKeyBinds.txt");
            KeyBindsObject LoadedKeyBindsObject = JsonUtility.FromJson<KeyBindsObject>(loadedString);

            try{
                Bindings[Binding.up] = (KeyCode)System.Enum.Parse(typeof(KeyCode),LoadedKeyBindsObject.Up);
                Bindings[Binding.down] = (KeyCode)System.Enum.Parse(typeof(KeyCode),LoadedKeyBindsObject.Down);
                Bindings[Binding.left] = (KeyCode)System.Enum.Parse(typeof(KeyCode),LoadedKeyBindsObject.Left);
                Bindings[Binding.right] = (KeyCode)System.Enum.Parse(typeof(KeyCode),LoadedKeyBindsObject.Right);
                Bindings[Binding.interact] = (KeyCode)System.Enum.Parse(typeof(KeyCode),LoadedKeyBindsObject.Interact);
            }
            catch{
                Bindings[Binding.up] = KeyCode.W;
                Bindings[Binding.down] = KeyCode.S;
                Bindings[Binding.left] = KeyCode.A;
                Bindings[Binding.right] = KeyCode.D;
                Bindings[Binding.interact] = KeyCode.Mouse0;

                Debug.Log("Initialising KeyBinds");
            }

            DataLoaded();
            Debug.Log("Keybinds successfully loaded!");
        }
        catch{
            File.Create(Application.dataPath + "/PlayerKeyBinds.txt");
            Debug.Log("Creating PlayerKeyBinds.txt");
        }
    }
}
