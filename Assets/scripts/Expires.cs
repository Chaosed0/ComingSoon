using System;
using UnityEngine;
using DG.Tweening;

class Expires : MonoBehaviour
{
    void Start() {
    }
    void Update() {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(5.0f);
        sequence.AppendCallback(() => UnityEngine.Object.Destroy(this));
    }
}
