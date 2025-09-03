using Dindyn.Commons.Exceptions;

namespace Dindyn.App.Services;

public interface IValidationService
{
    List<ErroInfo> Validate<T>(T model);
    bool IsValid<T>(T model);
}
