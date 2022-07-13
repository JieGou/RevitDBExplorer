﻿using System.Collections.Generic;
using System.Linq.Expressions;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.MemberAccessors
{
    internal class Element_GetEntity : MemberAccessorByType<Element>, IHaveFactoryMethod
    {
        protected override IEnumerable<LambdaExpression> HandledMembers { get { yield return (Element x, Schema s) => x.GetEntity(s); } }
        IMemberAccessor IHaveFactoryMethod.Create() => new Element_GetEntity();   


        protected override bool CanBeSnoooped(Document document, Element element)
        {
            foreach (var id in element.GetEntitySchemaGuids())
            {
                var schema = Schema.Lookup(id);
                if(schema.ReadAccessGranted()) return true;
            }
            return false;
        }
        protected override string GetLabel(Document document, Element element)
        {          
            return $"[{nameof(Entity)}]";
        }
        protected override IEnumerable<SnoopableObject> Snooop(Document document, Element element)
        {
            var schemas = Schema.ListSchemas();

            foreach (var schema in schemas)
            {
                if (!schema.ReadAccessGranted()) continue;
                var entity = element.GetEntity(schema);
                if (!entity.IsValid()) continue;

                yield return new SnoopableObject(schema, document, new SnoopableObject(entity, document));
            }         
        }       
    }
}