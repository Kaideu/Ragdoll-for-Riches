using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
public class AddMoney : MonoBehaviour
{
    [SerializeField]
    LayerMask obstacles;
    public enum BodyPart{head, torso, arm, leg}
    public BodyPart bodyPart;
    private void OnCollisionExit(Collision other) {
        if (Kaideu.Utils.Helpers.IsInLayerMask(obstacles, other.gameObject.layer)){
            MoneyManager.Instance.UpdateBalance(bodyPart.ToString());
        }
    }
}