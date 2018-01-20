using UnityEngine;
using System.Collections;

[SerializeField]
public struct Float2
{
    public float x;
    public float y;

    public Float2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Float2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Float2 operator +(Float2 left, Float2 right)
    {
        return new Float2(left.x + right.x, left.y + right.y);
    }

    public static Float2 operator *(Float2 left, float right)
    {
        return new Float2(left.x * right, left.y * right);
    }

    public static Float2 zero = new Float2(0, 0);
    public static Float2 one = new Float2(1, 1);
    public static Float2 up = new Float2(1, 0);
    public static Float2 down = new Float2(-1, 0);
    public static Float2 left = new Float2(0, -1);
    public static Float2 right = new Float2(0, 1);
}
