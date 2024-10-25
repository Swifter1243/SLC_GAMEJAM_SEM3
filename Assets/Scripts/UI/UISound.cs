using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class UISound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
	[SerializeField] private AudioClip clipEnter;
	[SerializeField] private AudioClip clipClick;
	private AudioSource source;

	private void Start()
	{
		source = GetComponent<AudioSource>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		source.PlayOneShot(clipEnter);
	}
	public void OnPointerClick(PointerEventData eventData)
	{
		source.PlayOneShot(clipClick);
	}
}
