using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Physics_Sim
{
    class Suvat : BaseScreen
    {
        Vector2 gravity = new Vector2(0f, 0.2f);
        public Suvat() { }

        public Suvat(int id, GraphicsDevice graphics) : base(id, graphics) { }
        
        public override void onLoad()
        {
            objects = new Object[2];
            Console.WriteLine("Loaded: " + Convert.ToString(id));
            Object particle1 = new Object(new Vector2(200f, 400f), new Vector2(10f, -12f), gravity, 30, 30);
            Object particle2 = new Object(new Vector2(20f, 400f), new Vector2(7f, -6f), gravity, 30, 30);
            particle2.setColor(Color.Crimson);
            particle2.makeTexture(graphics);
            particle1.setColor(Color.Black);
            particle1.makeTexture(graphics);
            objects[0] = particle1;
            objects[1] = particle2;

            instruments = new Instrument[4];
            EnergyInstrument instrumet1 = new EnergyInstrument(ref objects[0], new Vector2(0, 0), 50, 50);
            instruments[0] = instrumet1;
            AngleInsstrument instrumen2 = new AngleInsstrument(ref objects[0], new Vector2(90, 40), 30, 4);
            instruments[1] = instrumen2;

            EnergyInstrument instrumet3 = new EnergyInstrument(ref objects[1], new Vector2(750, 0), 50, 50);
            instruments[2] = instrumet3;
            AngleInsstrument instrumen4 = new AngleInsstrument(ref objects[1], new Vector2(690, 40), 30, 4);
            instruments[3] = instrumen4;
        }

        public override void onClose()
        {
            objects = null;
            Console.WriteLine("Closed: " + Convert.ToString(id));
        }

        public override void phyUpdate()
        {
            //Console.WriteLine("Physics Updated " + Convert.ToString(id));
            // updateing objects
            foreach (Object o in objects)
            {

                // change position
                if(o.vel.X != 0)
                {
                    o.pos.X += o.vel.X;
                };
                if(o.vel.Y != 0)
                {
                    o.pos.Y += o.vel.Y;
                }

                //change velocity
                if( o.acc.X != 0 || o.acc.Y != 0)
                {
                    o.vel.X += o.acc.X;
                    o.vel.Y += o.acc.Y;
                }

                // changing knetic energy
                o.KE = o.mass * (o.vel.X*o.vel.X+o.vel.Y*o.vel.Y)/2;

                // change in GPE
                o.GPE = o.mass * gravity.Y * (799 - o.pos.Y);
                // angle against the horozontal
                if(o.vel.Y == 0)
                {
                    o.Theta = (o.vel.X < 0) ? 1.3 * Math.PI : 0.5 * Math.PI;
                }
                o.Theta = Math.Atan(Math.Abs(o.vel.X / o.vel.Y));
                if(o.vel.Y>0 && o.vel.X > 0)
                {
                    o.Theta = Math.PI - o.Theta;
                } else if(o.vel.Y>0 && o.vel.X < 0)
                {
                    o.Theta = Math.PI + o.Theta;
                } else if(o.vel.Y<0 && o.vel.X < 0)
                {
                    o.Theta = 2 * Math.PI - o.Theta;
                }

                // checking for the edge
                if (o.elastic)
                {
                    if (o.pos.X > 790 - o.width || o.pos.X < 10)
                    {
                        o.vel.X *= (float)-0.8;
                    }
                    if (o.pos.Y > 479 - o.height)
                    {
                        o.vel.Y *= (float)-0.8;
                    }
                }
                else
                {//inelastic
                    if (o.pos.X > 799 - o.width)
                    {
                        o.vel.X = 0;
                        o.acc.X = 0;
                    }
                    if (o.pos.Y > 479 - o.height)
                    {
                        o.vel.Y = 0;
                        o.acc.Y = 0;
                    }
                }
            }

            // updating instruments
            base.phyUpdate();
        }
    }
}
