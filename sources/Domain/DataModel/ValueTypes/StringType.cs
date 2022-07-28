﻿// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.ValueTypes
{
    internal sealed class StringType : Base.ValueType<string>
    {
        protected override bool CanBeSnoooped(string stringValue) => false;
        protected override string ToLabel(string stringValue)
        {            
            return stringValue;
        }
    }
}