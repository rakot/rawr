﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rawr.Moonkin
{
    public class MoonkinSimulator
    {
        public long SimulationCount { get; set; }
        public int FightLength { get; set; }

        public double HasteLevel { get; set; }

        public bool HasGlyphOfStarfire { get; set; }
        public bool Has4T12 { get; set; }
        public bool Has4T13 { get; set; }

        private double ShootingStarsChance = 0.04;
        private double EuphoriaChance = 0.24;
        private double NaturesGraceHaste = 0.15;

        private double BaseStarfireCastTime = 2.7;
        private double BaseWrathCastTime = 2.0;
        private double BaseStarsurgeCastTime = 2.0;
        private double BaseGlobalCooldown = 1.5;
        private double BaseTickRate = 2.0;
        private double BaseMFDuration = 18.0;
        private double BaseISDuration = 18.0;

        private double CurrentStarfireCastTime;
        private double CurrentWrathCastTime;
        private double CurrentStarsurgeCastTime;
        private double CurrentGlobalCooldown;
        private double CurrentTickRate;
        private double CurrentMFDuration;
        private double CurrentISDuration;

        private double NGStarfireCastTime;
        private double NGWrathCastTime;
        private double NGStarsurgeCastTime;
        private double NGGlobalCooldown;
        private double NGTickRate;
        private double NGMFDuration;
        private double NGISDuration;

        private double averageRotationLength;
        private double averageNGUptime;
        private double percentMoonfiresExtended;

        private Random rng;

        public MoonkinSimulator()
        {
            SimulationCount = 25000;
            FightLength = 450;
        }

        public double[] GenerateCycle()
        {
            rng = new Random((int)DateTime.Now.Ticks);
            long[] castCounts = new long[12];
            long[] iterationCounts = new long[12];
            averageNGUptime = averageRotationLength = percentMoonfiresExtended = 0;

            CurrentStarfireCastTime = Math.Max(1, BaseStarfireCastTime / (1 + HasteLevel));
            CurrentStarsurgeCastTime = Math.Max(1, BaseStarsurgeCastTime / (1 + HasteLevel));
            CurrentWrathCastTime = Math.Max(1, BaseWrathCastTime / (1 + HasteLevel));
            CurrentGlobalCooldown = Math.Max(1, BaseGlobalCooldown / (1 + HasteLevel));
            CurrentTickRate = Math.Round(BaseTickRate / (1 + HasteLevel), 3);
            CurrentMFDuration = Math.Round(BaseMFDuration / CurrentTickRate) * CurrentTickRate;
            CurrentISDuration = Math.Round(BaseISDuration / CurrentTickRate) * CurrentTickRate;

            NGStarfireCastTime = Math.Max(1, CurrentStarfireCastTime / (1 + NaturesGraceHaste));
            NGStarsurgeCastTime = Math.Max(1, CurrentStarsurgeCastTime / (1 + NaturesGraceHaste));
            NGWrathCastTime = Math.Max(1, CurrentWrathCastTime / (1 + NaturesGraceHaste));
            NGGlobalCooldown = Math.Max(1, CurrentGlobalCooldown / (1 + NaturesGraceHaste));
            NGTickRate = Math.Round(CurrentTickRate / (1 + NaturesGraceHaste), 3);
            NGMFDuration = Math.Round(BaseMFDuration / NGTickRate) * NGTickRate;
            NGISDuration = Math.Round(BaseISDuration / NGTickRate) * NGTickRate;

            for (long iteration = 0; iteration < SimulationCount; ++iteration)
            {
                double ngTime, rotationLength, mfExtended;
                GetIterationValues(iterationCounts, out ngTime, out rotationLength, out mfExtended);
                for (int idx = 0; idx < 12; ++idx)
                {
                    castCounts[idx] += iterationCounts[idx];
                }
                averageNGUptime += ngTime;
                averageRotationLength += rotationLength;
                percentMoonfiresExtended += mfExtended;
            }

            averageNGUptime /= SimulationCount;
            averageRotationLength /= SimulationCount;
            percentMoonfiresExtended /= SimulationCount;

            long castCount = castCounts.Sum();

            double[] retval = new double[12];

            for (int idx = 0; idx < 12; ++idx)
            {
                retval[idx] = castCounts[idx] / (double)castCount;
            }

            return retval;
        }

        public double GetRotationLength()
        {
            return averageRotationLength;
        }

        public double GetNGUptime()
        {
            return averageNGUptime;
        }

        public double GetPercentMoonfiresExtended()
        {
            return percentMoonfiresExtended;
        }

        private void GetIterationValues(long[] castCounts, out double ngUptime, out double rotationLength, out double percentMoonfiresExtended)
        {
            ngUptime = 0;
            double currentDuration = 0;
            double completeRotationTime = 0;
            double ngTimeSpent = 0;
            int rotationCount = 0;
            int eclipseEnergy = 100;
            int eclipseDirection = -1;
            int wrathCounter = 0;
            int mfExtendedCount = 0;
            int numMoonfiresExtended = 0;
            int numMoonfiresCast = 0;

            double currentMFTimer = 0;
            double currentISTimer = 0;
            double currentNGTimer = 0;
            double currentSSCooldown = 0;
            double currentShSProc = 0;
            double currentNGCooldown = 0;
            bool currentMFHasNG = false;
            bool currentISHasNG = false;

            do
            {
                bool inEclipse = (eclipseEnergy > 0 && eclipseEnergy <= 100 && eclipseDirection == -1) ||
                    (eclipseEnergy < 0 && eclipseEnergy >= -100 && eclipseDirection == 1);

                double currentActionTime = 0;

                double mfTicksSoFar = Math.Floor(currentMFTimer / (currentMFHasNG ? NGTickRate : CurrentTickRate));
                double mfTickTime = mfTicksSoFar * (currentMFHasNG ? NGTickRate : CurrentTickRate);
                double timeSinceLastMFTick = (currentMFHasNG ? NGTickRate : CurrentTickRate) - (currentMFTimer - mfTickTime);

                double isTicksSoFar = Math.Floor(currentISTimer / (currentISHasNG ? NGTickRate : CurrentTickRate));
                double isTickTime = isTicksSoFar * (currentISHasNG ? NGTickRate : CurrentTickRate);
                double timeSinceLastISTick = (currentMFHasNG ? NGTickRate : CurrentTickRate) - (currentISTimer - isTickTime);

                // If we're eligible for Nature's Grace and both dots are up for refresh, cast Insect Swarm first
                /*if (currentNGCooldown == 0 && currentISTimer <= CurrentTickRate && currentMFTimer <= CurrentTickRate)
                {
                    double isCastTime = (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown);
                    if (currentNGTimer > 0) ngTimeSpent += isCastTime;
                    currentMFTimer = Math.Max(0, currentMFTimer - isCastTime);
                    currentISTimer = (currentNGTimer > 0 ? NGISDuration : CurrentISDuration) - isCastTime;
                    currentSSCooldown = Math.Max(0, currentSSCooldown - isCastTime);
                    currentISHasNG = currentNGTimer > 0;
                    currentNGTimer = currentNGCooldown == 0 ? 15 - (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown) : Math.Max(0, currentNGTimer - NGGlobalCooldown);
                    currentNGCooldown = 60 - isCastTime;
                    currentActionTime = isCastTime;
                    if (inEclipse && eclipseDirection == -1) ++castCounts[11];
                    else ++castCounts[9];
                }*/
                // Second priority: Refresh Insect Swarm
                if (currentISTimer < (currentISHasNG ? NGTickRate : CurrentTickRate))
                {
                    double isCastTime = (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown);
                    if (currentNGTimer > 0) ngTimeSpent += isCastTime;
                    currentMFTimer = Math.Max(0, currentMFTimer - isCastTime);
                    currentISTimer = (currentNGTimer > 0 ? NGISDuration : CurrentISDuration) - isCastTime;
                    currentSSCooldown = Math.Max(0, currentSSCooldown - isCastTime);
                    currentISHasNG = currentNGTimer > 0;
                    currentNGTimer = currentNGCooldown == 0 ? 15 - (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown) : Math.Max(0, currentNGTimer - NGGlobalCooldown);
                    if (currentNGCooldown == 0) currentNGCooldown = 60 - isCastTime;
                    currentActionTime = isCastTime;
                    if (inEclipse && eclipseDirection == -1) ++castCounts[11];
                    else ++castCounts[9];
                }
                // Top priority: Refresh Moonfire
                else if (currentMFTimer < (currentMFHasNG ? NGTickRate : CurrentTickRate))
                {
                    ++numMoonfiresCast;
                    double mfCastTime = (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown);
                    if (currentNGTimer > 0) ngTimeSpent += mfCastTime;
                    mfExtendedCount = 0;
                    currentISTimer = Math.Max(0, currentISTimer - mfCastTime);
                    currentMFTimer = (currentNGTimer > 0 ? NGMFDuration : CurrentMFDuration) - mfCastTime;
                    currentSSCooldown = Math.Max(0, currentSSCooldown - mfCastTime);
                    currentMFHasNG = currentNGTimer > 0;
                    currentNGTimer = currentNGCooldown == 0 ? 15 - (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown) : Math.Max(0, currentNGTimer - NGGlobalCooldown);
                    if (currentNGCooldown == 0) currentNGCooldown = 60 - mfCastTime;
                    currentActionTime = mfCastTime;
                    if (inEclipse) ++castCounts[10];
                    else ++castCounts[8];
                }
                // Third priority: Shooting Stars proc
                else if (currentShSProc > 0)
                {
                    double ssCastTime = (currentNGTimer > 0 ? NGGlobalCooldown : CurrentGlobalCooldown);
                    if (currentNGTimer > 0) ngTimeSpent += ssCastTime;
                    currentMFTimer = Math.Max(0, currentMFTimer - ssCastTime);
                    currentISTimer = Math.Max(0, currentISTimer - ssCastTime);
                    currentSSCooldown = 15 - ssCastTime;
                    currentNGTimer = Math.Max(0, currentNGTimer - ssCastTime);
                    currentNGCooldown = Math.Max(0, currentNGCooldown - ssCastTime);
                    currentActionTime = ssCastTime;
                    currentShSProc = 0;
                    eclipseEnergy = Math.Min(100, Math.Max(-100, eclipseEnergy + eclipseDirection * ((!inEclipse && Has4T13) ? 30 : 15)));
                    if (eclipseEnergy == -100 || eclipseEnergy == 100)
                    {
                        currentNGCooldown = 0;
                        eclipseDirection = -eclipseDirection;
                    }
                    if (eclipseEnergy == 100)
                    {
                        ++rotationCount;
                        ngUptime = ngTimeSpent;
                        completeRotationTime = currentDuration + ssCastTime;
                    }
                    if (inEclipse) ++castCounts[7];
                    else ++castCounts[3];
                }
                // Fourth priority: Starsurge
                else if (currentSSCooldown == 0)
                {
                    double ssCastTime = (currentNGTimer > 0 ? NGStarsurgeCastTime : CurrentStarsurgeCastTime);
                    if (currentNGTimer > 0) ngTimeSpent += ssCastTime;
                    currentMFTimer = Math.Max(0, currentMFTimer - ssCastTime);
                    currentISTimer = Math.Max(0, currentISTimer - ssCastTime);
                    currentSSCooldown = 15 - ssCastTime;
                    currentNGTimer = Math.Max(0, currentNGTimer - ssCastTime);
                    currentNGCooldown = Math.Max(0, currentNGCooldown - ssCastTime);
                    currentActionTime = ssCastTime;
                    eclipseEnergy = Math.Min(100, Math.Max(-100, eclipseEnergy + eclipseDirection * ((!inEclipse && Has4T13) ? 30 : 15)));
                    if (eclipseEnergy == -100 || eclipseEnergy == 100)
                    {
                        currentNGCooldown = 0;
                        eclipseDirection = -eclipseDirection;
                    }
                    if (eclipseEnergy == 100)
                    {
                        ++rotationCount;
                        ngUptime = ngTimeSpent;
                        completeRotationTime = currentDuration + ssCastTime;
                    }
                    if (inEclipse) ++castCounts[6];
                    else ++castCounts[2];
                }
                // Fifth priority: Current nuke
                // Headed in the Solar direction, cast Starfire
                else if (eclipseDirection == 1)
                {
                    double sfCastTime = (currentNGTimer > 0 ? NGStarfireCastTime : CurrentStarfireCastTime);
                    if (currentNGTimer > 0) ngTimeSpent += sfCastTime;
                    double mfExtension = 0;
                    if (HasGlyphOfStarfire && currentMFTimer > 0)
                    {
                        if (mfExtendedCount == 0)
                            ++numMoonfiresExtended;

                        if (mfExtendedCount < 3)
                        {
                            mfExtension = 2 * (currentNGTimer > 0 ? NGTickRate : CurrentTickRate);
                            ++mfExtendedCount;
                        }
                        // Any MF refreshed with GoSF loses NG when a non-NG'd SF is cast, regardless of if it extends
                        currentMFHasNG = currentNGTimer > 0;
                    }
                    currentMFTimer = Math.Max(0, currentMFTimer - sfCastTime + mfExtension);
                    currentISTimer = Math.Max(0, currentISTimer - sfCastTime);
                    currentSSCooldown = Math.Max(0, currentSSCooldown - sfCastTime);
                    currentNGTimer = Math.Max(0, currentNGTimer - sfCastTime);
                    currentNGCooldown = Math.Max(0, currentNGCooldown - sfCastTime);
                    currentActionTime = sfCastTime;
                    int sfEclipseEnergy = 20;
                    if (Has4T12 && !inEclipse)
                        sfEclipseEnergy = 25;
                    if (!inEclipse && eclipseEnergy <= 35 && rng.NextDouble() <= EuphoriaChance)
                    {
                        sfEclipseEnergy *= 2;
                    }
                    eclipseEnergy = Math.Min(100, eclipseEnergy + sfEclipseEnergy);
                    if (eclipseEnergy == 100)
                    {
                        currentNGCooldown = 0;
                        eclipseDirection = -1;
                        ++rotationCount;
                        ngUptime = ngTimeSpent;
                        completeRotationTime = currentDuration + sfCastTime;
                    }
                    if (inEclipse) ++castCounts[4];
                    else ++castCounts[0];
                }
                // Headed in the Lunar direction, cast Wrath
                else
                {
                    double wrCastTime = (currentNGTimer > 0 ? NGWrathCastTime : CurrentWrathCastTime);
                    if (currentNGTimer > 0) ngTimeSpent += wrCastTime;
                    currentMFTimer = Math.Max(0, currentMFTimer - wrCastTime);
                    currentISTimer = Math.Max(0, currentISTimer - wrCastTime);
                    currentSSCooldown = Math.Max(0, currentSSCooldown - wrCastTime);
                    currentNGTimer = Math.Max(0, currentNGTimer - wrCastTime);
                    currentNGCooldown = Math.Max(0, currentNGCooldown - wrCastTime);
                    currentActionTime = wrCastTime;
                    int wrEclipseEnergy = GetWrathEnergy(Has4T12 && !inEclipse, wrathCounter);
                    wrathCounter = (wrathCounter + 1) % 3;
                    if (!inEclipse && eclipseEnergy >= -35 && rng.NextDouble() <= EuphoriaChance)
                    {
                        wrEclipseEnergy += GetWrathEnergy(Has4T12, wrathCounter);
                        wrathCounter = (wrathCounter + 1) % 3;
                    }
                    eclipseEnergy = Math.Max(-100, eclipseEnergy - wrEclipseEnergy);
                    if (eclipseEnergy == -100)
                    {
                        currentNGCooldown = 0;
                        eclipseDirection = 1;
                    }
                    if (inEclipse) ++castCounts[5];
                    else ++castCounts[1];
                }
                // Determine the chance to proc Shooting Stars over the last action time
                double mfTicks = (timeSinceLastMFTick + currentActionTime) / (currentMFHasNG ? NGTickRate : CurrentTickRate);
                double isTicks = (timeSinceLastISTick + currentActionTime) / (currentISHasNG ? NGTickRate : CurrentTickRate);
                double dotTicks = (currentMFTimer > 0 ? mfTicks : 0) + (currentISTimer > 0 ? isTicks : 0);
                if (rng.NextDouble() <= 1 - Math.Pow(1 - ShootingStarsChance, dotTicks))
                {
                    currentShSProc = 1;
                }
                // Increment the timer to the next timestep
                currentDuration += currentActionTime;
            } while (currentDuration <= FightLength - CurrentGlobalCooldown);

            rotationLength = completeRotationTime / rotationCount;
            ngUptime /= completeRotationTime;
            percentMoonfiresExtended = numMoonfiresExtended / (double)numMoonfiresCast;
        }
        private int GetWrathEnergy(bool t12BonusActive, int currentWrathCounter)
        {
            if (t12BonusActive)
                return currentWrathCounter == 2 ? 16 : 17;
            else
                return currentWrathCounter == 2 ? 14 : 13;
        }
    }
}