
using AutoMapper;
using BlogSF.BLL.Service;
using BlogSF.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogSF
{
    public class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            //using (var db = new AppContext())
            //{
            //    var user1 = new User { FirstName = "Admin", LastName = "admin", Email = "admin@mail.ru", Role = "admin" };
            //    var user2 = new User { FirstName = "Петров", LastName = "Пётр", Email = "petr@mail.ru", Role = "user" };
            //    var user3 = new User { FirstName = "Иванов", LastName = "Иван", Email = "ivan@mail.ru", Role = "user" };
            //    db.Users.AddRange(user1, user2, user3);
            //    //db.SaveChanges();//сохраняем данные в таблицы
            //}
#endif

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            // Регистрирую строку подключения
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppContext>();
            //optionsBuilder.UseSqlite(@"Data Source=C:\SF\2023\blog.db");
            //builder.Services.AddDbContext<AppContext>(option => option.UseSqlite(connectionString), ServiceLifetime.Singleton);
            //var mapperConfig = new MapperConfiguration((v) =>
            //{
            //    v.AddProfile(new MappingProfile());
            //});

            //IMapper mapper = mapperConfig.CreateMapper();

            //// регистрация сервисов репозитория для взаимодействия с базой данных
            //builder.Services.AddSingleton(mapper);
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // регистрация сервиса репозиториев для взаимодействия с базой данных
            builder.Services.AddScoped<IBookRepositories, BookRepository>();
            builder.Services.AddScoped<ICommentRepositories, CommentRepository>();
            builder.Services.AddScoped<ITagRepositories, TagRepository>();
            builder.Services.AddScoped<IUserRepositories, UserRepository>();
            
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}