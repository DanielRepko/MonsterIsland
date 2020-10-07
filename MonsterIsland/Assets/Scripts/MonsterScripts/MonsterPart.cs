using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;

public abstract class MonsterPart : MonoBehaviour
{
    protected float MaxCordDeviation = float.MaxValue;
    protected float MaxTangentAngle = Mathf.PI / 2f;
    protected float SamplingStepDistance = 100;
    protected float StepDistance = 1;
    protected float SvgPixelsPerUnit = 10;
    protected VectorUtils.Alignment Alignment = VectorUtils.Alignment.Custom;
    protected float CustomPivotX;
    protected float CustomPivotY;
    protected float GradientResolution = 128;
}
