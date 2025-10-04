using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using System.Reflection;
using TestApplication.Infrastructure.Databases.Write;
using TestApplication.UseCase.Abstractions.Behaviors;
using TestApplication.UseCase.Abstractions.Messaging;

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
            //-����������� -> �������� ������������ �����:include; -... .
            //-��������� ����� -> �������� ������������ �����:include; -... .
            //-�������� ���� -> �������� ������� ������� 2�� �������������:include; -... .
            //-��������� �������� ����; -... . 
            //-�������� ���� -> ��������, �������� �� ��������� ���������� ����� ����:include; -... .
            //-��������� ��������� � ����; -... .
            //-��������� ��������� � ����; -... .
            //-�������� ��������� � ���� -> ��������, ������ �� ������ � ��������� ���������:include; -... .
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
            //-�������� ���������� ���� �������������;
            //-�������� ���������� ���� �������;
            //-�������� ���������� ���� ��������� � ����;
            //-�������� ���������� ���� ��������� � ������;


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

            
            builder.Services.AddScoped<AppDbContext>();
            builder.Services.Scan(
                scan => scan.FromAssembliesOf(typeof(AppDbContext))
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                        .AsMatchingInterface()
                        .WithScopedLifetime());
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
                        .WithScopedLifetime()
                );
            builder.Services.Decorate(typeof(ICommandHandler<>), typeof(ValidationBehaviorDecorator.CommandHandler<>));
            builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationBehaviorDecorator.CommandHandler<,>));
            builder.Services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationBehaviorDecorator.QueryHandler<,>));
            builder.Services.Decorate(typeof(ICommandHandler<>), typeof(LoggingBehaviorDecorator.CommandHandler<>));
            builder.Services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingBehaviorDecorator.CommandHandler<,>));
            builder.Services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingBehaviorDecorator.QueryHandler<,>));


            var app = builder.Build();


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
