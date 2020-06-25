using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StandardButton : MonoBehaviour
{
	[SerializeField] private Button button;


	private void Awake() {
		button.onClick.AsObservable().Subscribe(_ => Debug.Log("OnClickAction"));
	}
}
