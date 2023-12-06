using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Security.Cryptography;

namespace WebApplication1.Models {
    public class Database : DbContext {
        private static string ConnectionString = "Server=localhost;Port=5432;User ID=postgres;Password=123;Database=db_lab1;";//"Server=localhost;Port=5432;User ID=postgres;Password=bd2109#;Database=db_lab1;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Menu>().Navigation(o => o.Restaurant).AutoInclude();
            modelBuilder.Entity<TableReservation>().Navigation(o => o.User).AutoInclude();
            modelBuilder.Entity<TableReservation>().Navigation(o => o.Table).AutoInclude();
            modelBuilder.Entity<Table>().Navigation(o => o.Restaurant).AutoInclude();
        }
        DbSet<Menu> menus { get; set; }
        DbSet<Table> tables { get; set; }
        DbSet<Restaurant> restaurants { get; set; }
        DbSet<User> users { get; set; }
        DbSet<TableReservation> tablereservations { get; set; }
        public void GenerateRestaurant() {
            for (int i = 0; i < 10; i++)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"restaurants\"(\"name\", \"adress\", \"cuisinetype\")\r\nValues((SELECT (chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int)\r\n     )),\r\n    (SELECT (chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) \r\n     )),\r\n    (SELECT (chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int) ||\r\n    chr(trunc(65 + random() * 25)::int)\r\n     )))", connection);
                    command.ExecuteNonQuery();
                }
               
            }

          
        }
        public void GenerateTable() {
            for(int i = 0;i < 10; i++)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO \"tables\"(\"restaurantid\", \"tablenumber\", \"seats\")\r\nValues((Select max(\"tables\".\"restaurantid\") from \"tables\"),\r\n    (SELECT random() * 20 + 1 AS RAND_1_11),\r\n    (SELECT random() * 5 + 1 AS RAND_1_11))", connection);
                    command.ExecuteNonQuery();
                }
            }

        }
        public void AddRestaurant(Restaurant r) {
            restaurants.Add(r);
            SaveChanges();
        }
        public void EditRestaurant(int id, Restaurant r) {
            // Оновлення існуючого ресторану в базі даних за ID
            Restaurant er = restaurants.Single(o => o.id == id);
            er.adress = r.adress;
            er.cuisinetype = r.cuisinetype;
            er.name = r.name;
            SaveChanges();
        }
        public void AddTable(Table table) {
            tables.Add(table);
            SaveChanges();
        }
        public void EditTable(int id, Table table) {
            // Оновлення існуючого столика в базі даних за ID
            Restaurant r = restaurants.Single(o => o.id == table.restaurantid);
            Table et = tables.Single(o => o.id == id);
            et.tablenumber = table.tablenumber;
            et.Restaurant = r;
            et.seats = table.seats;
            SaveChanges();
        }
        public void AddTableReservation(TableReservation reservation) {
            reservation.reservationdatetime = reservation.reservationdatetime.ToUniversalTime();
            // Додавання нового бронювання столика в базу даних
            tablereservations.Add(reservation);
            SaveChanges();
        }
        public void EditTableReservation(int id, TableReservation reservation) {
            // Оновлення існуючого бронювання столика в базі даних за ID
            reservation.reservationdatetime = reservation.reservationdatetime.ToUniversalTime();
            Table t = tables.Single(o => o.id == reservation.tableid);
            User u = users.Single(o => o.id == reservation.userid);
            TableReservation tr = tablereservations.Single(o => o.id == id);
            tr.reservationdatetime = reservation.reservationdatetime;
            tr.Table = t;
            tr.User = u;
            SaveChanges();
        }
        public void DeleteTableReservation(int id) {
            // Видалення бронювання столика з бази даних за ID
            TableReservation tr = tablereservations.Single(o => o.id == id);
            tablereservations.Remove(tr);
            SaveChanges();
        }
    }
}
