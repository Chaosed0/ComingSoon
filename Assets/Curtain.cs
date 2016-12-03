using System;
using UnityEngine;

using DG.Tweening;

class Curtain : _Mono
{
    public Vector2 target = new Vector2(0.0f, 0.0f);
    public float time = 2.0f;

    void Start() {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1.0f);
        seq.Append(DOTween.To(()=> this.xy, vec => this.xy = vec, target, time));
    }
}
