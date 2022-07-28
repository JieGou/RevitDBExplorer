﻿using Autodesk.Revit.DB;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.DataModel.ValueTypes
{
    internal class ScheduleFieldType : Base.ValueType<ScheduleField>
    {
        protected override bool CanBeSnoooped(ScheduleField value) => value is not null;
        protected override string ToLabel(ScheduleField field)
        {
            return $"[{field.FieldIndex}] {field.ColumnHeading}";
        }
    }
}