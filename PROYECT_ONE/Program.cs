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

        bool salir = true;
        while (salir)
        {
            Console.WriteLine("Registrar nuevo cliente  opc 1");
            Console.WriteLine("Asignar cuenta ahorro - corriente - nomina  opc 2");
            Console.WriteLine("Asignar tarjeta de credito  opc #");
            Console.WriteLine("SALIR opc 3");
            
            Console.WriteLine("Seleccione una opcion");
            int opcion = int.Parse(Console.ReadLine());

        switch (opcion)
            {
                case 1:
                {
                    try
                    {
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
                            ins_direccion, ins_telefono);


                        ClienteRepository repo = new ClienteRepository(connString);
                        int id = repo.insertar_cliente(cliente);
                        Console.WriteLine($"Cliente {id}  insertado com sucesso!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                        throw;
                    }

                    break;

                }

                case 2:
                {
                    try
                    {
                        Console.WriteLine("Ingrese id del cliente a registrar cuenta");
                        int id_clinet = int.Parse(Console.ReadLine());
                        
                        Console.WriteLine("Ingresa tipo de cuenta a asignar ahorro, corriente o nomina");
                        string tipo = Console.ReadLine();
                        
                        Cuenta cun = new Cuenta(tipo, id_clinet);

                        CuentaRepository repocun = new CuentaRepository(connString);
                        int id = repocun.InsertarCuenta(cun);
                        Console.WriteLine($"Cuenta con id {id} insertada exitosamente!");
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine($"Error: {e.Message}");
                        throw;
                    }

                    break;
                }

                case 3:
                {
                    salir = true;
                    break;
                }

            }
            
        }
        
       
        
        
       


      

    }
}

