using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Physics_Sim
{
    class EnergyInstrument : Instrument
    {
        Double totalEnergy;
        int KE;
        int GPE;
        public EnergyInstrument() { }

        public EnergyInstrument(ref Object monitering, Vector2 pos, int width, int height)
            : base(ref monitering, pos, width, height) { }

        public override void makeTexture(GraphicsDevice graphics)
        {
            color = new Color[width * height];
            for(int i = 0; i<width* height; i++)
            {
                if (i < GPE)
                {
                    color[i] = Color.Yellow; // yellow is KE red is GPE
                }else
                {
                    color[i] = Color.Red;
                }
            }
            base.makeTexture(graphics);
        }

        public override void update()
        {
            totalEnergy = monitering.GPE + monitering.KE;
            KE = (int)Math.Round(width * height * monitering.KE / totalEnergy);
            GPE = (int)Math.Round(width * height * monitering.GPE / totalEnergy);
        }

    }
}
