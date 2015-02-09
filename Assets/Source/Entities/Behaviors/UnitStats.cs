﻿using UnityEngine;

public class UnitStats : MonoBehaviour 
{
	public int Health;
	public int MaxHealth;
	public float Loyalty;
	public int Hunger;
	public float Speed;
	public Vector3 Position;

	private GameObject UnitUiCanvas;
	private RectTransform HpBarRatio;
	public Camera UnitUiCamera;

	void Start()
	{
		UnitUiCanvas = (GameObject)Instantiate(Resources.Load("UnitUI"), Vector3.up * 7, Quaternion.identity);
		UnitUiCanvas.transform.SetParent(transform, false);
		HpBarRatio = UnitUiCanvas.transform.Find("HpRatio").gameObject.GetComponent<RectTransform>();
	}

	void Update()
	{
		Position = transform.position;

		HpBarRatio.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 512f * (float)Health / (float)MaxHealth);
		UnitUiCanvas.transform.LookAt(new Vector3(transform.position.x, UnitUiCamera.transform.position.y, transform.position.z));
	}
}
