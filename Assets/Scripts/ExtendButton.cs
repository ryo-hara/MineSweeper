using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using UniRx;


public class ExtendButton : Button
{

	public UnityEvent onLeftClick;
	public UnityEvent onRightClick;

	public override void OnPointerClick(PointerEventData eventData) 
	{
		switch (eventData.pointerId) {
			case -1:
				onLeftClick.Invoke();
				break;
			case -2:
				onRightClick.Invoke();
				break;
			case -3:
				//中央クリック
				break;
		}
	}
}
