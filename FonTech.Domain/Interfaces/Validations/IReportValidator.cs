using FonTech.Domain.Entity;
using FonTech.Domain.Result;


namespace FonTech.Domain.Interfaces.Validations
{
    public interface IReportValidator : IBaseValidator<Report>
    {
        /// <summary>
        /// Проверка на повторную попытку использование имени отчета (не допустимо).
        /// Проверка наличия в БД пользователя по UserId.
        /// </summary>
        /// <param name="report"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Report report, User user);
    }
}
