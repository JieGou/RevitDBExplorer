﻿using System;
using System.Globalization;
using System.Windows.Data;
using RevitDBExplorer.Domain.DataModel;
using RevitDBExplorer.Domain.DataModel.ValueViewModels;

namespace RevitDBExplorer.WPF.Converters
{
    internal class SnoopableMemberToToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var snoopableMember = value as SnoopableMember;

            if (snoopableMember == null) return null;

            if (snoopableMember.ValueViewModel is DefaultPresenter presenter)
            {
                if (!string.IsNullOrEmpty(presenter.ValueContainer?.ToolTip))
                {
                    return presenter.ValueContainer.ToolTip;
                }
                return presenter.Label; 
            }

            if (snoopableMember.ValueViewModel is ErrorPresenter errorPresenter)
            {
                return errorPresenter.Label;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
