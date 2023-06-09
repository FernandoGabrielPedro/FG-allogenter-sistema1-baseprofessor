using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Univali.Api;
using Univali.Api.Configuration;
using Univali.Api.DbContexts;
using Univali.Api.Extensions;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Features.Customers.Commands.DeleteCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Queries.GetCustomerDetail;
using Univali.Api.Features.Customers.Queries.GetCustomerDetailByCpf;
using Univali.Api.Features.Customers.Queries.GetCustomersDetail;
using Univali.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
   options.ListenLocalhost(5000);
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<Data>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new() {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"]!)
        )
    };
});

builder.Services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql("Host=localhost;Database=Univali;Username=postgres;Password=123456"));
builder.Services.AddDbContext<PublisherContext>();

// Add services to the container.

builder.Services.AddControllers(options =>{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());

}).ConfigureApiBehaviorOptions(setupAction =>
       {
            setupAction.SuppressModelStateInvalidFilter = true;
           setupAction.InvalidModelStateResponseFactory = context =>
           {
               // Cria a fábrica de um objeto de detalhes de problema de validação
               var problemDetailsFactory = context.HttpContext.RequestServices
                   .GetRequiredService<ProblemDetailsFactory>();


               // Cria um objeto de detalhes de problema de validação
               var validationProblemDetails = problemDetailsFactory
                   .CreateValidationProblemDetails(
                       context.HttpContext,
                       context.ModelState);


               // Adiciona informações adicionais não adicionadas por padrão
               validationProblemDetails.Detail =
                   "See the errors field for details.";
               validationProblemDetails.Instance =
                   context.HttpContext.Request.Path;


               // Relata respostas do estado de modelo inválido como problemas de validação
               validationProblemDetails.Type =
                   "https://courseunivali.com/modelvalidationproblem";
               validationProblemDetails.Status =
                   StatusCodes.Status422UnprocessableEntity;
               validationProblemDetails.Title =
                   "One or more validation errors occurred.";


               return new UnprocessableEntityObjectResult(
                   validationProblemDetails)
               {
                   ContentTypes = { "application/problem+json" }
               };
           };
       });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
   /*
   Retorna o nome do assembly atual como uma string por reflection

   "Assembly.GetExecutingAssembly()" retorna uma referência para o assembly
   que contém o código que está sendo executado atualmente.

   "GetName()" é chamado na referência do assembly para obter um objeto do tipo AssemblyName,
   que contém informações sobre o assembly, como seu nome, versão, cultura e chave pública.

   "Name" é lido a partir do objeto AssemblyName para obter o nome do assembly como uma string.
   */
   var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
   
   // "Path.Combine" cria um formato de caminho válido com os parâmetros
   // "AppContext.BaseDirectory" é uma propriedade que retorna o caminho base do diretório em que a aplicação está sendo executada
   var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);


   // Inclui os comentários XML na documentação do Swagger.
   setupAction.IncludeXmlComments(xmlCommentsFullPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.ResetDatabaseAsync();

app.Run();