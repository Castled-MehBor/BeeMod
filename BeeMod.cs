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
        internal static List<IndexedColor> colors = new List<IndexedColor>
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
        internal bool[] vanityAdded = new bool[6];
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (ModContent.GetInstance<BeeModConfig>().rainbowSprite && instance.canUpdateColor && ++instance.colorUpdate > ModContent.GetInstance<BeeModConfig>().rainbowSpriteDelay)
            {
                UpdateSets();
                instance.colorUpdate = 0;
                //Main.NewText(Main.DiscoColor);
            }
            //Main.NewText(Main.armorHeadTexture[60] == null);
            if (ModContent.GetInstance<BeeModConfig>().colSpriteVanity)
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
            if (ModContent.GetInstance<BeeModConfig>().colSpriteNPC)
            {
                Main.instance.LoadNPC(NPCID.QueenBee);
                Main.instance.LoadNPC(NPCID.Bee);
                Main.instance.LoadNPC(NPCID.BeeSmall);
                instance.vanillaTextures.Add(new TextureHelper(TextureHelper.NPC, NPCID.QueenBee, Main.npcTexture[NPCID.QueenBee]));
                instance.vanillaTextures.Add(new TextureHelper(TextureHelper.NPC, NPCID.Bee, Main.npcTexture[NPCID.Bee]));
                instance.vanillaTextures.Add(new TextureHelper(TextureHelper.NPC, NPCID.BeeSmall, Main.npcTexture[NPCID.BeeSmall]));
                instance.vanillaTextures.Add(new TextureHelper(TextureHelper.BossHead, 14, Main.npcHeadBossTexture[14]));
            }
            if (ModContent.GetInstance<BeeModConfig>().colSpriteItems)
            {
                for (int a = 0; a < Main.itemTexture.Length; a++)
                {
                    if (beeItem.Contains(a))
                        vanillaTextures.Add(new TextureHelper(TextureHelper.Item, a, Main.itemTexture[a]));
                }
                Main.instance.LoadProjectile(ProjectileID.Beenade);
                instance.vanillaTextures.Add(new TextureHelper(TextureHelper.Projectile, ProjectileID.Beenade, Main.projectileTexture[ProjectileID.Beenade]));
            }
            if (ModContent.GetInstance<BeeModConfig>().colSpriteGore)
                for (int a = 0; a < Main.goreTexture.Length; a++)
            {
                if (QBGore.Contains(a))
                {
                    Main.instance.LoadGore(a);
                    vanillaTextures.Add(new TextureHelper(TextureHelper.Gore, a, Main.goreTexture[a]));
                }
            }
            if (ModContent.GetInstance<BeeModConfig>().colSpriteMount)
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
                if (tH.operationsDetermined)
                    UpdateTextures(ref tH);
                else
                    DetermineOperations(tH);
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
                        for (int b = 0; b < colors.Count; b++)
                        {
                            if (array[a] == colors[b].color)
                            {
                                t.operations[a] = colors[b].index;
                                break;
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == tH.operations[a])
                                    {
                                        array[a] = colors[b].color;
                                        break;
                                    }
                                }
                            }
                        }
                        tex.SetData(array);
                    }
                }
            }
        }
        internal void UpdateTextures(ref TextureHelper t)
        {
            int divider = ModContent.GetInstance<BeeModConfig>().colorIntensity;
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                    for (int b = 0; b < colors.Count; b++)
                                    {
                                        if (colors[b].index == t.operations[a])
                                        {
                                            array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
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
                                for (int b = 0; b < colors.Count; b++)
                                {
                                    if (colors[b].index == t.operations[a])
                                    {
                                        array[a] = new Color(Monochrome(colors[b].color).R + UpdateColor().R / divider, Monochrome(colors[b].color).G + UpdateColor().G / divider, Monochrome(colors[b].color).B + UpdateColor().B / divider, Monochrome(colors[b].color).A + UpdateColor().A / divider);
                                        break;
                                    }
                                }
                            }
                        }
                        Main.goreTexture[t.ID].SetData(array);
                    }
                    break;
            }
            Color Monochrome(Color color) => new Color(color.B, color.B, color.B, color.A);
            Color UpdateColor()
            {
                if (ModContent.GetInstance<BeeModConfig>().rainbowSprite)
                    return Main.DiscoColor;
                return ModContent.GetInstance<BeeModConfig>().SetColor;
            }
        }
    }
    [Label("Bee Stripes Configuration")]
    internal class BeeModConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("[i:1129] [i:2870] [C/FFAF19:Apiarist's Magic]")]

        [DefaultValue(false)]
        [Label("Rainbow Bee Stripes")]
        [Tooltip("When custom bee stripes colors are enabled, all dark colors on bee related textures will have a rainbow hue\nIf this is enabled, it will override the custom color set above this config.\nWarning: May cause performance issues if the 'Rainbow Bee Stripes Delay' config is set to a low value")]
        public bool rainbowSprite;

        [Range(1, 60)]
        [DefaultValue(15)]
        [Label("Rainbow Bee Stripes Delay")]
        [Tooltip("Changes the frequency of how often the 'Rainbow Bee Stripes' changes color, in ticks.")]
        public int rainbowSpriteDelay;

        [Range(1, 10)]
        [DefaultValue(4)]
        [Label("Custom Bee Stripes Intensity Reducer")]
        [Tooltip("Changes the intensity of the set colors\nSetting a higher number will reduce the intensity")]
        public int colorIntensity;

        [DefaultValue(typeof(Color), "155, 1, 125, 255")]
        [Label("Custom Bee Stripes Hue")]
        [Tooltip("When custom bee stripes colors are enabled, all dark colors on bee related textures will have a hue of this color")]
        public Color SetColor { get; set; }

        [Header("[i:1129] [i:3611] [C/FFAF19:Apiarist's Magic (Custom Bee Stripes Menu)]")]

        [DefaultValue(false)]
        [Label("Custom Bee Stripes (Items) [R]")]
        [Tooltip("The Stripes and/or dark colors of Queen Bee weapons and Queen Bee's treasure bag are affected\nNote: A reload is required to change this config")]
        [ReloadRequired]
        public bool colSpriteItems;

        [DefaultValue(false)]
        [Label("Custom Bee Stripes (Equipment) [R]")]
        [Tooltip("The Stripes and/or dark colors of the Bee Set and Queen Bee Mask are affected\nNote: A reload is required to change this config")]
        [ReloadRequired]
        public bool colSpriteVanity;

        [DefaultValue(false)]
        [Label("Custom Bee Stripes (NPCs) [R]")]
        [Tooltip("Stripes and/or dark colors of the Queen Bee (and bees in general) are affected\nNote: this will also affect Queen Bee's Boss-Head Texture\nNote 2 | Electric Boogaloo: A reload is required to change this config")]
        [ReloadRequired]
        public bool colSpriteNPC;

        [DefaultValue(false)]
        [Label("Custom Bee Stripes (Bee Mount) [R]")]
        [Tooltip("The Stripes and/or dark colors of the Bee Mount are affected\nNote: A reload is required to change this config")]
        [ReloadRequired]
        public bool colSpriteMount;

        [DefaultValue(false)]
        [Label("Custom Bee Stripes (Gore) [R]")]
        [Tooltip("Stripes and/or dark colors of Queen Bee gore are affected\nNote: A reload is required to change this config")]
        [ReloadRequired]
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
            get { return "Toggles Custom Bee Colors"; }
        }

        public override void Action(CommandCaller caller, string input, string[] args) 
        { 
            BeeMod.instance.canUpdateColor = !BeeMod.instance.canUpdateColor;
            if (!BeeMod.instance.canUpdateColor)
                BeeMod.instance.RevertAllTextures();
            if (BeeMod.instance.canUpdateColor && !ModContent.GetInstance<BeeModConfig>().rainbowSprite)
                BeeMod.instance.UpdateSets();
        }
    }
}