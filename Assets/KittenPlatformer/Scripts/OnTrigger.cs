﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour {

    public UnityEvent OnTriggerEvent;
    public enum Conditions {
        Enter,
        Exit,
        Both
    }
    public string targetTag = "Player";
    public Conditions condition = Conditions.Enter;
       
    void OnTriggerEnter2D(Collider2D col){
        if( !col.CompareTag(targetTag) ){
            return;
        }
        if( condition == Conditions.Enter || condition == Conditions.Both ){
            OnTriggerEvent.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if( !col.CompareTag(targetTag) ){
            return;
        }
        if( condition == Conditions.Exit || condition == Conditions.Both ){
            OnTriggerEvent.Invoke();
        }
    }
}
