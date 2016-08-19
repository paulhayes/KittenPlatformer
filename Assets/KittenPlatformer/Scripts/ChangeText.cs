using UnityEngine;
using System.Collections;

public class ChangeText : MonoBehaviour {

	TextMesh textField;

    void Awake(){
        textField = GetComponent<TextMesh>();
    }

    public void SetText(string text){
        textField.text = text;
    }

    public void SetText(int text){
        textField.text = text.ToString();
    }
}
