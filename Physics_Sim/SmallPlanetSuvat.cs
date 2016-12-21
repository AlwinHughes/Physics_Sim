using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Physics_Sim
{
    class SmallPlanetSuvat : BaseScreen
    {
        Vector2 COM = new Vector2(400,200);

        float G_CONST = 100f;
        public SmallPlanetSuvat() { }
        Double phi;
        Double r;
        CenterOfMass[] COMs;
        

        public SmallPlanetSuvat(int id, GraphicsDevice graphics) : base(id, graphics) { }

        public override void onLoad()
        {
            objects = new Object[6];

            Object particle1 = new Object(new Vector2(345, 400), new Vector2(0f, -0.4f), new Vector2(), 5, 5);
            particle1.setColor(Color.Red);
            particle1.makeTexture(graphics);
            particle1.usedgravity = true;
            objects[0] = particle1;

            //Object particl2 = new Object(COM, new Vector2(), new Vector2(),5,5);
            //particl2.setColor(Color.Black);
            //particl2.makeTexture(graphics);
            //objects[1] = particl2;
            instruments = new Instrument[1];

            CenterOfMass com1 = new CenterOfMass(Color.Black,new Vector2(300, 250), new Vector2(1.6f, 1.4f), 5f);
            com1.makeTexture(graphics);
            objects[1] = com1;

            COMs = new CenterOfMass[3];
            COMs[0] = com1;

            CenterOfMass com2 = new CenterOfMass(Color.Yellow, new Vector2(400, 150), new Vector2(-2.7f,-1.0f), 2f);
            com2.makeTexture(graphics);
            objects[2] = com2;
            COMs[1] = com2;

            CenterOfMass com3 = new CenterOfMass(Color.Violet, new Vector2(350, 200), 4);
            com3.makeTexture(graphics);
            objects[3] = com3;
            COMs[2] = com3;

            Object particle2 = new Object(new Vector2(300, 400), new Vector2(2f, -0.4f), new Vector2(), 5, 5);
            particle2.setColor(Color.Red);
            particle2.makeTexture(graphics);
            particle2.usedgravity = true;
            objects[4] = particle2;

            Object particle3 = new Object(new Vector2(0, 400), new Vector2(4f, -1.5f), new Vector2(), 5, 5);
            particle3.setColor(Color.Red);
            particle3.makeTexture(graphics);
            particle3.usedgravity = true;
            objects[5] = particle3;
        }

        public override void onClose()
        {
            objects = null;
        }

        public override void phyUpdate()
        {
            foreach(Object o in objects)
            {
                if (o.usedgravity)
                {
                    Vector2 a = new Vector2();
                    foreach(CenterOfMass m in COMs)
                    {
                        if(o != m)
                        {
                            phi = Math.Atan2(m.pos.Y - o.pos.Y, m.pos.X - o.pos.X);
                            r = Math.Sqrt((m.pos.Y - o.pos.Y) * (m.pos.Y - o.pos.Y) + (m.pos.X - o.pos.X) * (m.pos.X - o.pos.X));
                            a += new Vector2((float)(m.mass * G_CONST * Math.Cos(phi) / (r * r)), (float)(m.mass * G_CONST * Math.Sin(phi) / (r * r)));
                        }
                    }
                    o.acc = a;
                }
                
                // change position
                if (o.vel.X != 0)
                {
                    o.pos.X += o.vel.X;
                };
                if (o.vel.Y != 0)
                {
                    o.pos.Y += o.vel.Y;
                }

                //change velocity
                if (o.acc.X != 0 || o.acc.Y != 0)
                {
                    o.vel.X += o.acc.X;
                    o.vel.Y += o.acc.Y;
                }
            }
            base.phyUpdate();
        }

    }
}
