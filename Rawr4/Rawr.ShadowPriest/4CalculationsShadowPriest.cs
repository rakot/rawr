﻿using System;
using System.Collections.Generic;
using System.Text;
using Rawr.ShadowPriest.Spells;
using System.Windows.Media;

namespace Rawr.ShadowPriest
{
    [Rawr.Calculations.RawrModelInfo("ShadowPriest", "Spell_Shadow_Shadowform", CharacterClass.Priest)]
    public class CalculationsShadowPriest : CalculationsBase
    {    
        #region Gemming
        private List<GemmingTemplate> _defaultGemmingTemplates = null;
        public override List<GemmingTemplate> DefaultGemmingTemplates
        {
            get
            {
                if (_defaultGemmingTemplates == null)
                {
                    // Meta
                    int chaotic = 41285;

                    //Cogwheel
                    int fracturedGW = 59480; //mastery
                    int ridgidGW = 59493; //hit
                    int smoothGW = 59378; //crit
                    int quickGW = 59479; //haste
                    int sparklingGW = 59496; //spirit

                    // [0] uncommon
                    // [1] rare
                    // [2] jewelcrafting
                    
                    // Reds
                    int[] brilliant = { 52084, 52207, 52257 }; //int
                    // Blue
                    int[] sparkling = { 52087, 0, 0 };//spirit
                    int[] rigid = { 0, 0, 52264 };//hit
                    // Yellow
                    int[] fractured = { 52094, 0, 52269 }; //mastery 
                    int[] quick = { 0, 0, 52268  }; //haste
                    int[] smooth = { 0, 0, 52266 }; //crit
                    // Purple
                    int[] veiled = { 52104, 0 }; //int+hit
                    int[] timeless = { 52098, 52248 }; //int+stam
                    int[] purified = { 0, 52236 }; //int+spirit
                    // Green
                    int[] senseis = { 52128, 52237 }; //mastery+hit
                    int[] piercing = { 52122, 52228 }; //crit+hit
                    int[] lightning = { 0, 52225 }; //haste+hit
                    int[] zen = { 52127, 52250 }; //mastery+spirit
                    int[] puissant = { 52126, 52231 }; //mastery+stam
                    int[] jagged = { 52121, 0 }; //crit+stam
                    int[] forceful = { 52124, 52218 }; //haste+stam
                    // Orange
                    int[] artful = { 52117, 52205 }; //int+mastery
                    int[] reckless = { 52113, 0 }; //int+haste
                    int[] potent = { 0, 52239 }; //int+crit

                    _defaultGemmingTemplates = new List<GemmingTemplate>();
                    AddGemmingTemplateGroup(_defaultGemmingTemplates, "Uncommon", false, brilliant[0], sparkling[0], rigid[0], quick[0], smooth[0], veiled[0], timeless[0], purified[0], senseis[0], piercing[0], lightning[0], zen[0], puissant[0], jagged[0], forceful[0],artful[0], reckless[0], potent[0], chaotic);

                    AddJCGemmingTemplateGroup(_defaultGemmingTemplates, "Jewelcrafting", false, brilliant[2], sparkling[2], rigid[2], fractured[2], quick[2], smooth[2], chaotic);
                }
             return _defaultGemmingTemplates;
            }
        }
        private void AddGemmingTemplateGroup(List<GemmingTemplate> list, string name, bool enabled, int brilliant, int sparkling, int rigid, int quick, int smooth, int veiled, int timeless, int purified, int senseis, int piercing, int lightning, int zen, int puissant, int jagged, int forceful, int artful, int reckless, int potent, int meta)
        {
            //int only
            list.Add(new GemmingTemplate() { Model = "ShadowPriest", Group = name, RedId = brilliant, YellowId = brilliant, BlueId = brilliant, PrismaticId = brilliant, MetaId = meta, Enabled = enabled });
        }

