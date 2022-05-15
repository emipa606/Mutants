using Verse;

namespace Ent;

public class HealingFactorProperties : DefModExtension
{
    public int healOldWoundStage;

    public float healWoundFactorMax = 5f;

    public float healWoundFactorMin = 0.01f;

    public int regrowBodypartStage;
    public int tendWoundStage;

    public int ticksBetweenMajorHeal = 2000;

    public int ticksBetweenMinorHeal = 20;
}