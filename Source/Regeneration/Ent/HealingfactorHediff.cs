using System.Linq;
using Ent.Logic;
using RimWorld;
using Verse;

namespace Ent;

public class HealingfactorHediff : HediffWithComps
{
    protected HealingFactorProperties healingFactorProps;

    public int ticksUntilNextMajorHeal;
    public int ticksUntilNextMinorHeal;

    public HealingFactorProperties HealingFactorProps
    {
        get
        {
            if (healingFactorProps != null)
            {
                return healingFactorProps;
            }

            healingFactorProps = def.GetModExtension<HealingFactorProperties>();
            if (healingFactorProps == null)
            {
                healingFactorProps = new HealingFactorProperties();
            }

            return healingFactorProps;
        }
    }

    public override void PostMake()
    {
        base.PostMake();
        SetNextMajorTick();
        SetNextMinorTick();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ticksUntilNextMinorHeal, "ticksUntilNextMinorHeal");
        Scribe_Values.Look(ref ticksUntilNextMajorHeal, "ticksUntilNextMajorHeal");
    }

    public override void Tick()
    {
        base.Tick();
        if (Current.Game.tickManager.TicksGame >= ticksUntilNextMinorHeal)
        {
            TryHealWounds();
            SetNextMinorTick();
        }

        if (Current.Game.tickManager.TicksGame < ticksUntilNextMajorHeal)
        {
            return;
        }

        if (CurStageIndex >= HealingFactorProps.healOldWoundStage)
        {
            TryHealRandomOldWound();
        }

        if (CurStageIndex >= HealingFactorProps.tendWoundStage)
        {
            TrySealWounds();
        }

        if (CurStageIndex >= HealingFactorProps.regrowBodypartStage)
        {
            TryRegrowBodyparts();
        }

        SetNextMajorTick();
    }

    public void TryHealWounds()
    {
        var enumerable = pawn.health.hediffSet.hediffs.Where(hd => hd is Hediff_Injury && !hd.IsPermanent());

        foreach (var item in enumerable)
        {
            item.Severity -= GenMath.LerpDouble(0f, def.maxSeverity, HealingFactorProps.healWoundFactorMin,
                HealingFactorProps.healWoundFactorMax, Severity);
        }
    }

    private void TryHealRandomOldWound()
    {
        if (pawn.health.hediffSet.hediffs.Where(hd => hd.IsPermanent()).TryRandomElement(out var result))
        {
            result.Severity = 0f;
        }
    }

    public void TrySealWounds()
    {
        var enumerable = pawn.health.hediffSet.hediffs.Where(hd => hd.Bleeding);

        foreach (var item in enumerable)
        {
            if (item is not HediffWithComps hd2)
            {
                continue;
            }

            var hediffComp_TendDuration = hd2.TryGetComp<HediffComp_TendDuration>();
            hediffComp_TendDuration.tendQuality = 2f;
            hediffComp_TendDuration.tendTicksLeft = Find.TickManager.TicksGame;
            pawn.health.Notify_HediffChanged(item);
        }
    }

    public void TryRegrowBodyparts()
    {
        foreach (var part in pawn.GetFirstMatchingBodyparts(pawn.RaceProps.body.corePart, HediffDefOf.MissingBodyPart,
                     EntHediffDefOf.EntProtoBodypart, hediff => hediff is Hediff_AddedPart))
        {
            var hediff2 = pawn.health.hediffSet.hediffs.First(hediff =>
                hediff.Part == part && hediff.def == HediffDefOf.MissingBodyPart);

            pawn.health.RemoveHediff(hediff2);
            pawn.health.AddHediff(EntHediffDefOf.EntProtoBodypart, part);
            pawn.health.hediffSet.DirtyCache();
        }
    }

    public void SetNextMajorTick()
    {
        ticksUntilNextMajorHeal = Current.Game.tickManager.TicksGame + HealingFactorProps.ticksBetweenMajorHeal;
    }

    public void SetNextMinorTick()
    {
        ticksUntilNextMinorHeal = Current.Game.tickManager.TicksGame + HealingFactorProps.ticksBetweenMinorHeal;
    }
}