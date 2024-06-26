﻿using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class CreateCourseAssignmentResponse : BaseResponse
{
    public CreateCourseAssignmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateCourseAssignmentResponse()
    {
    }

    public CourseAssignmentRecord CourseAssignment { get; set; }
}
