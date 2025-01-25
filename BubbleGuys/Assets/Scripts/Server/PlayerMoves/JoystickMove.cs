using System;
using Unity.Mathematics;
using UnityEngine;

namespace Server.PlayerMoves
{
    public struct JoystickMove
    {
        public float angle; //0-2pi
        public float power; //0.0 - 1.0
        public Vector2 move;

        public JoystickMove(float power, float angle)
        {
            this.power = power;
            this.angle = angle;
            move = new Vector2(-1 * power * Mathf.Cos(angle), power * Mathf.Sin(angle));
        }
    }
}