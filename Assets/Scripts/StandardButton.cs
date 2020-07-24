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

	private System.Action onClickCallBack = null;

	private void Awake() {
		button.onClick.AsObservable().Subscribe(_ => {
			onClickCallBack?.Invoke();
		});
	}

	public void SetButtonAction(System.Action _onClickCallBack) {
		this.onClickCallBack = _onClickCallBack;
	}

}