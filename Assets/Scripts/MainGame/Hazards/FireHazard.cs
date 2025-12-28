using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FireHazard : MonoBehaviour
{
    //public event UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;
    
    // public FireHazardScriptableObject fireHazardData;
    //public int damageMin, damageMax;
    
  //  [SerializeField] public UnityEvent onCharacterEntered;
   // [SerializeField] private UnityEvent<int> onCharacterEntered;
     public Action<int> onCharacterEntered;
    //With special parameters
    // [SerializeField] private UnityEvent<FireEnteredEventArgs> onCharacterEntered = new UnityEvent<FireEnteredEventArgs>();

    // public void SetScriptableData(FireHazardScriptableObject fireHazardScriptableObject)
    // {
    //     fireHazardData = fireHazardScriptableObject;
    // }
    // private void Start()
    // { 
    //     if(onCharacterEnteredAction != null)
    //        onCharacterEntered.AddListener(onCharacterEnteredAction);
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharacter"))
        {
            //Bad, coupled way.
            //  int damageDealt = Random.Range(damageMin, damageMax + 1);
            // if(GameManager.Instance)
            //     GameManager.Instance.playerCharacterController.TakeDamage(damageDealt);
            // if(UIManager.Instance)
            //     UIManager.Instance.RefreshHPText( GameManager.Instance.playerCharacterController.Hp);
            
            //Better, event based.
            // Debug.Log("Player entered this hazard");
            // int damageDealt = fireHazardData.GetRandomFireDamage(); 
            // onCharacterEntered.Invoke(damageDealt);
            //onCharacterEntered.Invoke();
            //  onCharacterEntered?.Invoke();





            // FireEnteredEventArgs fireEnteredEventArgs = new FireEnteredEventArgs
            // {
            //     damageDealt = damageDealt,
            //     targetCharacterController = other.GetComponent<PlayerCharacterController>()
            // };

            //  onCharacterEnteredAction.Invoke(fireEnteredEventArgs);
        }
    }
}

public class FireEnteredEventArgs
{
    public int damageDealt;
    public PlayerCharacterController targetCharacterController;
}
