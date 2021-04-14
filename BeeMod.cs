using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria.UI;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using System;

namespace BeeMod
{
	public class BeeMod : Mod
	{
        internal int colorUpdate = 0;
        internal static BeeMod instance;
        internal bool canUpdateColor;
        internal List<int> beeItem = new List<int>
        {
            ItemID.BeeGun,
            ItemID.BeeKeeper,
            ItemID.BeesKnees,
            ItemID.Beenade,
            ItemID.BeeMask,
            ItemID.BeeHat,
            ItemID.BeeShirt,
            ItemID.BeePants,
            ItemID.QueenBeeBossBag
        };
        internal List<int> QBGore = new List<int>
        {
            303,
            304,
            305,
            306,
            307
        };
        internal List<TextureHelper> vanillaTextures = new List<TextureHelper>();
        public static List<int> armorCond = new List<int>
                {
                    TextureHelper.PlayerH,
                    TextureHelper.PlayerB,
                    TextureHelper.PlayerFB,
                    TextureHelper.PlayerBArm
                };
        internal static List<IndexedColor> colorStripes = new List<IndexedColor>
        {
            new IndexedColor(new Color(0, 0, 0), 1), // 1
            new IndexedColor(new Color(12, 7, 23), 2), // 2
            new IndexedColor(new Color(12, 6, 23), 3), // 3
            new IndexedColor(new Color(20, 13, 37), 4), // 4
            new IndexedColor(new Color(24, 16, 41), 5), // 5
            new IndexedColor(new Color(23, 16, 41), 6), // 6
            new IndexedColor(new Color(49, 34, 56), 7), // 7
            new IndexedColor(new Color(34, 23, 60), 8), // 8
            new IndexedColor(new Color(38, 25, 66), 9), // 9
            new IndexedColor(new Color(63, 39, 105), 10), // 10
            new IndexedColor(new Color(139, 109, 191), 11), // 11
            new IndexedColor(new Color(83, 111, 157), 12), // 12
            new IndexedColor(new Color(120, 134, 172), 13), // 13
            //Bee Set
            new IndexedColor(new Color(12, 10, 0), 14), // 14
            new IndexedColor(new Color(17, 16, 0), 15), // 15
            new IndexedColor(new Color(22, 19, 0), 16), // 16
            new IndexedColor(new Color(38, 27, 0), 17) // 17
        };
        internal static List<IndexedColor> colorBase = new List<IndexedColor>
        {
            new IndexedColor(new Color(54, 20, 1), -1), // 1
            new IndexedColor(new Color(140, 81, 49), -2), // 2
            new IndexedColor(new Color(254, 246, 37), -3), // 3
            new IndexedColor(new Color(254, 194, 20), -4), // 4
            new IndexedColor(new Color(212, 131, 11), -5), // 5
            new IndexedColor(new Color(87, 39, 13), -6), // 6
            new IndexedColor(new Color(61, 46, 39), -7), // 7
            new IndexedColor(new Color(117, 52, 18), -8), // 8
            new IndexedColor(new Color(150, 67, 22), -9), // 9
            new IndexedColor(new Color(230, 227, 73), -10), // 10
            new IndexedColor(new Color(179, 80, 27), -11), // 11
            new IndexedColor(new Color(186, 136, 22), -12), // 12
            new IndexedColor(new Color(186, 135, 22), -13), // 13
            new IndexedColor(new Color(184, 133, 21), -14), // 14
            new IndexedColor(new Color(187, 136, 24), -15), // 15
            new IndexedColor(new Color(186, 170, 22), -16), // 16
            new IndexedColor(new Color(227, 223, 69), -17), // 17
            new IndexedColor(new Color(232, 228, 74), -18), // 18
            new IndexedColor(new Color(209, 207, 67), -19), // 19
            new IndexedColor(new Color(232, 229, 74), -20), // 20
            new IndexedColor(new Color(242, 241, 145), -21), // 21
            new IndexedColor(new Color(186, 132, 22), -22), //22
            new IndexedColor(new Color(180, 111, 30), -23), //23
            new IndexedColor(new Color(73, 32, 11), -24), //24
            //Bee Set
            new IndexedColor(new Color(255, 140, 0), -25), // 25
            new IndexedColor(new Color(255, 197, 35), -26), // 26
            new IndexedColor(new Color(255, 243, 82), -27), // 27
            new IndexedColor(new Color(255, 238, 0), -28), // 28
            new IndexedColor(new Color(255, 230, 68), -29), // 29
            new IndexedColor(new Color(255, 243, 85), -30), // 30
            new IndexedColor(new Color(255, 238, 60), -31), // 31
            new IndexedColor(new Color(255, 241, 45), -32), // 32
            new IndexedColor(new Color(255, 153, 0), -33), // 33
            new IndexedColor(new Color(222, 207, 0), -34), // 34
            //Other
            new IndexedColor(new Color(165, 100, 23), -35),
        };
        internal bool[] vanityAdded = new bool[6];
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if ((ModContent.GetInstance<BeeConfig1>().rainbowSprite || ModContent.GetInstance<BeeConfig2>().rainbowSprite) && instance.canUpdateColor && ++instance.colorUpdate > ModContent.GetInstance<BeeConfig1>().rainbowSpriteDelay)
            {
                UpdateSets();
                instance.colorUpdate = 0;
                //Main.NewText(Main.DiscoColor);
            }
            //Main.NewText(Main.armorHeadTexture[60] == null);
            if (ModContent.GetInstance<BeeConfig3>().colSpriteVanity)
                AttemptVanityAdd();
            void AttemptVanityAdd()
            {
                if (Main.armorHeadTexture[60] != null && !instance.vanityAdded[0])
                {
                    instance.vanillaTextures.Add(new TextureHelper(TextureHelper.PlayerH, 60, Main.armorHeadTexture[60]));
                    instance.vanityAdded[0] = true;
                }
                if (Main.armorBodyTexture[40] != null && !instance.vanityAdded[1])
                {
                    instance.vanillaTextures.Add(new TextureHelper(TextureHelper.PlayerB, 40, Main.armorBodyTexture[40]));
                    instance.vanityAdded[1] = true;
                }
                if (Main.femaleBodyTexture[40] != null && !instance.vanityAdded[2])
                {
                    instance.vanillaTextures.Add(new TextureHelper(TextureHelper.PlayerFB, 40, Main.femaleBodyTexture[40]));
                    instance.vanityAdded[2] = true;
                }
                if (Main.armorArmTexture[40] != null && !instance.vanityAdded[3])
                {
                    instance.vanillaTextures.Add(new TextureHelper(TextureHelper.PlayerBArm, 40, Main.armorArmTexture[40]));
                    instance.vanityAdded[3] = true;
                }
                if (Main.armorLegTexture[38] != null && !instance.vanityAdded[4])
                {
                    instance.vanillaTextures.Add(new TextureHelper(TextureHelper.PlayerLeg, 38, Main.armorLegTexture[38]));
                    instance.vanityAdded[4] = true;
                }
                if (Main.armorHeadTexture[150] != null && !instance.vanityAdded[5])
                {
                    instance.vanillaTextures.Add(new TextureHelper(TextureHelper.PlayerH, 150, Main.armorHeadTexture[150]));
                    instance.vanityAdded[5] = true;
                }
            }
        }
        public override void Load()
        {
            instance = this;
            Main.instance.LoadNPC(NPCID.QueenBee);
            Main.instance.LoadNPC(NPCID.Bee);
            Main.instance.LoadNPC(NPCID.BeeSmall);
            instance.vanillaTextures.Add(new TextureHelper(TextureHelper.NPC, NPCID.QueenBee, Main.npcTexture[NPCID.QueenBee]));
            instance.vanillaTextures.Add(new TextureHelper(TextureHelper.NPC, NPCID.Bee, Main.npcTexture[NPCID.Bee]));
            instance.vanillaTextures.Add(new TextureHelper(TextureHelper.NPC, NPCID.BeeSmall, Main.npcTexture[NPCID.BeeSmall]));
            instance.vanillaTextures.Add(new TextureHelper(TextureHelper.BossHead, 14, Main.npcHeadBossTexture[14]));
            for (int a = 0; a < Main.itemTexture.Length; a++)
            {
                if (beeItem.Contains(a))
                    vanillaTextures.Add(new TextureHelper(TextureHelper.Item, a, Main.itemTexture[a]));
            }
            Main.instance.LoadProjectile(ProjectileID.Beenade);
            instance.vanillaTextures.Add(new TextureHelper(TextureHelper.Projectile, ProjectileID.Beenade, Main.projectileTexture[ProjectileID.Beenade]));
            for (int a = 0; a < Main.goreTexture.Length; a++)
            {
                if (QBGore.Contains(a))
                {
                    Main.instance.LoadGore(a);
                    vanillaTextures.Add(new TextureHelper(TextureHelper.Gore, a, Main.goreTexture[a]));
                }
            }
            instance.vanillaTextures.Add(new TextureHelper(TextureHelper.Mount, MountID.Bee, Main.beeMountTexture[0]));
        }
        public override void Unload()
        {
            if (!Main.dedServ)
                RevertAllTextures();
            instance.vanillaTextures.Clear();
            instance = null;
        }
        internal void UpdateSets()
        {
            for (int a = 0; a < instance.vanillaTextures.Count; a++)
            {
                TextureHelper tH = instance.vanillaTextures[a];
                if (tH.operationsDetermined && CanChange(tH))
                {
                    if (ModContent.GetInstance<BeeConfig1>().stripes)
                        UpdateTextures(ref tH, false);
                    if (ModContent.GetInstance<BeeConfig2>().beeBase)
                        UpdateTextures(ref tH, true);
                }
                else
                    DetermineOperations(tH);
            }
            bool CanChange(TextureHelper t)
            {
                BeeConfig3 config = ModContent.GetInstance<BeeConfig3>();
                if (TextureHelper.IsItemType(t) && config.colSpriteItems)
                    return true;
                if (t.imgType == TextureHelper.Mount && config.colSpriteMount)
                    return true;
                if (t.imgType == TextureHelper.Gore && config.colSpriteGore)
                    return true;
                if ((t.imgType == TextureHelper.NPC || t.imgType == TextureHelper.BossHead) && config.colSpriteNPC)
                    return true;
                if (TextureHelper.IsArmorType(t) && config.colSpriteVanity)
                    return true;
                return false;
            }
            void DetermineOperations(TextureHelper t)
            {
                Texture2D bee = ModContent.GetTexture("BeeMod/Content/RainbowBee/Bee");
                Color[] bitmap = new Color[bee.Width * bee.Height];
                bee.GetData(bitmap);
                Texture2D beeArmor = ModContent.GetTexture("BeeMod/Content/RainbowBee/BeeArmor");
                Color[] bitmapArmor = new Color[beeArmor.Width * beeArmor.Height];
                beeArmor.GetData(bitmapArmor);

                Color[] array = new Color[t.cache.Width * t.cache.Height];
                t.cache.GetData(array);
                Array.Resize(ref t.operations, array.Length);
                for (int a = 0; a < array.Length; a++)
                {
                    t.operations[a] = 0;
                    if (array[a] != Color.Transparent)
                    {
                        bool foundStripes = false;
                        for (int b = 0; b < colorStripes.Count; b++)
                        {
                            if (array[a] == colorStripes[b].color)
                            {
                                t.operations[a] = colorStripes[b].index;
                                foundStripes = true;
                                break;
                            }
                        }
                        if (!foundStripes)
                        {
                            for (int b = 0; b < colorBase.Count; b++)
                            {
                                if (array[a] == colorBase[b].color)
                                {
                                    t.operations[a] = colorBase[b].index;
                                    break;
                                }
                            }
                        }
                    }
                }
                t.operationsDetermined = true;
            }
        }
        internal void RevertAllTextures()
        {
            foreach (TextureHelper t in instance.vanillaTextures)
            {
                switch (t.imgType)
                {
                    case TextureHelper.Item:
                        RevertTexture(t, Main.itemTexture[t.ID]);
                        break;
                    case TextureHelper.NPC:
                        RevertTexture(t, Main.npcTexture[t.ID]);
                        break;
                    case TextureHelper.Projectile:
                        RevertTexture(t, Main.projectileTexture[t.ID]);
                        break;
                    case TextureHelper.Mount:
                        if (t.ID == MountID.Bee)
                            RevertTexture(t, Main.beeMountTexture[0]);
                        break;
                    case TextureHelper.PlayerH:
                        RevertTexture(t, Main.armorHeadTexture[t.ID]);
                        break;
                    case TextureHelper.PlayerB:
                        RevertTexture(t, Main.armorBodyTexture[t.ID]);
                        break;
                    case TextureHelper.PlayerFB:
                        RevertTexture(t, Main.femaleBodyTexture[t.ID]);
                        break;
                    case TextureHelper.PlayerBArm:
                        RevertTexture(t, Main.armorArmTexture[t.ID]);
                        break;
                    case TextureHelper.PlayerLeg:
                        RevertTexture(t, Main.armorLegTexture[t.ID]);
                        break;
                    case TextureHelper.BossHead:
                        RevertTexture(t, Main.npcHeadBossTexture[t.ID]);
                        break;
                    case TextureHelper.Gore:
                        RevertTexture(t, Main.goreTexture[t.ID]);
                        break;
                }
                void RevertTexture(TextureHelper tH, Texture2D tex)
                {
                    if (tH != null && tex != null && tH.operations.Length > 0)
                    {
                        Color[] array = new Color[tH.cache.Width * tH.cache.Height];
                        tH.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                bool foundStripes = false;
                                for (int b = 0; b < colorStripes.Count; b++)
                                {
                                    if (colorStripes[b].index == tH.operations[a])
                                    {
                                        array[a] = colorStripes[b].color;
                                        foundStripes = true;
                                        break;
                                    }
                                }
                                if (!foundStripes)
                                {
                                    for (int b = 0; b < colorBase.Count; b++)
                                    {
                                        if (colorBase[b].index == tH.operations[a])
                                        {
                                            array[a] = colorBase[b].color;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        tex.SetData(array);
                    }
                }
            }
        }
        internal void UpdateTextures(ref TextureHelper t, bool isBase)
        {
            List<IndexedColor> theList = isBase ? colorBase : colorStripes;
            int divider = isBase ? ModContent.GetInstance<BeeConfig2>().colorIntensity : ModContent.GetInstance<BeeConfig1>().colorIntensity;
            Texture2D bee = ModContent.GetTexture("BeeMod/Content/RainbowBee/Bee");
            Color[] bitmap = new Color[bee.Width * bee.Height];
            bee.GetData(bitmap);

            Texture2D beeArmor = ModContent.GetTexture("BeeMod/Content/RainbowBee/BeeArmor");
            Color[] bitmapArmor = new Color[beeArmor.Width * beeArmor.Height];
            beeArmor.GetData(bitmapArmor);
            switch (t.imgType)
            {
                case TextureHelper.Item:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.itemTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.NPC:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.npcTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.Projectile:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.projectileTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.Mount:
                    {
                        if (t.ID == MountID.Bee)
                        {
                            Color[] array = new Color[t.cache.Width * t.cache.Height];
                            t.cache.GetData(array);
                            for (int a = 0; a < array.Length; a++)
                            {
                                if (array[a] != Color.Transparent)
                                {
                                    for (int b = 0; b < theList.Count; b++)
                                    {
                                        if (theList[b].index == t.operations[a])
                                        {
                                            array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                            break;
                                        }
                                    }
                                }
                            }
                            Main.beeMountTexture[0].SetData(array);
                        }
                    }
                    break;
                case TextureHelper.PlayerH:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.armorHeadTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.PlayerB:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.armorBodyTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.PlayerFB:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.femaleBodyTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.PlayerBArm:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.armorArmTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.PlayerLeg:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.armorLegTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.BossHead:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.npcHeadBossTexture[t.ID].SetData(array);
                    }
                    break;
                case TextureHelper.Gore:
                    {
                        Color[] array = new Color[t.cache.Width * t.cache.Height];
                        t.cache.GetData(array);
                        for (int a = 0; a < array.Length; a++)
                        {
                            if (array[a] != Color.Transparent)
                            {
                                for (int b = 0; b < theList.Count; b++)
                                {
                                    if (theList[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(theList[b].color).R + UpdateColor(isBase).R / divider, Monochrome(theList[b].color).G + UpdateColor(isBase).G / divider, Monochrome(theList[b].color).B + UpdateColor(isBase).B / divider, Monochrome(theList[b].color).A + UpdateColor(isBase).A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.goreTexture[t.ID].SetData(array);
                    }
                    break;
            }
            Color Monochrome(Color color)
            {
                int divide = isBase ? 3 : 1;
                int a = DecideColor();
                int DecideColor()
                {
                    if (color.R >= color.G && color.R >= color.B)
                        return color.R;
                    if (color.G >= color.R && color.G >= color.B)
                        return color.G;
                    if (color.B >= color.G && color.B >= color.R)
                        return color.B;
                    return 0;
                }
                return new Color(a / divide, a / divide, a / divide, color.A);
            };
            Color UpdateColor(bool colorBase)
            {
                if (!colorBase)
                {
                    if (ModContent.GetInstance<BeeConfig1>().rainbowSprite)
                        return Main.DiscoColor;
                    return ModContent.GetInstance<BeeConfig1>().SetColor;
                }
                else
                {
                    if (ModContent.GetInstance<BeeConfig2>().rainbowSprite)
                        return Main.DiscoColor;
                    return ModContent.GetInstance<BeeConfig2>().SetColor;
                }
            }
        }
    }
    #region Config Menus
    [Label("Bee Stripes Configuration")]
    internal class BeeConfig1 : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("[i:1129] [i:2870] [C/FFAF19:Apiarist's Magic (Stripes)]")]

        [DefaultValue(true)]
        [Label("Change Bee Stripes")]
        [Tooltip("When custom bee colors are enabled, all dark colors on bee related textures will be affected")]
        public bool stripes;

        [DefaultValue(false)]
        [Label("Rainbow Bee Stripes")]
        [Tooltip("When custom bee colors are enabled, all dark colors on bee related textures will have a rainbow hue\nIf this is enabled, it will override the custom color set above this config.\nWarning: May cause performance issues.")]
        public bool rainbowSprite;

        [Range(1, 60)]
        [DefaultValue(15)]
        [Label("Rainbow Bee Stripes Delay")]
        [Tooltip("Changes the frequency of how often the 'Rainbow Bee Stripes' changes color, in ticks.")]
        public int rainbowSpriteDelay;

        [Range(1, 10)]
        [DefaultValue(4)]
        [Label("Custom Bee Stripes Color Intensity Reducer")]
        [Tooltip("Changes the intensity of the set colors\nSetting a higher number will reduce the intensity")]
        public int colorIntensity;

        [DefaultValue(typeof(Color), "155, 1, 125, 255")]
        [Label("Custom Bee Stripes Hue")]
        [Tooltip("When custom bee stripes colors are enabled, all dark colors on bee related textures will have a hue of this color")]
        public Color SetColor { get; set; }
        public override void OnChanged() 
        { 
            if (BeeMod.instance != null)
            {
                BeeMod.instance.RevertAllTextures();
                BeeMod.instance.canUpdateColor = false;
            }
        }
    }
    [Label("Bee Base Configuration")]
    internal class BeeConfig2 : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("[i:1129] [i:2870] [C/FFAF19:Apiarist's Magic (Base)]")]

        [DefaultValue(true)]
        [Label("Change Bee Base")]
        [Tooltip("When custom bee colors are enabled, all yellow-ish colors on bee related textures will be affected")]
        public bool beeBase;

        [DefaultValue(false)]
        [Label("Rainbow Bee Base")]
        [Tooltip("When custom bee colors are enabled, all yellow-ish colors on bee related textures will have a rainbow hue\nIf this is enabled, it will override the custom color set above this config.\nWarning: May cause performance issues.")]
        public bool rainbowSprite;

        [Range(1, 60)]
        [DefaultValue(15)]
        [Label("Rainbow Bee Base Delay")]
        [Tooltip("Changes the frequency of how often the 'Rainbow Bee Base' changes color, in ticks.")]
        public int rainbowSpriteDelay;

        [Range(1, 10)]
        [DefaultValue(4)]
        [Label("Custom Bee Base Color Intensity Reducer")]
        [Tooltip("Changes the intensity of the set colors\nSetting a higher number will reduce the intensity")]
        public int colorIntensity;

        [DefaultValue(typeof(Color), "155, 1, 125, 255")]
        [Label("Custom Bee Base Hue")]
        [Tooltip("When custom bee base colors are enabled, all yellow-ish colors on bee related textures will have a hue of this color")]
        public Color SetColor { get; set; }
        public override void OnChanged()
        {
            if (BeeMod.instance != null)
            {
                BeeMod.instance.RevertAllTextures();
                BeeMod.instance.canUpdateColor = false;
            }
        }
    }
    [Label("Modifiable Texture Types")]
    internal class BeeConfig3 : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("[i:1129] [i:3611] [C/FFAF19:Apiarist's Magic (Custom Bee Stripes Menu)]")]

        [DefaultValue(false)]
        [Label("Custom Bee colors (Items) [i/1:1123]")]
        [Tooltip("The chosen colors of Queen Bee drops are affected\nNote: A reload is required to change this config")]
        public bool colSpriteItems;

        [DefaultValue(false)]
        [Label("Custom Bee colors (Equipment) [i/1:843]")]
        [Tooltip("The chosen colors of the Bee Set and Queen Bee Mask are affected\nNote: A reload is required to change this config")]
        public bool colSpriteVanity;

        [DefaultValue(false)]
        [Label("Custom Bee colors (NPCs) [i/1:1364]")]
        [Tooltip("The chosen colors of the Queen Bee (and bees in general) are affected\nNote: this will also affect Queen Bee's Boss-Head Texture\nNote 2 | Electric Boogaloo: A reload is required to change this config")]
        public bool colSpriteNPC;

        [DefaultValue(false)]
        [Label("Custom Bee colors (Bee Mount) [i/1:2502]")]
        [Tooltip("The chosen colors of the Bee Mount are affected\nNote: A reload is required to change this config")]
        public bool colSpriteMount;

        [DefaultValue(false)]
        [Label("Custom Bee colors (Gore) [i/1:1521]")]
        [Tooltip("The chosen colors of Queen Bee gore are affected\nNote: A reload is required to change this config")]
        public bool colSpriteGore;

        public override void OnChanged()
        {
            if (BeeMod.instance != null)
            {
                BeeMod.instance.RevertAllTextures();
                BeeMod.instance.canUpdateColor = false;
            }
        }
    }
    #endregion
    internal class TextureHelper
    {
        public const int Item = 0;
        public const int NPC = 1;
        public const int Mount = 2;
        public const int Projectile = 3;
        public const int PlayerH = 4;
        public const int PlayerB = 5;
        public const int PlayerFB = 6;
        public const int PlayerBArm = 7;
        public const int PlayerLeg = 8;
        public const int BossHead = 9;
        public const int Gore = 10;
        public int imgType;
        public int ID;
        public Texture2D cache;
        public bool operationsDetermined = false;
        public int[] operations = new int[0];
        public TextureHelper(int objType, int type, Texture2D save)
        {
            imgType = objType;
            ID = type;
            cache = save;
        }
        public static bool IsArmorType(TextureHelper t) => t.imgType == PlayerH || t.imgType == PlayerB || t.imgType == PlayerFB || t.imgType == PlayerBArm || t.imgType == PlayerLeg;
        public static bool IsItemType(TextureHelper t) => t.imgType == Item || t.imgType == Projectile;
        public override string ToString()
        {
            string width = cache != null ? $"{cache.Width}" : "null";
            string height = cache != null ? $"{cache.Height}" : "null";
            return $"Type : {imgType} | ID : {ID} | Texture: W {width} H: {height}";
        }
    }
    internal class IndexedColor
    {
        public Color color;
        public int index;
        public IndexedColor(Color c, int i) { color = c; index = i; }
    }
    class BeeCommand : ModCommand
    {
        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return "beecolor"; }
        }

        public override string Description
        {
            get { return "Toggles Custom Bee colors"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args) 
        { 
            BeeMod.instance.canUpdateColor = !BeeMod.instance.canUpdateColor;
            if (!BeeMod.instance.canUpdateColor)
                BeeMod.instance.RevertAllTextures();
            if (BeeMod.instance.canUpdateColor && !ModContent.GetInstance<BeeConfig1>().rainbowSprite)
                BeeMod.instance.UpdateSets();
        }
    }
}