using Verse;

namespace Ent;

public class HealingFactorProperties : DefModExtension
{
    public readonly float healWoundFactorMax = 5f;

    public readonly float healWoundFactorMin = 0.01f;

    public readonly int ticksBetweenMajorHeal = 2000;

    public readonly int ticksBetweenMinorHeal = 20;
    public int healOldWoundStage;

    public int regrowBodypartStage;
    public int tendWoundStage;
}