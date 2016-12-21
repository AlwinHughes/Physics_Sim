using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Physics_Sim
{
    class CenterOfMass : Object
    {
        public CenterOfMass() { }

        // fixed COM
        public CenterOfMass(Color color, Vector2 pos, float mass) : base(pos, new  Vector2(0,0), new Vector2(0, 0), 5, 5)
        {
            this.mass = mass;
            isCOM = true;
            setColor(color);
            usedgravity = false;
        }

        // moving COM
        public CenterOfMass(Color color, Vector2 pos, Vector2 vel, float mass) : base(pos, vel, new Vector2(0, 0), 5, 5)
        {
            this.mass = mass;
            isCOM = true;
            setColor(color);
            usedgravity = true;
        }


    }
}
