﻿using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.MemberAccessors
{
    internal abstract class MemberAccessorByType<TSnoopedObjectType> : MemberAccessorTyped<TSnoopedObjectType> where TSnoopedObjectType : class
    {
        public override ReadResult Read(SnoopableContext context, TSnoopedObjectType @object)
        {           
            string label = GetLabel(context.Document, @object);
            bool canBeSnooped = CanBeSnoooped(context.Document, @object);

            return new ReadResult(label, GetType().GetCSharpName(), canBeSnooped);
        }
        protected abstract bool CanBeSnoooped(Document document, TSnoopedObjectType value);
        protected abstract string GetLabel(Document document, TSnoopedObjectType value);

        public override IEnumerable<SnoopableObject> Snoop(SnoopableContext context, TSnoopedObjectType @object)
        {
            return this.Snooop(context.Document, @object);
        }
        protected virtual IEnumerable<SnoopableObject> Snooop(Document document, TSnoopedObjectType value) => Enumerable.Empty<SnoopableObject>();       
    }
}