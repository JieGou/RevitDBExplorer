﻿using System;
using System.Collections.Generic;
using Autodesk.Revit.DB.Structure;
using RevitDBExplorer.Domain.RevitDatabaseQuery.Filters;
using RevitDBExplorer.WPF.Controls;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.RevitDatabaseQuery.Parser.Commands
{
    internal class StructuralTypeCmdFactory : CommandFactory<StructuralTypeMatch>
    {
        public override IAutocompleteItem GetAutocompleteItem() => new AutocompleteItem("s:[structural type] - select elements matching a structural type", "s: ");

        public override IEnumerable<string> GetClassifiers()
        {
            yield return "s";
            yield return "stru";
            yield return "structual";
        }
        public override IEnumerable<string> GetKeywords()
        {
            yield break;
        }
        public override bool CanRecognizeArgument(string argument)
        {
            if (argument.StartsWith(nameof(StructuralType), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }


        public override ICommand Create(string cmdText, IList<ILookupResult> arguments)
        {
            return new Command(CmdType.StructuralType, cmdText, arguments, null) { IsBasedOnQuickFilter = true };
        }
        public override IEnumerable<ILookupResult> ParseArgument(string argument)
        {
            var arg = argument.RemovePrefix(nameof(StructuralType));
            return FuzzySearchEngine.Lookup(arg, FuzzySearchEngine.LookFor.StructuralType);
        }
    }


    internal class StructuralTypeMatch : LookupResult<StructuralType>
    {
        public StructuralTypeMatch(StructuralType value, double levensteinScore) : base(value, levensteinScore)
        {
            CmdType = CmdType.StructuralType;
            Name = $"StructuralType.{value}";
        }
    }
}