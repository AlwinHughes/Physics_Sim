using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Physics_Sim
{
    class Object
    {
        public Texture2D texture;
        public Vector2 pos = new Vector2();
        public Vector2 vel = new Vector2();
        public Vector2 acc = new Vector2();
        public int width;
        public int height;
        Color[] color;
        public float mass = 0;
        bool usemass = false;
        public Double KE;
        public Double GPE;
        public Double Theta;
        bool colission = false;
        public bool elastic = true;
        public bool usedgravity = false;
        public bool isCOM = false;

        public Object() { }

        // constructure with texture already set
        public Object(Texture2D texture, Vector2 pos, Vector2 vel, Vector2 acc, int width, int height)
        {
            this.texture = texture;
            this.pos = pos;
            this.vel = vel;
            this.acc = acc;
            this.width = width;
            this.height = height;
            color = new Color[width * height];
        }
        // constructure with texture not already set
        public Object(Vector2 pos, Vector2 vel, Vector2 acc, int width, int height)
        {
            this.pos = pos;
            this.vel = vel;
            this.acc = acc;
            this.width = width;
            this.height = height;
            color = new Color[width * height];
        }

        // sets all of the objects texture to one colour
        public void setColor(Color c)
        {
            for(int i = 0; i<width* height; i++)
            {
                color[i] = c;
            }
        }

        // generates and returns the Texture2D of the object
        public virtual Texture2D makeTexture(GraphicsDevice graphics)
        {
            texture = new Texture2D(graphics, width, height);
            texture.SetData(color);
            return texture;
        }
    }
}
