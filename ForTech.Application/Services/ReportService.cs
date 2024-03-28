using AutoMapper;
using FonTech.Domain.Dto.Report;
using FonTech.Domain.Entity;
using FonTech.Domain.Enum;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Interfaces.Validations;
using FonTech.Domain.Result;

using ForTech.Application.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace ForTech.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger _logger;
        private readonly IReportValidator _reportValidator;
        private readonly IMapper _mapper;
        

        public ReportService(IBaseRepository<Report> reportRepository, ILogger logger,
            IBaseRepository<User> userRepository, IReportValidator reportValidator, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _logger = logger;
            _userRepository = userRepository;
            _reportValidator = reportValidator;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x=>x.Id == dto.UserId);
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(r=>r.Name == dto.Name);
                var result = _reportValidator.CreateValidator(report, user);
                if (!result.isSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = ErrorMessage.InternalServerError,
                        ErrorCode = (int)ErrorCodes.InternalServerError
                    };
                }
                report = new Report()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UserId = user.Id
                };

                await _reportRepository.CreateAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report),
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                };
            }
        }

        /// <inheritdoc />
        public async Task<BaseResult<ReportDto>> GetReportByIdAsyncOrNull(long id)
        {
            ReportDto? report;
            try
            {
                report = await _reportRepository.GetAll()
                    .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreatedAt.ToLongDateString()))
                    .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                };
            }

            if (report == null)
            {
                _logger.LogWarning($"Отчет с {id} не найден");
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound,
                };
            }

            return new BaseResult<ReportDto>()
            {
                Data = report
            };
        }

        /// <inheritdoc />
        public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
        {
            List<ReportDto> reports;
            try
            {
                reports = await _reportRepository.GetAll()
                    .Where(r => r.UserId == userId)
                    .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreatedAt.ToLongDateString()))
                    .ToListAsync();                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CollectionResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError,
                };
            }

            if (!reports.Any())
            {
                _logger.LogWarning(ErrorMessage.ReportsNotFound, reports.Count);
                return new CollectionResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.ReportsNotFound,
                    ErrorCode = (int)ErrorCodes.ReportsNotFound,
                };
            }
                   
            return new CollectionResult<ReportDto>()
            {
                Data = reports,
                Count = reports.Count
            };
        }
    }
}
