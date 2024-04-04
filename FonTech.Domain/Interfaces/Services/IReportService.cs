using FonTech.Domain.Dto.Report;
using FonTech.Domain.Result;

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


        /// <summary>
        /// Удаление отчета по Id
        /// </summary>
        /// <param name="id"></param>
        Task<BaseResult<ReportDto>> DeleteReportAsync(long id);


        /// <summary>
        /// Обновление отчета
        /// </summary>
        /// <param name="dto"></param>
        Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
    }
}
