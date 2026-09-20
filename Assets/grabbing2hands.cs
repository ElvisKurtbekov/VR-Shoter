using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class grabbing2hands : XRGrabInteractable
{
    [SerializeField] private Transform leftHandAttachTransform;

    [SerializeField] private Transform rightHandAttachTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag( "lehtHand") )
        {
            attachTransform = leftHandAttachTransform;
        }
        if (args.interactorObject.transform.CompareTag ("rightHand")) 
        {
            attachTransform = rightHandAttachTransform;
        }

        base.OnSelectEntered(args);
    }
}
