using StudyGarden.Core.Models;

namespace StudyGarden.Infrastructure.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(User user);
}