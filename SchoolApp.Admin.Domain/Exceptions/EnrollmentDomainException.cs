using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Domain.Exceptions;
public class AdminDomainException : Exception
{
    public AdminDomainException()
    { }

    public AdminDomainException(string message)
        : base(message)
    { }

    public AdminDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}