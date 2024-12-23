﻿using StudyGarden.Core.Models;

namespace StudyGarden.Core.Abstractions;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByLogin(string login); 
}