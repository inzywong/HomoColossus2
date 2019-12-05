using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleInformation : MonoBehaviour
{
    public Button btn;
    bool state = false;
    void Start(){
        btn =GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	
    }

    void  TaskOnClick(){
        state = !state;
        this.gameObject.SetActive(state);
    }
}
