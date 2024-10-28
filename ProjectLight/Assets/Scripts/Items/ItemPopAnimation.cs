using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class ItemPopAnimation : MonoBehaviour
{
    [Header("动画设置")]
    [Tooltip("上升动画帧数")]
    public int upFrames = 2;

    [Tooltip("下降动画帧数")]
    public int downFrames = 6;
    public float MoveSpeed = 20;

    private int status = 0;

    [SerializeField]
    private AnimationCurve anim_x; // x轴动画 Curve

    [SerializeField]
    private AnimationCurve anim_y;

    private Animator animator;

    private Keyframe[] kx;
    private Keyframe[] ky;

    [Header("关键帧坐标")]
    // [SerializeField]
    public List<float[]> keyPoints;

    [HideInInspector]
    public Vector2 dropPoint; //掉落位置

    [HideInInspector]
    public float popHeight = 0;

    private float existTime;
    private float createTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // pop
        if (status == 0)
        {
            if (dropPoint != null)
            {
                CreateAnimCurve();
                createTime = Time.time;
                status = 1;
            }
        }
        if (status == 1)
        {   
            existTime = Time.time - createTime;
            transform.localPosition = new Vector3(
                anim_x.Evaluate(existTime),
                anim_y.Evaluate(existTime),
                0
            );

            // // Debug
            // Debug.Log(
            //     "生成LocalPosition:" + anim_x.Evaluate(existTime) +"  "+ anim_y.Evaluate(existTime)
            // );
            // Debug.Log(existTime);
            // Debug.Log("帧数：" + anim_x.length);
            // Debug.Log(transform.localPosition);
            // Debug.Log(keyPoints[0][upFrames + downFrames]);
            // Debug.Log(keyPoints[1][upFrames + downFrames]);
           

            if (transform.localPosition.x == keyPoints[0][upFrames + downFrames])
            {
                status = 2;
            }
        }
        if (status == 2)
        {
            // 旋转
            animator.SetTrigger("InRotate");
            status = 3;
        }
    }

    private void CreateAnimCurve()
    {
        Vector2 point_1 = new Vector2(0, 0);
        Vector2 point_2 = new Vector2(0.5f * dropPoint.x, 0.5f * dropPoint.y + popHeight);
        Vector2 point_3 = new Vector2(dropPoint.x, dropPoint.y);

        Double[] QFuctionParam = SolveQuadraticFunction(point_1, point_2, point_3); // param:a,b,c
        keyPoints = CreateKeyPoints(QFuctionParam, point_1, point_2, point_3, upFrames, downFrames); //List([x_i],[y_i])

        int frameCount = upFrames + downFrames + 1;
        kx = new Keyframe[frameCount];
        ky = new Keyframe[frameCount];

        for (var i = 0; i < kx.Length; i++)
        {
            kx[i] = new Keyframe(i / MoveSpeed, keyPoints[0][i]);
            ky[i] = new Keyframe(i / MoveSpeed, keyPoints[1][i]);
        }

        anim_x = new AnimationCurve(kx);
        anim_y = new AnimationCurve(ky);

#if UNITY_EDITOR
        // 自动钳制切线
        for (int j = 1; j < kx.Length - 1; j++)
        {
            AnimationUtility.SetKeyLeftTangentMode(
                anim_x,
                j,
                AnimationUtility.TangentMode.ClampedAuto
            );
            AnimationUtility.SetKeyRightTangentMode(
                anim_x,
                j,
                AnimationUtility.TangentMode.ClampedAuto
            );
            AnimationUtility.SetKeyLeftTangentMode(
                anim_y,
                j,
                AnimationUtility.TangentMode.ClampedAuto
            );
            AnimationUtility.SetKeyRightTangentMode(
                anim_y,
                j,
                AnimationUtility.TangentMode.ClampedAuto
            );
        }
#endif
    }

    private double[] SolveQuadraticFunction(Vector2 point_1, Vector2 point_2, Vector2 point_3)
    {
        // List<double> outputs = new List<double>();
        double[] outputs = new double[3];
        double[] coefficients = new double[9];
        double[] constants = new double[3];

        double x1 = point_1.x;
        double y1 = point_1.y;
        double x2 = point_2.x;
        double y2 = point_2.y;
        double x3 = point_3.x;
        double y3 = point_3.y;

        coefficients[0] = x1 * x1;
        coefficients[1] = x1;
        coefficients[2] = 1;
        constants[0] = y1;

        coefficients[3] = x2 * x2;
        coefficients[4] = x2;
        coefficients[5] = 1;
        constants[1] = y2;

        coefficients[6] = x3 * x3;
        coefficients[7] = x3;
        coefficients[8] = 1;
        constants[2] = y3;

        double[,] matrix = new double[3, 3];
        for (int i = 0; i < 3; i++)
        {
            matrix[i, 0] = coefficients[i * 3];
            matrix[i, 1] = coefficients[i * 3 + 1];
            matrix[i, 2] = coefficients[i * 3 + 2];
        }

        double determinant =
            matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1])
            - matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0])
            + matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);

        if (determinant == 0)
        {
            // 直线
            if (x2 - x1 != 0)
            {
                outputs[1] = (y2 - y1) / (x2 - x1);
                outputs[2] = y1 - outputs[1] * x1;
            }
            else
            {
                outputs[1] = 0;
                outputs[2] = y1;
            }
            return outputs;
        }

        double[,] inverseMatrix = new double[3, 3];
        inverseMatrix[0, 0] =
            (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1]) / determinant;
        inverseMatrix[0, 1] =
            -(matrix[0, 1] * matrix[2, 2] - matrix[0, 2] * matrix[2, 1]) / determinant;
        inverseMatrix[0, 2] =
            (matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1]) / determinant;
        inverseMatrix[1, 0] =
            -(matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]) / determinant;
        inverseMatrix[1, 1] =
            (matrix[0, 0] * matrix[2, 2] - matrix[0, 2] * matrix[2, 0]) / determinant;
        inverseMatrix[1, 2] =
            -(matrix[0, 0] * matrix[1, 2] - matrix[0, 2] * matrix[1, 0]) / determinant;
        inverseMatrix[2, 0] =
            (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]) / determinant;
        inverseMatrix[2, 1] =
            -(matrix[0, 0] * matrix[2, 1] - matrix[0, 1] * matrix[2, 0]) / determinant;
        inverseMatrix[2, 2] =
            (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]) / determinant;

        double[] result = new double[3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                result[i] += inverseMatrix[i, j] * constants[j];
            }
        }
        return result;
    }

    private List<float[]> CreateKeyPoints(
        Double[] QFuctionParam,
        Vector2 point_1,
        Vector2 point_2,
        Vector2 point_3,
        int upFrames,
        int downFrames
    )
    {
        // Calculate X

        float[] X1 = Interpolation(point_1.x, point_2.x, upFrames);
        float[] X2 = Interpolation(point_2.x, point_3.x, downFrames);
        float[] X = new float[upFrames + downFrames + 1];
        if (X1.Length > 0)
        {
            // X = new float[upFrames + downFrames + 1];
            Array.Copy(X1, 0, X, 0, X1.Length - 1);
            Array.Copy(X2, 0, X, X1.Length - 1, X2.Length);
        }
        else
        {
            X = X2;
        }

        // Calculate Y
        float[] Y = new float[X.Length];
        double a = QFuctionParam[0];
        double b = QFuctionParam[1];
        double c = QFuctionParam[2];

        for (int i = 0; i < X.Length; i++)
        {
            float x = X[i];
            Y[i] = (float)(a * x * x + b * x + c);
        }

        List<float[]> result = new List<float[]>();
        result.Add(X);
        result.Add(Y);

        return result;
    }

    private float[] Interpolation(float a, float b, int m)
    {
        // a : startValue; b : endValue; m : pointCount


        float step = (b - a) / m;
        float[] result = new float[m + 1];
        for (int i = 0; i < m + 1; i++)
        {
            result[i] = a + i * step;
        }

        return result;
    }
}
