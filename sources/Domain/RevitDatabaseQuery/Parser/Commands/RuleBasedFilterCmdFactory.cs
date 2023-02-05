﻿using System.Collections.Generic;
using Autodesk.Revit.DB;
using RevitDBExplorer.Domain.RevitDatabaseQuery.Filters;
using RevitDBExplorer.WPF.Controls;

// (c) Revit Database Explorer https://github.com/NeVeSpl/RevitDBExplorer/blob/main/license.md

namespace RevitDBExplorer.Domain.RevitDatabaseQuery.Parser.Commands
{
    internal class RuleBasedFilterCmdFactory : CommandFactory<RuleMatch>
    {
        public override IAutocompleteItem GetAutocompleteItem() => new AutocompleteItem("f:[filter] - select elements that pass rule-based filter defined in Revit", "f: ");

        public override IEnumerable<string> GetClassifiers()
        {
            yield return "f";
            yield return "filter";        
        }
        public override IEnumerable<string> GetKeywords()
        {
            yield break;
        }
        public override bool CanRecognizeArgument(string argument)
        {
            return false;
        }


        public override ICommand Create(string cmdText, IList<ILookupResult> arguments)
        {
            return new Command(CmdType.RuleBasedFilter, cmdText, arguments, null);
        }
        public override IEnumerable<ILookupResult> ParseArgument(string argument)
        {
            return FuzzySearchEngine.Lookup(argument, FuzzySearchEngine.LookFor.RuleBasedFilter);
        }
    }


    internal class RuleMatch : LookupResult<ElementId>
    {
        public RuleMatch(ElementId filterId, double levensteinScore, string name) : base(filterId, levensteinScore)
        {
            CmdType = CmdType.RuleBasedFilter;
            Name = $"new ElementId({filterId})";
            Label = name;
        }
    }
}