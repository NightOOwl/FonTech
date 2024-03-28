using FonTech.Domain.Dto.Report;
using FonTech.Domain.Result;
using System.Threading.Tasks;

namespace FonTech.Domain.Interfaces.Services
{
    /// <summary>
    /// Сервис, отвечающий за работу с доменной частью отчета (Report)
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получение всех отчетов пользователя
        /// </summary>
        /// <param name="userId"></param>
        Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

        /// <summary>
        /// Получение отчета по идентификатору
        /// </summary>
        /// <param name="id"></param>
        Task<BaseResult<ReportDto>> GetReportByIdAsyncOrNull(long id);

       
        /// <summary>
        /// Создание нового отчета
        /// </summary>
        /// <param name="dto"></param>
        Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);
    }
}
