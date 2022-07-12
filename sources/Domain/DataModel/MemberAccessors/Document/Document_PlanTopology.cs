﻿using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.MemberAccessors
{
    internal class Document_PlanTopology : MemberAccessorByType<Document>, IHaveFactoryMethod
    {
        public override string MemberName => "PlanTopologies";
        public override string MemberParams => typeof(Phase).Name;
        IMemberAccessor IHaveFactoryMethod.Create() => new Document_PlanTopology();


        protected override bool CanBeSnoooped(Document document, Document value) => false;// document.Phases.Size > 0;
        protected override string GetLabel(Document document, Document value) => "[Phase:PlanTopology]";

        protected override IEnumerable<SnoopableObject> Snooop(Document document, Document value)
        {
            var transaction = document.IsModifiable == false ? new Transaction(document, GetType().Name) : null;
            transaction?.Start();
            try
            {
                foreach (Phase phase in document.Phases)
                {
                    var topologies = document.get_PlanTopologies(phase).OfType<PlanTopology>().Select(x => new SnoopableObject(x, document));
                    yield return new SnoopableObject(phase, document, null, topologies);                    
                }
            }
            finally
            {
                transaction?.RollBack();
            }

        }
    }
}