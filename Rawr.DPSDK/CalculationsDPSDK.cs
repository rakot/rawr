﻿using System;
using System.Collections.Generic;
using System.Text;
#if RAWR3
using System.Windows.Media;
#else
using System.Drawing;
#endif
using System.Xml.Serialization;

namespace Rawr.DPSDK
{
    [Rawr.Calculations.RawrModelInfo("DPSDK", "spell_deathknight_classicon", CharacterClass.DeathKnight)]
    public class CalculationsDPSDK : CalculationsBase
    {
        public static double hawut = new Random().NextDouble() * DateTime.Now.ToOADate();
        public override List<GemmingTemplate> DefaultGemmingTemplates
        {
            get
            {
                ////Relevant Gem IDs for DPSDKs

                //Red
                int[] bold = { 39900, 39996, 40111, 42142 };

                //Purple
                int[] sovereign = { 39934, 40022, 40129 };

                //Orange
                int[] inscribed = { 39947, 40037, 40142 };

                // Prismatic 
                int[] tear = { 42702, 42702, 49110 };

                //Meta
                int chaotic = 41285;
                int relentless = 41398;

                return new List<GemmingTemplate>()
				{
					new GemmingTemplate() { Model = "DPSDK", Group = "Rare", //Max Strength
						RedId = bold[1], YellowId = bold[1], BlueId = bold[1], PrismaticId = bold[1], MetaId = chaotic },
					new GemmingTemplate() { Model = "DPSDK", Group = "Rare", //Strength
						RedId = bold[1], YellowId = inscribed[1], BlueId = sovereign[1], PrismaticId = bold[1], MetaId = chaotic },
					new GemmingTemplate() { Model = "DPSDK", Group = "Rare", //Relentless
						RedId = bold[1], YellowId = bold[1], BlueId = bold[1], PrismaticId = tear[1], MetaId = relentless },
						
					new GemmingTemplate() { Model = "DPSDK", Group = "Epic", Enabled = true, //Max Strength
						RedId = bold[2], YellowId = bold[2], BlueId = bold[2], PrismaticId = bold[2], MetaId = chaotic },
					new GemmingTemplate() { Model = "DPSDK", Group = "Epic", Enabled = true, //Strength
						RedId = bold[2], YellowId = inscribed[2], BlueId = sovereign[2], PrismaticId = bold[2], MetaId = chaotic },
					new GemmingTemplate() { Model = "DPSDK", Group = "Epic", Enabled = true, //Relentless
						RedId = bold[2], YellowId = inscribed[2], BlueId = sovereign[2], PrismaticId = tear[2], MetaId = relentless },

					new GemmingTemplate() { Model = "DPSDK", Group = "Jeweler", //Max Strength
						RedId = bold[3], YellowId = bold[3], BlueId = bold[3], PrismaticId = bold[3], MetaId = chaotic },
					new GemmingTemplate() { Model = "DPSDK", Group = "Jeweler", //Strength
						RedId = bold[2], YellowId = bold[3], BlueId = bold[3], PrismaticId = bold[2], MetaId = chaotic },
				};
            }
        }
        public float BonusMaxRunicPower = 0f;

        public static float AddStatMultiplierStat(float statMultiplier, float newValue)
        {
            float updatedStatModifier = ((1 + statMultiplier) * (1 + newValue)) - 1f;
            return updatedStatModifier;
        }

        
        private Dictionary<string, Color> _subPointNameColors = null;
        /// <summary>
        /// Dictionary<string, Color> that includes the names of each rating which your model will use,
        /// and a color for each. These colors will be used in the charts.
        /// 
        /// EXAMPLE: 
        /// subPointNameColors = new Dictionary<string, System.Drawing.Color>();
        /// subPointNameColors.Add("Mitigation", System.Drawing.Colors.Red);
        /// subPointNameColors.Add("Survival", System.Drawing.Colors.Blue);
        /// </summary>
        public override Dictionary<string, Color> SubPointNameColors
        {
            get
            {
                if (_subPointNameColors == null)
                {
#if RAWR3
                    _subPointNameColors = new Dictionary<string, System.Windows.Media.Color>();
                    _subPointNameColors.Add("DPS", System.Windows.Media.Color.FromArgb(255,0,0,255));
#else
                    _subPointNameColors = new Dictionary<string, System.Drawing.Color>();
                    _subPointNameColors.Add("DPS", System.Drawing.Color.FromArgb(255, 0, 0, 255));
#endif
                }
                return _subPointNameColors;
            }
        }


        private string[] _characterDisplayCalculationLabels = null;
        /// <summary>
        /// An array of strings which will be used to build the calculation display.
        /// Each string must be in the format of "Heading:Label". Heading will be used as the
        /// text of the group box containing all labels that have the same Heading.
        /// Label will be the label of that calculation, and may be appended with '*' followed by
        /// a description of that calculation which will be displayed in a tooltip for that label.
        /// Label (without the tooltip string) must be unique.
        /// 
        /// EXAMPLE:
        /// characterDisplayCalculationLabels = new string[]
        /// {
        ///		"Basic Stats:Health",
        ///		"Basic Stats:Armor",
        ///		"Advanced Stats:Dodge",
        ///		"Advanced Stats:Miss*Chance to be missed"
        /// };
        /// </summary>
        public override string[] CharacterDisplayCalculationLabels {
            get {
                if (_characterDisplayCalculationLabels == null) {
                    List<string> labels = new List<string>(new string[] {
                        "Basic Stats:Health",
					    "Basic Stats:Strength",
					    "Basic Stats:Agility",
					    "Basic Stats:Attack Power",
					    "Basic Stats:Crit Rating",
					    "Basic Stats:Hit Rating",
					    "Basic Stats:Expertise",
					    "Basic Stats:Haste Rating",
					    "Basic Stats:Armor Penetration Rating",
                        "Basic Stats:Armor",
					    "Advanced Stats:Weapon Damage*Damage before misses and mitigation",
					    "Advanced Stats:Attack Speed",
					    "Advanced Stats:Crit Chance",
					    "Advanced Stats:Avoided Attacks",
					    "Advanced Stats:Enemy Mitigation",
                        "DPS Breakdown:White",
                        "DPS Breakdown:BCB*Blood Caked Blade",
                        "DPS Breakdown:Necrosis",
                        "DPS Breakdown:Death Coil",
                        "DPS Breakdown:Icy Touch",
                        "DPS Breakdown:Plague Strike",
                        "DPS Breakdown:Frost Fever",
                        "DPS Breakdown:Blood Plague",
                        "DPS Breakdown:Scourge Strike",
                        "DPS Breakdown:Unholy Blight",
                        "DPS Breakdown:Frost Strike",
                        "DPS Breakdown:Howling Blast",
                        "DPS Breakdown:Obliterate",
                        "DPS Breakdown:Death Strike",
                        "DPS Breakdown:Blood Strike",
                        "DPS Breakdown:Heart Strike",
                        "DPS Breakdown:DRW*Dancing Rune Weapon",
                        "DPS Breakdown:Gargoyle",
                        "DPS Breakdown:Wandering Plague",
                        "DPS Breakdown:Ghoul",
                        "DPS Breakdown:Bloodworms",
                        "DPS Breakdown:Other",
                        "DPS Breakdown:Total DPS",
                    });
                    _characterDisplayCalculationLabels = labels.ToArray();
                }
                return _characterDisplayCalculationLabels;
            }
        }

        private string[] _customChartNames = null;
        public override string[] CustomChartNames {
            get {
                if (_customChartNames == null) 
                { 
                    _customChartNames = new string[] 
                        { 
                            "Item Budget (10 point steps)", 
                            "Item Budget (25 point steps)", 
                            "Item Budget (50 point steps)", 
                            "Item Budget (100 point steps)",
                            "Presences"
                        }; }
                return _customChartNames;
            }
        }

#if RAWR3
        private ICalculationOptionsPanel _calculationOptionsPanel = null;
        public override ICalculationOptionsPanel CalculationOptionsPanel
#else
        private CalculationOptionsPanelBase _calculationOptionsPanel = null;
        public override CalculationOptionsPanelBase CalculationOptionsPanel
#endif
        {
            get { return _calculationOptionsPanel ?? (_calculationOptionsPanel = new CalculationOptionsPanelDPSDK()); }
        }

        private List<ItemType> _relevantItemTypes = null;
        public override List<ItemType> RelevantItemTypes {
            get {
                return _relevantItemTypes ?? (_relevantItemTypes = new List<ItemType>(new ItemType[] {
						ItemType.None,
                        ItemType.Leather,
                        ItemType.Mail,
                        ItemType.Plate,
                        ItemType.Sigil,
                        ItemType.Polearm,
                        ItemType.TwoHandAxe,
                        ItemType.TwoHandMace,
                        ItemType.TwoHandSword,
                        ItemType.OneHandAxe,
                        ItemType.OneHandMace,
                        ItemType.OneHandSword
					}));
            }
        }

        public override CharacterClass TargetClass { get { return CharacterClass.DeathKnight; } }
        public override ComparisonCalculationBase CreateNewComparisonCalculation() { return new ComparisonCalculationDPSDK(); }

        public override CharacterCalculationsBase CreateNewCharacterCalculations() { return new CharacterCalculationsDPSDK(); }

        public override ICalculationOptionBase DeserializeDataObject(string xml) {
            XmlSerializer serializer = new XmlSerializer(typeof(CalculationOptionsDPSDK));
            System.IO.StringReader reader = new System.IO.StringReader(xml);
            CalculationOptionsDPSDK calcOpts = serializer.Deserialize(reader) as CalculationOptionsDPSDK;
            return calcOpts;
        }

