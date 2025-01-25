using System;
using Unity.Mathematics;
using UnityEngine;

namespace Server.PlayerMoves
{
    public struct JoystickMove
    {
        private float angle; //0-2pi
        private float power; //0.0 - 1.0
        private Vector2 move;

        public JoystickMove(float power, float angle)
        {
            this.power = power;
            this.angle = angle;
            move = new Vector2(power * Mathf.Cos(angle), power * Mathf.Sin(angle));
        }
    }
}