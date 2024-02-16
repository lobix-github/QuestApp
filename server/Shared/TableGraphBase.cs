using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using QuestApp.DTO;
using QuestApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Shared
{
    public abstract class TableGraphBase : ComponentBase
    {
        [Inject] protected IStringLocalizer<SharedRes> SharedResL { get; set; }

        [Parameter] public string IndexTitle { get; set; }
        [Parameter] public Dictionary<SectorId, List<double>> Rows { get; set; }
        [Parameter] public List<CategoryDTO> Categories { get; set; }

        protected string GetRowStyle(SectorId sectorId, int col)
        {
            var result = $"background: {GetValue(sectorId, col)};";

            if (col == -1)
            {
                result += $"border-left: {BoldBorderStyle}; border-right: {BoldBorderStyle};";

                if (sectorId == Rows.Keys.Last())
                {
                    result += $"border-bottom: {BoldBorderStyle};";
                }
            }

            return result;
        }

        protected string BoldBorderStyle => "5px solid #6FAAB0";

        private string GetValue(SectorId sectorId, int col)
        {
            if (col == -1)
            {
                return GetColor(Rows[sectorId].AverageOrZero());
            }

            return GetColor(Rows[sectorId][col]);
        }

        protected string GetColor(double value) => value switch
        {
            double val when val <= 2 =>  $"#{Convert.ToHexString(new byte[] { 255 / 5 * 5, 255 / 5 * 5, 255 / 5 * 5 })}",
            double val when val <= 4 =>  $"#{Convert.ToHexString(new byte[] { 255 / 5 * 4, 255 / 5 * 4, 255 / 5 * 4 })}",
            double val when val <= 6 =>  $"#{Convert.ToHexString(new byte[] { 255 / 5 * 3, 255 / 5 * 3, 255 / 5 * 3 })}",
            double val when val <= 8 =>  $"#{Convert.ToHexString(new byte[] { 255 / 5 * 2, 255 / 5 * 2, 255 / 5 * 2 })}",
            double val when val <= 10 => $"#{Convert.ToHexString(new byte[] { 255 / 5 * 1, 255 / 5 * 1, 255 / 5 * 1 })}",
            _ => throw new InvalidOperationException()
        };

        protected string GetColorCaption(int value) => value switch
        {
            int val when val == 2 => "0 - 2",
            int val when val == 4 => "2 - 4",
            int val when val == 6 => "4 - 6",
            int val when val == 8 => "6 - 8",
            int val when val == 10 => "8 - 10",
            _ => throw new InvalidOperationException()
        };
    }
}
