﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class Barracks : Building
    {
        public Barracks(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, 128, 128, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Barracks", 800, 700, 450, Util.Units.PEASANT, 500, Util.Buildings.BARRACKS);

            List<Sprite> sprites = new List<Sprite>();
            // BUILDING
            sprites.Add(new Sprite(576, 708, 48, 39));
            sprites.Add(new Sprite(572, 836, 61, 52));
            sprites.Add(new Sprite(135, 132, 116, 128));
            sprites.Add(new Sprite(135, 4, 128, 128));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("building", new Frame(0, 4));

            this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

            ui = new UI.Buildings.Barracks(managerMouse, this);
            textureName = "Human Buildings (Summer)";

            commands.Add(new BuilderUnits(Util.Units.ELVEN_ARCHER, information.BuildTime));
            commands.Add(new BuilderUnits(Util.Units.FOOTMAN, information.BuildTime));
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < commands.Count; i++)
            {
                var c = (commands[i] as BuilderUnits);
                c.Update();

                if (c.completed)
                {
                    var p = new Point(((int)position.X / 32) + ((width / Warcraft.TILE_SIZE) / 2), ((int)position.Y / 32) + ((height / Warcraft.TILE_SIZE)));
                    managerUnits.Factory(c.type, p.X, p.Y, target.X, target.Y);
                    c.completed = false;
                    c.remove = true;
                }
            }
        }
    }
}
