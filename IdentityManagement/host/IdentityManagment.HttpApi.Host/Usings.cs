﻿global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Serilog;
global using System;
global using System.IO;
global using System.Threading.Tasks;
global using Volo.Abp;
global using Volo.Abp.AspNetCore.Mvc;
global using Volo.Abp.AspNetCore.Serilog;
global using Volo.Abp.Auditing;
global using Volo.Abp.Autofac;
global using Volo.Abp.Caching.StackExchangeRedis;
global using Volo.Abp.EntityFrameworkCore;
global using Volo.Abp.EntityFrameworkCore.SqlServer;
global using Volo.Abp.EventBus.RabbitMq;
global using Volo.Abp.Localization;
global using Volo.Abp.Modularity;
global using Volo.Abp.PermissionManagement.EntityFrameworkCore;
global using Volo.Abp.Swashbuckle;
global using Volo.Abp.VirtualFileSystem;