        /// <summary>
        /// GetCharacterCalculations is the primary method of each model, where a majority of the calculations
        /// and formulae will be used. GetCharacterCalculations should call GetCharacterStats(), and based on
        /// those total stats for the character, and any calculationoptions on the character, perform all the 
        /// calculations required to come up with the final calculations defined in 
        /// CharacterDisplayCalculationLabels, including an Overall rating, and all Sub ratings defined in 
        /// SubPointNameColors.
        /// </summary>
        /// <param name="character">The character to perform calculations for.</param>
        /// <param name="additionalItem">An additional item to treat the character as wearing.
        /// This is used for gems, which don't have a slot on the character to fit in, so are just
        /// added onto the character, in order to get gem calculations.</param>
        /// <returns>A custom CharacterCalculations object which inherits from CharacterCalculationsBase,
        /// containing all of the final calculations defined in CharacterDisplayCalculationLabels. See
        /// CharacterCalculationsBase comments for more details.</returns>
        public override CharacterCalculationsBase GetCharacterCalculations(Character character, Item additionalItem, bool referenceCalculation, bool significantChange, bool needsDisplayCalculations) {
            CalculationOptionsDPSDK calcOpts = character.CalculationOptions as CalculationOptionsDPSDK;
            int targetLevel = calcOpts.TargetLevel;
            //GetTalents(character); // calcOpts.talents was causing infinite recursion, and it shouldn't have been saved there in the first place

#if RAWR3
   /*         if (character != null && character.MainHand != null && character.MainHand.Item == null)
            {
                character.MainHand.Item = new Item("Test Weapon", ItemQuality.Artifact, ItemType.TwoHandAxe, 12345, "",
                   ItemSlot.TwoHand, "", false, new Stats() { Strength = 100f }, new Stats() { }, ItemSlot.None, ItemSlot.None,
                   ItemSlot.None, 500, 1500, ItemDamageType.Physical, 3.6f, "");
            }*/
#endif
            Stats stats = new Stats();

            stats = GetCharacterStats(character, additionalItem);

            CharacterCalculationsDPSDK calcs = new CharacterCalculationsDPSDK();
            calcs.BasicStats = stats;
            calcs.ActiveBuffs = new List<Buff>(character.ActiveBuffs);
            calcs.Talents = character.DeathKnightTalents;

            CombatTable combatTable = new CombatTable(character, calcs, stats, calcOpts/*, additionalItem*/);
            
            //DPS Subgroups
            float dpsWhite = 0f;
            float dpsBCB = 0f;
            float dpsDancingRuneWeapon = 0f;
            float dpsGargoyle = 0f;
            float dpsGhoul = 0f;
            float dpsOtherShadow = 0f;
            float dpsOtherArcane = 0f;
            float dpsOtherFrost = 0f;
            float dpsOtherFire = 0f;
            float dpsBloodworms = 0f;

            //shared variables
            DeathKnightTalents talents = character.DeathKnightTalents;
            bool DW = combatTable.DW;
            float missedSpecial = 0f;

            float dpsWhiteBeforeArmor = 0f;
            float dpsWhiteMinusGlancing = 0f;
            float fightDuration = calcOpts.FightLength * 60;
            float mitigation;
            //float KMProcsPerRotation = 0f;
            float CinderglacierMultiplier = 1f;

            float MHExpertise = stats.Expertise;
            float OHExpertise = stats.Expertise;

            //damage multipliers
            float spellPowerMult = stats.BonusSpellPowerMultiplier;
            float frostSpellPowerMult = stats.BonusSpellPowerMultiplier + Math.Max((stats.BonusFrostDamageMultiplier - stats.BonusShadowDamageMultiplier), 0f);

            float physPowerMult = stats.BonusPhysicalDamageMultiplier;
            // Covers all % physical damage increases.  Blood Frenzy, FI.
            float partialResist = 0.94f; // Average of 6% damage lost to partial resists on spells
            physPowerMult *= 1f + stats.BonusDamageMultiplier;
            spellPowerMult *= 1f + stats.BonusDamageMultiplier;
            frostSpellPowerMult *= 1f + stats.BonusDamageMultiplier;

            // The Bonus Damage Multiplier needs to get applied to the stats object that gets passed into the Ability Handler.
            stats.BonusPhysicalDamageMultiplier = physPowerMult;
            stats.BonusSpellPowerMultiplier = spellPowerMult;
            stats.BonusFrostDamageMultiplier = frostSpellPowerMult;

            //spell AP multipliers, for diseases its per tick
            float HowlingBlastAPMult = 0.2f;
            float IcyTouchAPMult = 0.1f;
            float FrostFeverAPMult = 0.055f;
            float BloodPlagueAPMult = 0.055f;
            float DeathCoilAPMult = 0.15f;
            float UnholyBlightAPMult = 0.013f;
            float GargoyleAPMult = 0.4f;   // pre 3.0.8 == 0.42f...now probably ~0.3f
                                            // TESTED on june 27th 2009 and found to be 0.40f
            float BloodwormsAPMult = 0.006f;

            //for estimating rotation pushback

            calcOpts.rotation.AvgDiseaseMult = calcOpts.rotation.NumDisease * (calcOpts.rotation.DiseaseUptime / 100);
            float commandMult = 0f;
            Rotation r = new Rotation();
            Rotation.Type rType = r.GetRotationType(talents);
            r.setRotation(rType);
            r.copyRotation(calcOpts.rotation);


            {
                float OHMult = 0.5f * (1f + (float)talents.NervesOfColdSteel * 0.083333333f);	//an OH multiplier that is useful sometimes
                //Boolean PTR = false; // enable and disable PTR things here

                #region Impurity Application
                {
                    float impurityMult = 1f + (.04f * (float)talents.Impurity);

                    HowlingBlastAPMult *= impurityMult;
                    IcyTouchAPMult *= impurityMult;
                    FrostFeverAPMult *= impurityMult;
                    BloodPlagueAPMult *= impurityMult;
                    DeathCoilAPMult *= impurityMult;
                    UnholyBlightAPMult *= impurityMult;
                    GargoyleAPMult *= impurityMult;
                }
                #endregion

                #region racials
                {
                    if (character.Race == CharacterRace.Orc) { commandMult += .05f; }
                }
                #endregion

               /* #region Killing Machine
                {
                    float KMPpM = (1f * talents.KillingMachine) * (1f + (StatConversion.GetHasteFromRating(stats.HasteRating, CharacterClass.DeathKnight))) * (1f + stats.PhysicalHaste); // KM Procs per Minute (Defined "1 per point" by Blizzard) influenced by Phys. Haste
                    KMPpM *= calcOpts.KMProcUsage;
                    KMPpM *= 1f - combatTable.totalMHMiss;
                    KMPpM += talents.Deathchill / 2f;

                    double KMPpR = KMPpM / (60f / temp.CurRotationDuration);
                    float totalAbilities = (float)(temp.FrostStrike + temp.IcyTouch + temp.HowlingBlast);
                    KMProcsPerRotation = (float)KMPpR;
                }
                #endregion*/

                

                #region Mitigation
                {
                    float targetArmor = calcOpts.BossArmor;

                    float arpenBuffs = talents.BloodGorged * 2f / 100;

                    mitigation = 1f - StatConversion.GetArmorDamageReduction(character.Level, targetArmor,
                                        stats.ArmorPenetration, arpenBuffs, Math.Max(0, stats.ArmorPenetrationRating));

                    calcs.EnemyMitigation = 1f - mitigation;
                    calcs.EffectiveArmor = mitigation;
                }
                #endregion

                #region White Dmg
                {
                    float MHDPS = 0f, OHDPS = 0f;
                    #region Main Hand
                    {
                        float dpsMHglancing = (StatConversion.WHITE_GLANCE_CHANCE_CAP[targetLevel - 80] * combatTable.MH.DPS) * 0.7f;
                        float dpsMHBeforeArmor = ((combatTable.MH.DPS * (1f - calcs.AvoidedAttacks - StatConversion.WHITE_GLANCE_CHANCE_CAP[targetLevel - 80])) * (1f + combatTable.physCrits)) + dpsMHglancing;
                        dpsWhiteMinusGlancing = dpsMHBeforeArmor - dpsMHglancing;
                        dpsWhiteBeforeArmor = dpsMHBeforeArmor;
                        MHDPS = dpsMHBeforeArmor * mitigation;
                    }
                    #endregion

                    #region Off Hand
                    if (DW || (character.MainHand == null && character.OffHand != null))
                    {
                        float dpsOHglancing = (StatConversion.WHITE_GLANCE_CHANCE_CAP[targetLevel - 80] * combatTable.OH.DPS) * 0.7f;
                        float dpsOHBeforeArmor = ((combatTable.OH.DPS * (1f - calcs.AvoidedAttacks - StatConversion.WHITE_GLANCE_CHANCE_CAP[targetLevel - 80])) * (1f + combatTable.physCrits)) + dpsOHglancing;
                        dpsOHBeforeArmor *= OHMult;
                        dpsWhiteMinusGlancing += dpsOHBeforeArmor - dpsOHglancing;
                        dpsWhiteBeforeArmor += dpsOHBeforeArmor;
                        OHDPS = dpsOHBeforeArmor * mitigation;

                    }
                    #endregion

                    dpsWhite = MHDPS + OHDPS;
                }
                #endregion

                AbilityHandler abilities = new AbilityHandler(stats, combatTable, character, calcOpts);

                // TODO: This gets very expensive.
                if ((calcOpts.GetRefreshForReferenceCalcs ? referenceCalculation : false)
                    || (calcOpts.GetRefreshForDisplayCalcs ? needsDisplayCalculations : false) 
                    || (calcOpts.GetRefreshForSignificantChange ? significantChange : false))
                {
                    if (rType == Rotation.Type.Blood)
                    {
                        BloodCycle cycle = new BloodCycle(character, combatTable, stats, calcOpts, abilities);
                        Rotation rot = cycle.GetDamage((int)(calcOpts.FightLength * 60 * 1000));
                        rot.AvgDiseaseMult = 2;
                        rot.NumDisease = 2;
                        rot.CurRotationDuration = calcOpts.FightLength * 60;
                        calcOpts.rotation.copyRotation(rot);
                        r.copyRotation(rot);
                    }
                    else if (rType == Rotation.Type.Unholy)
                    {
                        UnholyCycle cycle = new UnholyCycle(character, combatTable, stats, calcOpts, abilities);
                        Rotation rot = cycle.GetDamage((int)(calcOpts.FightLength * 60 * 1000));
                        rot.AvgDiseaseMult = 3;
                        rot.NumDisease = 3;
                        rot.CurRotationDuration = calcOpts.FightLength * 60;
                        calcOpts.rotation.copyRotation(rot);
                        r.copyRotation(rot);
                    }
                    else if (rType == Rotation.Type.Frost)
                    {
                        FrostCycle primaryCycle = new FrostCycle(character, combatTable, stats, calcOpts, abilities);
                        Rotation rot = primaryCycle.GetDamage((int)(calcOpts.FightLength * 60 * 1000));
                        rot.AvgDiseaseMult = 2;
                        rot.NumDisease = 2;
                        rot.CurRotationDuration = calcOpts.FightLength * 60;
                        calcOpts.rotation.copyRotation(rot);
                        r.copyRotation(rot);
                    }
                    else
                    {
                        r.copyRotation(calcOpts.rotation);
                    }
                }
                else
                {
                    r.copyRotation(calcOpts.rotation);
                }
                #region Blood Caked Blade
                {
                    float dpsMHBCB = 0f;
                    float dpsOHBCB = 0f;
                    if ((combatTable.OH.damage != 0) && (DW || combatTable.MH.damage == 0))
                    {
                        float OHBCBDmg = (float)(combatTable.OH.damage * (.25f + .125f * calcOpts.rotation.AvgDiseaseMult));
                        dpsOHBCB = OHBCBDmg / combatTable.OH.hastedSpeed;
                        dpsOHBCB *= OHMult;
                    }
                    if (combatTable.MH.damage != 0)
                    {
                        float MHBCBDmg = (float)(combatTable.MH.damage * (.25f + .125f * calcOpts.rotation.AvgDiseaseMult));
                        dpsMHBCB = MHBCBDmg / combatTable.MH.hastedSpeed;
                    }
                    dpsBCB = dpsMHBCB + dpsOHBCB;
                    dpsBCB *= .1f * (float)talents.BloodCakedBlade;
                }
                #endregion
                #region Bloodworms
                {
                    if (talents.Bloodworms > 0)
                    {
                        float BloodwormSwing = 50f + BloodwormsAPMult * stats.AttackPower;
                        float BloodwormSwingDPS = BloodwormSwing / 2.0f;    // any haste benefits?
                        float TotalBloodworms = ((fightDuration / combatTable.MH.hastedSpeed) + calcOpts.rotation.getMeleeSpecialsPerSecond() * fightDuration)
                            * (0.03f * talents.Bloodworms)
                            * 3f; // 3 bloodworms per proc
                        dpsBloodworms = ((TotalBloodworms * BloodwormSwingDPS * 20f) / fightDuration);
                    }
                }
                #endregion

                #region Trinket direct-damage procs, razorice damage, etc
                {
                    dpsOtherArcane = stats.ArcaneDamage;
                    dpsOtherShadow = stats.ShadowDamage;
                    dpsOtherFire = stats.FireDamage;
                    dpsOtherFrost = stats.FrostDamage;

                    // TODO: Differentiate between MH razorice and OH razorice. Hardly matters though, since razorice hits only based off of base damage
                    if (combatTable.MH.baseDamage != 0 && combatTable.MH.hastedSpeed != 0)
                    {
                        dpsOtherFrost += stats.BonusFrostWeaponDamage * combatTable.MH.baseDamage / combatTable.MH.hastedSpeed;
                    }
                    if (combatTable.OH.baseDamage != 0 && combatTable.OH.hastedSpeed != 0)
                    {
                        dpsOtherFrost += stats.BonusFrostWeaponDamage * combatTable.OH.baseDamage / combatTable.OH.hastedSpeed;
                    }
                    if (DW) dpsOtherFrost /= 2f; //razorice only actually effects the weapon its on, not both. this is closer than it would be otherwise.

                    float OtherCritDmgMult = .5f * (1f + stats.BonusCritMultiplier);
                    float OtherCrit = 1f + ((combatTable.spellCrits) * OtherCritDmgMult);
                    dpsOtherArcane *= OtherCrit;
                    //dpsOtherShadow *= OtherCrit;
                    dpsOtherFire *= OtherCrit;
                    //dpsOtherFrost *= OtherCrit;
                }
                #endregion
                    
                    #region Gargoyle
                    {
                        if (calcOpts.rotation.GargoyleDuration > 0f && talents.SummonGargoyle > 0f)
                        {
                            float GargoyleCastTime = 2.0f;  //2.0 second base cast time
                            GargoyleCastTime *= combatTable.MH.hastedSpeed / combatTable.MH.baseSpeed;
                            // benefits from all haste effects

                            float GargoyleStrike = 120f + GargoyleAPMult * stats.AttackPower;
                            float GargoyleStrikeCount = 30f / GargoyleCastTime;
                            float GargoyleDmg = GargoyleStrike * GargoyleStrikeCount;
                            GargoyleDmg *= 1f + (.5f * .05f);  // crit rate is uninfluenced by stats, but roughly crap
                            dpsGargoyle = GargoyleDmg / 180f;
                        }
                    }
                    #endregion

                    #region Ghoul
                    {
                        if (calcOpts.Ghoul)
                        {
                            float uptime = 1f;
                            if (talents.MasterOfGhouls == 0)
                            {
                                float timeleft = calcOpts.FightLength * 60;
                                float numCDs = timeleft / ((4f - .75f * talents.NightOfTheDead) * 60f);
                                float duration = numCDs * 60f;

                                uptime = duration / timeleft;
                            }

                            float GhoulBaseStrength = 331f;
                            float GhoulBaseStrengthMult = 0.7f;
                            float GhoulBaseAP = 836f;
                            float GhoulBaseSpeed = 2f;
                            float GhoulAPMult = 1f;                 // 1 Str = x AP     
                            float ClawCD = 4f;
                            float GhoulWeaponBaseDamage = 101.3f;   // I found these values to be fairly correct after a number of tests (v3.0.8)
                            float GhoulAPdivisor = 14f;           // 

                            float GlyphofGhoulValue = 0f;
                            if (talents.GlyphoftheGhoul) GlyphofGhoulValue = 0.4f;

                            float GhoulStrengthMult = GhoulBaseStrengthMult + (GhoulBaseStrengthMult * (0.2f * (float)talents.RavenousDead)) + GlyphofGhoulValue;
                            float GhoulStrength = GhoulBaseStrength + stats.Strength * GhoulStrengthMult;

                            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);

                            GhoulStrength *= 1f + statsBuffs.BonusStrengthMultiplier;
                            GhoulStrength += statsBuffs.Strength;

                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Flask of Chromatic Wonder"))) GhoulStrength -= 18f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Flask of Chromatic Wonder (Mixology)"))) GhoulStrength -= 4f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Elixir of Mighty Strength"))) GhoulStrength -= 50f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Elixir of Mighty Strength (Mixology)"))) GhoulStrength -= 16f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Guru's Elixir"))) GhoulStrength -= 20f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Guru's Elixir (Mixology)"))) GhoulStrength -= 6f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Scroll of Strength VIII"))) GhoulStrength -= 30f;

                            float GhoulAP = GhoulBaseAP + GhoulStrength * GhoulAPMult;

                            GhoulAP *= 1f + statsBuffs.BonusAttackPowerMultiplier;
                            GhoulAP += statsBuffs.AttackPower;

                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Flask of Endless Rage"))) GhoulAP -= 180f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Flask of Endless Rage (Mixology)"))) GhoulAP -= 64f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Wrath Elixir"))) GhoulAP -= 90f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Wrath Elixir (Mixology)"))) GhoulAP -= 32f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Attack Power Food"))) GhoulAP -= 80f;
                            if (character.ActiveBuffs.Contains(Buff.GetBuffByName("Fish Feast"))) GhoulAP -= 80f;

                            float GhoulhastedSpeed = 0f;
                            if (combatTable.MH != null)
                                GhoulhastedSpeed = ((combatTable.MH.hastedSpeed == 0 ? 1.0f : combatTable.MH.hastedSpeed) / (combatTable.MH.baseSpeed == 0 ? 1.0f : combatTable.MH.baseSpeed)) * GhoulBaseSpeed;
                            else
                                GhoulhastedSpeed = GhoulBaseSpeed;

                            if (talents.GhoulFrenzy > 0f)
                            {
                                float GhoulFrenzyHaste = (float)(0.25f * (calcOpts.rotation.GhoulFrenzy / combatTable.realDuration) * 30f);
                                GhoulhastedSpeed /= 1f + GhoulFrenzyHaste;
                            }
                            GhoulhastedSpeed /= 1f + statsBuffs.PhysicalHaste;

                            float dmgSwing = GhoulWeaponBaseDamage + (GhoulAP / GhoulAPdivisor) * GhoulBaseSpeed;
                            float dpsSwing = dmgSwing / GhoulhastedSpeed;
                            float dpsGhoulGlancing = (dpsSwing * 0.24f) * 0.75f;

                            float dpsClaw = (1.5f * dmgSwing) / ClawCD;

                            dpsSwing *= (1f - missedSpecial) - 0.24f;   // the Ghoul only has one weapon || Glancings added further down
                            dpsClaw *= 1f - missedSpecial;

                            dpsSwing *= 1f + .054f;// + stats.LotPCritRating / 4591f; // needs other crit modifiers, but doesn't inherit crit from master
                            dpsSwing += dpsGhoulGlancing;

                            dpsClaw *= 1f + .054f;// + stats.LotPCritRating / 4591f; // needs other crit modifiers, but doesn't inherit crit from master

                            dpsGhoul = dpsSwing + dpsClaw;

                            //dpsGhoul *= 1f + statsBuffs.BonusPhysicalDamageMultiplier; 
                            // commented out because ghoul doesn't benefit from most bonus physical damage multipliers (ie blood presence, bloody vengeance, etc)
                            int targetArmor = calcOpts.BossArmor;
                            float modArmor = 1f - StatConversion.GetArmorDamageReduction(character.Level, targetArmor,
                                0f/*stats.ArmorPenetration*/, 0f, 0f * stats.ArmorPenetrationRating);

                            dpsGhoul *= modArmor;
                            dpsGhoul *= 1f - (.0065f * missedSpecial);	// pet expertise now scales with player hit rating. Hopefully goes off of melee rather than spell hit.
                            dpsGhoul *= uptime;
                        }
                        else
                            dpsGhoul = 0;
                    }
                    #endregion

                    float BCBMult = 1f;
                    float DancingRuneWeaponMult = 1f;
                    float GargoyleMult = 1f + commandMult;
                    float GhoulMult = 1f + commandMult;
                    float BloodwormsMult = 1f + commandMult;
                    float WhiteMult = 1f;
                    float otherShadowMult = 1f;
                    float otherArcaneMult = 1f;
                    float otherFrostMult = 1f;

                    #region Apply Physical Mitigation
                    {
                        float physMit = mitigation;
                        physMit *= 1f + (!DW ? .02f * talents.TwoHandedWeaponSpecialization : 0f);

                        dpsBCB *= physMit;
                        dpsBloodworms *= 1f - StatConversion.GetArmorDamageReduction(character.Level, calcOpts.BossArmor, 0f/*stats.ArmorPenetration*/, 0f, 0f);

                        WhiteMult *= physPowerMult * (1f + (!DW ? .02f * talents.TwoHandedWeaponSpecialization : 0f));
                        BCBMult *= physPowerMult * (1f + (!DW ? .02f * talents.TwoHandedWeaponSpecialization : 0f));
                    }
                    #endregion

                    #region Apply Magical Mitigation
                    {
                        // some of this applies to necrosis, I wonder if it is ever accounted for
                        float magicMit = partialResist /** combatTable.spellResist*/;
                        // magicMit = 1f - magicMit;

                        otherShadowMult *= spellPowerMult;
                        otherArcaneMult *= spellPowerMult;
                        otherFrostMult *= frostSpellPowerMult;
                    }
                    #endregion

                    #region Cinderglacier multipliers
                    {
                        #region Cinderglacier
                        {
                            if (stats.CinderglacierProc > 0f && combatTable.MH != null)
                            {
                                float shadowFrostAbilitiesPerSecond = (float)((r.DeathCoil + r.FrostStrike +
                                    r.ScourgeStrike + r.IcyTouch + r.HowlingBlast) /
                                    combatTable.realDuration);
                                float CGPercentChance = 1.5f / (60f / combatTable.MH.baseSpeed);
                                float CGPPM = ((60f / combatTable.MH.hastedSpeed) * CGPercentChance) * 1f - combatTable.totalMHMiss; // KM Procs per Minute (Defined "1 per point" by Blizzard) influenced by Phys. Haste
                                CGPPM += (1.5f / (60f / combatTable.MH.baseSpeed)) * ((combatTable.totalMeleeAbilities * (1f - combatTable.missedSpecial) + combatTable.totalSpellAbilities * (1f - combatTable.spellResist)) * 60f / combatTable.realDuration);
                                float ProcsPerAbility = CGPPM / (shadowFrostAbilitiesPerSecond * 60f);
                                CinderglacierMultiplier = 1f + 2f * 0.2f * (ProcsPerAbility);

                            }
                        }
                        #endregion
                        abilities.DC.DamageMod *= CinderglacierMultiplier;
                        abilities.HB.DamageMod *= CinderglacierMultiplier;
                        abilities.IT.DamageMod *= CinderglacierMultiplier;
                        abilities.FS.DamageMod *= CinderglacierMultiplier;
                    }
                    #endregion

                    #region Apply Multi-Ability Talent Multipliers
                    {
                        float BloodyVengeanceMult = .03f * (float)talents.BloodyVengeance;
                        BCBMult *= 1 + BloodyVengeanceMult;
                        WhiteMult *= 1 + BloodyVengeanceMult;

                        float HysteriaCoeff = .2f / 6f; // current uptime is 16.666...%
                        float HysteriaMult = HysteriaCoeff * (float)talents.Hysteria;
                        BCBMult *= 1 + HysteriaMult;
                        WhiteMult *= 1 + HysteriaMult;

                        float BlackIceMult = .02f * (float)talents.BlackIce;
                        otherShadowMult *= 1 + BlackIceMult;
                        otherFrostMult *= 1 + BlackIceMult;

                        float DesecrationMult = .01f * (float)talents.Desolation;  	// the new desecration is basically a flat 1% per point
                        BCBMult *= 1 + DesecrationMult;
                        WhiteMult *= 1 + DesecrationMult;
                        otherShadowMult *= 1 + DesecrationMult;
                        otherArcaneMult *= 1 + DesecrationMult;
                        otherFrostMult *= 1 + DesecrationMult;

                        if ((float)talents.BoneShield >= 1f)
                        {
                            float BoneMult = .02f;
                            BCBMult *= 1 + BoneMult;
                            WhiteMult *= 1 + BoneMult;
                            otherShadowMult *= 1 + BoneMult;
                            otherArcaneMult *= 1 + BoneMult;
                            otherFrostMult *= 1 + BoneMult;
                        }
                    }
                    #endregion

                    #region Pet uptime modifiers
                    {
                        BloodwormsMult *= calcOpts.BloodwormsUptime;
                        GhoulMult *= calcOpts.GhoulUptime;
                    }
                    #endregion
                
                //feed variables for output
                calcs.BCBDPS = dpsBCB * BCBMult;
                calcs.DRWDPS = dpsDancingRuneWeapon * DancingRuneWeaponMult;
                calcs.GargoyleDPS = dpsGargoyle * GargoyleMult;
                calcs.GhoulDPS = dpsGhoul * GhoulMult;
                calcs.BloodwormsDPS = dpsBloodworms * BloodwormsMult;
                calcs.NecrosisDPS = dpsWhite * (.04f * talents.Necrosis);
                calcs.WhiteDPS = dpsWhite * WhiteMult;
                calcs.OtherDPS = dpsOtherShadow * otherShadowMult +
                    dpsOtherArcane * otherArcaneMult +
                    dpsOtherFire * otherArcaneMult +
                    dpsOtherFrost * otherFrostMult;

                // TODO: Re-work this to properly evaluate a base rotation and subrotations w/ special strikes.
                // First let's get the common stuff out of the ifs
                // Diseases & the abilities that cause them
                calcs.PlagueStrikeDPS = (float)((abilities.PS.Damage * r.PlagueStrike) / (calcOpts.FightLength * 60));
                calcs.BloodPlagueDPS = (float)((abilities.BP.Damage * r.BPTick) / (calcOpts.FightLength * 60));
                calcs.IcyTouchDPS = (float)((abilities.IT.Damage * r.IcyTouch) / (calcOpts.FightLength * 60));
                calcs.FrostFeverDPS = (float)((abilities.FF.Damage * r.FFTick) / (calcOpts.FightLength * 60));
                calcs.BloodStrikeDPS = (float)((abilities.BS.Damage * r.BloodStrike) / (calcOpts.FightLength * 60));
                // RP based
                calcs.DeathCoilDPS = (float)((abilities.DC.Damage * r.DeathCoil) / (calcOpts.FightLength * 60));

                if (rType == Rotation.Type.Frost)
                {
                    calcs.BloodStrikeDPS = (float)((abilities.BS.Damage * r.BloodStrike) / (calcOpts.FightLength * 60));
                    if ( talents.FrostStrike > 0 )
                        calcs.FrostStrikeDPS = (float)((abilities.FS.Damage * r.FrostStrike + abilities.FS.SecondaryDamage * r.KMFS) / (calcOpts.FightLength * 60));
                    else
                        calcs.DeathCoilDPS = (float)((abilities.DC.Damage * r.DeathCoil) / (calcOpts.FightLength * 60));

                    if (talents.HowlingBlast > 0)
                        calcs.HowlingBlastDPS = (float)((abilities.HB.Damage * r.HowlingBlast + abilities.HB.SecondaryDamage * r.KMRime) / (calcOpts.FightLength * 60));
                    calcs.ObliterateDPS = (float)((abilities.OB.Damage * r.Obliterate) / (calcOpts.FightLength * 60));
                }

                else if (rType == Rotation.Type.Blood)
                {
                    if (talents.HeartStrike > 0 )
                        calcs.HeartStrikeDPS = (float)((abilities.HS.Damage * r.HeartStrike) / (calcOpts.FightLength * 60));
                    else
                        calcs.BloodStrikeDPS = (float)((abilities.BS.Damage * r.BloodStrike) / (calcOpts.FightLength * 60));
                    calcs.DeathStrikeDPS = (float)((abilities.DS.Damage * r.DeathStrike) / (calcOpts.FightLength * 60));
                    calcs.DeathCoilDPS = (float)((abilities.DC.Damage * r.DeathCoil) / (calcOpts.FightLength * 60));
                }

                else if (rType == Rotation.Type.Unholy)
                {
                    if (talents.ScourgeStrike > 0)
                        calcs.ScourgeStrikeDPS = (float)(((abilities.SS.Damage + abilities.SS.SecondaryDamage) * r.ScourgeStrike) / (calcOpts.FightLength * 60));
                    else
                        calcs.ObliterateDPS = (float)((abilities.OB.Damage * r.Obliterate) / (calcOpts.FightLength * 60));
                    calcs.BloodStrikeDPS = (float)((abilities.BS.Damage * r.BloodStrike) / (calcOpts.FightLength * 60));
                    calcs.DeathCoilDPS = (float)((abilities.DC.Damage * r.DeathCoil) / (calcOpts.FightLength * 60));
                    if (talents.UnholyBlight > 0)
                    {
                        float modifier = (0.1f * (talents.GlyphofUnholyBlight ? 1.4f : 1));
                        calcs.UnholyBlightDPS = calcs.DeathCoilDPS * modifier;
                    }
                }

                calcs.DPSPoints = calcs.BCBDPS + calcs.BloodPlagueDPS + calcs.BloodStrikeDPS + calcs.DeathCoilDPS + calcs.FrostFeverDPS + calcs.FrostStrikeDPS +
                                  calcs.GargoyleDPS + calcs.GhoulDPS + calcs.WanderingPlagueDPS + calcs.HeartStrikeDPS + calcs.HowlingBlastDPS + calcs.IcyTouchDPS +
                                  calcs.NecrosisDPS + calcs.ObliterateDPS + calcs.DeathStrikeDPS + calcs.PlagueStrikeDPS + calcs.ScourgeStrikeDPS + calcs.UnholyBlightDPS +
                                  calcs.WhiteDPS + calcs.OtherDPS + calcs.BloodwormsDPS;

                #region Dancing Rune Weapon
                {
                    if (talents.DancingRuneWeapon > 0)
                    {
                        float dpsDRWMaximum = 0f;
                        String effectsStats = "";
                        Stats maxStats = GetCharacterStatsMaximum(character, additionalItem, 90f);
                        DRW drw;
                        Stats tempStats;

                        foreach (SpecialEffect effect in maxStats.SpecialEffects())
                        {
                            if (effect.Trigger != Trigger.Use)
                            {
                                tempStats = new Stats();
                                tempStats += maxStats;
                                tempStats += effect.Stats;



                                tempStats.Strength += tempStats.HighestStat + tempStats.Paragon;

                                tempStats.Agility = (float)Math.Floor(tempStats.Agility * (1 + tempStats.BonusAgilityMultiplier));
                                tempStats.Strength = (float)Math.Floor(tempStats.Strength * (1 + tempStats.BonusStrengthMultiplier));
                                tempStats.Stamina = (float)Math.Floor(tempStats.Stamina * (1 + tempStats.BonusStaminaMultiplier));
                                tempStats.Intellect = (float)Math.Floor(tempStats.Intellect * (1 + tempStats.BonusIntellectMultiplier));
                                tempStats.Spirit = (float)Math.Floor(tempStats.Spirit * (1 + tempStats.BonusSpiritMultiplier));
                                tempStats.Health = (float)Math.Floor(tempStats.Health + (tempStats.Stamina * 10f));
                                tempStats.Mana = (float)Math.Floor(tempStats.Mana + (tempStats.Intellect * 15f));
                                tempStats.AttackPower = (float)Math.Floor(tempStats.AttackPower + tempStats.Strength * 2);
                                // Copy from TankDK.
                                // tempStats.Armor = (float)Math.Floor((tempStats.Armor + tempStats.BonusArmor + 2f * tempStats.Agility) * 1f);
                                tempStats.Armor = (float)Math.Floor(StatConversion.GetArmorFromAgility(tempStats.Agility) +
                                                    StatConversion.ApplyMultiplier(tempStats.Armor, tempStats.BaseArmorMultiplier) +
                                                    StatConversion.ApplyMultiplier(tempStats.BonusArmor, tempStats.BonusArmorMultiplier));

                                tempStats.AttackPower += (tempStats.Armor / 180f) * (float)talents.BladedArmor;

                                tempStats.BonusSpellPowerMultiplier = tempStats.BonusShadowDamageMultiplier;

                                tempStats.AttackPower *= 1f + tempStats.BonusAttackPowerMultiplier;

                                drw = new DRW(combatTable, calcs, calcOpts, tempStats, character, talents);
                                if (drw.dpsDancingRuneWeapon > dpsDRWMaximum)
                                {
                                    dpsDRWMaximum = drw.dpsDancingRuneWeapon;
                                    effectsStats = effect.ToString();
                                }
                            }

                        }
                        dpsDancingRuneWeapon = dpsDRWMaximum;
                        float DRWUptime = (12f + (talents.GlyphofDancingRuneWeapon ? 5f : 0f)) / 90f;
                        dpsDancingRuneWeapon /= 90f;
                        calcs.DPSPoints += dpsDancingRuneWeapon;
                        calcs.DRWDPS = dpsDancingRuneWeapon;
                        calcs.DRWStats = effectsStats;
                    }
                }
                #endregion

                calcs.OverallPoints = calcs.DPSPoints;

                // TODO: Display rotation data:

            }

            return calcs;
        }

        private Stats GetRaceStats(Character character) 
        {
            return BaseStats.GetBaseStats(character.Level, CharacterClass.DeathKnight, character.Race);
        }

        private Stats GetPresenceStats(CalculationOptionsDPSDK.Presence p)
        {
            Stats PresenceStats = new Stats();
            switch(p)
            {
                case CalculationOptionsDPSDK.Presence.Blood:
                {
                    PresenceStats.BonusPhysicalDamageMultiplier = .15f;
                    PresenceStats.BonusSpellPowerMultiplier = .15f;
                    PresenceStats.BonusShadowDamageMultiplier = .15f;
                    PresenceStats.BonusFrostDamageMultiplier = .15f;
                    break;
                }
                case CalculationOptionsDPSDK.Presence.Unholy:
                {
                    PresenceStats.PhysicalHaste = 0.15f;
                    PresenceStats.SpellHaste = 0.15f;
                    break;
                }
                case CalculationOptionsDPSDK.Presence.Frost:
                {
                    PresenceStats.BonusStaminaMultiplier = 0.08f;
                    PresenceStats.BaseArmorMultiplier = .60f;
                    PresenceStats.DamageTakenMultiplier = -.08f;
                    break;
                }
            }
            return PresenceStats;
        }

        /// <summary>
        /// GetCharacterStats is the 2nd-most calculation intensive method in a model. Here the model will
        /// combine all of the information about the character, including race, gear, enchants, buffs,
        /// calculationoptions, etc., to form a single combined Stats object. Three of the methods below
        /// can be called from this method to help total up stats: GetItemStats(character, additionalItem),
        /// GetEnchantsStats(character), and GetBuffsStats(character.ActiveBuffs).
        /// </summary>
        /// <param name="character">The character whose stats should be totaled.</param>
        /// <param name="additionalItem">An additional item to treat the character as wearing.
        /// This is used for gems, which don't have a slot on the character to fit in, so are just
        /// added onto the character, in order to get gem calculations.</param>
        /// <returns>A Stats object containing the final totaled values of all character stats.</returns>
        public override Stats GetCharacterStats(Character character, Item additionalItem) {
            CalculationOptionsDPSDK calcOpts = character.CalculationOptions as CalculationOptionsDPSDK;
            DeathKnightTalents talents = character.DeathKnightTalents;

            Stats statsRace = GetRaceStats(character);
            Stats statsBaseGear = GetItemStats(character, additionalItem);

            // Filter out the duplicate Runes:
            if (character.OffHandEnchant == Enchant.FindEnchant(3368, ItemSlot.OneHand, character)
                && character.MainHandEnchant == character.OffHandEnchant)
            {
                bool bFC1Found = false;
                bool bFC2Found = false;
                foreach (SpecialEffect se1 in statsBaseGear.SpecialEffects())
                {
                    // if we've already found them, and we're seeing them again, then remove these repeats.
                    if (bFC1Found && _SE_FC1.Stats.Equals(se1.Stats) && _SE_FC1.Trigger.Equals(se1.Trigger))
                        statsBaseGear.RemoveSpecialEffect(se1);
                    else if (bFC2Found && _SE_FC2.Stats.Equals(se1.Stats) && _SE_FC2.Trigger.Equals(se1.Trigger))
                        statsBaseGear.RemoveSpecialEffect(se1);
                    else if (_SE_FC1.Stats.Equals(se1.Stats) && _SE_FC1.Trigger.Equals(se1.Trigger))
                        bFC1Found = true;
                    else if (_SE_FC2.Stats.Equals(se1.Stats) && _SE_FC2.Trigger.Equals(se1.Trigger))
                        bFC2Found = true;
                }
            }


            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);
            Stats statsPresence = GetPresenceStats(calcOpts.CurrentPresence);
            Stats statsTalents = new Stats()
            {
            // TODO: Expand this since I know in TankDK this was a much broader set of Talent->Stat adjustments.
                BonusStrengthMultiplier = .01f * (float)(talents.AbominationsMight + talents.RavenousDead) + .02f * (float)(/*talents.ShadowOfDeath + */talents.VeteranOfTheThirdWar + talents.EndlessWinter),
                BaseArmorMultiplier = .03f * (float)(talents.Toughness),
                BonusStaminaMultiplier = .02f * (float)(/*talents.ShadowOfDeath + */talents.VeteranOfTheThirdWar),
                Expertise = (float)(talents.TundraStalker + talents.RageOfRivendare) + 2f * (float)(talents.VeteranOfTheThirdWar),

                //ArmorPenetration = talents.BloodGorged * 2f / 100,
                PhysicalHaste = 0.04f * talents.IcyTalons + .05f * talents.ImprovedIcyTalons
            };
            if (talents.UnbreakableArmor > 0)
            {
                statsTalents.AddSpecialEffect(new SpecialEffect(Trigger.Use, new Stats(){ BonusStrengthMultiplier = 0.2f}, 20f, 60f));
            }

            Stats statsTotal = new Stats();
            statsTotal.Accumulate(statsBaseGear);
            statsTotal.Accumulate(statsBuffs);
            statsTotal.Accumulate(statsRace);
            statsTotal.Accumulate(statsPresence);
            statsTotal.Accumulate(statsTalents);

            statsTotal = GetRelevantStats(statsTotal);
            statsTotal.Expertise += (float)StatConversion.GetExpertiseFromRating(statsTotal.ExpertiseRating);

            StatsSpecialEffects se = new StatsSpecialEffects(character, statsTotal, new CombatTable(character, statsTotal, calcOpts));
            float tempCap = StatConversion.RATING_PER_ARMORPENETRATION * (1f - statsTotal.ArmorPenetration);

            foreach (SpecialEffect effect in statsTotal.SpecialEffects())
            {
                if (HasRelevantStats(effect.Stats))
                {
                    // TODO: This code is very expensive for some reason.  
                    // Need to isolate and fix what's going on here to improve performance
                    if (effect.Stats.ArmorPenetrationRating > 0f
                        && ((effect.Stats.ArmorPenetrationRating + statsTotal.ArmorPenetrationRating) > tempCap))
                    {
                        SpecialEffect tempEffect = new SpecialEffect(effect.Trigger, effect.Stats.Clone(), effect.Duration, effect.Cooldown, effect.Chance, effect.MaxStack);
                        tempEffect.Stats.ArmorPenetrationRating = Math.Max(tempCap - statsTotal.ArmorPenetrationRating, 0f);
                        se = new StatsSpecialEffects(character, statsTotal, new CombatTable(character, statsTotal, calcOpts));
                        statsTotal.Accumulate(se.getSpecialEffects(calcOpts, tempEffect));
                    }
                    else
                    {
                        se = new StatsSpecialEffects(character, statsTotal, new CombatTable(character, statsTotal, calcOpts));
                        statsTotal.Accumulate(se.getSpecialEffects(calcOpts, effect));
                    }
                }
            }

            statsTotal.Strength += statsTotal.HighestStat + statsTotal.Paragon + statsTotal.DeathbringerProc/3;
            statsTotal.HasteRating += statsTotal.DeathbringerProc/3;
            statsTotal.CritRating += statsTotal.DeathbringerProc/3;

            statsTotal.Agility = (float)Math.Floor(statsTotal.Agility * (1 + statsTotal.BonusAgilityMultiplier));
            statsTotal.Strength = (float)Math.Floor(statsTotal.Strength * (1 + statsTotal.BonusStrengthMultiplier));
            statsTotal.Stamina = (float)Math.Floor(statsTotal.Stamina * (1 + statsTotal.BonusStaminaMultiplier));
            statsTotal.Intellect = (float)Math.Floor(statsTotal.Intellect * (1 + statsTotal.BonusIntellectMultiplier));
            statsTotal.Spirit = (float)Math.Floor(statsTotal.Spirit * (1 + statsTotal.BonusSpiritMultiplier));
            statsTotal.Health = (float)Math.Floor(statsTotal.Health + (statsTotal.Stamina * 10f));
            statsTotal.Mana = (float)Math.Floor(statsTotal.Mana + (statsTotal.Intellect * 15f));
            statsTotal.AttackPower = (float)Math.Floor(statsTotal.AttackPower + statsTotal.Strength * 2);
            statsTotal.Armor = (float)Math.Floor(StatConversion.GetArmorFromAgility(statsTotal.Agility) +
                                StatConversion.ApplyMultiplier(statsTotal.Armor, statsTotal.BaseArmorMultiplier) +
                                StatConversion.ApplyMultiplier(statsTotal.BonusArmor, statsTotal.BonusArmorMultiplier));

            statsTotal.AttackPower += (statsTotal.Armor / 180f) * (float)talents.BladedArmor;

            statsTotal.BonusSpellPowerMultiplier++;
            statsTotal.BonusFrostDamageMultiplier++;
            statsTotal.BonusShadowDamageMultiplier++;

            statsTotal.AttackPower *= 1f + statsTotal.BonusAttackPowerMultiplier;

            statsTotal.BonusPhysicalDamageMultiplier++;

            if (calcOpts.CurrentPresence == CalculationOptionsDPSDK.Presence.Blood)  // a final, multiplicative component
            {
                statsTotal.BonusPhysicalDamageMultiplier *= 1.15f;
                statsTotal.BonusSpellPowerMultiplier *= 1.15f;
                statsTotal.BonusShadowDamageMultiplier *= 1.15f;
                statsTotal.BonusFrostDamageMultiplier *= 1.15f;
            }
            else if (calcOpts.CurrentPresence == CalculationOptionsDPSDK.Presence.Unholy)  // a final, multiplicative component
            {
                statsTotal.PhysicalHaste += 0.15f;
                statsTotal.SpellHaste += 0.15f;
            }

            return (statsTotal);
        }

        public Stats GetCharacterStatsMaximum(Character character, Item additionalItem, float abilityCooldown)
        {
            CalculationOptionsDPSDK calcOpts = character.CalculationOptions as CalculationOptionsDPSDK;
            DeathKnightTalents talents = character.DeathKnightTalents;
            Stats statsRace = GetRaceStats(character);
            Stats statsBaseGear = GetItemStats(character, additionalItem);
            //Stats statsEnchants = GetEnchantsStats(character);
            Stats statsBuffs = GetBuffsStats(character.ActiveBuffs);
            Stats statsTalents = new Stats()
            {
                BonusStrengthMultiplier = .01f * (float)(talents.AbominationsMight + talents.RavenousDead) + .02f * (float)(/*talents.ShadowOfDeath + */talents.VeteranOfTheThirdWar),
                BaseArmorMultiplier = .03f * (float)(talents.Toughness),
                BonusStaminaMultiplier = .02f * (float)(/*talents.ShadowOfDeath + */talents.VeteranOfTheThirdWar),
                Expertise = (float)(talents.TundraStalker + talents.RageOfRivendare) + 2f * (float)(talents.VeteranOfTheThirdWar),
                BonusPhysicalDamageMultiplier = .02f * (float)(talents.BloodGorged + talents.RageOfRivendare) + 0.03f * talents.TundraStalker,
                BonusSpellPowerMultiplier = .02f * (float)(talents.BloodGorged + talents.RageOfRivendare) + 0.03f * talents.TundraStalker,
            };
            if (talents.UnbreakableArmor > 0)
            {
                statsTalents.AddSpecialEffect(_SE_UnbreakableArmor[character.DeathKnightTalents.GlyphofUnbreakableArmor ? 1 : 0][0]);
            }
            Stats statsTotal = new Stats();
            Stats statsGearEnchantsBuffs = new Stats();

            statsGearEnchantsBuffs = statsBaseGear + statsBuffs + statsRace + statsTalents;

            statsTotal = GetRelevantStats(statsGearEnchantsBuffs);
            statsTotal.Expertise += (float)StatConversion.GetExpertiseFromRating(statsGearEnchantsBuffs.ExpertiseRating);

            StatsSpecialEffects se = new StatsSpecialEffects(character, statsTotal, new CombatTable(character, statsTotal, calcOpts));
            int temp = 0;
            Stats statsTemp = new Stats();
            foreach (SpecialEffect effect in statsTotal.SpecialEffects())
            {
                statsTemp.AddSpecialEffect(effect);
            }
            foreach (SpecialEffect effect in statsTemp.SpecialEffects())
            {
                if (HasRelevantStats(effect.Stats))
                {
                    if (effect.Trigger == Trigger.Use)
                    {
                        float uptimeMult = 0f;
                        if (effect.Cooldown > abilityCooldown)
                        {
                            for (int i = 0; i * effect.Cooldown < calcOpts.FightLength * 60f; i++)
                            {
                                if (i * effect.Cooldown % abilityCooldown == 0)
                                {
                                    uptimeMult++;
                                }
                            }
                            uptimeMult /= calcOpts.FightLength * 60f / abilityCooldown;
                        }
                        float tempCap = StatConversion.RATING_PER_ARMORPENETRATION * (1f - statsTotal.ArmorPenetration);
                        if (effect.Stats.ArmorPenetrationRating > 0f
                            && effect.Stats.ArmorPenetrationRating + statsTotal.ArmorPenetrationRating > tempCap)
                        {
                            Stats tempStats = new Stats();
                            tempStats += effect.Stats;
                            tempStats.ArmorPenetrationRating =
                                (tempCap - statsTotal.ArmorPenetrationRating > 0f ? tempCap - statsTotal.ArmorPenetrationRating : 0f);
                            statsTotal += tempStats * uptimeMult;
                        }
                        else
                        {
                            statsTotal += effect.Stats * uptimeMult;
                        }
                    }
                    else
                    {
                        statsTotal.AddSpecialEffect(effect);
                        temp++;
                    }
                }
            }
            return (statsTotal);
        }

        #region Custom Charts
        public override ComparisonCalculationBase[] GetCustomChartData(Character character, string chartName)
        {
            List<ComparisonCalculationBase> comparisonList = new List<ComparisonCalculationBase>();
            CharacterCalculationsDPSDK baseCalc, calc;
            ComparisonCalculationBase comparison;
            float[] subPoints;
            float fMultiplier = 1f;

            baseCalc = GetCharacterCalculations(character) as CharacterCalculationsDPSDK;

            string[] statList = new string[] 
            {
                "Strength",
                "Agility",
                "Attack Power",
                "Crit Rating",
                "Hit Rating",
                "Expertise Rating",
                "Haste Rating",
                "Armor Penetration Rating",
            };

            switch (chartName)
            {

                //"Item Budget (10 point steps)","Item Budget (25 point steps)","Item Budget(50 point steps)","Item Budget (100 point steps)"
                case "Item Budget (10 point steps)":
                    {
                        fMultiplier = 1f;
                        break;
                    }
                case "Item Budget (25 point steps)":
                    {
                        fMultiplier = 2.5f;
                        break;
                    }
                case "Item Budget (50 point steps)":
                    {
                        fMultiplier = 5f;
                        break;
                    }
                case "Item Budget (100 point steps)":
                    {
                        fMultiplier = 10f;
                        break;
                    }
                case "Presences":
                    {
                        string[] listPresence = new string[] {
                            "None",
                            "Blood",
                            "Unholy",
                            "Frost",
                        };

                        // Set this to have no presence enabled.
                        Character baseCharacter = character.Clone();
                        (baseCharacter.CalculationOptions as CalculationOptionsDPSDK).CurrentPresence = CalculationOptionsDPSDK.Presence.None;
                        // replacing pre-factored base calc since this is different than the Item budget lists. 
                        baseCalc = GetCharacterCalculations(baseCharacter, null, true, false, false) as CharacterCalculationsDPSDK;

                        // Set these to have the key presence enabled.
                        for (int index = 1; index < listPresence.Length; index++)
                        {
                            (character.CalculationOptions as CalculationOptionsDPSDK).CurrentPresence = (CalculationOptionsDPSDK.Presence)index;
                            
                            calc = GetCharacterCalculations(character, null, false, true, true) as CharacterCalculationsDPSDK;

                            comparison = CreateNewComparisonCalculation();
                            comparison.Name = listPresence[index];
                            comparison.Equipped = false;
                            comparison.OverallPoints = calc.OverallPoints - baseCalc.OverallPoints;
                            subPoints = new float[calc.SubPoints.Length];
                            for (int i = 0; i < calc.SubPoints.Length; i++)
                            {
                                subPoints[i] = calc.SubPoints[i] - baseCalc.SubPoints[i];
                            }
                            comparison.SubPoints = subPoints;

                            comparisonList.Add(comparison);
                        }
                        return comparisonList.ToArray();
                    }
                default:
                    return new ComparisonCalculationBase[0];
            }

            // Item Budget list. since it's used multiple times.
            Item[] itemList = new Item[] 
            {
                new Item() { Stats = new Stats() { Strength = 10 * fMultiplier } },
                new Item() { Stats = new Stats() { Agility = 10 * fMultiplier } },
                new Item() { Stats = new Stats() { AttackPower = 20 * fMultiplier } },
                new Item() { Stats = new Stats() { CritRating = 10 * fMultiplier } },
                new Item() { Stats = new Stats() { HitRating = 10 * fMultiplier } },
                new Item() { Stats = new Stats() { ExpertiseRating = 10 * fMultiplier } },
                new Item() { Stats = new Stats() { HasteRating = 10 * fMultiplier } },
                new Item() { Stats = new Stats() { ArmorPenetrationRating = 10 * fMultiplier } },
            };
            // Do the math.
            for (int index = 0; index < itemList.Length; index++)
            {
                calc = GetCharacterCalculations(character, itemList[index]) as CharacterCalculationsDPSDK;

                comparison = CreateNewComparisonCalculation();
                comparison.Name = statList[index];
                comparison.Equipped = false;
                comparison.OverallPoints = calc.OverallPoints - baseCalc.OverallPoints;
                subPoints = new float[calc.SubPoints.Length];
                for (int i = 0; i < calc.SubPoints.Length; i++)
                {
                    subPoints[i] = calc.SubPoints[i] - baseCalc.SubPoints[i];
                }
                comparison.SubPoints = subPoints;

                comparisonList.Add(comparison);
            }
            return comparisonList.ToArray();
        }
        #endregion

        #region Relevant Stats?
        public override bool IsItemRelevant(Item item)
        {
            if (item.Slot == ItemSlot.OffHand /*  ||
                (item.Slot == ItemSlot.Ranged && item.Type != ItemType.Sigil) */)
                return false;
            return base.IsItemRelevant(item);
        }

        public override Stats GetRelevantStats(Stats stats)
        {
            Stats s = new Stats()
            {
                Health = stats.Health,
                Strength = stats.Strength,
                Agility = stats.Agility,
                Stamina = stats.Stamina,
                Armor = stats.Armor,
                BonusArmor = stats.BonusArmor,
                HighestStat = stats.HighestStat,
                Paragon = stats.Paragon,

                AttackPower = stats.AttackPower,
                HitRating = stats.HitRating,
                CritRating = stats.CritRating,
                ArmorPenetrationRating = stats.ArmorPenetrationRating,
                ArmorPenetration = stats.ArmorPenetration,
                Expertise = stats.Expertise,
                ExpertiseRating = stats.ExpertiseRating,
                HasteRating = stats.HasteRating,
                WeaponDamage = stats.WeaponDamage,
                PhysicalCrit = stats.PhysicalCrit,
                PhysicalHaste = stats.PhysicalHaste,
                PhysicalHit = stats.PhysicalHit,
                
                SpellHit = stats.SpellHit,
                SpellCrit = stats.SpellCrit,
                SpellCritOnTarget = stats.SpellCritOnTarget,
                SpellHaste = stats.SpellHaste,

                BonusStrengthMultiplier = stats.BonusStrengthMultiplier,
                BonusStaminaMultiplier = stats.BonusStaminaMultiplier,
                BonusAgilityMultiplier = stats.BonusAgilityMultiplier,
                BonusAttackPowerMultiplier = stats.BonusAttackPowerMultiplier,
                BonusCritMultiplier = stats.BonusCritMultiplier,
                BonusDamageMultiplier = stats.BonusDamageMultiplier,
                BaseArmorMultiplier = stats.BaseArmorMultiplier,
                BonusPhysicalDamageMultiplier = stats.BonusPhysicalDamageMultiplier,
                
                BonusShadowDamageMultiplier = stats.BonusShadowDamageMultiplier,
                BonusFrostDamageMultiplier = stats.BonusFrostDamageMultiplier,
                BonusDiseaseDamageMultiplier = stats.BonusDiseaseDamageMultiplier,
                BonusSpellPowerMultiplier = stats.BonusSpellPowerMultiplier,

                BonusBloodStrikeDamage = stats.BonusBloodStrikeDamage,
                BonusDeathCoilDamage = stats.BonusDeathCoilDamage,
                BonusDeathStrikeDamage = stats.BonusDeathStrikeDamage,
                BonusFrostStrikeDamage = stats.BonusFrostStrikeDamage,
                BonusHeartStrikeDamage = stats.BonusHeartStrikeDamage,
                BonusIcyTouchDamage = stats.BonusIcyTouchDamage,
                BonusObliterateDamage = stats.BonusObliterateDamage,
                BonusScourgeStrikeDamage = stats.BonusScourgeStrikeDamage,
                DiseasesCanCrit = stats.DiseasesCanCrit,

                BonusDeathCoilCrit = stats.BonusDeathCoilCrit,
                BonusDeathStrikeCrit = stats.BonusDeathStrikeCrit,
                BonusFrostStrikeCrit = stats.BonusFrostStrikeCrit,
                BonusObliterateCrit = stats.BonusObliterateCrit,
                BonusHeartStrikeMultiplier = stats.BonusHeartStrikeMultiplier,
                BonusScourgeStrikeMultiplier = stats.BonusScourgeStrikeMultiplier,
                BonusObliterateMultiplier = stats.BonusObliterateMultiplier,
                BonusPerDiseaseBloodStrikeDamage = stats.BonusPerDiseaseBloodStrikeDamage,
                BonusPerDiseaseHeartStrikeDamage = stats.BonusPerDiseaseHeartStrikeDamage,
                BonusPerDiseaseObliterateDamage = stats.BonusPerDiseaseObliterateDamage,
                BonusPerDiseaseScourgeStrikeDamage = stats.BonusPerDiseaseScourgeStrikeDamage,
                BonusPlagueStrikeCrit = stats.BonusPlagueStrikeCrit,
                BonusRPFromDeathStrike = stats.BonusRPFromDeathStrike,
                BonusRPFromObliterate = stats.BonusRPFromObliterate,
                BonusRPFromScourgeStrike = stats.BonusRPFromScourgeStrike,
                BonusRuneStrikeMultiplier = stats.BonusRuneStrikeMultiplier,
                BonusScourgeStrikeCrit = stats.BonusScourgeStrikeCrit,
                DeathbringerProc = stats.DeathbringerProc,

                BonusFrostWeaponDamage = stats.BonusFrostWeaponDamage,
                CinderglacierProc = stats.CinderglacierProc
            };

            foreach (SpecialEffect effect in stats.SpecialEffects())
            {
                if (HasRelevantStats(effect.Stats))
                {
                    if (effect.Trigger == Trigger.DamageDone ||
                        effect.Trigger == Trigger.DamageOrHealingDone ||
                        effect.Trigger == Trigger.DamageSpellCast ||
                        effect.Trigger == Trigger.DamageSpellCrit ||
                        effect.Trigger == Trigger.DamageSpellHit ||
                        effect.Trigger == Trigger.SpellCast ||
                        effect.Trigger == Trigger.SpellCrit ||
                        effect.Trigger == Trigger.SpellHit ||
                        effect.Trigger == Trigger.DoTTick ||
                        effect.Trigger == Trigger.MeleeCrit ||
                        effect.Trigger == Trigger.MeleeHit ||
                        effect.Trigger == Trigger.PhysicalCrit ||
                        effect.Trigger == Trigger.PhysicalHit ||
                        effect.Trigger == Trigger.BloodStrikeHit ||
                        effect.Trigger == Trigger.HeartStrikeHit ||
                        effect.Trigger == Trigger.BloodStrikeOrHeartStrikeHit ||
                        effect.Trigger == Trigger.ScourgeStrikeHit ||
                        effect.Trigger == Trigger.ObliterateHit ||
                        effect.Trigger == Trigger.DeathStrikeHit ||
                        effect.Trigger == Trigger.IcyTouchHit ||
                        effect.Trigger == Trigger.PlagueStrikeHit ||
                        effect.Trigger == Trigger.RuneStrikeHit ||
                        effect.Trigger == Trigger.Use)
                    {
                        s.AddSpecialEffect(effect);
                    }
                }
            }

            return s;
        }

        public override bool HasRelevantStats(Stats stats)
        {
            foreach (SpecialEffect effect in stats.SpecialEffects())
            {
                if (relevantStats(effect.Stats))
                {
                    if (effect.Trigger == Trigger.DamageDone ||
                        effect.Trigger == Trigger.DamageOrHealingDone ||
                        effect.Trigger == Trigger.DamageSpellCast ||
                        effect.Trigger == Trigger.DamageSpellCrit ||
                        effect.Trigger == Trigger.DamageSpellHit ||
                        effect.Trigger == Trigger.SpellCast ||
                        effect.Trigger == Trigger.SpellCrit ||
                        effect.Trigger == Trigger.SpellHit ||
                        effect.Trigger == Trigger.DoTTick ||
                        effect.Trigger == Trigger.MeleeCrit ||
                        effect.Trigger == Trigger.MeleeHit ||
                        effect.Trigger == Trigger.PhysicalCrit ||
                        effect.Trigger == Trigger.PhysicalHit ||
                        effect.Trigger == Trigger.BloodStrikeHit ||
                        effect.Trigger == Trigger.HeartStrikeHit ||
                        effect.Trigger == Trigger.BloodStrikeOrHeartStrikeHit ||
                        effect.Trigger == Trigger.ScourgeStrikeHit ||
                        effect.Trigger == Trigger.ObliterateHit ||
                        effect.Trigger == Trigger.DeathStrikeHit ||
                        effect.Trigger == Trigger.IcyTouchHit ||
                        effect.Trigger == Trigger.PlagueStrikeHit ||
                        effect.Trigger == Trigger.RuneStrikeHit ||
                        effect.Trigger == Trigger.Use)
                    {
                        foreach (SpecialEffect e in effect.Stats.SpecialEffects())
                        {
                            HasRelevantStats(e.Stats);
                        }
                        return relevantStats(effect.Stats);
                        
                    }
                }
            }
            return relevantStats(stats);
        }

        private bool relevantStats(Stats stats)
        {
            bool bResults = (stats.Health + stats.Strength + stats.Agility + stats.Stamina + stats.AttackPower +
                stats.HitRating + stats.CritRating + stats.ArmorPenetrationRating + stats.ArmorPenetration +
                stats.ExpertiseRating + stats.HasteRating + stats.WeaponDamage +
                stats.BonusStrengthMultiplier + stats.BonusStaminaMultiplier + stats.BonusAgilityMultiplier + stats.BonusCritMultiplier +
                stats.BonusAttackPowerMultiplier + stats.BonusPhysicalDamageMultiplier + stats.ShadowDamage +
                stats.BonusShadowDamageMultiplier + stats.SpellHaste
                + stats.BonusFrostDamageMultiplier + stats.BonusScourgeStrikeDamage + stats.PhysicalCrit + stats.PhysicalHaste
                + stats.PhysicalHit + stats.SpellCrit + stats.SpellCritOnTarget + stats.SpellHit + stats.SpellHaste + stats.BonusDiseaseDamageMultiplier
                + stats.BonusBloodStrikeDamage + stats.BonusDeathCoilDamage + stats.BonusDeathStrikeDamage + stats.BonusFrostStrikeDamage
                + stats.BonusHeartStrikeDamage + stats.BonusIcyTouchDamage + stats.BonusObliterateDamage + stats.BonusScourgeStrikeDamage
                + stats.BonusDeathCoilCrit + stats.BonusDeathStrikeCrit + stats.BonusFrostStrikeCrit + stats.BonusObliterateCrit
                + stats.BonusPerDiseaseBloodStrikeDamage + stats.BonusPerDiseaseHeartStrikeDamage + stats.BonusPerDiseaseObliterateDamage
                + stats.BonusPerDiseaseScourgeStrikeDamage + stats.BonusPlagueStrikeCrit + stats.BonusRPFromDeathStrike
                + stats.BonusRPFromObliterate + stats.BonusRPFromScourgeStrike + stats.BonusRuneStrikeMultiplier + stats.BonusScourgeStrikeCrit
                + stats.ShadowDamage + stats.ArcaneDamage + stats.CinderglacierProc + stats.BonusFrostWeaponDamage + stats.DiseasesCanCrit + 
                stats.HighestStat + stats.BonusCritMultiplier + stats.Paragon + stats.FireDamage + stats.Armor + stats.BonusArmor
                + stats.BonusDamageMultiplier + stats.BonusPhysicalDamageMultiplier + stats.BonusObliterateMultiplier +
                stats.BonusHeartStrikeMultiplier + stats.BonusScourgeStrikeMultiplier + stats.DeathbringerProc + stats.FrostDamage) != 0;

            // Filter out caster gear:
            if (bResults && 
                // Let's make sure that if we've got some stats that may be interesting
                (stats.Strength == 0 
                && stats.AttackPower == 0 
                && stats.ExpertiseRating == 0 
                && stats.HitRating == 0))
            {
                bResults = !((stats.Intellect != 0)
                    || (stats.Spirit != 0)
                    || (stats.Mp5 != 0)
                    || (stats.SpellPower != 0)
                    || (stats.Mana != 0)
                    );
            }
            return bResults;
        }
        #endregion

        private string[] _optimizableCalculationLabels = null;
        public override string[] OptimizableCalculationLabels
        {
            get
            {
                if (_optimizableCalculationLabels == null)
                    _optimizableCalculationLabels = new string[] {
                        "Health",
                        "Crit Rating",
                        "Expertise Rating",
                        "Hit Rating",
                        "Haste Rating",
                        "Target Miss %",
                        "Target Dodge %"
                    };

                return _optimizableCalculationLabels;
            }
        }

        #region Static SpecialEffects
        private static Dictionary<float, SpecialEffect[]> _SE_SpellDeflection = new Dictionary<float, SpecialEffect[]>();
        private static readonly SpecialEffect _SE_T10_4P = new SpecialEffect(Trigger.Use, new Stats() { DamageTakenMultiplier = -0.12f }, 10f, 60f);
        private static readonly SpecialEffect _SE_FC1 = new SpecialEffect(Trigger.DamageDone, new Stats() { BonusStrengthMultiplier = .15f }, 15f, 0f, -2f, 1);
        private static readonly SpecialEffect _SE_FC2 = new SpecialEffect(Trigger.DamageDone, new Stats() { HealthRestoreFromMaxHealth = .03f }, 0, 0f, -2f, 1);
        private static readonly SpecialEffect[][] _SE_VampiricBlood = new SpecialEffect[][] {
            new SpecialEffect[] { new SpecialEffect(Trigger.Use, null, 10 + 0 * 5, 60f - (false ? 0 : 10)), new SpecialEffect(Trigger.Use, null, 10 + 0 * 5, 60f - (true ? 0 : 10)),},
            new SpecialEffect[] { new SpecialEffect(Trigger.Use, null, 10 + 1 * 5, 60f - (false ? 0 : 10)), new SpecialEffect(Trigger.Use, null, 10 + 1 * 5, 60f - (true ? 0 : 10)),},
        };
        private static readonly SpecialEffect[] _SE_RuneTap = new SpecialEffect[] {
            new SpecialEffect(Trigger.Use, null, 0, 60f - 10 * 0),
            new SpecialEffect(Trigger.Use, null, 0, 60f - 10 * 1),
            new SpecialEffect(Trigger.Use, null, 0, 60f - 10 * 2),
            new SpecialEffect(Trigger.Use, null, 0, 60f - 10 * 3),
        };
        private static readonly SpecialEffect[] _SE_BloodyVengeance1 = new SpecialEffect[] {
            null,
            new SpecialEffect(Trigger.DamageSpellCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 0 }, 30, 0, 1, 3),
            new SpecialEffect(Trigger.DamageSpellCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 1 }, 30, 0, 1, 3),
            new SpecialEffect(Trigger.DamageSpellCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 2 }, 30, 0, 1, 3),
            new SpecialEffect(Trigger.DamageSpellCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 3 }, 30, 0, 1, 3),
        };
        private static readonly SpecialEffect[] _SE_BloodyVengeance2 = new SpecialEffect[] {
            null,
            new SpecialEffect(Trigger.MeleeCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 0 }, 30, 0, 1, 3),
            new SpecialEffect(Trigger.MeleeCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 1 }, 30, 0, 1, 3),
            new SpecialEffect(Trigger.MeleeCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 2 }, 30, 0, 1, 3),
            new SpecialEffect(Trigger.MeleeCrit, new Stats() { BonusPhysicalDamageMultiplier = .01f * 3 }, 30, 0, 1, 3),
        };
        private static Dictionary<float, SpecialEffect[]> _SE_Bloodworms = new Dictionary<float, SpecialEffect[]>();
        private static readonly SpecialEffect[] _SE_WillOfTheNecropolis = new SpecialEffect[] {
            null,
            new SpecialEffect(Trigger.DamageTaken, new Stats() { DamageTakenMultiplier = -(0.05f * 1) }, 0, 0, 0.35f),
            new SpecialEffect(Trigger.DamageTaken, new Stats() { DamageTakenMultiplier = -(0.05f * 2) }, 0, 0, 0.35f),
            new SpecialEffect(Trigger.DamageTaken, new Stats() { DamageTakenMultiplier = -(0.05f * 3) }, 0, 0, 0.35f),
        };
        private static readonly SpecialEffect[] _SE_IcyTalons = new SpecialEffect[] {
            null,
            new SpecialEffect(Trigger.FrostFeverHit, new Stats() { PhysicalHaste = (0.04f * 1) }, 20f, 0f),
            new SpecialEffect(Trigger.FrostFeverHit, new Stats() { PhysicalHaste = (0.04f * 2) }, 20f, 0f),
            new SpecialEffect(Trigger.FrostFeverHit, new Stats() { PhysicalHaste = (0.04f * 3) }, 20f, 0f),
            new SpecialEffect(Trigger.FrostFeverHit, new Stats() { PhysicalHaste = (0.04f * 4) }, 20f, 0f),
            new SpecialEffect(Trigger.FrostFeverHit, new Stats() { PhysicalHaste = (0.04f * 5) }, 20f, 0f),
        };
        private static readonly SpecialEffect[][] _SE_UnbreakableArmor = new SpecialEffect[][] {
            new SpecialEffect[] {
                    new SpecialEffect(Trigger.Use, new Stats() { BonusStrengthMultiplier = 0.20f, BaseArmorMultiplier = .25f + (false ? .20f : 0f), BonusArmorMultiplier = .25f + (false ? .20f : 0f) }, 20f, 60f - 0 * 10f),
                    new SpecialEffect(Trigger.Use, new Stats() { BonusStrengthMultiplier = 0.20f, BaseArmorMultiplier = .25f + (true  ? .20f : 0f), BonusArmorMultiplier = .25f + (true  ? .20f : 0f) }, 20f, 60f - 0 * 10f),
            },
            new SpecialEffect[] {
                    new SpecialEffect(Trigger.Use, new Stats() { BonusStrengthMultiplier = 0.20f, BaseArmorMultiplier = .25f + (false ? .20f : 0f), BonusArmorMultiplier = .25f + (false ? .20f : 0f) }, 20f, 60f - 1 * 10f),
                    new SpecialEffect(Trigger.Use, new Stats() { BonusStrengthMultiplier = 0.20f, BaseArmorMultiplier = .25f + (true  ? .20f : 0f), BonusArmorMultiplier = .25f + (true  ? .20f : 0f) }, 20f, 60f - 1 * 10f),
            },
        };
        private static readonly SpecialEffect[] _SE_Acclimation = new SpecialEffect[] {
            null,
            new SpecialEffect(Trigger.DamageTakenMagical, new Stats() { FireResistance = 50f, FrostResistance = 50f, ArcaneResistance = 50f, ShadowResistance = 50f, NatureResistance = 50f, }, 18f, 0f, (0.10f * 1), 3),
            new SpecialEffect(Trigger.DamageTakenMagical, new Stats() { FireResistance = 50f, FrostResistance = 50f, ArcaneResistance = 50f, ShadowResistance = 50f, NatureResistance = 50f, }, 18f, 0f, (0.10f * 2), 3),
            new SpecialEffect(Trigger.DamageTakenMagical, new Stats() { FireResistance = 50f, FrostResistance = 50f, ArcaneResistance = 50f, ShadowResistance = 50f, NatureResistance = 50f, }, 18f, 0f, (0.10f * 3), 3),
        };
        private static readonly SpecialEffect _SE_AntiMagicZone = new SpecialEffect(Trigger.Use, new Stats() { SpellDamageTakenMultiplier = -0.75f }, 10f, 2f * 60f);
        #endregion
    }
}
