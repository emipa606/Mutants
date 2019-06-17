using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace Mutants
{
    [DefOf]
    public static class ConversionDefOf
    {
        //replace this with the pawnkinddef you want to use to assign correct race
        public static PawnKindDef StrangerInBlack;
    }

    public class HediffComp_Conversion : HediffComp
    {

        public Pawn ConvertPawn(Pawn pawnToConvert)
        {

            PawnGenerationRequest request = new PawnGenerationRequest(
                ConversionDefOf.StrangerInBlack,
                faction: Faction.OfPlayer,
                forceGenerateNewPawn: true,
                canGeneratePawnRelations: false,
                colonistRelationChanceFactor: 0f,
                fixedBiologicalAge: pawnToConvert.ageTracker.AgeBiologicalYearsFloat,
                fixedChronologicalAge: pawnToConvert.ageTracker.AgeChronologicalYearsFloat,
                fixedGender: pawnToConvert.gender,
                allowFood: false);
            Pawn pawn = PawnGenerator.GeneratePawn(request);

            //No pregenerated equipment.
            pawn?.equipment?.DestroyAllEquipment();
            pawn?.apparel?.DestroyAll();
            pawn?.inventory?.DestroyAll();

            //Transfer everything from old pawn to new pawn
            pawn.drugs = pawnToConvert.drugs;
            pawn.foodRestriction = pawnToConvert.foodRestriction;
            //pawn.guilt = pawnToConvert.guilt;
            //pawn.health = pawnToConvert.health;
            pawn.health.hediffSet = pawnToConvert.health.hediffSet;
            //pawn.needs = pawnToConvert.needs;
            pawn.records = pawnToConvert.records;
            pawn.skills = pawnToConvert.skills;
            pawn.story = pawnToConvert.story;
            pawn.timetable = pawnToConvert.timetable;
            pawn.workSettings = pawnToConvert.workSettings;
            pawn.Name = pawnToConvert.Name;

            return pawn;
        }
    }
}