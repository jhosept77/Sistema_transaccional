namespace PROYECT_ONE;
using Npgsql;

public class Program
{
    static void Main()
    {
        var connString = "Host=localhost;Username=postgres;Password=Gorilas117;Database=banco";
        using var conn = new NpgsqlConnection(connString);

        try
        {
            conn.Open();
            Console.WriteLine("Conectado a POSTGRESQL!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
        
        Console.WriteLine("Ingrese nombre del cliente");
        string ins_nombre = Console.ReadLine();
        
        Console.WriteLine("Ingrese apellido del cliente");
        string ins_apellido = Console.ReadLine();
        
        Console.WriteLine("Ingrese fecha de nacimiento en AAAA-MM-DD");
        string ins_fecha_nacimiento = Console.ReadLine();
        DateTime fechanacido = DateTime.Parse(ins_fecha_nacimiento);
        
        Console.WriteLine("Ingrese cedula del cliente");
        string ins_cedula = Console.ReadLine();
        
        Console.WriteLine("Ingrese correo del cliente");
        string ins_correo = Console.ReadLine();
        
        Console.WriteLine("Ingrese direccion del cliente");
        string ins_direccion = Console.ReadLine();
        
        Console.WriteLine("Ingrese telefono del cliente");
        string ins_telefono = Console.ReadLine();


        Cliente cliente = new Cliente(ins_nombre, ins_apellido, fechanacido, ins_cedula, ins_correo,
            ins_direccion, ins_telefono );


        ClienteRepository repo = new ClienteRepository(connString);
        int id = repo.insertar_cliente(cliente);
        Console.WriteLine($"Cliente {id}  insertado com sucesso!");
        
        
        Console.WriteLine("Ingrese tipo de cunta AHORROS, CORRIENTE O EMPRESARIAL");
        string ins_tipo_de_cuenta = Console.ReadLine();
        
        Console.WriteLine("");


        Cuenta cuenta = new Cuenta()
        {
            Fecha_apertura = DateTime.Now,
            Fecha_vencimiento = DateTime.Now.AddYears(5)
        };

    }
}

