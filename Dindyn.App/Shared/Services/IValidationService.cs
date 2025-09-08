using Dindyn.Commons.Exceptions;

namespace Dindyn.App.Shared.Services;

public interface IValidationService
{
    List<ErroInfo> Validate<T>(T model);
    bool IsValid<T>(T model);
}
