using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using System.Reflection;
using TestApplication.Domain.Chats.Services;
using TestApplication.Domain.Users.Services;
using TestApplication.Infrastructure.Databases.Write;
using TestApplication.UseCase.Abstractions.Behaviors;
using TestApplication.UseCase.Abstractions.Messaging;
using TestApplication.UseCase.Users.Commands.CreateUserCommand;

namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ///TODO: ������� ����������. ��� ����������� � ��������������, ������ ������.
            /// ���������� �� ������������� � ������� ����� ������ ����������, ���������� ��������� �� ������������.

            ///����������:
            //  -� ��� ������������, ����:
            //-�����������, ��� ����������� ������������;
            //-����������� ��������� ������ �����;
            //-����������� ��������� ����, ������� ��, � ����� ������ ��������;
            //-������/��������/������� ��������� � ���� � ������, ��� ������� � ������� ��������������;
            //-����������� �������/��������(��������)/������� �����, ��� ����� �������/��������� �� ���� ��� ����� ����
            //-�������� ������ ����: �������������/�����/�������;
            //-�������� �����������: ������������/����(� ��� ���������)/������(� ��������� ���);
            //  -� ��� �����, ����:
            //-����������� ���� �������������, ���� ������� ���������������;
            //-�������� ������ ���������� �������������/�������;
            //-�������� ������ ����� ��������� � ������������ ����/������;

            ///UseCase scenarios(�������� �������������):
            //  -������������:
            //-����������� -> �������� ������������ �����:include; -d.
            //-��������� ����� -> �������� ������������ �����:include; -d.
            //-�������� ���� -> �������� ������� ������� 2�� �������������:include; -p.
            //-��������� �������� ����; -... . 
            //-�������� ���� -> ��������, �������� �� ��������� ���������� ����� ����:include; -d.
            //-��������� ��������� � ����; -... .
            //-��������� ��������� � ����; -... .
            //-�������� ��������� � ���� -> ��������, ������ �� ������ � ��������� ���������:include; -d.
            //-�������� ������ -> �������� ������������ ��������:include; -... .
            //-��������� �������� ������ -> �������� ������������ ��������:include; -... .
            //-�������� ������ -> ��������, �������� �� ��������� ���������� ����� ������:include; -... .
            //-��������� ��������� � ������ -> ��������:include; -... .
            //-��������� ��������� � ������ -> ��������:include; -... .
            //-�������� ��������� � ������ -> ��������:include; -... .
            //-��������� ������ ���� �������������; -... .
            //-��������� ������ ���� ����� �����; -... .
            //-��������� ������ ���� �������; -... .
            //-�������� ����������� ������������; -... .
            //-�������� ����������� ������������; -... .
            //-�������� ����������� ����(��������� ���); -... .
            //-�������� ����������� ������(��������� ���); -... .
            //  -�����:
            //-��� ������������; -... .
            //-�������� ���������� ���� �������������; -... .
            //-�������� ���������� ���� �������; -... .
            //-�������� ���������� ���� ��������� � ����; -... .
            //-�������� ���������� ���� ��������� � ������; -... .


            /// ���������� �������:
            ///��������:
            //������������(����, �����, ���������);
            //���(���������, ���������, ���������);
            //�����(���������, ���������);

            ///������-�������:
            //-���������� ��� ������������;
            //-���������� �������� ������;
            //-��� ��������� ��� ������� ������� ���� ����������(���������, ������������);
            //-��� ����� ������� ������ ��������� ���;
            //-��������� � ���� ��������� � ������� ������ ����� �����������;
            //-������ � ��������� ������ ����� ������ ��� ���������;
            //-����� ����� ������� ������ ��� ���������;





            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            builder.Services.AddScoped<AppDbContext>();
            builder.Services.Scan(
                scan => scan.FromAssembliesOf(typeof(AppDbContext))
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Rep")))
                        .AsMatchingInterface()
                        .WithScopedLifetime());
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ChatService>();
            //builder.Services.AddScoped<UserService>();
            builder.Services.Scan(
                scan => scan.FromAssembliesOf(typeof(ICommandHandler<>))
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
            builder.Services.Decorate(typeof(ICommandHandler<>), typeof(ValidationBehaviorDecorator.CommandHandler<>));
            //builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationBehaviorDecorator.CommandHandler<,>));
            //builder.Services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationBehaviorDecorator.QueryHandler<,>));
            builder.Services.Decorate(typeof(ICommandHandler<>), typeof(LoggingBehaviorDecorator.CommandHandler<>));
            //builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingBehaviorDecorator.CommandHandler<,>));
            //builder.Services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingBehaviorDecorator.QueryHandler<,>));


            var app = builder.Build();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();

            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}
