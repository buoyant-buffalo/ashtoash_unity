using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    // single dictionary of events
    private Dictionary <string, UnityEvent> eventDictionary;

    // static instance of the dictionary
    private static EventManager eventManager;

    // getter for the instance, if the eventManager instance doesnt exist, 
    // then find it otherwise log an error, if found then initialize it
    private static EventManager instance
    {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.Log("There needs to be one active EventManager script on a GameObject in your scene.");
                } else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    // if the dictionary is null, create it
    void Init ()
    {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    // take an event and action, add the action to the event OR create a new entry for the event
    public static void StartListening (string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    // find the entry if it exists and remove it
    public static void StopListening (string eventName, UnityAction listener)
    {
        if (eventManager == null)
            return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    // find the entry in the key-value pair and invoke it
    public static void TriggerEvent (string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }
}
