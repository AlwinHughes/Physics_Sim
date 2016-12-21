using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Physics_Sim
{
    class PathDot
    {
        Color[] color = new Color[4];
        public Vector2 pos;
        public Texture2D texture;
        public PathDot() { }

        public PathDot(Color color, Vector2 pos)
        {
            this.pos = pos;
            for (int i = 0; i < 4; i++)
            {
                this.color[i] = color;
            }
        }

        public void makeTexture(GraphicsDevice graphics)
        {
            texture = new Texture2D(graphics, 2, 2);
            texture.SetData(color);
        }


    }
}
