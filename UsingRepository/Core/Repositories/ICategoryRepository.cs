﻿using System.Collections.Generic;
using UsingRepository.Core.Models;

namespace UsingRepository.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesNotDeleted();
    }
}