using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "StateMachine/SpiderState/Move", fileName = "SpiderState_Move")]
public class SpiderState_Move : SpiderState
{
    private Vector2 playerPos;
    private Vector2 targetPos;

    private bool isChangingState = false;
    private bool canMove = false;

    public override void Enter()
    {
        base.Enter();
        isChangingState = false;
        canMove = false;
        stateMachine.SetCurrentStateType(SpiderStateType.Move);
        playerPos = stateMachine.GetPlayerPosition();
        Vector2 currentPos = stateMachine.transform.position;

        /* 目标点计算1
        // 计算目标点
        Vector2 intersectionPointWithExternalCircle = MathTool.CalculateIntersectionPointWithCircle(playerPos, targetAreaExternalRadius, currentPos);
        Vector2 intersectionPointWithInnerCircle = MathTool.CalculateIntersectionPointWithCircle(playerPos, targetAreaInnerRadius, currentPos);

        if ((playerPos - currentPos).magnitude <= targetAreaExternalRadius)
        {
            currentPos = playerPos + (currentPos - playerPos).normalized * targetAreaExternalRadius * 2;
        }
        
        
        // 计算目标点
        (float, float) externalAngleRange =
            MathTool.CalculateAngleRange(playerPos, targetAreaExternalRadius, currentPos);
        (float, float) innerAngleRange = MathTool.CalculateAngleRange(playerPos, targetAreaInnerRadius, currentPos);
        float targetAngle = UnityEngine.Random.Range(externalAngleRange.Item1, externalAngleRange.Item2);
        Debug.Log("targetAngle: " + targetAngle);
        (float, float) lengthRange = GetLengthRange(playerPos, targetAreaExternalRadius, targetAreaInnerRadius,
            currentPos, targetAngle);
        float targetLength = UnityEngine.Random.Range(lengthRange.Item1, lengthRange.Item2);
        Vector2 unitVectorPC = (playerPos - currentPos).normalized;
        // 旋转向量
        Vector2 targetVector = new Vector2(
            unitVectorPC.x * Mathf.Cos(targetAngle) - unitVectorPC.y * Mathf.Sin(targetAngle),
            unitVectorPC.x * Mathf.Sin(targetAngle) + unitVectorPC.y * Mathf.Cos(targetAngle));
        targetPos = currentPos + targetVector * targetLength;
        

        
        
        // 绘制路径
        Debug.DrawLine(currentPos, currentPos + targetVector * lengthRange.Item2, Color.blue, 1f);
        Debug.DrawLine(currentPos, intersectionPointWithExternalCircle, Color.red, 1f);
        // Debug.DrawLine(currentPos, targetPos, Color.yellow, 1f);
        */

        #region 目标点计算
        Vector2 pc = currentPos - playerPos;
        float distance = pc.magnitude;

        (float, float) externalAngleRange = MathTool.CalculateAngleRange(playerPos, stateMachine.TargetAreaExternalRadius, currentPos);
        float oppsiteAngle = Mathf.Rad2Deg * (externalAngleRange.Item2 - externalAngleRange.Item1);
        float centralAngle = distance <= stateMachine.TargetAreaExternalRadius
            ? 180
            : Mathf.Clamp(oppsiteAngle, stateMachine.MinTargetAngle, stateMachine.MaxTargetAngle);
        float targetAngle = UnityEngine.Random.Range(-centralAngle / 2, centralAngle / 2);
        float targetLength = UnityEngine.Random.Range(stateMachine.TargetAreaInnerRadius, stateMachine.TargetAreaExternalRadius);
        Vector2 unitDirectionVector = MathTool.RotateVector2(pc.normalized, Mathf.Deg2Rad * targetAngle);
        targetPos = playerPos + unitDirectionVector * targetLength;
        if (targetPos.x < stateMachine.rangeX.x)
        {
            targetPos.x = stateMachine.rangeX.x;
        }
        else if (targetPos.x > stateMachine.rangeX.y)
        {
            targetPos.x = stateMachine.rangeX.y;
        }
        if (targetPos.y < stateMachine.rangeY.x)
        {
            targetPos.y = stateMachine.rangeY.x;
        }
        else if (targetPos.y > stateMachine.rangeY.y)
        {
            targetPos.y = stateMachine.rangeY.y;
        }

        Debug.DrawLine(currentPos, targetPos, Color.yellow, 1f);

        #endregion 目标点计算
        
        animator.Play("Spider_Cocoon2Spider");
        AnimationTool.AwaitCurrentAnimWhenEnd(animator, () =>
        {
            animator.Play("Spider_Walk");
            canMove = true;
        });
    }

    public override void Execute()
    {
        base.Execute();

        // 判断是否到达目标点
        if (Vector2.Distance(stateMachine.transform.position, targetPos) <= 0.1f && !isChangingState)
        {
            isChangingState = true;
            animator.Play("Spider_Spider2Cocoon");
            AnimationTool.AwaitCurrentAnimWhenEnd(animator, () => { stateMachine.GoToNextState(); });
            //stateMachine.animationPlayControl.PlayAnimation("Spider_Spider2Cocoon", null,
            //    () => { stateMachine.GoToNextState(); });
            // stateMachine.GoToNextState();
        }
    }

    public override void FixedExecute()
    {
        base.FixedExecute();
        // 移动到目标点
        if (canMove)
        {
            stateMachine.rb.MovePosition(Vector2.MoveTowards(stateMachine.transform.position, targetPos, stateMachine.MoveSpeed * Time.fixedDeltaTime));
        }
    }

    private (float, float) GetLengthRange(Vector2 center, float externalRadius, float innerRadius, Vector2 point,
        float angle)
    {
        (float, float) externalAngleRange = MathTool.CalculateAngleRange(center, externalRadius, point);
        Debug.Log("externalAngleRange: " + externalAngleRange);
        (float, float) innerAngleRange = MathTool.CalculateAngleRange(center, innerRadius, point);
        Debug.Log("innerAngleRange" + innerAngleRange);

        if (angle > innerAngleRange.Item1 && angle < innerAngleRange.Item2)
        {
            float externalLength = MathTool.CalculateDistanceToCircle(center, externalRadius, point, angle);
            float innerLength = MathTool.CalculateDistanceToCircle(center, innerRadius, point, angle);
            Debug.Log("(externalLength, innerLength): " + (externalLength, innerLength));
            return (externalLength, innerLength);
        }
        else if (angle <= innerAngleRange.Item1 && angle >= externalAngleRange.Item1 ||
                 angle >= innerAngleRange.Item2 && angle <= externalAngleRange.Item2)
        {
            float externalLength = MathTool.CalculateDistanceToCircle(center, externalRadius, point, angle);

            float distance = (center - point).magnitude;
            float baseAngle = Mathf.Asin(externalRadius / distance);
            float externalAngle = baseAngle - angle;
            float externalTangentLineLength = Mathf.Sqrt(distance * distance - externalRadius * externalRadius);
            Debug.Log("externalTangentLineLength: " + externalTangentLineLength);
            float innerLength = externalTangentLineLength / Mathf.Cos(externalAngle);
            Debug.Log("(externalLength, innerLength): " + (externalLength, innerLength));
            return (externalLength, innerLength);
        }
        else
        {
            Debug.LogError("Invalid Angle:" + angle);
            return (0, 0);
        }
    }
}