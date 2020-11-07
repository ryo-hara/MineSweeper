using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{

    [SerializeField] private StandardButton continueButton;
    [SerializeField] private StandardButton exitButton;


    public Subject<Unit> clickContinueButton = new Subject<Unit>();
    public Subject<Unit> clickExitButton = new Subject<Unit>();


    private void Awake() {
        this.continueButton.SetButtonAction(this.continueAction);
        this.exitButton.SetButtonAction(this.exitAction);
    }

    // Update is called once per frame
    void Update() {

    }

    public void continueAction() {
        Debug.Log("ContinueClick");
        this.clickContinueButton.OnNext(Unit.Default);
    }

    public void exitAction() {
        Debug.Log("ExitClick");
        this.clickExitButton.OnNext(Unit.Default);
    }

}
