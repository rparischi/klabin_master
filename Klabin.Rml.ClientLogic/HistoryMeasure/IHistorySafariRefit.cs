using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Klabin.Rml.ClientLogic.HistoryMeasure
{
    interface IHistorySafariRefit
    {
        [Get("/api/rolao/metragem-linear")]
        Task<List<HistoryLinearMeasureDataResponseDto>> GetLinearMeasureHistory(HistoryLinearMeasureDataRequest request);
    }
}
