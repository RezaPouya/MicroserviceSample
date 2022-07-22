using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace IdentityManagment.Configurations
{

public static class AuthenticationConfigurationExtension
{
	public static void ConfigureAuthentication(this ServiceConfigurationContext context)
	{
		var configuration = context.Services.GetConfiguration();


		context.Services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(opt =>
			{
				opt.Authority = configuration["AuthServer:Authority"];
				opt.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"].ToString());
				opt.Audience = "identity_management";

				opt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,

					// it security used for secure multi tenant application
					ValidateIssuer = false
				};
			});
	}
}
}