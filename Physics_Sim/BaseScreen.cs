﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Physics_Sim
{
    class BaseScreen
    {
        public int id;// used to identify each screen
        public GraphicsDevice graphics;
        public Object[] objects;
        public Instrument[] instruments;
        public BaseScreen() { }

        public BaseScreen(int id, GraphicsDevice graphics)
        {
            this.id = id;
            this.graphics = graphics;
        }

        public virtual void onLoad() { Console.WriteLine("onLoad not overriden: " + Convert.ToString(id)); }

        public virtual void onClose() { Console.WriteLine("onClose not overriden: " + Convert.ToString(id)); }

        public virtual void phyUpdate()
        {
            foreach(Instrument i in instruments)
            {
                if(i != null)
                {
                    i.update();
                }
            }
            //Console.WriteLine("phyUpdate not overriden: " + Convert.ToString(id)); 
        }

        public virtual Object[] drawUpdateObjects() { return objects; }

        public virtual Instrument[] drawUpdateInstrument()
        {
            foreach (Instrument i in instruments)
            {
                if(i != null)
                    i.makeTexture(graphics);
            }
            return instruments;
        }
    }
}
