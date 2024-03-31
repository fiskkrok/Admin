using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Domain.Exceptions;
public class EnrollmentDomainException : Exception
{
    public EnrollmentDomainException()
    { }

    public EnrollmentDomainException(string message)
        : base(message)
    { }

    public EnrollmentDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}