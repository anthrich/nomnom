using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Nomnom.GameCode.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nomnom.GameCode
{
    class Physics
    {
        public World World;
        DebugViewXNA DebugView;

        public Physics()
        {
            World = new World(new Microsoft.Xna.Framework.Vector2());
            DebugView = new DebugViewXNA(World);
        }

        public void RegisterNom(Nom nom)
        {
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f);
            float width = ConvertUnits.ToSimUnits(nom.Width);
            float height = ConvertUnits.ToSimUnits(nom.Height);
            Vector2 pos = ConvertUnits.ToSimUnits(nom.GetPosition());
            Body nomBody = BodyFactory.CreateEllipse(World, width, height * 0.16f, 8, 1, pos, nom);
            //Body nomBody = BodyFactory.CreateRectangle(World, width, height, 1, pos, nom);
            nomBody.UserData = nom;
            nomBody.BodyType = BodyType.Dynamic;
            nomBody.AngularDamping = 3f;
            nom.RegisterBody(nomBody);
        }

        public void Update(GameTime gameTime)
        {
            float step = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
            World.Step(step);
            SetNomPositions();
        }

        private void SetNomPositions()
        {
            foreach(Body body in World.BodyList)
            {
                var thisNom = (body.UserData as Nom);
                thisNom.SetPosition(ConvertUnits.ToDisplayUnits(body.Position));
                thisNom.SetRotation(body.Rotation);
            }
        }
    }
}
