﻿using System.Collections.Generic;
using Autodesk.Revit.DB;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.ValueTypes
{
    internal sealed class CategoryType : Base.ValueType<Category>
    {       
        protected override bool CanBeSnoooped(Category category) => category is not null;
        protected override string ToLabel(Category category)
        {
            return $"{category.Name}";
        }
        protected override IEnumerable<SnoopableObject> Snooop(Document document, Category category)
        {
            yield return new SnoopableObject(document, category);
        }
    }
}