        private void AddJCGemmingTemplateGroup(List<GemmingTemplate> list, string name, bool enabled, int brilliant, int sparkling, int rigid, int fractured, int quick, int smooth, int meta)
        {
            //int only
            list.Add(new GemmingTemplate() { Model = "ShadowPriest", Group = name, RedId = brilliant, YellowId = brilliant, BlueId = brilliant, PrismaticId = brilliant, MetaId = meta, Enabled = enabled });
        }
        #endregion
        #region DeserializeDataObject
        public override ICalculationOptionBase DeserializeDataObject(string xml)
        {
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(CalculationOptionsShadowPriest));
            System.IO.StringReader reader = new System.IO.StringReader(xml);
            CalculationOptionsShadowPriest calcOpts = serializer.Deserialize(reader) as CalculationOptionsShadowPriest;
            return calcOpts;
        }
        #endregion
        #region Charts
        #region Subpoints
        //shows bar for each on graphs
        private Dictionary<string, Color> _subPointNameColors = null;
        public override Dictionary<string, Color> SubPointNameColors
        {
            get
            {
                if (_subPointNameColors == null)
                {
                    _subPointNameColors = new Dictionary<string, Color>();
                    _subPointNameColors.Add("DPS", Color.FromArgb(255, 0, 0, 255));
                }
                return _subPointNameColors;
            }
        }
        #endregion
        #region Custom charts
        private string[] _customChartNames = {};
        public override string[] CustomChartNames
        {
            get
            {
                return _customChartNames;
            }
        }
        #endregion
        #endregion
        #region CharacterDisplayCalculationLabels
        private string[] _characterDisplayCalculationLabels = null;
        public override string[] CharacterDisplayCalculationLabels
        {
            get
            {
                if (_characterDisplayCalculationLabels == null)
                    _characterDisplayCalculationLabels = new string[] {
					"Basic Stats:Health",
					"Basic Stats:Mana",
					"Basic Stats:Stamina",
					"Basic Stats:Intellect",
					"Basic Stats:Spirit",
					"Basic Stats:Hit",
					"Basic Stats:Spell Power",
					"Basic Stats:Crit",
					"Basic Stats:Haste",
                    "Basic Stats:Mastery",
                    "Simulation:Rotation",
                    "Simulation:Castlist",
                    "Simulation:DPS",
//                    "Simulation:SustainDPS",
                    "Shadow:Vampiric Touch",
                    "Shadow:SW Pain",
                    "Shadow:Devouring Plague",
                    "Shadow:Imp. Devouring Plague",
				    "Shadow:SW Death",
                    "Shadow:Mind Blast",
                    "Shadow:Mind Flay",
                    "Shadow:Shadowfiend",
                    "Shadow:Mind Spike",
                    "Shadow:Mind Sear",
                    "Holy:PW Shield",
                    "Holy:Smite",
                    "Holy:Holy Fire",
                    "Holy:Penance"
                     
                };
                return _characterDisplayCalculationLabels;
            }
        }
        #endregion
        #region CalculationsOptionsPanel
        public ICalculationOptionsPanel _calculationOptionsPanel = null;
        public override ICalculationOptionsPanel CalculationOptionsPanel {
            get
            {
                if (_calculationOptionsPanel == null)
                {
                    _calculationOptionsPanel = new CalculationOptionsPanelShadowPriest();
                }
                return _calculationOptionsPanel;
            }
        }
        #endregion
        #region Character
        public override CharacterClass TargetClass { get { return CharacterClass.Priest; } }
        #region Character Stats
        public override Stats GetCharacterStats(Character character, Item additionalItem)
        {
            CalculationOptionsShadowPriest calcOpts = character.CalculationOptions as CalculationOptionsShadowPriest;

            Stats statsRace = BaseStats.GetBaseStats(character);
            Stats statsItems = GetItemStats(character, additionalItem);
            Stats statsBuffs = GetBuffsStats(character, calcOpts);

            Stats statsTotal = statsRace + statsItems + statsBuffs;
            //Assume all equiped items are cloth
            statsTotal.BonusIntellectMultiplier += .05f;

            statsTotal.Stamina *= 1 + statsTotal.BonusStaminaMultiplier;
            statsTotal.Intellect *= 1 + statsTotal.BonusIntellectMultiplier;
            statsTotal.Spirit *= 1 + statsTotal.BonusSpiritMultiplier;

            statsTotal.Stamina = (float)Math.Floor(statsTotal.Stamina);
            statsTotal.Intellect = (float)Math.Floor(statsTotal.Intellect);
            statsTotal.Spirit = (float)Math.Floor(statsTotal.Spirit);

            statsTotal.Mana += StatConversion.GetManaFromIntellect(statsTotal.Intellect);
            statsTotal.Mana *= (float)Math.Round(1f + statsTotal.BonusManaMultiplier);

            statsTotal.Health += StatConversion.GetHealthFromStamina(statsTotal.Stamina);
            statsTotal.Health *= (float)Math.Round(1f + statsTotal.BonusHealthMultiplier);

            statsTotal.SpellCrit += StatConversion.GetSpellCritFromRating(statsTotal.CritRating);
            statsTotal.SpellCrit += StatConversion.GetSpellCritFromIntellect(statsTotal.Intellect);
            statsTotal.SpellCrit += statsTotal.SpellCritOnTarget;

            statsTotal.SpellHaste += StatConversion.GetSpellHasteFromRating(statsTotal.SpellHaste);

            //talents
            statsTotal.HitRating += (.5f * character.PriestTalents.TwistedFaith) * statsTotal.Spirit;
            statsTotal.SpellHaste *= 1 + (.01f * character.PriestTalents.Darkness);
            statsTotal.SpellPower *= 1 + (.02f * character.PriestTalents.TwinDisciplines);
            statsTotal.ShadowDamage *= 1 + (.01f * character.PriestTalents.TwistedFaith);

            //Assume Shadow Spec
            //Shadow Power
            statsTotal.BonusSpellPowerMultiplier += 0.25f;
            statsTotal.BonusSpellCritMultiplier += 1f;

            statsTotal.SpellHit += StatConversion.GetSpellHitFromRating(statsTotal.HitRating);

            return statsTotal;
        }
        public Stats GetBuffsStats(Character character, CalculationOptionsShadowPriest calcOpts)
        {
            List<Buff> removedBuffs = new List<Buff>();
            List<Buff> addedBuffs = new List<Buff>();

            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);

