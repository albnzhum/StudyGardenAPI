using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StudyGarden.Contracts.UserRequests;

public record LoginUserRequest(
    [Required, NotNull] string Login,
    [Required, NotNull] string Password
);