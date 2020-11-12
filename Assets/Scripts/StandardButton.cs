using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using UniRx;

public class StandardButton : MonoBehaviour
{
	[SerializeField] 
	private ExtendButton button;
	private System.Action onClickCallBack = null;
	private System.Action onRightClickCallBack = null;


	private void Awake() 
	{
		button.onLeftClick.AsObservable().Subscribe(_ => {
			onClickCallBack?.Invoke();
		});
		button.onRightClick.AsObservable().Subscribe(_ => {
			onRightClickCallBack?.Invoke();
		});

	}

	public void SetButtonAction(System.Action _onClickCallBack) 
	{
		this.onClickCallBack = _onClickCallBack;
	}

	public void SetRightClickButtonAction(System.Action _onClickCallBack) {
		this.onRightClickCallBack = _onClickCallBack;
	}


}