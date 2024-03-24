﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ardalis.Specification;

namespace SchoolApp.Admin.Application.SeedWork;
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
