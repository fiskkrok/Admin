﻿
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Ardalis.Specification.EntityFrameworkCore;
global using MediatR;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.Data.SqlClient;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Metadata;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using SchoolApp.Admin.Domain.AggregateModels.CourseAggregate;
global using SchoolApp.Admin.Domain.AggregateModels.CourseAssignmentAggregate;
global using SchoolApp.Admin.Domain.AggregateModels.EnrollmentAggregate;
global using SchoolApp.Admin.Domain.AggregateModels.FacultyAggregate;
global using SchoolApp.Admin.Domain.AggregateModels.StudentAggregate;
global using SchoolApp.Admin.Domain.SeedWork;
global using SchoolApp.Admin.Infrastructure.Data;
global using SchoolApp.Admin.Infrastructure.EntityConfigurations;