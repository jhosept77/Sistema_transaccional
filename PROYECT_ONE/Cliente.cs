using Npgsql;

namespace PROYECT_ONE;

public class Cliente
{
    public string Nombre { get; set; }
    public string Apellido {get; set;}
    public DateTime FechaNacimiento { get; set; }
    public string Cedula { get; set; }
    public string Correo { get; set; }
    public string Direccion { get; set; }
    public string  Telefono { get; set; }
   


    public Cliente (string nombre, string apellido ,DateTime fechaNacimiento, string cedula, string correo, string direccion, string telefono)
    {
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaNacimiento = fechaNacimiento;
        this.Cedula = cedula;
        this.Correo = correo;
        this.Direccion = direccion;
        this.Telefono = telefono;
        
    }
    
    public Cliente()
    {}
    
}


public class ClienteRepository
{
    private readonly string _ConnString;

    public ClienteRepository(string connString)
    {
        _ConnString = connString;
    }

    public int insertar_cliente(Cliente cli)
    {
        using var puerta = new NpgsqlConnection(_ConnString);
        puerta.Open();
        
        var banco = "INSERT INTO clientes(nombre, apellido, fecha_nacido, cedula, email, direccion, telefono)" +
                  "VALUES (@nombre, @apellido, @fecha_nacido, @cedula, @email, @direccion, @telefono)" +
                  "RETURNING id_clientes";

        using var puertafinal = new NpgsqlCommand(banco, puerta);
        puertafinal.Parameters.AddWithValue("nombre", cli.Nombre);
        puertafinal.Parameters.AddWithValue("apellido", cli.Apellido);
        puertafinal.Parameters.AddWithValue("fecha_nacido", cli.FechaNacimiento);
        puertafinal.Parameters.AddWithValue("cedula", cli.Cedula);
        puertafinal.Parameters.AddWithValue("email", cli.Correo);
        puertafinal.Parameters.AddWithValue("direccion", cli.Direccion);
        puertafinal.Parameters.AddWithValue("telefono", cli.Telefono);
        
            
        
        var id = (int)puertafinal.ExecuteScalar();
        return id;

    }
    
}
       

      