            foreach (Buff b in removedBuffs)
            {
                character.ActiveBuffsAdd(b);
            }
            foreach (Buff b in addedBuffs)
            {
                character.ActiveBuffs.Remove(b);
            }

            return statsBuffs;
        }

        #endregion
        #region Character Calculations
        public override CharacterCalculationsBase GetCharacterCalculations(Character character, Item additionalItem, bool referenceCalculation, bool significantChange, bool needsDisplayCalculations)
        {
            CalculationOptionsShadowPriest calcOpts = character.CalculationOptions as CalculationOptionsShadowPriest;
            if (calcOpts == null) calcOpts = new CalculationOptionsShadowPriest();
            BossOptions bossOpts = character.BossOptions;
            if (bossOpts == null) bossOpts = new BossOptions();
            Stats stats = GetCharacterStats(character, additionalItem);

            CharacterCalculationsShadowPriest calculatedStats = new CharacterCalculationsShadowPriest();
            calculatedStats.BasicStats = stats;
            calculatedStats.LocalCharacter = character;

            return calculatedStats;
        }
        #endregion
        #region Relevant Items
        private List<ItemType> _relevantItemTypes = null;
        public override List<ItemType> RelevantItemTypes
        {
            get
            {
                if (_relevantItemTypes == null)
                {
                    _relevantItemTypes = new List<ItemType>(new ItemType[]{
                        ItemType.None,
                        ItemType.Cloth,
                        ItemType.Dagger,
                        ItemType.Wand,
                        ItemType.OneHandMace,
                        ItemType.Staff
                    });
                }
                return _relevantItemTypes;
            }
        }
        #endregion
        #endregion
        #region CalculationBase
        public override ComparisonCalculationBase[] GetCustomChartData(Character character, string chartName)
        {
            return new ComparisonCalculationBase[0];
        }  
        public override ComparisonCalculationBase CreateNewComparisonCalculation() { return new ComparisonCalculationShadowPriest(); }
        public override CharacterCalculationsBase CreateNewCharacterCalculations() { return new CharacterCalculationsShadowPriest(); }
        #endregion
        #region Stats
        public override Stats GetRelevantStats(Stats stats)
        {
            Stats s = new Stats()
            {
                Stamina = stats.Stamina,
                Health = stats.Health,
                Resilience = stats.Resilience,
                Intellect = stats.Intellect,
                Mana = stats.Mana,
                Spirit = stats.Spirit,
                SpellPower = stats.SpellPower,
                SpellShadowDamageRating = stats.SpellShadowDamageRating,
                CritRating = stats.CritRating,
                SpellCrit = stats.SpellCrit,
                SpellCritOnTarget = stats.SpellCritOnTarget,
                HitRating = stats.HitRating,
                SpellHit = stats.SpellHit,
                SpellHaste = stats.SpellHaste,
                HasteRating = stats.HasteRating,
                BonusSpiritMultiplier = stats.BonusSpiritMultiplier,
                BonusIntellectMultiplier = stats.BonusIntellectMultiplier,
                BonusManaPotion = stats.BonusManaPotion,
                ThreatReductionMultiplier = stats.ThreatReductionMultiplier,
                BonusDamageMultiplier = stats.BonusDamageMultiplier,
                BonusShadowDamageMultiplier = stats.BonusShadowDamageMultiplier,
                BonusHolyDamageMultiplier = stats.BonusHolyDamageMultiplier,
                BonusDiseaseDamageMultiplier = stats.BonusDiseaseDamageMultiplier,
                PriestInnerFire = stats.PriestInnerFire,
                MovementSpeed = stats.MovementSpeed,
                SWPDurationIncrease = stats.SWPDurationIncrease,
                BonusMindBlastMultiplier = stats.BonusMindBlastMultiplier,
                MindBlastCostReduction = stats.MindBlastCostReduction,
                ShadowWordDeathCritIncrease = stats.ShadowWordDeathCritIncrease,
                WeakenedSoulDurationDecrease = stats.WeakenedSoulDurationDecrease,
                DevouringPlagueBonusDamage = stats.DevouringPlagueBonusDamage,
                MindBlastHasteProc = stats.MindBlastHasteProc,
                PriestDPS_T9_2pc = stats.PriestDPS_T9_2pc,
                PriestDPS_T9_4pc = stats.PriestDPS_T9_4pc,
                PriestDPS_T10_2pc = stats.PriestDPS_T10_2pc,
                PriestDPS_T10_4pc = stats.PriestDPS_T10_4pc,
                BonusSpellCritMultiplier = stats.BonusSpellCritMultiplier,
                ManaRestore = stats.ManaRestore,
                SpellsManaReduction = stats.SpellsManaReduction,
                HighestStat = stats.HighestStat,
                ArcaneDamage = stats.ArcaneDamage,
                FireDamage = stats.FireDamage,
                FrostDamage = stats.FrostDamage,
                HolyDamage = stats.HolyDamage,
                NatureDamage = stats.NatureDamage,
                ShadowDamage = stats.ShadowDamage,
                ValkyrDamage = stats.ValkyrDamage,

                /*ManaRestoreFromBaseManaPerHit = stats.ManaRestoreFromBaseManaPerHit,
                SpellPowerFor15SecOnUse90Sec = stats.SpellPowerFor15SecOnUse90Sec,
                SpellPowerFor15SecOnUse2Min = stats.SpellPowerFor15SecOnUse2Min,
                SpellPowerFor20SecOnUse2Min = stats.SpellPowerFor20SecOnUse2Min,
                HasteRatingFor20SecOnUse2Min = stats.HasteRatingFor20SecOnUse2Min,
                HasteRatingFor20SecOnUse5Min = stats.HasteRatingFor20SecOnUse5Min,
                SpellPowerFor10SecOnCast_15_45 = stats.SpellPowerFor10SecOnCast_15_45,
                SpellPowerFor10SecOnHit_10_45 = stats.SpellPowerFor10SecOnHit_10_45,
                SpellHasteFor10SecOnCast_10_45 = stats.SpellHasteFor10SecOnCast_10_45,
                TimbalsProc = stats.TimbalsProc,
                PendulumOfTelluricCurrentsProc = stats.PendulumOfTelluricCurrentsProc,
                ExtractOfNecromanticPowerProc = stats.ExtractOfNecromanticPowerProc,
                */

                Armor = stats.Armor,
                BonusArmor = stats.BonusArmor,
                ArcaneResistance = stats.ArcaneResistance,
                ArcaneResistanceBuff = stats.ArcaneResistanceBuff,
                FireResistance = stats.FireResistance,
                FireResistanceBuff = stats.FireResistanceBuff,
                FrostResistance = stats.FrostResistance,
                FrostResistanceBuff = stats.FrostResistanceBuff,
                NatureResistance = stats.NatureResistance,
                NatureResistanceBuff = stats.NatureResistanceBuff,
                ShadowResistance = stats.ShadowResistance,
                ShadowResistanceBuff = stats.ShadowResistanceBuff,
            };

            return s;
        }
        public override bool HasRelevantStats(Stats stats)
        {
            #region Yes
            bool Yes = (
                stats.Intellect + stats.Mana + stats.Spirit + stats.SpellPower
                + stats.SpellShadowDamageRating + stats.CritRating
                + stats.SpellCrit + stats.HitRating + stats.SpellHit + stats.SpellCritOnTarget
                + stats.SpellHaste + stats.HasteRating

                + stats.BonusSpiritMultiplier
                + stats.BonusIntellectMultiplier + stats.BonusManaPotion
                + stats.ThreatReductionMultiplier + stats.BonusDamageMultiplier
                + stats.BonusShadowDamageMultiplier + stats.BonusHolyDamageMultiplier
                + stats.BonusDiseaseDamageMultiplier
                + stats.PriestInnerFire + stats.MovementSpeed

                + stats.SWPDurationIncrease + stats.BonusMindBlastMultiplier
                + stats.MindBlastCostReduction + stats.ShadowWordDeathCritIncrease
                + stats.WeakenedSoulDurationDecrease
                + stats.DevouringPlagueBonusDamage + stats.MindBlastHasteProc
                + stats.PriestDPS_T9_2pc + stats.PriestDPS_T9_4pc
                + stats.PriestDPS_T10_2pc + stats.PriestDPS_T10_4pc
                + stats.ManaRestoreFromBaseManaPPM + stats.BonusSpellCritMultiplier
                + stats.ManaRestore + stats.SpellsManaReduction + stats.HighestStat
                + stats.ArcaneDamage + stats.FireDamage + stats.FrostDamage
                + stats.HolyDamage + stats.NatureDamage + stats.ShadowDamage
                + stats.ValkyrDamage

                /*+ stats.SpellPowerFor15SecOnUse90Sec
                + stats.SpellPowerFor15SecOnUse2Min + stats.SpellPowerFor20SecOnUse2Min
                + stats.HasteRatingFor20SecOnUse2Min + stats.HasteRatingFor20SecOnUse5Min
                + stats.SpellPowerFor10SecOnCast_15_45 + stats.SpellPowerFor10SecOnHit_10_45
                + stats.SpellHasteFor10SecOnCast_10_45 + stats.TimbalsProc
                + stats.PendulumOfTelluricCurrentsProc + stats.ExtractOfNecromanticPowerProc*/
            ) > 0;
            #endregion
            #region Maybe
            bool Maybe = (
                stats.Stamina + stats.Health + stats.Resilience
                + stats.Armor + stats.BonusArmor + stats.Agility
                + stats.ArcaneResistance + stats.ArcaneResistanceBuff
                + stats.FireResistance + stats.FireResistanceBuff
                + stats.FrostResistance + stats.FrostResistanceBuff
                + stats.NatureResistance + stats.NatureResistanceBuff
                + stats.ShadowResistance + stats.ShadowResistanceBuff
            ) > 0;
            #endregion
            #region No
            bool No = (
                stats.Strength + stats.AttackPower
                + stats.ArmorPenetration + stats.ArmorPenetrationRating
                + stats.TargetArmorReduction
                + stats.ExpertiseRating
                + stats.Dodge + stats.DodgeRating
                + stats.Parry + stats.ParryRating
                + stats.Defense + stats.DefenseRating
            ) > 0;
            #endregion
            return Yes || (Maybe && !No);
        }
        #endregion
    }

    public static class Constants
    {
        // Source: http://bobturkey.wordpress.com/2010/09/28/priest-base-mana-pool-and-mana-regen-coefficient-at-85/
        public static float BaseMana = 20590;
    }
}