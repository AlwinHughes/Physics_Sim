using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Physics_Sim
{
    class SmallPlanetSuvat : BaseScreen
    {
        Vector2 COM = new Vector2(400,200);

        float G_CONST = 5f;
        public SmallPlanetSuvat() { }
        Double phi;
        Double r;
        CenterOfMass[] COMs;
        

        public SmallPlanetSuvat(int id, GraphicsDevice graphics) : base(id, graphics) { }

        public override void onLoad()
        {
            objects = new Object[4];

            //Object particle1 = new Object(new Vector2(345, 400), new Vector2(0f, -0.4f), new Vector2(), 5, 5);
            //particle1.setColor(Color.Red);
            //particle1.makeTexture(graphics);
            //particle1.usedgravity = true;
            //objects[0] = particle1;

            //Object particle2 = new Object(new Vector2(500, 400), new Vector2(0f, -0.4f), new Vector2(), 5, 5);
            //particle2.setColor(Color.Black);
            //particle2.makeTexture(graphics);
            //particle2.usedgravity = true;
            //objects[1] = particle2;

            //Object particl2 = new Object(COM, new Vector2(), new Vector2(),5,5);
            //particl2.setColor(Color.Black);
            //particl2.makeTexture(graphics);
            //objects[1] = particl2;
            instruments = new Instrument[1];
            COMs = new CenterOfMass[4];

            CenterOfMass com1 = new CenterOfMass(Color.Black,new Vector2(300, 200), new Vector2(0f, 1.4f), 200f);
            com1.makeTexture(graphics);
            com1.track = true;
            com1.id = 0;
            paths.Add(new List<PathDot>());
            objects[0] = com1;
            COMs[0] = com1;

            CenterOfMass com2 = new CenterOfMass(Color.Yellow, new Vector2(450, 200), new Vector2(0f, -1.866f), 150f);
            com2.makeTexture(graphics);
            com2.track = true;
            paths.Add(new List<PathDot>());
            objects[1] = com2;
            COMs[1] = com2;

            CenterOfMass com3 = new CenterOfMass(Color.Brown, new Vector2(200, 200), new Vector2(0.5f,-4), 10f);
            com3.makeTexture(graphics);
            com3.track = true;
            paths.Add(new List<PathDot>());
            objects[2] = com3;
            COMs[2] = com3;

            CenterOfMass com4 = new CenterOfMass(Color.Brown, new Vector2(800, 0),new Vector2(-3,3), 40f);
            com4.makeTexture(graphics);
            objects[3] = com4;
            COMs[3] = com4;

            //CenterOfMass com3 = new CenterOfMass(Color.Violet, new Vector2(350, 200), 4);
            //com3.makeTexture(graphics);
            //objects[3] = com3;
            //COMs[2] = com3;

            //Object particle2 = new Object(new Vector2(300, 400), new Vector2(2f, -0.4f), new Vector2(), 5, 5);
            //particle2.setColor(Color.Red);
            //particle2.makeTexture(graphics);
            //particle2.usedgravity = true;
            //objects[4] = particle2;

            //Object particle3 = new Object(new Vector2(0, 400), new Vector2(0f, -0f), new Vector2(), 5, 5);
            //particle3.setColor(Color.Red);
            //particle3.makeTexture(graphics);
            //particle3.usedgravity = true;
            //objects[5] = particle3;
        }

        public override void phyUpdate()
        {
            for(int i = 0; i < objects.Length; i++)
            {
                //adding path
                if (objects[i].track && objects[i].tick_count % 5 == 0) 
                {
                    PathDot p = new PathDot(objects[i].color[0], objects[i].pos);
                    p.makeTexture(graphics);
                    paths[i].Add(p);
                    if (paths[i].Count > 50)
                    {
                        paths[i].RemoveAt(0);
                    }
                }

                objects[i].tick_count++;

                // center of mass acceleration 
                if (objects[i].usedgravity)
                {
                    Vector2 a = new Vector2();
                    foreach(CenterOfMass m in COMs)
                    {
                        if(objects[i] != m)
                        {
                            phi = Math.Atan2(m.pos.Y - objects[i].pos.Y, m.pos.X - objects[i].pos.X);
                            r = Math.Sqrt((m.pos.Y - objects[i].pos.Y) * (m.pos.Y - objects[i].pos.Y) + (m.pos.X - objects[i].pos.X) * (m.pos.X - objects[i].pos.X));
                            Double r_pow = Math.Pow(r, -2);
                            a += new Vector2((float)(m.mass * G_CONST * Math.Cos(phi) * r_pow), (float)(m.mass * G_CONST * Math.Sin(phi) * r_pow));
                        }
                    }
                    objects[i].acc = a;
                }
                
                // change position
                if (objects[i].vel.X != 0)
                {
                    objects[i].pos.X += objects[i].vel.X;
                };
                if (objects[i].vel.Y != 0)
                {
                    objects[i].pos.Y += objects[i].vel.Y;
                }

                //change velocity
                if (objects[i].acc.X != 0 || objects[i].acc.Y != 0)
                {
                    objects[i].vel.X += objects[i].acc.X;
                    objects[i].vel.Y += objects[i].acc.Y;
                }
            }
            base.phyUpdate();
        }

    }
}
