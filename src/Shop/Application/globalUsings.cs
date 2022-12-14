global using Application.Common.Interfaces;
global using Application.Common.Authentication.TokenLogic;
global using Microsoft.Extensions.Configuration;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using System.Text.Json;
global using Domain.Entities.User;
global using Domain.Entities.Product;
global using Microsoft.EntityFrameworkCore;
global using static Application.Common.Help.Helper;
global using MediatR;
global using static Application.Common.Authentication.TokenLogic.Tokenlogic;
global using Application.Common.Help;