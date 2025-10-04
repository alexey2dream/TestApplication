using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using System.Reflection;
using TestApplication.Domain.Channels.Services;
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
            ///TODO: Простой мессенджер. Без авторизации и аутентификации, только логика.
            /// Разделение на пользователей и админов здесь больше логическое, функционал разбросан по контроллерам.

            ///Функционал:
            //  -Я как пользователь, хочу:
            //-Регистрацию, для пользования мессенджером;
            //-Возможность изменения своего имени;
            //-Возможность создавать чаты, удалять их, а также менять названия;
            //-Писать/изменять/удалять сообщения в чате и канале, для общения с другими пользователями;
            //-Возможность создать/изменить(название)/удалить канал, для своих заметок/выражений по тому или иному делу
            //-Просмотр списка всех: пользователей/чатов/каналов;
            //-Просмотр конкретного: пользователя/чата(и его сообщений)/канала(и сообщения его);
            //  -Я как админ, хочу:
            //-Возможность бана пользователей, если кажутся подозрительными;
            //-Просмотр общего количества пользователей/каналов;
            //-Просмотр общего колва сообщений в определенном чате/канале;

            ///UseCase scenarios(сценарии использования):
            //  -Пользователь:
            //-Регистрация -> проверка уникальности имени:include; -d.
            //-Изменение имени -> проверка уникальности имени:include; -d.
            //-Создание чата -> проверка наличия минимум 2ух пользователей:include; -d.
            //-Изменение названия чата; -d.
            //-Удаление чата -> проверка, является ли удаляющий создателем этого чата:include; -d.
            //-Написание сообщения в чате -> проверка, является есть ли отправлящий в чате:include; -d.
            //-Удаление сообщения в чате -> проверка, прошла ли минуты с написания сообщения:include; -d.
            //-Создание канала -> проверка уникальности названия:include; -d.
            //-Изменение названия канала -> проверка уникальности названия:include; -d.
            //-Удаление канала -> проверка, является ли удаляющий создателем этого канала:include; -d.
            //-Написание сообщения в канале -> проверка:include; -d.
            //-Удаление сообщения в канале -> проверка:include; -d.
            //-Получение списка всех пользователей; -... .
            //-Получение списка всех своих чатов; -... .
            //-Получение списка всех каналов; -... .
            //-Просмотр конкретного пользователя; -... .
            //-Просмотр конкретного пользователя; -... .
            //-Просмотр конкретного чата(сообщений его); -... .
            //-Просмотр конкретного канала(сообщений его); -... .
            //  -Админ:
            //-Бан пользователя; -d.
            //-Просмотр количества всех пользователей; -... .
            //-Просмотр количества всех каналов; -... .
            //-Просмотр количества всех сообщений в чате; -... .
            //-Просмотр количества всех сообщений в канале; -... .


            /// Предметная область:
            ///Сущности:
            //Пользователь(чаты, канал, сообщения);
            //Чат(создатель, участники, сообщения);
            //Канал(создатель, сообщения);

            ///Бизнес-правила:
            //-Уникальное имя пользователя;
            //-Уникальное название канала;
            //-Чат создается при наличии минимум двух участников(создатель, приглашенный);
            //-Чат может удалять только создатель его;
            //-Сообщение в чате удаляется в течении минуты после отправления;
            //-Доступ к сообщения канала имеет только его создатель;
            //-Канал может удалять только его создатель;





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
            builder.Services.AddScoped<ChannelService>();
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
