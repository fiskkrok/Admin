using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.AggregateModels.StudentAggregate;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StudentStatus
{
    Active,
    Inactive
}
