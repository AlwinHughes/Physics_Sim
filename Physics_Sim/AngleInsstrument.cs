using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Physics_Sim
{
    class AngleInsstrument : Instrument
    {
        int velocity = 0;
        public AngleInsstrument(ref Object monitering, Vector2 pos, int width, int height)
            : base(ref monitering, pos, width, height) { }

        public AngleInsstrument() { }

        public override void makeTexture(GraphicsDevice graphics)
        {
            width = velocity + 10;
            if (width == 0)
            {
                width = 1;
            }
            color = new Color[width * height];
            for (int i = 0; i < width * height; i++)
            {
                color[i] = Color.Black;
            }
            
            base.makeTexture(graphics);
        }

        public override void update()
        {
            Theta = monitering.Theta;
            velocity = (int)Math.Pow(monitering.vel.X * monitering.vel.X + monitering.vel.Y * monitering.vel.Y, 0.5);
        }
    }
}
