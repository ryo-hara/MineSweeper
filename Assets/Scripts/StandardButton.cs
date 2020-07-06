using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UniRx;

public class StandardButton : MonoBehaviour
{
	[SerializeField] 
	private Button button;

	private ButtonAction onClickCallBack = null;

	private void Awake() {
		button.onClick.AsObservable().Subscribe(_ => {
			if(onClickCallBack != null)onClickCallBack.onClick();
//			Debug.Log("OnClickAction");
			});
	}

	public void SetButtonAction(ButtonAction _onClickCallBack) {
		this.onClickCallBack = _onClickCallBack;
	}

}

public abstract class ButtonAction{
	public void onClick() { }
}