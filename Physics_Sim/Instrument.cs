using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Physics_Sim
{
    class Instrument
    {
        public Texture2D texture;
        public Object monitering;
        public Vector2 pos;
        public Color[] color;
        public int width;
        public int height;
        public Double Theta;

        public Instrument() { }

        public Instrument(ref Object monitering, Vector2 pos, int width, int height)
        {
            this.monitering = monitering;
            this.pos = pos;
            this.width = width;
            this.height = height;
        }

        public virtual void makeTexture(GraphicsDevice graphics) {
            texture = new Texture2D(graphics, height, width);
            texture.SetData(color);
        }

        public virtual void update() { }
    }
}
