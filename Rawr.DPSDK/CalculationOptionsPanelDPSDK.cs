﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rawr.DPSDK
{
    public partial class CalculationOptionsPanelDPSDK : CalculationOptionsPanelBase
    {

        public CalculationOptionsPanelDPSDK()
        {
            InitializeComponent();
        }
        protected override void LoadCalculationOptions()
        {
			if (Character.CalculationOptions == null)
				Character.CalculationOptions = new CalculationOptionsDPSDK();

			CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;

            if (calcOpts.rotation == null)
                calcOpts.rotation = new Rotation();
           

            cbTargetLevel.SelectedItem = calcOpts.TargetLevel.ToString();
            nudFightLength.Value = (int)(calcOpts.FightLength * 60);
            cbDisplayCalcs.Checked = calcOpts.GetRefreshForDisplayCalcs;
            cbGhoul.Checked = calcOpts.Ghoul;
            cbRefCalcs.Checked = calcOpts.GetRefreshForReferenceCalcs;
            cbSignificantChange.Checked = calcOpts.GetRefreshForSignificantChange;

            nudTargetArmor.Value = calcOpts.BossArmor;
        }

        private void cbTargetLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.TargetLevel = int.Parse(cbTargetLevel.SelectedItem.ToString());
            Character.OnCalculationsInvalidated();
        }

        private void rbUnholyPresence_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUnholyPresence.Checked)
            {
                CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
                calcOpts.CurrentPresence = CalculationOptionsDPSDK.Presence.Unholy;
                Character.OnCalculationsInvalidated();
            }
        }
        private void rbBloodPresence_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBloodPresence.Checked)
            {
                CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
                calcOpts.CurrentPresence = CalculationOptionsDPSDK.Presence.Blood;
                Character.OnCalculationsInvalidated();
            }
        }

        // TODO: Update this to the default graphs
        #region StatsGraph
        private Stats[] BuildStatsList()
        {
            List<Stats> statsList = new List<Stats>();
            statsList.Add(new Stats() { Strength = 1f });
//            statsList.Add(new Stats() { Stamina = 1f });
            statsList.Add(new Stats() { Agility = 1f });
            statsList.Add(new Stats() { AttackPower = 2f });
            statsList.Add(new Stats() { CritRating = 1f });
            statsList.Add(new Stats() { HitRating = 1f });
            statsList.Add(new Stats() { ExpertiseRating = 1f });
            statsList.Add(new Stats() { HasteRating = 1f });
            statsList.Add(new Stats() { ArmorPenetrationRating = 1f });
//            statsList.Add(new Stats() { DefenseRating = 1f });
//            statsList.Add(new Stats() { DodgeRating = 1f });
//            statsList.Add(new Stats() { ParryRating = 1f });
            return statsList.ToArray();
        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            Stats[] statsList = BuildStatsList();
            Rawr.Base.Graph graph = new Rawr.Base.Graph();
            string explanatoryText = "This graph shows how adding or subtracting\nmultiples of a stat affects your Overall Score.\n\nAt the Zero position is your current Overall.\n" +
                         "To the right of the zero vertical is adding stats.\nTo the left of the zero vertical is subtracting stats.\n" +
                         "The vertical axis shows the amount of Overall points added or lost";
            graph.SetupStatsGraph(Character, statsList, 1000, explanatoryText, null);
            graph.Show();
        }
        #endregion
        
        /*
        private void btnGraph_Click(object sender, EventArgs e)
        {
            CalculationsDPSDK DKCalc = new CalculationsDPSDK();
            CharacterCalculationsDPSDK baseCalc = DKCalc.GetCharacterCalculations(Character) as CharacterCalculationsDPSDK;
            Bitmap _prerenderedGraph = global::Rawr.DPSDK.Properties.Resources.GraphBase;
            Graphics g = Graphics.FromImage(_prerenderedGraph);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            float graphHeight = 700f, graphStart = 100f;
            Color[] colors = new Color[] {
                Color.FromArgb(127,202,180,96), // Strength
                Color.FromArgb(127,101,225,240), // Agility
                Color.FromArgb(127,0,4,3), // Attack Power
                Color.FromArgb(127,123,238,199), // Crit Rating
                Color.FromArgb(127,45,112,63), // Hit Rating
                Color.FromArgb(127,121,72,210), //Expertise Rating
                Color.FromArgb(127,217,100,54), // Haste Rating
                Color.FromArgb(127,210,72,195), // Armor Penetration
                // Color.FromArgb(127,206,189,191), // Spell Damage
            };
            Stats[] statsList = new Stats[] {
                new Stats() { Strength = 10 },
                new Stats() { Agility = 10 },
                new Stats() { AttackPower = 20 },
                new Stats() { CritRating = 10 },
                new Stats() { HitRating = 10 },
                new Stats() { ExpertiseRating = 10 },
                new Stats() { HasteRating = 10 },
                new Stats() { ArmorPenetrationRating = 10 }
            };
            #region Graph Ticks
            float graphWidth = 500f;// this.Width - 150f;
            float graphEnd = graphStart + graphWidth;
            //float graphStartY = 16f;
            float maxScale = 100f;
            float[] ticks = new float[] {(float)Math.Round(graphStart + graphWidth * 0.5f),
							(float)Math.Round(graphStart + graphWidth * 0.75f),
							(float)Math.Round(graphStart + graphWidth * 0.25f),
							(float)Math.Round(graphStart + graphWidth * 0.125f),
							(float)Math.Round(graphStart + graphWidth * 0.375f),
							(float)Math.Round(graphStart + graphWidth * 0.625f),
							(float)Math.Round(graphStart + graphWidth * 0.875f)};
            Pen black200 = new Pen(Color.FromArgb(200, 0, 0, 0));
            Pen black150 = new Pen(Color.FromArgb(150, 0, 0, 0));
            Pen black75 = new Pen(Color.FromArgb(75, 0, 0, 0));
            Pen black50 = new Pen(Color.FromArgb(50, 0, 0, 0));
            Pen black25 = new Pen(Color.FromArgb(25, 0, 0, 0));
            StringFormat formatTick = new StringFormat();
            formatTick.LineAlignment = StringAlignment.Far;
            formatTick.Alignment = StringAlignment.Center;
            Brush black200brush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
            Brush black150brush = new SolidBrush(Color.FromArgb(150, 0, 0, 0));
            Brush black75brush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));
            Brush black50brush = new SolidBrush(Color.FromArgb(50, 0, 0, 0));
            Brush black25brush = new SolidBrush(Color.FromArgb(25, 0, 0, 0));

            g.DrawLine(black200, graphStart - 4, 20, graphEnd + 4, 20);
            g.DrawLine(black200, graphStart, 16, graphStart, _prerenderedGraph.Height - 16);
            g.DrawLine(black200, graphEnd, 16, graphEnd, 19);
            g.DrawLine(black200, ticks[0], 16, ticks[0], 19);
            g.DrawLine(black150, ticks[1], 16, ticks[1], 19);
            g.DrawLine(black150, ticks[2], 16, ticks[2], 19);
            g.DrawLine(black75, ticks[3], 16, ticks[3], 19);
            g.DrawLine(black75, ticks[4], 16, ticks[4], 19);
            g.DrawLine(black75, ticks[5], 16, ticks[5], 19);
            g.DrawLine(black75, ticks[6], 16, ticks[6], 19);
            g.DrawLine(black75, graphEnd, 21, graphEnd, _prerenderedGraph.Height - 4);
            g.DrawLine(black75, ticks[0], 21, ticks[0], _prerenderedGraph.Height - 4);
            g.DrawLine(black50, ticks[1], 21, ticks[1], _prerenderedGraph.Height - 4);
            g.DrawLine(black50, ticks[2], 21, ticks[2], _prerenderedGraph.Height - 4);
            g.DrawLine(black25, ticks[3], 21, ticks[3], _prerenderedGraph.Height - 4);
            g.DrawLine(black25, ticks[4], 21, ticks[4], _prerenderedGraph.Height - 4);
            g.DrawLine(black25, ticks[5], 21, ticks[5], _prerenderedGraph.Height - 4);
            g.DrawLine(black25, ticks[6], 21, ticks[6], _prerenderedGraph.Height - 4);
            g.DrawLine(black200, graphStart - 4, _prerenderedGraph.Height - 20, graphEnd + 4, _prerenderedGraph.Height - 20);

            Font tickFont = new Font("Calibri", 11);
            g.DrawString((0f).ToString(), tickFont, black200brush, graphStart, 16, formatTick);
            g.DrawString((maxScale).ToString(), tickFont, black200brush, graphEnd, 16, formatTick);
            g.DrawString((maxScale * 0.5f).ToString(), tickFont, black200brush, ticks[0], 16, formatTick);
            g.DrawString((maxScale * 0.75f).ToString(), tickFont, black150brush, ticks[1], 16, formatTick);
            g.DrawString((maxScale * 0.25f).ToString(), tickFont, black150brush, ticks[2], 16, formatTick);
            g.DrawString((maxScale * 0.125f).ToString(), tickFont, black75brush, ticks[3], 16, formatTick);
            g.DrawString((maxScale * 0.375f).ToString(), tickFont, black75brush, ticks[4], 16, formatTick);
            g.DrawString((maxScale * 0.625f).ToString(), tickFont, black75brush, ticks[5], 16, formatTick);
            g.DrawString((maxScale * 0.875f).ToString(), tickFont, black75brush, ticks[6], 16, formatTick);

            g.DrawString((0f).ToString(), tickFont, black200brush, graphStart, _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale).ToString(), tickFont, black200brush, graphEnd, _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.5f).ToString(), tickFont, black200brush, ticks[0], _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.75f).ToString(), tickFont, black150brush, ticks[1], _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.25f).ToString(), tickFont, black150brush, ticks[2], _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.125f).ToString(), tickFont, black75brush, ticks[3], _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.375f).ToString(), tickFont, black75brush, ticks[4], _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.625f).ToString(), tickFont, black75brush, ticks[5], _prerenderedGraph.Height - 16, formatTick);
            g.DrawString((maxScale * 0.875f).ToString(), tickFont, black75brush, ticks[6], _prerenderedGraph.Height - 16, formatTick);
            #endregion

            Graph graph = new Graph(_prerenderedGraph);
            graph.Show();
            for (int index = 0; index < statsList.Length; index++)
            {
                Stats newStats = new Stats();
                Point[] points = new Point[100];
                for (int count = 0; count < 100; count++)
                {
                    newStats = newStats + statsList[index];

                    CharacterCalculationsDPSDK currentCalc = DKCalc.GetCharacterCalculations(Character, new Item() { Stats = newStats }) as CharacterCalculationsDPSDK;
                    float overallPoints = currentCalc.DPSPoints - baseCalc.DPSPoints;

                    if ((graphHeight - overallPoints) > 16)
                        points[count] = new Point(Convert.ToInt32(graphStart + count * 5), (Convert.ToInt32(graphHeight - overallPoints)));
                    else
                        points[count] = points[count-1];

                }
                Brush statBrush = new SolidBrush(colors[index]);
                g.DrawLines(new Pen(statBrush, 3), points);
            }

            graph.Invalidate();
            graph.Update();
        }*/

        private void nudTargetArmor_ValueChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.BossArmor = (int)nudTargetArmor.Value;
            Character.OnCalculationsInvalidated();
        }
        private void nudFightLength_ValueChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.FightLength = (float)(nudFightLength.Value) / 60f;
            Character.OnCalculationsInvalidated();
        }

        private void cbGhoul_CheckedChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.Ghoul = cbGhoul.Checked;
            Character.OnCalculationsInvalidated();
        }
        
        private void btnRotation_Click(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            RotationViewer RV = new RotationViewer(calcOpts, Character);
            RV.ShowDialog();            
            Character.OnCalculationsInvalidated();
        }

        private void BloodwormUptime_Scroll(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.BloodwormsUptime = BloodwormUptime.Value / 100f;
            lbBloodwormTime.Text = (BloodwormUptime.Value / 100f).ToString("P");
            Character.OnCalculationsInvalidated();
        }

        private void GhoulUptime_Scroll(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.GhoulUptime = GhoulUptime.Value / 100f;
            lbGhoulTime.Text = (GhoulUptime.Value / 100f).ToString("P");
            Character.OnCalculationsInvalidated();
        }

        private void KMProcUsage_Scroll(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.KMProcUsage = KMProcUsage.Value / 100f;
            lbKMProcUsage.Text = (KMProcUsage.Value / 100f).ToString("P");
            Character.OnCalculationsInvalidated();
        }

        private void cbDisplayCalcs_CheckedChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.GetRefreshForDisplayCalcs = cbDisplayCalcs.Checked;
            Character.OnCalculationsInvalidated();
        }

        private void cbRefCalcs_CheckedChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.GetRefreshForReferenceCalcs = cbRefCalcs.Checked;
            Character.OnCalculationsInvalidated();
        }

        private void cbSignificantChange_CheckedChanged(object sender, EventArgs e)
        {
            CalculationOptionsDPSDK calcOpts = Character.CalculationOptions as CalculationOptionsDPSDK;
            calcOpts.GetRefreshForSignificantChange = cbSignificantChange.Checked;
            Character.OnCalculationsInvalidated();
        }

       



    }
}